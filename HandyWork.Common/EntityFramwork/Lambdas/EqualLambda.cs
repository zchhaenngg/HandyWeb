using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramwork.Lambdas
{
    public class EqualLambda<TEntity, TProperty> : BaseLambda<TEntity, TProperty>
    {
        public EqualLambda(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue) : base(entityProperty, entityValue)
        {
        }

        public override Expression<Func<TEntity, bool>> Build()
        {
            var parameter = Expression.Parameter(typeof(TEntity), "o");
            var propertyName = (PropertyExpression.Body as MemberExpression).Member.Name;
            var member = Expression.Property(parameter, propertyName);
            var binary = Expression.MakeBinary(ExpressionType.Equal, member, Expression.Constant(Value));
            return Expression.Lambda<Func<TEntity, bool>>(binary, parameter);
        }
    }
}
