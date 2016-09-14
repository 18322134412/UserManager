using System.Linq;
using System.Collections.Generic;
using UserManager.DAO;
using UserManager.Model;
namespace UserManager.BLL
{
    public partial class UserRoleBLL
    {
        /// <summary>
        /// ���ݿ��������
        /// </summary>
        private UserRoleDAO _dao = new UserRoleDAO();
        #region �����ݿ������һ����¼ +bool; Insert(UserRole model)
        /// <summary>
        /// �����ݿ������һ����¼
        /// </summary>
        /// <param name="model">Ҫ��ӵ�ʵ��</param>
        /// <returns>�Ƿ�ɹ�</returns>
        public bool Insert(UserRole model)
        {
            return _dao.Insert(model);
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
            return _dao.Delete(id);
        }
        #endregion

        #region ��������ID����һ����¼ +bool Update(UserRole model)
        /// <summary>
        /// ������������һ����¼
        /// </summary>
        /// <param name="model">���º��ʵ��</param>
        /// <returns>ִ�н����Ӱ������</returns>
        public bool Update(UserRole model)
        {
            return _dao.Update(model);
        }
        #endregion

        #region ��ѯ���� +int QueryCount(object wheres)
        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="wheres">��ѯ����</param>
        /// <returns>ʵ��</returns>
        public int QueryCount(object wheres)
        {
            return _dao.QueryCount(wheres);
        }
        #endregion

        #region ��ȡ���н�ɫ +IEnumerable<UserRole> GetAllRole()
        /// <summary>
        /// ��ȡ���н�ɫ
        /// </summary>
        /// <returns>��ɫ����</returns>
        public IEnumerable<UserRole> GetAllRole()
        {
            return _dao.GetAllRole();
        }
        #endregion

    }
}