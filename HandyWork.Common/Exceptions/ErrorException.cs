using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Exceptions
{
    public class ErrorException : LogException
    {
        public ErrorException(string message) 
            : base(message, true)
        {
        }
    }
}
