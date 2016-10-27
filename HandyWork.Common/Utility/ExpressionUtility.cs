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
        public static Expression<Func<TEntity, bool>> Build<TEntity>(BaseTag condition, params BaseLambda[] lambdas)
        {
            Expression<Func<TEntity, bool>> expression = null;
            if (condition.IsPassed)
            {
                foreach (var lambda in lambdas)
                {
                    expression = expression.And(lambda.Build<TEntity>());
                }
            }
            return expression;
        }
        
        public static LambdaExpression GetLambdaExpressionOfProperty<T>(string propertyStr, ParameterExpression paramExpression = null)
        {////获取每级属性如c.Users.Proiles.UserId
            paramExpression = paramExpression ?? Expression.Parameter(typeof(T), "c");
            Expression propertyAccess = paramExpression;
            var props = propertyStr.Split('.');
            var typeOfProp = typeof(T);
            foreach (var item in props)
            {
                var property = typeOfProp.GetProperty(item);
                if (property == null)
                {
                    throw new Exception(string.Format("实体没有属性 {0}", item));
                }
                typeOfProp = property.PropertyType;
                propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
            }
            return Expression.Lambda(propertyAccess, paramExpression);
        }
        
    }
}
