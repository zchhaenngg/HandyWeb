using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Helper
{
    public static class LogHelper
    {
        public static ILog Log { get; } = LogManager.GetLogger("Default");
        public static ILog ErrorLog { get; } = LogManager.GetLogger("ErrorLog");

    }
}
