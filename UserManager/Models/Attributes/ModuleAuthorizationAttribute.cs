using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManager
{
    /// <summary>
    /// 权限控制标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ModuleAuthorizationAttribute : Attribute
    {
        public ModuleAuthorizationAttribute(params Role[] authorization)
        {
            this.Authorizations = authorization;
        }

        /// <summary>
        /// 允许访问角色
        /// </summary>
        public Role[] Authorizations { get; set; }
    }
}