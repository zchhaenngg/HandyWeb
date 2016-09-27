using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.UTC
{
    public struct UTCTime
    {
        private DateTime _datetime;

        public UTCTime(DateTime utc): this()
        {
            Value = utc;
        }

        public DateTime Value
        {
            get
            {
                return _datetime.ToUniversalTime();
            }
            set
            {
                if (value.Kind == DateTimeKind.Unspecified)
                {
                    throw new InvalidCastException("Unspecified 时间无法转换成UTC时间");
                }
                _datetime = value.ToUniversalTime();
            }
        }

        public static implicit operator UTCTime(DateTime utctime)
        {
            return new UTCTime { Value = utctime };
        }
    }
}
