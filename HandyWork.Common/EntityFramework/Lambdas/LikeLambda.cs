using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramework.Lambdas
{
    /// <summary>
    /// 只支持string
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class LikeLambda<TEntity> : BaseLambda<TEntity>
    {
        public string ValueStr => Value as string;

        public LikeLambda(Type propertyType, string peopertyName, object entityValue) : base(propertyType, peopertyName, entityValue)
        {
        }

        public override Expression<Func<TEntity, bool>> Build()
        {
            var parameter = Expression.Parameter(typeof(TEntity), "o");
            var member = Expression.Property(parameter, PropertyName);

            var body = Expression.Call(member, typeof(string).GetMethod(nameof(string.Contains)), Expression.Constant(ValueStr));
            return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
        }
    }
}
