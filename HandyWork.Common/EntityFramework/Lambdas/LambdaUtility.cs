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
        public static BaseLambda<TEntity> GetContainLambda<TValue>(Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return GetLambda(QueryMethod.Contain, entityProperty, value);
        }

        public static BaseLambda<TEntity> GetEqualLambda<TValue>(Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return GetLambda(QueryMethod.Equal, entityProperty, value);
        }

        public static BaseLambda<TEntity> GetGreaterThanLambda<TValue>(Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return GetLambda(QueryMethod.GreaterThan, entityProperty, value);
        }

        public static BaseLambda<TEntity> GetGreaterThanOrEqualLambda<TValue>(Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return GetLambda(QueryMethod.GreaterThanOrEqual, entityProperty, value);
        }

        public static BaseLambda<TEntity> GetLessThanLambda<TValue>(Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return GetLambda(QueryMethod.LessThan, entityProperty, value);
        }

        public static BaseLambda<TEntity> GetLessThanOrEqualLambda<TValue>(Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return GetLambda(QueryMethod.LessThanOrEqual, entityProperty, value);
        }

        public static BaseLambda<TEntity> GetLikeLambda<TValue>(Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return GetLambda(QueryMethod.Like, entityProperty, value);
        }

        public static BaseLambda<TEntity> GetNotEqualLambda<TValue>(Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return GetLambda(QueryMethod.NotEqual, entityProperty, value);
        }

        public static BaseLambda<TEntity> GetLambda<TValue>(QueryMethod method, Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            var propertyName = (entityProperty.Body as MemberExpression).Member.Name;
            return GetLambda(method, typeof(TValue), propertyName, value);
        }
        public static BaseLambda<TEntity> GetLambda(QueryItem item)
        {
            var propertyType = typeof(TEntity).GetProperty(item.Field).PropertyType;
            return GetLambda(item.Method, propertyType, item.Field, item.Value);
        }
        public static BaseLambda<TEntity> GetLambda(QueryMethod method, Type propertyType, string peopertyName, object entityValue)
        {
            switch (method)
            {
                case QueryMethod.Equal:
                    return new EqualLambda<TEntity>(propertyType, peopertyName, entityValue);
                case QueryMethod.LessThan:
                    return new LessThanLambda<TEntity>(propertyType, peopertyName, entityValue);
                case QueryMethod.GreaterThan:
                    return new GreaterThanLambda<TEntity>(propertyType, peopertyName, entityValue);
                case QueryMethod.LessThanOrEqual:
                    return new LessThanOrEqualLambda<TEntity>(propertyType, peopertyName, entityValue);
                case QueryMethod.GreaterThanOrEqual:
                    return new GreaterThanOrEqualLambda<TEntity>(propertyType, peopertyName, entityValue);
                case QueryMethod.Like:
                    return new LikeLambda<TEntity>(propertyType, peopertyName, entityValue);
                case QueryMethod.Contain:
                    return new ContainLambda<TEntity>(propertyType, peopertyName, entityValue);
                case QueryMethod.NotEqual:
                    return new NotEqualLambda<TEntity>(propertyType, peopertyName, entityValue);
                case QueryMethod.NotContain:
                case QueryMethod.NotLike:
                default:
                    throw new NotSupportedException(string.Format("不支持QueryMethod {0} 的GetLambda", method.ToString()));
            }
        }
       
    }
}
