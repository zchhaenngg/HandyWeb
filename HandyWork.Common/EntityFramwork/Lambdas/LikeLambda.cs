using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramwork.Lambdas
{
    public class LikeLambda<TEntity> : BaseLambda<TEntity, string>
    {
        public string ValueStr => Value as string;

        public LikeLambda(Expression<Func<TEntity, string>> entityProperty, string entityValue) : base(entityProperty, entityValue)
        {
        }

        public override Expression<Func<TEntity, bool>> Build()
        {
            var parameter = Expression.Parameter(typeof(TEntity), "o");
            var propertyName = (PropertyExpression.Body as MemberExpression).Member.Name;
            var member = Expression.Property(parameter, propertyName);

            var body = Expression.Call(member, typeof(string).GetMethod(nameof(string.Contains)), Expression.Constant(ValueStr));
            return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
        }
    }
}
