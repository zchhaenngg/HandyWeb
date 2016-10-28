using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramework.Lambdas
{
    /// <summary>
    /// 数字、时间
    /// </summary>
    public class GreaterThanOrEqualLambda : BaseLambda
    {
        public GreaterThanOrEqualLambda(Type propertyType, string peopertyName, object entityValue) : base(propertyType, peopertyName, entityValue)
        {
            ExpressionType = ExpressionType.GreaterThanOrEqual;
        }
    }
}
