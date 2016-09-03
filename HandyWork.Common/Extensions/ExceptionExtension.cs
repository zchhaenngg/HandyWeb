using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Extensions
{
    public static class ExceptionExtension
    {
        /// <summary>
        /// 从Exception中获取指定类型的Exception如果没有返回null
        /// </summary>
        public static T GetException<T>(this Exception ex)
            where T : Exception
        {
            if (ex is T)
            {
                return ex as T;
            }
            else
            {
                if (ex.InnerException != null)
                {
                    return ex.InnerException.GetException<T>();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 从Exception中获取指定类型的Exception的Message如果没有返回null
        /// </summary>
        public static string GetExceptionMessage<T>(this Exception ex)
            where T : Exception
        {
            T exception = ex.GetException<T>();
            return exception == null ? null : exception.Message;
        }
    }
}
