using HandyWork.Common.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Ext
{
    public static class DateTimeEx
    {
        public static DateTime ToDay(this DateTime dt)
        {
            return Convert.ToDateTime(dt.ToDayString());
        }
        public static Nullable<DateTime> ToDay(this Nullable<DateTime> dt)
        {
            if (dt == null)
            {
                return null;
            }
            else
            {
                return dt.Value.ToDay();
            }
        }
        public static string ToDayString(this DateTime dt)
        {
            return dt.ToString(DateTimeFormat.ToDay);
        }
        public static string ToDayString(this Nullable<DateTime> dt)
        {
            return dt == null ? null : dt.Value.ToDayString();
        }
        public static string ToMinString(this DateTime dt)
        {
            return dt.ToString(DateTimeFormat.ToMin);
        }
        public static string ToMinString(this Nullable<DateTime> dt)
        {
            return dt == null ? null : dt.Value.ToMinString();
        }
        public static string ToSecondString(this DateTime dt)
        {
            return dt.ToString(DateTimeFormat.ToSecond);
        }
        public static string ToSecondString(this Nullable<DateTime> dt)
        {
            return dt == null ? null : dt.Value.ToSecondString();
        }
        public static string ToMillsecondString(this DateTime dt)
        {
            return dt.ToString(DateTimeFormat.ToMillsecond);
        }
        public static string ToMillsecondString(this Nullable<DateTime> dt)
        {
            return dt == null ? null : dt.Value.ToMillsecondString();
        }

        /// <summary>
        /// Null返回0,返回dt.Value.Hour
        /// </summary>
        public static int Hour(this Nullable<DateTime> dt)
        {
            if (dt == null)
            {
                return 0;
            }
            return dt.Value.Hour;
        }
        /// <summary>
        /// dt-dt2,dt或dt2为null则返回0,四舍五入
        /// </summary>
        public static int GetHoursByMinus(this Nullable<DateTime> dt, Nullable<DateTime> dt2)
        {
            if (dt == null || dt2 == null)
            {
                return 0;
            }
            else
            {
                double minutes = (dt.Value - dt2.Value).TotalMinutes + 30;
                return (int)(minutes / 60);
            }
        }

        public static bool IsToday(this DateTime dt)
        {
            return dt.ToDayString().Equals(DateTime.Now.ToDayString());
        }
        public static bool IsToday(this Nullable<DateTime> dt)
        {
            return dt == null ? false : dt.Value.IsToday();
        }
    }
}
