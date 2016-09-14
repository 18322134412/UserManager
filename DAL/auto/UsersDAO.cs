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
        #region 向数据库中添加一条记录 +bool Insert(Users model)
        ///<summary>
        ///向数据库中添加一条记录
        ///</summary>
        ///<param name="model">要添加的实体</param>
        public bool Insert(Users model)
        {
            const string sql = @"INSERT INTO [dbo].[Users] (id,phone,password,nick,UserRoleId,times,isLock,createdAt,updatedAt) VALUES (@id,@phone,@password,@nick,@UserRoleId,@times,@isLock,@createdAt,@updatedAt)";
            int res = SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@id", model.id.ToDBValue()), new SqlParameter("@phone", model.phone.ToDBValue()), new SqlParameter("@password", model.password.ToDBValue()), new SqlParameter("@nick", model.nick.ToDBValue()), new SqlParameter("@UserRoleId", model.UserRole.id.ToDBValue()), new SqlParameter("@times", model.times.ToDBValue()), new SqlParameter("@isLock", model.isLock.ToDBValue()), new SqlParameter("@createdAt", model.createdAt.ToDBValue()), new SqlParameter("@updatedAt", model.updatedAt.ToDBValue()));
            return res > 0;
        }
        #endregion
        #region 删除一条记录 +bool Delete(string id)
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>是否成功</returns>
        public bool Delete(string id)
        {
            const string sql = "DELETE FROM [dbo].[Users] WHERE [id] = @id";
            return SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@id", id)) > 0;
        }
        #endregion
        #region 根据主键ID更新一条记录 +bool Update(Users model)
        /// <summary>
        /// 根据主键更新一条记录
        /// </summary>
        /// <param name="model">更新后的实体</param>
        /// <returns>是否成功</returns>
        public bool Update(Users model)
        {
            const string sql = @"UPDATE [dbo].[Users] SET  phone=@phone,password=@password,nick=@nick,UserRoleId=@UserRoleId,times=@times,isLock=@isLock,createdAt=@createdAt,updatedAt=@updatedAt  WHERE [id] = @id";
            return SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@id", model.id.ToDBValue()), new SqlParameter("@phone", model.phone.ToDBValue()), new SqlParameter("@password", model.password.ToDBValue()), new SqlParameter("@nick", model.nick.ToDBValue()), new SqlParameter("@UserRoleId", model.UserRole.id.ToDBValue()), new SqlParameter("@times", model.times.ToDBValue()), new SqlParameter("@isLock", model.isLock.ToDBValue()), new SqlParameter("@createdAt", model.createdAt.ToDBValue()), new SqlParameter("@updatedAt", model.updatedAt.ToDBValue())) > 0;
        }
        #endregion
        #region 查询条数 +int QueryCount(object wheres)
        /// <summary>
        /// 查询条数
        /// </summary>
        /// <param name="wheres">查询条件</param>
        /// <returns>条数</returns>
        public int QueryCount(object wheres)
        {
            List<SqlParameter> list = null;
            string str = wheres.parseWheres(out list);
            str = str == "" ? str : "where " + str;
            string sql = "SELECT COUNT(1) from Users " + str; var res = SqlHelper.ExecuteScalar(sql, list.ToArray());
            return res == null ? 0 : Convert.ToInt32(res);
        }
        #endregion
        #region 查询单个模型实体 +Users QuerySingleById(string id)
        /// <summary>
        /// 查询单个模型实体
        /// </summary>
        /// <param name="id">id</param>);
        /// <returns>实体</returns>);
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
        #region 查询单个模型实体 +_User QuerySingleByIdX(string objectId,string columns){
        ///<summary>
        ///查询单个模型实体
        ///</summary>
        ///<param name=id>主键</param>);
        ///<returns>实体</returns>);
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
        #region 查询单个模型实体 +Users QuerySingleByWheres(object wheres)
        ///<summary>
        ///查询单个模型实体
        ///</summary>
        ///<param name="wheres">条件匿名类</param>
        ///<returns>实体</returns>
        public Users QuerySingleByWheres(object wheres)
        {
            var list = QueryList(1, 1, wheres);
            return list != null && list.Any() ? list.FirstOrDefault() : null;
        }
        #endregion
        #region 查询单个模型列集合 +Dictionary<string, object> QuerySingleByWheresX(object wheres,object columns)
        ///<summary>
        ///查询单个模型实体
        ///</summary>
        ///<param name="wheres">条件</param>
        ///<param name="columns">列集合</param>
        ///<returns>实体</returns>
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
        #region 分页查询一个集合 +IEnumerable<Users> QueryList(int index, int size, object wheres=null, string orderField=id, bool isDesc = true)
        ///<summary>
        ///分页查询一个集合
        ///</summary>
        ///<param name="index">页码</param>
        ///<param name="size">页大小</param>
        ///<param name="wheres">条件匿名类</param>
        ///<param name="orderField">排序字段</param>
        ///<param name="isDesc">是否降序排序</param>
        ///<returns>实体集合</returns>
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
        #region 分页查询一个集合 +IEnumerable<Dictionary<string, object>> QueryListX(int index, int size, object columns = null, object wheres = null, string orderField=id, bool isDesc = true)
        ///<summary>
        ///分页查询一个集合
        ///</summary>
        ///<param name="index">页码</param>
        ///<param name="size">页大小</param>
        ///<param name="columns">指定的列</param>
        ///<param name="wheres">条件匿名类</param>
        ///<param name="orderField">排序字段</param>
        ///<param name="isDesc">是否降序排序</param>
        ///<returns>实体集合</returns>
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
        #region 根据主键修改指定列 +bool UpdateById(string id,object columns)
        /// <summary>
        /// 根据主键更新指定记录
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="columns">列集合对象</param>
        /// <returns>是否成功</returns>
        public bool UpdateById(string id, object columns)
        {
            List<SqlParameter> list = null;
            string[] column = columns.parseColumns(out list);
            list.Add(new SqlParameter("@id", id.ToDBValue()));
            string sql = string.Format(@"UPDATE [dbo].[Users] SET  {0}  WHERE [{1}] = @{1}", string.Join(",", column), "id");
            return SqlHelper.ExecuteNonQuery(sql, list.ToArray()) > 0;
        }
        #endregion
        #region 根据条件更新记录+bool UpdateByWheres(object wheres, object columns)
        /// <summary>
        /// 根据条件更新记录
        /// </summary>
        /// <param name="wheres">条件集合实体实体</param>
        /// <param name="columns">列集合对象</param>
        /// <returns>是否成功</returns>
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