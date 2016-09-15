using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Consts
{
    public enum QueryCondition
    {
        IsNull = 1,
        IsNotNull = 2,
        /// <summary>
        /// 如果Entity是varchar，则需要查出null和WhiteSpace两种情况
        /// </summary>
        IsNullOrWhiteSpace = 3,
        /// <summary>
        /// 如果Entity是varchar，则需要查出null和WhiteSpace两种情况
        /// </summary>
        IsNotNullOrWhiteSpace = 4
    }
}
