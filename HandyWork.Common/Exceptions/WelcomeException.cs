using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Exceptions
{
    public class WelcomeException : LogException
    {
        public WelcomeException(string message) 
            : base(message, false)
        {
        }
    }
}
