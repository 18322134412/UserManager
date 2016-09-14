using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using UserManager.Model;
using UserManager.Utility;
using System.Data;
namespace UserManager{
    public partial class tokensDAO
    {
        #region 向数据库中添加一条记录 +bool Insert(tokens model)
        ///<summary>
        ///向数据库中添加一条记录
        ///</summary>
        ///<param name="model">要添加的实体</param>
        public static bool Insert(tokens model)
        {
            const string sql = @"INSERT INTO [dbo].[Tokens] (expire,userId,token,Role) VALUES (@expire,@userId,@token,@Role)";
            int res = SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@expire", model.expire.ToDBValue()), new SqlParameter("@userId", model.userId.ToDBValue()), new SqlParameter("@token", model.token.ToDBValue()), new SqlParameter("@Role", (int)(model.Role).ToDBValue()));
            return res > 0;
        }
        #endregion
        #region 删除一条记录 +bool Delete(string token)
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="UserToken">token</param>
        /// <returns>是否成功</returns>
        public static bool Delete(string token)
        {
            const string sql = "DELETE FROM [dbo].[Tokens] WHERE [token] = @token";
            return SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@token", token)) > 0;
        }
        #endregion
        #region 分页查询一个集合 +DataTable QueryList()
        ///<summary>
        ///一个表集合
        ///</summary>
        ///<returns>实体集合</returns>
        public static DataTable QueryList()
        {
            const string sql1 = "DELETE FROM [dbo].[Tokens] WHERE [expire] < @expire";
            SqlHelper.ExecuteNonQuery(sql1, new SqlParameter("@expire", DateTime.Now));

            string sql = "select * from [dbo].[Tokens]";
            using (var dt = SqlHelper.ExecuteDataTable(sql))
            {
                return dt;
            }
        }
        #endregion
       
        #region 根据条件更新记录+bool Update( string userId,DateTime expire)
        /// <summary>
        /// 根据条件更新记录
        /// </summary>
        /// <param name="uuid">主键</param>
        /// <param name="expire">过期时间</param>
        /// <returns>是否成功</returns>
        public static bool Update(string userId, DateTime expire)
        {
            string sql = @"UPDATE [dbo].[Tokens] SET  expire=@expire where userId=@userId";
            return SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@expire", expire), new SqlParameter("@userId", userId)) > 0;
        }
        #endregion
    }
}