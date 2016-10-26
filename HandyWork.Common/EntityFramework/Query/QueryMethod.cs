using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramework.Query
{
    public enum QueryMethod
    {
        /// <summary>
        /// 如entity.a == 5
        /// </summary>
        Equal = 1,
        /// <summary>
        /// entity.a 小于 5
        /// </summary>
        LessThan = 2,
        /// <summary>
        /// entity.a > 5
        /// </summary>
        GreaterThan = 4,
        /// <summary>
        /// entity.a 小于等于 5
        /// </summary>
        LessThanOrEqual = 8,
        /// <summary>
        /// entity.a >= 5
        /// </summary>
        GreaterThanOrEqual = 16,
        /// <summary>
        /// entity.a 的模糊查询
        /// </summary>
        Like = 32,
        /// <summary>
        /// 数组、集合。  如果是关于string的请使用LikeLambda
        /// </summary>
        Contain = 64,
        /// <summary>
        /// entity.a != 5
        /// </summary>
        NotEqual = 128,
        NotLike = 256,
        NotContain = 512,
    }
}
