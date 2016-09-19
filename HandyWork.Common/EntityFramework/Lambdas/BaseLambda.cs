using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramework.Lambdas
{
    public abstract class BaseLambda<TEntity, TProperty>
    {
        protected object Value { get; set; }

        protected ExpressionType ExpressionType { get; set; }

        protected Expression<Func<TEntity, TProperty>> PropertyExpression { get; set; }

        public BaseLambda(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue)
        {
            PropertyExpression = entityProperty;
            Value = entityValue;
        }

        public virtual Expression<Func<TEntity, bool>> Build()
        {
            var parameter = Expression.Parameter(typeof(TEntity), "o");
            var propertyName = (PropertyExpression.Body as MemberExpression).Member.Name;
            var member = Expression.Property(parameter, propertyName);
            var binary = Expression.MakeBinary(ExpressionType, member, Expression.Constant(Value));
            return Expression.Lambda<Func<TEntity, bool>>(binary, parameter);
        }
    }
}
