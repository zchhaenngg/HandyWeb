using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Model
{
    /// <summary>
    /// UI主动抛出的异常，需要被处理，且被认为程序正常运行时可能会被触发的，不需要记录日志。
    /// </summary>
    public class UIAjaxException : Exception
    {
        public UIAjaxException(string message)
            :base(message)
        {
        }
    }
}
