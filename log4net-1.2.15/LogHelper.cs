using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace log4net
{
    public static class LogHelper
    {
        public static ILog Log { get; } = LogManager.GetLogger("Default");
        public static ILog ErrorLog { get; } = LogManager.GetLogger("ErrorLog");
    }
}
