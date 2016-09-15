﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramwork.Lambdas
{
    public class NotEqualLambda<TEntity, TProperty> : BaseLambda<TEntity, TProperty>
    {
        public NotEqualLambda(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue) : base(entityProperty, entityValue)
        {
            ExpressionType = ExpressionType.NotEqual; 
        }
    }
}
