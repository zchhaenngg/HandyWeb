using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Common.Extensions;

namespace HandyWork.Common.EntityFramework.Lambdas
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

        public static ContainLambda<TEntity, TProperty> For(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue)
        {
            return new ContainLambda<TEntity, TProperty>(entityProperty, entityValue);
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
            return Value.GetType().GetMethod("Contains");
        }
    }
}
