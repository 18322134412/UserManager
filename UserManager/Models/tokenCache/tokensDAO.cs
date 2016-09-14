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
        #region �����ݿ������һ����¼ +bool Insert(tokens model)
        ///<summary>
        ///�����ݿ������һ����¼
        ///</summary>
        ///<param name="model">Ҫ��ӵ�ʵ��</param>
        public static bool Insert(tokens model)
        {
            const string sql = @"INSERT INTO [dbo].[Tokens] (expire,userId,token,Role) VALUES (@expire,@userId,@token,@Role)";
            int res = SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@expire", model.expire.ToDBValue()), new SqlParameter("@userId", model.userId.ToDBValue()), new SqlParameter("@token", model.token.ToDBValue()), new SqlParameter("@Role", (int)(model.Role).ToDBValue()));
            return res > 0;
        }
        #endregion
        #region ɾ��һ����¼ +bool Delete(string token)
        /// <summary>
        /// ɾ��һ����¼
        /// </summary>
        /// <param name="UserToken">token</param>
        /// <returns>�Ƿ�ɹ�</returns>
        public static bool Delete(string token)
        {
            const string sql = "DELETE FROM [dbo].[Tokens] WHERE [token] = @token";
            return SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@token", token)) > 0;
        }
        #endregion
        #region ��ҳ��ѯһ������ +DataTable QueryList()
        ///<summary>
        ///һ������
        ///</summary>
        ///<returns>ʵ�弯��</returns>
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
       
        #region �����������¼�¼+bool Update( string userId,DateTime expire)
        /// <summary>
        /// �����������¼�¼
        /// </summary>
        /// <param name="uuid">����</param>
        /// <param name="expire">����ʱ��</param>
        /// <returns>�Ƿ�ɹ�</returns>
        public static bool Update(string userId, DateTime expire)
        {
            string sql = @"UPDATE [dbo].[Tokens] SET  expire=@expire where userId=@userId";
            return SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@expire", expire), new SqlParameter("@userId", userId)) > 0;
        }
        #endregion
    }
}