﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramework.Lambdas
{
    public class LessThanOrEqualLambda<TEntity, TProperty> : BaseLambda<TEntity, TProperty>
    {
        public LessThanOrEqualLambda(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue) : base(entityProperty, entityValue)
        {
            ExpressionType = ExpressionType.LessThanOrEqual;
        }

        public static LessThanOrEqualLambda<TEntity, TProperty> For(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue)
        {
            return new LessThanOrEqualLambda<TEntity, TProperty>(entityProperty, entityValue);
        }
    }
}