using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace System.Web
{
    public static class RequestHelper
    {
        /// <summary>
        /// 获取当前Request是否为POST请求
        /// </summary>
        /// <param name="request">获取当前Request</param>
        /// <returns>true/false</returns>
        public static bool IsPostBack(this HttpRequest request)
        {
            return request.HttpMethod.Equals("post", StringComparison.InvariantCultureIgnoreCase);
        }
       

    }
}
