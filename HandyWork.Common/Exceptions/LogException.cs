using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Exceptions
{
    [Serializable]
    public class LogException : Exception
    {
        public bool ShouldWriteLog { get; set; }

        public LogException(string message, bool shouldWriteLog = true)
            : base(message)
        {
            this.ShouldWriteLog = shouldWriteLog;
        }
    }
}
