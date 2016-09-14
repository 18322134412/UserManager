using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManager.Models
{
    /// <summary>
    /// 系统错误日志
    /// </summary>
    public class ErrorLog
    {
        /// <summary>
        /// ID(GUID字符串)
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime time { get; set; }

        /// <summary>
        /// 日志错误信息
        /// </summary>
        public string message { get; set; }


        /// <summary>
        /// 错误级别
        /// </summary>
        public string level { get; set; }

        /// <summary>
        /// 记录器（PRMMS.Logger）
        /// </summary>
        public string logger { get; set; }

        /// <summary>
        /// 日志产生位置
        /// </summary>
        public string source { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public string userid { get; set; }


        /// <summary>
        /// 自动创建ID
        /// </summary>
        public ErrorLog()
        {
            this.id = Guid.NewGuid().ToString();
        }
    }
}