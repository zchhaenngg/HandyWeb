using HandyWork.Common.EntityFramework.Lambdas;
using HandyWork.Common.EntityFramework.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Utility
{
    public static class QueryItemUtility
    {
        public static BaseLambda<TEntity, TProperty> GetLambda<TEntity, TProperty>(QueryItem model, ParameterExpression paramExpression = null)
        {
            ExpressionUtility.GetLambdaExpressionOfProperty<TEntity>(model.Field, paramExpression);
            throw new Exception();
        }
    }
}
