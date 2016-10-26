using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using HandyWork.Common.EntityFramework.Query;

namespace HandyWork.Common.EntityFramework.Lambdas
{
    public class LambdaFactory<TEntity>
    {
        private ParameterExpression _parameterExpression;
        public ParameterExpression ParameterExpression
        {
            get
            {
                if (_parameterExpression == null)
                {
                    _parameterExpression = Expression.Parameter(typeof(TEntity), "o");
                }
                return _parameterExpression;
            }
        }

        /********List<QueryItem>,产生一个最终的Expression**********/
    }
}
