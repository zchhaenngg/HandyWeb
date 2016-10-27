using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using HandyWork.Common.EntityFramework.Query;

namespace HandyWork.Common.EntityFramework.Lambdas
{
    public static class LambdaUtility<TEntity>
    {
        public static BaseLambda GetContainLambda<TValue>(Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return GetLambda(QueryMethod.Contain, entityProperty, value);
        }

        public static BaseLambda GetEqualLambda<TValue>(Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return GetLambda(QueryMethod.Equal, entityProperty, value);
        }

        public static BaseLambda GetGreaterThanLambda<TValue>(Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return GetLambda(QueryMethod.GreaterThan, entityProperty, value);
        }

        public static BaseLambda GetGreaterThanOrEqualLambda<TValue>(Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return GetLambda(QueryMethod.GreaterThanOrEqual, entityProperty, value);
        }

        public static BaseLambda GetLessThanLambda<TValue>(Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return GetLambda(QueryMethod.LessThan, entityProperty, value);
        }

        public static BaseLambda GetLessThanOrEqualLambda<TValue>(Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return GetLambda(QueryMethod.LessThanOrEqual, entityProperty, value);
        }

        public static BaseLambda GetLikeLambda<TValue>(Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return GetLambda(QueryMethod.Like, entityProperty, value);
        }

        public static BaseLambda GetNotEqualLambda<TValue>(Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return GetLambda(QueryMethod.NotEqual, entityProperty, value);
        }

        public static BaseLambda GetLambda<TValue>(QueryMethod method, Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            var propertyName = (entityProperty.Body as MemberExpression).Member.Name;
            return GetLambda(method, typeof(TValue), propertyName, value);
        }
        public static BaseLambda GetLambda(QueryItem item)
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
       
    }
}
