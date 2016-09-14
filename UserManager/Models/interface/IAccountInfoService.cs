using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager
{
    public interface IAccountInfoService
    {
        bool Exist_User_IsForzen(string GUUId);
    }
}
