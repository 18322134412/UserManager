using log4net.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManager.Models
{
    public class CustomLayout : PatternLayout 
    {
        public CustomLayout()
        {

            base.AddConverter("LogId", typeof(LogId));
            base.AddConverter("LogUserid", typeof(LogUserid));
            base.AddConverter("LogMessage", typeof(LogMessage));
        }
    }
}