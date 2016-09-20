﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramework.Lambdas
{
    /// <summary>
    /// 数字、时间
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public class GreaterThanOrEqualLambda<TEntity, TProperty> : BaseLambda<TEntity, TProperty>
    {
        public GreaterThanOrEqualLambda(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue) : base(entityProperty, entityValue)
        {
            ExpressionType = ExpressionType.GreaterThanOrEqual;
        }
        public static GreaterThanOrEqualLambda<TEntity, TProperty> For(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue)
        {
            return new GreaterThanOrEqualLambda<TEntity, TProperty>(entityProperty, entityValue);
        }
    }
}
