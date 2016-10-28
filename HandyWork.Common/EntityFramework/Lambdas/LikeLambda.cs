using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramework.Lambdas
{
    /// <summary>
    /// 只支持string
    /// </summary>
    public class LikeLambda : BaseLambda
    {
        public LikeLambda(Type propertyType, string peopertyName, object entityValue) : base(propertyType, peopertyName, entityValue)
        {
        }
    }
}
