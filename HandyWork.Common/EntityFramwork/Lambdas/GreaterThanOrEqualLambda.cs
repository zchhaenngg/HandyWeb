using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramwork.Lambdas
{
    public class GreaterThanOrEqualLambda<TEntity, TProperty> : BaseLambda<TEntity, TProperty>
    {
        public GreaterThanOrEqualLambda(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue) : base(entityProperty, entityValue)
        {
            ExpressionType = ExpressionType.GreaterThanOrEqual;
        }
        public GreaterThanOrEqualLambda<TEntity, TProperty> For(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue)
        {
            return new GreaterThanOrEqualLambda<TEntity, TProperty>(entityProperty, entityValue);
        }
    }
}
