using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager.Utility
{
    public class SecurityHelper
    {
        public static string GetUserIp()
        {
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                return System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
            else
                return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        public static string GetTimeStamp()
        {
            DateTime start = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime now = DateTime.Now;
            TimeSpan toNow = now.Subtract(start);
            string timeStamp = toNow.Ticks.ToString();
            return timeStamp.Substring(0, timeStamp.Length - 7);
        }
       
        /// <summary>
        /// 读取密码盐
        /// </summary>
        /// <returns></returns>
        public static string GetPasswordSalt()
        {
            string salt = ConfigurationManager.AppSettings["passwordSalt"];
            return salt;
        }
        /// <summary>
        /// 序列化对象到指定的文件里
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="obj">要序列化的对象</param>
        public static void serialize(string path, object obj)
        {
            using (FileStream fsWrite = new FileStream(@"H:\a.txt", FileMode.OpenOrCreate, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fsWrite, obj);
            }
        }
        /// <summary>
        /// 反序列化对象到指定的文件里
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>序列化后的对象</returns>
        public static T deserialize<T>(string path)
        {
            using (FileStream fsRead = new FileStream(@"H:\a.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (T)bf.Deserialize(fsRead);
            }
        }
    }
}
