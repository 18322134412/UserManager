using System.Linq;
using System.Collections.Generic;
using UserManager.DAO;
using UserManager.Model;
namespace UserManager.BLL
{
    public partial class UserRoleBLL
    {
        /// <summary>
        /// 数据库操作对象
        /// </summary>
        private UserRoleDAO _dao = new UserRoleDAO();
        #region 向数据库中添加一条记录 +bool; Insert(UserRole model)
        /// <summary>
        /// 向数据库中添加一条记录
        /// </summary>
        /// <param name="model">要添加的实体</param>
        /// <returns>是否成功</returns>
        public bool Insert(UserRole model)
        {
            return _dao.Insert(model);
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
            return _dao.Delete(id);
        }
        #endregion

        #region 根据主键ID更新一条记录 +bool Update(UserRole model)
        /// <summary>
        /// 根据主键更新一条记录
        /// </summary>
        /// <param name="model">更新后的实体</param>
        /// <returns>执行结果受影响行数</returns>
        public bool Update(UserRole model)
        {
            return _dao.Update(model);
        }
        #endregion

        #region 查询条数 +int QueryCount(object wheres)
        /// <summary>
        /// 查询条数
        /// </summary>
        /// <param name="wheres">查询条件</param>
        /// <returns>实体</returns>
        public int QueryCount(object wheres)
        {
            return _dao.QueryCount(wheres);
        }
        #endregion

        #region 获取所有角色 +IEnumerable<UserRole> GetAllRole()
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns>角色集合</returns>
        public IEnumerable<UserRole> GetAllRole()
        {
            return _dao.GetAllRole();
        }
        #endregion

    }
}