using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManager
{
    public class UserLoginException:Exception
    {
        public UserLoginException(string e):base(e)
        {
        }
    }
}