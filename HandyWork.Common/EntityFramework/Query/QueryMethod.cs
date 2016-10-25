using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramework.Query
{
    public enum QueryMethod
    {
        Equal = 1,
        LessThan = 2,
        GreaterThan = 4,
        LessThanOrEqual = 8,
        GreaterThanOrEqual = 16,
        Like = 32,
        Contain = 64,
        NotEqual = 128,
        NotLike = 256,
        NotContain = 512,
    }
}
