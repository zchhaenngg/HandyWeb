using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Time
{
    /// <summary>
    /// UTCTime的Value始终会是UTC时间
    /// </summary>
    public struct UTCTime
    {
        private DateTime _datetime;

        public UTCTime(DateTime time): this()
        {
            Value = time;
        }

        public DateTime Value
        {
            get
            {
                if (_datetime == DateTime.MinValue)
                {//默认值也要转换为UTC时间
                    _datetime = DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);
                }
                return _datetime;
            }
            set
            {
                
                if (value.Kind == DateTimeKind.Unspecified)
                {
                    if (value == DateTime.MinValue)
                    {//默认值也要转换为UTC时间
                        value = DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);
                    }
                    else
                    {
                        throw new InvalidCastException("Unspecified 时间无法转换成UTC时间");
                    }
                }
                _datetime = value.ToUniversalTime();
            }
        }
        
        public static UTCTime Now => new UTCTime(DateTime.UtcNow);

        //datetime->UTCTime
        public static implicit operator UTCTime(DateTime time)
        {
            return new UTCTime { Value = time };
        }
        public static implicit operator UTCTime?(DateTime? time)
        {
            if (time == null)
            {
                return null;
            }
            else
            {
                return time.Value;
            }
            
        }
        public static explicit operator UTCTime(DateTime? time)
        {
            if (time == null)
            {
                throw new InvalidCastException("time为空无法转换成UTCTime");
            }
            else
            {
                return time.Value;
            }

        }
        //UTCTime->datetime
        public static implicit operator DateTime(UTCTime time)
        {
            return time.Value;
        }
        public static implicit operator DateTime? (UTCTime? time)
        {
            if (time == null)
            {
                return null;
            }
            else
            {
                return time.Value;
            }

        }
        public static explicit operator DateTime(UTCTime? time)
        {
            if (time == null)
            {
                throw new InvalidCastException("time为空无法转换成DateTime");
            }
            else
            {
                return time.Value;
            }

        }
    }
}
