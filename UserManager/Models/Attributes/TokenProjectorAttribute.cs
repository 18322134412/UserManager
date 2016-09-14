using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace UserManager
{
    /// <summary>
    /// 用户令牌验证/// </summary>
    public class TokenProjectorAttribute : ActionFilterAttribute
    {
        private const string UserToken = "token";
        private readonly IAccountInfoService accountInfoService = ServiceLocator.Instance.GetService<IAccountInfoService>();
        private readonly ITempCacheService tempCacheService = ServiceLocator.Instance.GetService<ITempCacheService>();

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // 匿名访问验证
            var anonymousAction = actionContext.ActionDescriptor.GetCustomAttributes<AnonymousAttribute>();
            if (!anonymousAction.Any())
            {
                // 验证token
                var token = TokenVerification();
                //验证权限
                AuthorizeCore(token, actionContext);
            }

            
            base.OnActionExecuting(actionContext);
        }

        /// <summary>
        /// 身份令牌验证
        /// </summary>
        /// <param name="actionContext"></param>
        protected virtual string TokenVerification()
        {
            // 获取token
            var token = GetToken();

            // 判断token是否有效
            if (!CacheManager.TokenIsExist(token))
            {
                throw new UserLoginException("Token已失效，请重新登陆!");
            }

            // 判断用户是否被冻结
            if (accountInfoService.Exist_User_IsForzen(CacheManager.GetUserId(token)))
            {
                CacheManager.RemoveToken(token);
                tempCacheService.Delete_OneTempCaches(token);
                throw new UserLoginException("此用户已被冻结,请联系客服!");
            }

            return token;
        }

        private string GetToken()
        {
            var token = "";
            token = HttpContext.Current.Request.Headers[UserToken];
            if (!string.IsNullOrEmpty(token)) {
                return token;
            }

            token = HttpContext.Current.Request[UserToken];
            if (string.IsNullOrEmpty(token))
            {
                throw new UserLoginException("未登录");
            }
            return token;
        }
        /// <summary>
        /// Action 访问权限验证
        /// </summary>
        /// <param name="token">身份令牌</param>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected virtual void AuthorizeCore(string token, HttpActionContext actionContext)
        {
            // 权限控制Action验证
            var moduleAuthorizationAction = actionContext.ActionDescriptor.GetCustomAttributes<ModuleAuthorizationAttribute>();
            if (moduleAuthorizationAction.Any())
            {
                var userRole = CacheManager.GetUserRole(token);
                if (!moduleAuthorizationAction[0].Authorizations.Contains(userRole))
                {
                    throw new Exception("用户非法跨权限访问");
                }
            }
        }
    }
}