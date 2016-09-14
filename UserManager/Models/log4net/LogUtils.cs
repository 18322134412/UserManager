using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManager.Models
{
    /// <summary>
    /// 日志工具类
    /// </summary>
    public class LogUtils
    {
        private static readonly log4net.ILog errorLog = log4net.LogManager.GetLogger("myLog");

        /// <summary>
        /// 将指定的<see cref="Exception"/>实例详细信息写入错误日志。
        /// </summary>
        /// <returns></returns>
        public static void ErrorLog(string userId, Exception exception)
        {
            if (exception != null)
            {
                var exceptionString = exception.ToString();
                if (exceptionString.Length > 1000)
                {
                    exceptionString = exceptionString.Substring(0, 999);
                }
                errorLog.Error(new ErrorLog
                {
                    userid=userId,
                    message=exceptionString
                });
            }
        }
        /// <summary>
        /// 将指定的<see cref="Exception"/>实例详细信息写入错误日志。
        /// </summary>
        /// <returns></returns>
        public static void ErrorLog(Exception exception)
        {
            if (exception != null)
            {
                var exceptionString = exception.ToString();
                if (exceptionString.Length > 1000)
                {
                    exceptionString = exception.ToString().Substring(0, 999);
                }
                errorLog.Error(new ErrorLog
                {
                    message = exceptionString
                });
            }
        }
    }
}