using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// 将字符串转换成合适的UTC时间
        /// </summary>
        public static DateTime? ToUTCFromLogic(this string logic, int greaterThanUTCInMinute)
        {
            DateTime time;
            if (DateTime.TryParse(logic, out time))
            {
                return time.ToUTC(greaterThanUTCInMinute);
            }
            else
            {
                return null;
            }
        }

        public static int? ToInt(this string s)
        {
            int i;
            if (int.TryParse(s, out i))
            {
                return i;
            }
            throw new FormatException(s);
        }
    }
}
