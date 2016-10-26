using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramework.Lambdas
{
    /// <summary>
    /// 支持所有类型
    /// </summary>
    public class NotEqualLambda<TEntity> : BaseLambda<TEntity>
    {
        public NotEqualLambda(Type propertyType, string peopertyName, object entityValue) : base(propertyType, peopertyName, entityValue)
        {
            ExpressionType = ExpressionType.NotEqual; 
        }
    }
}
