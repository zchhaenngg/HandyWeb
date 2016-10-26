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
    public class ContainLambda<TEntity> : BaseLambda<TEntity>
    {
        public ContainLambda(Type propertyType, string peopertyName, object entityValue) : base(propertyType, peopertyName, entityValue)
        {
        }

        public override Expression<Func<TEntity, bool>> Build()
        {
            var parameter = Expression.Parameter(typeof(TEntity), "o");
            var member = Expression.Property(parameter, PropertyName);

            var body = Expression.Call(Expression.Constant(Value), GetContainMethodInfo(), member);
            return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
        }

        public virtual MethodInfo GetContainMethodInfo()
        {
            return Value.GetType().GetMethod("Contains");
        }
    }
}
