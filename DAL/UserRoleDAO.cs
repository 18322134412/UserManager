using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using UserManager.Model;
using UserManager.Utility;
namespace UserManager.DAO
{
    public partial class UserRoleDAO
    {
        #region �����ݿ������һ����¼ +bool Insert(UserRole model)
        ///<summary>
        ///�����ݿ������һ����¼
        ///</summary>
        ///<param name="model">Ҫ��ӵ�ʵ��</param>
        public bool Insert(UserRole model)
        {
            const string sql = @"INSERT INTO [dbo].[UserRole] (id,name) VALUES (@id,@name)";
            int res = SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@id", model.id.ToDBValue()), new SqlParameter("@name", model.name.ToDBValue()));
            return res > 0;
        }
        #endregion
        #region ɾ��һ����¼ +bool Delete(string id)
        /// <summary>
        /// ɾ��һ����¼
        /// </summary>
        /// <param name="id">����</param>
        /// <returns>�Ƿ�ɹ�</returns>
        public bool Delete(string id)
        {
            const string sql = "DELETE FROM [dbo].[UserRole] WHERE [id] = @id";
            return SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@id", id)) > 0;
        }
        #endregion
        #region ��������ID����һ����¼ +bool Update(UserRole model)
        /// <summary>
        /// ������������һ����¼
        /// </summary>
        /// <param name="model">���º��ʵ��</param>
        /// <returns>�Ƿ�ɹ�</returns>
        public bool Update(UserRole model)
        {
            const string sql = @"UPDATE [dbo].[UserRole] SET  name=@name  WHERE [id] = @id";
            return SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@id", model.id.ToDBValue()), new SqlParameter("@name", model.name.ToDBValue())) > 0;
        }
        #endregion
        #region ��ѯ���� +int QueryCount(object wheres)
        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="wheres">��ѯ����</param>
        /// <returns>����</returns>
        public int QueryCount(object wheres)
        {
            List<SqlParameter> list = null;
            string str = wheres.parseWheres(out list);
            str = str == "" ? str : "where " + str;
            string sql = "SELECT COUNT(1) from UserRole " + str; var res = SqlHelper.ExecuteScalar(sql, list.ToArray());
            return res == null ? 0 : Convert.ToInt32(res);
        }
        #endregion

        #region ��ѯ����ģ��ʵ�� +UserRole QuerySingleById(string id)
        /// <summary>
        /// ��ѯ����ģ��ʵ��
        /// </summary>
        /// <param name="id">id</param>);
        /// <returns>ʵ��</returns>);
        public UserRole QuerySingleById(string id)
        {
            const string sql = "SELECT TOP 1 id,name from UserRole WHERE [id] = @id";
            using (var reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@id", id)))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    UserRole model = SqlHelper.MapEntity<UserRole>(reader);
                    return model;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region ��ȡ���н�ɫ +IEnumerable<UserRole> GetAllRole()
        /// <summary>
        /// ��ȡ���н�ɫ
        /// </summary>
        /// <returns>��ɫ����</returns>
        public IEnumerable<UserRole> GetAllRole()
        {
            string sql = "select id,name from UserRole";
            using (var reader = SqlHelper.ExecuteReader(sql))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserRole model = SqlHelper.MapEntity<UserRole>(reader);
                        yield return model;
                    }
                }
            }
        }
        #endregion

        #region ��ѯ����ģ��ʵ�� +_User QuerySingleByIdX(string objectId,string columns){
        ///<summary>
        ///��ѯ����ģ��ʵ��
        ///</summary>
        ///<param name=id>����</param>);
        ///<returns>ʵ��</returns>);
        public Dictionary<string, object> QuerySingleByIdX(string id, object columns)
        {
            string cols = "id,name";
            if (columns != null)
            {
                cols = string.Join(",", (string[])columns);
            }
            string sql = "SELECT TOP 1 " + cols + " from UserRole WHERE [objectId] = @objectId";
            using (var reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@id", id)))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    var model = SqlHelper.MapEntity(reader, cols.Split(','));
                    return model;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion
               
    }
}