using HandyWork.Common.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Extensions
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// 获取此实例的日期部分如2016-09-30 00:00:00
        /// </summary>
        public static DateTime ToDay(this DateTime dt)
        {
            return dt.Date;
        }
        /// <summary>
        /// 获取此实例的日期部分如2016-09-30 00:00:00
        /// </summary>
        public static DateTime? ToDay(this DateTime? dt)
        {
            if (dt == null)
            {
                return null;
            }
            else
            {
                return dt.Value.Date;
            }
        }
        /// <summary>
        /// yyyy-MM-dd
        /// </summary>
        public static string ToDayString(this DateTime dt)
        {
            return dt.ToString(Formats.ToDay);
        }
        /// <summary>
        /// yyyy-MM-dd
        /// </summary>
        public static string ToDayString(this DateTime? dt)
        {
            return dt == null ? null : dt.Value.ToDayString();
        }
        /// <summary>
        /// yyyy-MM-dd HH:mm
        /// </summary>
        public static string ToMinString(this DateTime dt)
        {
            return dt.ToString(Formats.ToMin);
        }
        /// <summary>
        /// yyyy-MM-dd HH:mm
        /// </summary>
        public static string ToMinString(this DateTime? dt)
        {
            return dt == null ? null : dt.Value.ToMinString();
        }
        /// <summary>
        /// yyyy-MM-dd HH:mm:ss
        /// </summary>
        public static string ToSecondString(this DateTime dt)
        {
            return dt.ToString(Formats.ToSecond);
        }
        /// <summary>
        /// yyyy-MM-dd HH:mm:ss
        /// </summary>
        public static string ToSecondString(this DateTime? dt)
        {
            return dt == null ? null : dt.Value.ToSecondString();
        }
        /// <summary>
        /// yyyy-MM-dd HH:mm:ss.fff
        /// </summary>
        public static string ToMillsecondString(this DateTime dt)
        {
            return dt.ToString(Formats.ToMillsecond);
        }
        /// <summary>
        /// yyyy-MM-dd HH:mm:ss.fff
        /// </summary>
        public static string ToMillsecondString(this DateTime? dt)
        {
            return dt == null ? null : dt.Value.ToMillsecondString();
        }
        /// <summary>
        /// dt-dt2,可能结果如-34,-5,0,6,23,46,520等
        /// </summary>
        public static int SubtractForHours(this DateTime? dt, DateTime? dt2)
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
        public static bool IsToday(this DateTime? dt)
        {
            return dt == null ? false : dt.Value.IsToday();
        }
        /// <summary>
        /// null返回0,返回0-23
        /// </summary>
        public static int Hour(this DateTime? dt)
        {
            if (dt == null)
            {
                return 0;
            }
            return dt.Value.Hour;
        }
    }
}
