using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramework.Lambdas
{
    /// <summary>
    /// 所有类型
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EqualLambda<TEntity> : BaseLambda<TEntity>
    {
        public EqualLambda(Type propertyType, string peopertyName, object entityValue) : base(propertyType, peopertyName, entityValue)
        {
            ExpressionType = ExpressionType.Equal;
        }
    }
}
