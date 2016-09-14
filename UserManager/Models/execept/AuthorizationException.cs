using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManager
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string e)
            : base(e)
        {
        }
    }
}