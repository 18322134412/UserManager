using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using UserManager.Model;
using UserManager.Utility;
namespace UserManager.DAO
{
    public partial class UsersDAO
    {
        #region �����ݿ������һ����¼ +bool Insert(Users model)
        ///<summary>
        ///�����ݿ������һ����¼
        ///</summary>
        ///<param name="model">Ҫ��ӵ�ʵ��</param>
        public bool Insert(Users model)
        {
            const string sql = @"INSERT INTO [dbo].[Users] (id,phone,password,nick,UserRoleId,times,isLock,createdAt,updatedAt) VALUES (@id,@phone,@password,@nick,@UserRoleId,@times,@isLock,@createdAt,@updatedAt)";
            int res = SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@id", model.id.ToDBValue()), new SqlParameter("@phone", model.phone.ToDBValue()), new SqlParameter("@password", model.password.ToDBValue()), new SqlParameter("@nick", model.nick.ToDBValue()), new SqlParameter("@UserRoleId", model.UserRole.id.ToDBValue()), new SqlParameter("@times", model.times.ToDBValue()), new SqlParameter("@isLock", model.isLock.ToDBValue()), new SqlParameter("@createdAt", model.createdAt.ToDBValue()), new SqlParameter("@updatedAt", model.updatedAt.ToDBValue()));
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
            const string sql = "DELETE FROM [dbo].[Users] WHERE [id] = @id";
            return SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@id", id)) > 0;
        }
        #endregion
        #region ��������ID����һ����¼ +bool Update(Users model)
        /// <summary>
        /// ������������һ����¼
        /// </summary>
        /// <param name="model">���º��ʵ��</param>
        /// <returns>�Ƿ�ɹ�</returns>
        public bool Update(Users model)
        {
            const string sql = @"UPDATE [dbo].[Users] SET  phone=@phone,password=@password,nick=@nick,UserRoleId=@UserRoleId,times=@times,isLock=@isLock,createdAt=@createdAt,updatedAt=@updatedAt  WHERE [id] = @id";
            return SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@id", model.id.ToDBValue()), new SqlParameter("@phone", model.phone.ToDBValue()), new SqlParameter("@password", model.password.ToDBValue()), new SqlParameter("@nick", model.nick.ToDBValue()), new SqlParameter("@UserRoleId", model.UserRole.id.ToDBValue()), new SqlParameter("@times", model.times.ToDBValue()), new SqlParameter("@isLock", model.isLock.ToDBValue()), new SqlParameter("@createdAt", model.createdAt.ToDBValue()), new SqlParameter("@updatedAt", model.updatedAt.ToDBValue())) > 0;
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
            string sql = "SELECT COUNT(1) from Users " + str; var res = SqlHelper.ExecuteScalar(sql, list.ToArray());
            return res == null ? 0 : Convert.ToInt32(res);
        }
        #endregion
        #region ��ѯ����ģ��ʵ�� +Users QuerySingleById(string id)
        /// <summary>
        /// ��ѯ����ģ��ʵ��
        /// </summary>
        /// <param name="id">id</param>);
        /// <returns>ʵ��</returns>);
        public Users QuerySingleById(string id)
        {
            const string sql = "SELECT TOP 1 id,phone,password,nick,UserRoleId,times,isLock,createdAt,updatedAt from Users WHERE [id] = @id";
            using (var reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@id", id)))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    Users model = SqlHelper.MapEntity<Users>(reader);
                    UserRoleDAO UserRoleDAO = new UserRoleDAO();
                    model.UserRole = UserRoleDAO.QuerySingleById((string)reader["UserRoleId"]);
                    return model;
                }
                else
                {
                    return null;
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
            string cols = "id,phone,password,nick,times,isLock,createdAt,updatedAt";
            if (columns != null)
            {
                cols = string.Join(",", (string[])columns);
            }
            string sql = "SELECT TOP 1 " + cols + " from Users WHERE [objectId] = @objectId";
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
        #region ��ѯ����ģ��ʵ�� +Users QuerySingleByWheres(object wheres)
        ///<summary>
        ///��ѯ����ģ��ʵ��
        ///</summary>
        ///<param name="wheres">����������</param>
        ///<returns>ʵ��</returns>
        public Users QuerySingleByWheres(object wheres)
        {
            var list = QueryList(1, 1, wheres);
            return list != null && list.Any() ? list.FirstOrDefault() : null;
        }
        #endregion
        #region ��ѯ����ģ���м��� +Dictionary<string, object> QuerySingleByWheresX(object wheres,object columns)
        ///<summary>
        ///��ѯ����ģ��ʵ��
        ///</summary>
        ///<param name="wheres">����</param>
        ///<param name="columns">�м���</param>
        ///<returns>ʵ��</returns>
        public Dictionary<string, object> QuerySingleByWheresX(object wheres, object columns)
        {
            string cols = "id,phone,password,nick,times,isLock,createdAt,updatedAt";
            if (columns != null)
            {
                cols = string.Join(", ", (string[])columns);
            }
            List<SqlParameter> list = null;
            string where = wheres.parseWheres(out list);
            where = string.IsNullOrEmpty(where) ? string.Empty : " where " + where;
            string sql = "SELECT TOP 1 " + cols + " from Users" + where;
            using (var reader = SqlHelper.ExecuteReader(sql, list.ToArray()))
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
        #region ��ҳ��ѯһ������ +IEnumerable<Users> QueryList(int index, int size, object wheres=null, string orderField=id, bool isDesc = true)
        ///<summary>
        ///��ҳ��ѯһ������
        ///</summary>
        ///<param name="index">ҳ��</param>
        ///<param name="size">ҳ��С</param>
        ///<param name="wheres">����������</param>
        ///<param name="orderField">�����ֶ�</param>
        ///<param name="isDesc">�Ƿ�������</param>
        ///<returns>ʵ�弯��</returns>
        public IEnumerable<Users> QueryList(int index, int size, object wheres = null, string orderField = "id", bool isDesc = true)
        {
            List<SqlParameter> list = null;
            string where = wheres.parseWheres(out list);
            orderField = string.IsNullOrEmpty(orderField) ? "id" : orderField;
            var sql = SqlHelper.GenerateQuerySql("Users", new string[] { "id", "phone", "password", "nick", "UserRoleId", "times", "isLock", "createdAt", "updatedAt" }, index, size, where, orderField, isDesc);
            using (var reader = SqlHelper.ExecuteReader(sql, list.ToArray()))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Users model = SqlHelper.MapEntity<Users>(reader);
                        UserRoleDAO UserRoleDAO = new UserRoleDAO();
                        model.UserRole = UserRoleDAO.QuerySingleById((string)reader["UserRoleId"]);
                        yield return model;
                    }
                }
            }
        }
        #endregion
        #region ��ҳ��ѯһ������ +IEnumerable<Dictionary<string, object>> QueryListX(int index, int size, object columns = null, object wheres = null, string orderField=id, bool isDesc = true)
        ///<summary>
        ///��ҳ��ѯһ������
        ///</summary>
        ///<param name="index">ҳ��</param>
        ///<param name="size">ҳ��С</param>
        ///<param name="columns">ָ������</param>
        ///<param name="wheres">����������</param>
        ///<param name="orderField">�����ֶ�</param>
        ///<param name="isDesc">�Ƿ�������</param>
        ///<returns>ʵ�弯��</returns>
        public IEnumerable<Dictionary<string, object>> QueryListX(int index, int size, object columns = null, object wheres = null, string orderField = "id", bool isDesc = true)
        {
            List<SqlParameter> list = null;
            string where = wheres.parseWheres(out list);
            orderField = string.IsNullOrEmpty(orderField) ? "id" : orderField;
            Dictionary<string, string[]> li;
            string[] clumns = new String[] { "id", "phone", "password", "nick", "times", "isLock", "createdAt", "updatedAt" };
            string[] cls = columns.parseColumnsX(clumns, "Users", out li);
            var sql = SqlHelper.GenerateQuerySql("Users", li["Users"], index, size, where, orderField, isDesc);
            using (var reader = SqlHelper.ExecuteReader(sql, list.ToArray()))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Dictionary<string, object> model = SqlHelper.MapEntity(reader, li["Users"]);
                        if (li.ContainsKey("UserRole"))
                        {
                            UserRoleDAO UserRoleDAO = new UserRoleDAO();
                            model["UserRole"] = UserRoleDAO.QuerySingleByIdX((string)reader["UserRoleId"], li["UserRole"]);
                        }

                        yield return model;
                    }
                }
            }
        }
        #endregion
        #region ���������޸�ָ���� +bool UpdateById(string id,object columns)
        /// <summary>
        /// ������������ָ����¼
        /// </summary>
        /// <param name="id">����</param>
        /// <param name="columns">�м��϶���</param>
        /// <returns>�Ƿ�ɹ�</returns>
        public bool UpdateById(string id, object columns)
        {
            List<SqlParameter> list = null;
            string[] column = columns.parseColumns(out list);
            list.Add(new SqlParameter("@id", id.ToDBValue()));
            string sql = string.Format(@"UPDATE [dbo].[Users] SET  {0}  WHERE [{1}] = @{1}", string.Join(",", column), "id");
            return SqlHelper.ExecuteNonQuery(sql, list.ToArray()) > 0;
        }
        #endregion
        #region �����������¼�¼+bool UpdateByWheres(object wheres, object columns)
        /// <summary>
        /// �����������¼�¼
        /// </summary>
        /// <param name="wheres">��������ʵ��ʵ��</param>
        /// <param name="columns">�м��϶���</param>
        /// <returns>�Ƿ�ɹ�</returns>
        public bool UpdateByWheres(object wheres, object columns)
        {
            List<SqlParameter> list = null;
            string[] column = columns.parseColumns(out list);
            List<SqlParameter> list1 = null;
            string where = wheres.parseWheres(out list1);
            where = string.IsNullOrEmpty(where) ? string.Empty : "where " + where;
            list.AddRange(list1);
            string sql = string.Format(@"UPDATE [dbo].[Users] SET  {0} ", string.Join(",", column), where);
            return SqlHelper.ExecuteNonQuery(sql, list.ToArray()) > 0;
        }
        #endregion
    }
}