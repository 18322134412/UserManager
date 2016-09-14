// ***********************************************************************
// Project          : Micua
// Assembly         : Micua.Infrastructure.Utility
// Author           : iceStone
// Created          : 2014-01-05 1:49 PM
// 
// Last Modified By : iceStone
// Last Modified On : 2014-01-05 1:49 PM
// ***********************************************************************
// <copyright file="StreamExtensions.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>文件流拓展方法</summary>
// ***********************************************************************

namespace System
{
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// 文件流拓展方法 
    /// </summary>
    public static class StreamExtension
    {
        #region 获取流的MD5值 +static string MD5(this Stream stream)
        /// <summary>
        /// 获取流的MD5值
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:53 Created By iceStone
        /// </remarks>
        /// <param name="stream">流</param>
        /// <returns>MD5值</returns>
        public static string GetMD5(this Stream stream)
        {
            var oMd5Hasher = new MD5CryptoServiceProvider();
            var arrbytHashValue = oMd5Hasher.ComputeHash(stream);

            // 由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
            string strHashData = BitConverter.ToString(arrbytHashValue);

            // 替换-
            return strHashData.Replace("-", string.Empty).ToLower();
        }
        #endregion
        /// <summary>
        /// 将文件里的数据读取出来
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string readFile(string path) {
            //  FileStream StreamReader StreamWriter
            using (FileStream fsRead = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
            {
                byte[] buffer = new byte[fsRead.Length];
                //表示本次读取实际读取到的有效字节数
                int r = fsRead.Read(buffer, 0, buffer.Length);
                return Encoding.Default.GetString(buffer, 0, r);
            }
        }
        /// <summary>
        /// 将文件里的数据读取出来
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] readFile(string path)
        {
            //  FileStream StreamReader StreamWriter
            using (FileStream fsRead = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
            {
                byte[] buffer = new byte[fsRead.Length];
                //表示本次读取实际读取到的有效字节数
                int r = fsRead.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }
        /// <summary>
        /// 将数据写到文件里
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static void writeFile(string path,string str)
        {
            //  FileStream StreamReader StreamWriter
            using (FileStream fsWrite = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                byte[] buffer = Encoding.Default.GetBytes(str);
                fsWrite.Write(buffer, 0, buffer.Length);
            }
        }
        /// <summary>
        /// 将数据写到文件里
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static void writeFile(string path, byte[] buffer)
        {
            //  FileStream StreamReader StreamWriter
            using (FileStream fsWrite = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                fsWrite.Write(buffer, 0, buffer.Length);
            }
        }
    }
}