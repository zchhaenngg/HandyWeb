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
    public class ContainLambda : BaseLambda
    {
        public ContainLambda(Type propertyType, string peopertyName, object entityValue) : base(propertyType, peopertyName, entityValue)
        {
        }



        public override Expression<Func<TEntity, bool>> Build<TEntity>()
        {
            var parameter = Expression.Parameter(typeof(TEntity), "o");
            var member = Expression.Property(parameter, PropertyName);
            
            var body = Expression.Call(Expression.Constant(Value), GetContainMethodInfo(), member);
            return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
        }

        public virtual MethodInfo GetContainMethodInfo()
        {
            var method = typeof(List<>).MakeGenericType(PropertyType).GetMethod("Contains");
            return method;
        }
    }
}
