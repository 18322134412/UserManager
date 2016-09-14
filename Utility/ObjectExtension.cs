// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectExtension.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ObjectExtension type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace System
{
    using System.Collections.Generic;
    using System.ComponentModel;

    public static class ObjectExtension
    {

        /// <summary>
        /// 判断一个对象是否为NULL
        /// </summary>
        /// <param name="input">判断对象</param>
        /// <returns>是否为NULL</returns>
        public static bool IsNull(this object input)
        {
            return input == null;
        }

        /// <summary>
        /// 判断一个对象是否不为NULL
        /// </summary>
        /// <param name="input">判断对象</param>
        /// <returns>是否不为NULL</returns>
        public static bool IsNotNull(this object input)
        {
            return input != null;
        }
		
		

        #region object to money
        /// <summary>
        /// 货币
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToMoney(this Object str, decimal def = default(Decimal))
        {
            return str == null ? string.Format("{0:C}", def) : string.Format("{0:C}", str);
        }
        #endregion
    }
}
