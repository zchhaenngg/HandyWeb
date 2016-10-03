using HandyWork.Common.EntityFramework.Elements;
using HandyWork.Common.EntityFramework.Lambdas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Common.Extensions;

namespace HandyWork.Common.Utility
{
    public static class ExpressionUtility
    {
        public static Expression<Func<T, bool>> True<T>()
        {
            Expression<Func<T, bool>> expression = o => true;
            return expression;
        }
        public static Expression<Func<T, bool>> False<T>()
        {
            Expression<Func<T, bool>> expression = o => false;
            return expression;
        }

        /// <summary>
        /// lambdas之前使用And相连
        /// </summary>
        public static Expression<Func<TEntity, bool>> Build<TEntity, TProperty>(BaseTag condition, params BaseLambda<TEntity, TProperty>[] lambdas)
        {
            Expression<Func<TEntity, bool>> expression = null;
            if (condition.IsPassed)
            {
                foreach (var lambda in lambdas)
                {
                    expression = expression.And(lambda.Build());
                }
            }
            return expression;
        }
    }
}
