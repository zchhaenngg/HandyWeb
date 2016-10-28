using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramework.Elements
{
    public abstract class BaseTag
    {
        public object Value { get; set; }

        /// <summary>
        /// 通过表达式构建前验证
        /// </summary>
        public abstract bool IsPassed { get; }
    }
}
