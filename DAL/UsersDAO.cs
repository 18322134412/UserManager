using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManager.Utility;

namespace UserManager.DAO
{
    public partial class UsersDAO
    {
        public bool existPhone(string phone) {
            string sql = "select top 1  phone from Users where phone=@phone";
            return SqlHelper.ExecuteScalar(sql, new SqlParameter("@phone", phone))==null;
        }
        public bool isForzen(string id)
        {
            string sql = "select top 1  isLock from Users where id=@id";
            return bool.Parse(SqlHelper.ExecuteScalar(sql, new SqlParameter("@id", id)).ToString());
        }
    }
}
