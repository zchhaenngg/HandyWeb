using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramework.Lambdas
{
    /// <summary>
    /// 支持所有类型
    /// </summary>
    public class NotEqualLambda<TEntity, TProperty> : BaseLambda<TEntity, TProperty>
    {
        public NotEqualLambda(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue) : base(entityProperty, entityValue)
        {
            ExpressionType = ExpressionType.NotEqual; 
        }

        public static NotEqualLambda<TEntity, TProperty> For(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue)
        {
            return new NotEqualLambda<TEntity, TProperty>(entityProperty, entityValue);
        }
    }
}
