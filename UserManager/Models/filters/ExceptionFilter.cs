using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http.Filters;
using UserManager.Models;
using UserManager.Utility;

namespace UserManager
{
    /// <summary>
    /// 异常拦截器
    /// </summary>
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private HttpResponseMessage GetResponse(int code, string message)
        {
            var resultModel = new resultBase() { code = code, error = message };

            return new HttpResponseMessage()
            {
                Content = new ObjectContent<resultBase>(
                    resultModel,
                    new JsonMediaTypeFormatter(),
                    "application/json"
                    )
            };
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var code = -1;
            var message = "请求失败!";

            //if (actionExecutedContext.Exception is UserLoginException)
            //{
            //    code = -1;
            //    message = actionExecutedContext.Exception.Message;
            //}
            //else if (actionExecutedContext.Exception is AuthorizationException)
            //{
            //    code = -2;
            //    message = actionExecutedContext.Exception.Message;
            //}
            //else 
            //{ 
              //记录错误日志
               LogUtils.ErrorLog("", actionExecutedContext.Exception);
            //}

            if (actionExecutedContext.Response == null)
            {
                actionExecutedContext.Response = GetResponse(code, message);
            }

            base.OnException(actionExecutedContext);
        }
    }
}