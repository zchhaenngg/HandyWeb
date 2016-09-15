using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramwork.Lambdas
{
    /// <summary>
    /// 数组、集合。  如果是关于string的请使用LikeLambda
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public class ContainLambda<TEntity, TProperty> : BaseLambda<TEntity, TProperty>
    {
        public ContainLambda(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue) : base(entityProperty, entityValue)
        {
        }

        public override Expression<Func<TEntity, bool>> Build()
        {
            var parameter = Expression.Parameter(typeof(TEntity), "o");
            var propertyName = (PropertyExpression.Body as MemberExpression).Member.Name;
            var member = Expression.Property(parameter, propertyName);

            var body = Expression.Call(Expression.Constant(Value), GetContainMethodInfo(), member);
            return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
        }

        public virtual MethodInfo GetContainMethodInfo()
        {
            if (Value is Array || Value is ICollection)
            {
                return Value.GetType().GetMethod("Contains");
            }
            else
            {
                throw new NotSupportedException(string.Format("ContainLambda {0}", Value.GetType().Name));
            }
        }
    }
}
