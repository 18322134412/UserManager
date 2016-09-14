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
        #region 向数据库中添加一条记录 +bool Insert(UserRole model)
        ///<summary>
        ///向数据库中添加一条记录
        ///</summary>
        ///<param name="model">要添加的实体</param>
        public bool Insert(UserRole model)
        {
            const string sql = @"INSERT INTO [dbo].[UserRole] (id,name) VALUES (@id,@name)";
            int res = SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@id", model.id.ToDBValue()), new SqlParameter("@name", model.name.ToDBValue()));
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
            const string sql = "DELETE FROM [dbo].[UserRole] WHERE [id] = @id";
            return SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@id", id)) > 0;
        }
        #endregion
        #region 根据主键ID更新一条记录 +bool Update(UserRole model)
        /// <summary>
        /// 根据主键更新一条记录
        /// </summary>
        /// <param name="model">更新后的实体</param>
        /// <returns>是否成功</returns>
        public bool Update(UserRole model)
        {
            const string sql = @"UPDATE [dbo].[UserRole] SET  name=@name  WHERE [id] = @id";
            return SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@id", model.id.ToDBValue()), new SqlParameter("@name", model.name.ToDBValue())) > 0;
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
            string sql = "SELECT COUNT(1) from UserRole " + str; var res = SqlHelper.ExecuteScalar(sql, list.ToArray());
            return res == null ? 0 : Convert.ToInt32(res);
        }
        #endregion

        #region 查询单个模型实体 +UserRole QuerySingleById(string id)
        /// <summary>
        /// 查询单个模型实体
        /// </summary>
        /// <param name="id">id</param>);
        /// <returns>实体</returns>);
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

        #region 获取所有角色 +IEnumerable<UserRole> GetAllRole()
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns>角色集合</returns>
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

        #region 查询单个模型实体 +_User QuerySingleByIdX(string objectId,string columns){
        ///<summary>
        ///查询单个模型实体
        ///</summary>
        ///<param name=id>主键</param>);
        ///<returns>实体</returns>);
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