using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramework.Query
{
    public enum QueryIf
    {
        /// <summary>
        /// 空对象，空数组、空集合、空值、空格
        /// </summary>
        IsEmpty = 1,
        /// <summary>
        /// 非空对象，非空数组、非空集合、非空值、非空格
        /// </summary>
        IsNotEmpty = 2,
        IsNotNull = 4,
        IsNull = 8
    }
}
