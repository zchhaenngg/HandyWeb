using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using HandyWork.Common.Extensions;
using HandyWork.Common.EntityFramework.Query;
using HandyWork.Common.EntityFramework.Elements;

namespace HandyWork.Common.EntityFramework.Lambdas
{
    public static class LambdaUtility
    {   
        public static BaseLambda GetLambda<TEntity, TValue>(QueryMethod method, Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            var propertyName = (entityProperty.Body as MemberExpression).Member.Name;
            return GetLambda(method, typeof(TValue), propertyName, value);
        }
        public static BaseLambda GetLambda<TEntity>(QueryItem item)
        {
            var propertyType = typeof(TEntity).GetProperty(item.Field).PropertyType;
            return GetLambda(item.Method, propertyType, item.Field, item.Value);
        }
        public static BaseLambda GetLambda(QueryMethod method, Type propertyType, string peopertyName, object entityValue)
        {
            switch (method)
            {
                case QueryMethod.Equal:
                    return new EqualLambda(propertyType, peopertyName, entityValue);
                case QueryMethod.LessThan:
                    return new LessThanLambda(propertyType, peopertyName, entityValue);
                case QueryMethod.GreaterThan:
                    return new GreaterThanLambda(propertyType, peopertyName, entityValue);
                case QueryMethod.LessThanOrEqual:
                    return new LessThanOrEqualLambda(propertyType, peopertyName, entityValue);
                case QueryMethod.GreaterThanOrEqual:
                    return new GreaterThanOrEqualLambda(propertyType, peopertyName, entityValue);
                case QueryMethod.Like:
                    return new LikeLambda(propertyType, peopertyName, entityValue);
                case QueryMethod.Contain:
                    return new ContainLambda(propertyType, peopertyName, entityValue);
                case QueryMethod.NotEqual:
                    return new NotEqualLambda(propertyType, peopertyName, entityValue);
                case QueryMethod.NotContain:
                case QueryMethod.NotLike:
                default:
                    throw new NotSupportedException(string.Format("不支持QueryMethod {0} 的GetLambda", method.ToString()));
            }
        }

        public static Expression<Func<TEntity, bool>> ToExpression<TEntity>(ParameterExpression param, BaseLambda lambda)
        {
            var member = Expression.Property(param, lambda.PropertyName);
            if (lambda is ContainLambda)
            {
                var methodInfo = lambda.Value.GetType().GetMethod("Contains");
                var body = Expression.Call(Expression.Constant(lambda.Value), methodInfo, member);
                return Expression.Lambda<Func<TEntity, bool>>(body, param);
            }
            else if (lambda is LikeLambda)
            {
                var body = Expression.Call(member, typeof(string).GetMethod(nameof(string.Contains)), Expression.Constant(lambda.Value as string));
                return Expression.Lambda<Func<TEntity, bool>>(body, param);
            }
            else
            {
                if (lambda.PropertyType.IsNullable())
                {
                    var binary = Expression.MakeBinary(lambda.ExpressionType, member, Expression.Convert(Expression.Constant(lambda.Value), lambda.PropertyType));
                    return Expression.Lambda<Func<TEntity, bool>>(binary, param);
                }
                else
                {
                    var binary = Expression.MakeBinary(lambda.ExpressionType, member, Expression.Constant(lambda.Value));
                    return Expression.Lambda<Func<TEntity, bool>>(binary, param);
                }
            }
        }

        public static BaseTag GetIfResult(QueryIf ifTag, object value)
        {
            switch (ifTag)
            {
                case QueryIf.IsEmpty:
                    return IsEmpty.For(value);
                case QueryIf.IsNotEmpty:
                    return IsNotEmpty.For(value);
                case QueryIf.IsNotNull:
                    return IsNotNull.For(value);
                case QueryIf.IsNull:
                    return IsNull.For(value);
                default:
                    throw new NotSupportedException(string.Format("不支持QueryIf为 {0}", ifTag.ToString()));
            }
        }
    }
}
