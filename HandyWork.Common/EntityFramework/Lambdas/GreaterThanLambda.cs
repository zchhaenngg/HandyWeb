﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramework.Lambdas
{
    public class GreaterThanLambda<TEntity, TProperty> : BaseLambda<TEntity, TProperty>
    {
        public GreaterThanLambda(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue) : base(entityProperty, entityValue)
        {
            ExpressionType = ExpressionType.GreaterThan;
        }

        public static GreaterThanLambda<TEntity, TProperty> For(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue)
        {
            return new GreaterThanLambda<TEntity, TProperty>(entityProperty, entityValue);
        }
    }
}