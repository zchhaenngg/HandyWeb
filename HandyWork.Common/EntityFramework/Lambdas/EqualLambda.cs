﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramework.Lambdas
{
    /// <summary>
    /// 所有类型
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public class EqualLambda<TEntity, TProperty> : BaseLambda<TEntity, TProperty>
    {
        public EqualLambda(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue) : base(entityProperty, entityValue)
        {
            ExpressionType = ExpressionType.Equal;
        }
        
        public static EqualLambda<TEntity, TProperty> For(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue)
        {
            return new EqualLambda<TEntity, TProperty>(entityProperty, entityValue);
        }
    }
}
