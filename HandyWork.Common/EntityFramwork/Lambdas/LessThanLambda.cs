using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramwork.Lambdas
{
    public class LessThanLambda<TEntity, TProperty> : BaseLambda<TEntity, TProperty>
    {
        public LessThanLambda(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue) : base(entityProperty, entityValue)
        {
            ExpressionType = ExpressionType.LessThan;
        }

        public LessThanLambda<TEntity, TProperty> For(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue)
        {
            return new LessThanLambda<TEntity, TProperty>(entityProperty, entityValue);
        }
    }
}
