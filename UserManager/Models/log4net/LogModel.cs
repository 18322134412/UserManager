using log4net.Core;
using log4net.Layout.Pattern;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace UserManager.Models
{
    internal sealed class LogId : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            var content = loggingEvent.MessageObject as ErrorLog;
            if (content != null)
            {
                writer.Write(content.id);
            }
        }
    }
    internal sealed class LogUserid : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            var content = loggingEvent.MessageObject as ErrorLog;
            if (content != null)
            {
                writer.Write(content.userid);
            }
        }
    }
    internal sealed class LogMessage : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            var content = loggingEvent.MessageObject as ErrorLog;
            if (content != null)
            {
                writer.Write(content.message);
            }
        }
    }
}