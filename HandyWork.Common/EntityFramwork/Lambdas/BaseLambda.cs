using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramwork.Lambdas
{
    public abstract class BaseLambda<TEntity, TProperty>
    {
        protected object Value { get; set; }
        protected Expression<Func<TEntity, TProperty>> PropertyExpression { get; set; }

        public BaseLambda(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue)
        {
            PropertyExpression = entityProperty;
            Value = entityValue;
        }

        public abstract Expression<Func<TEntity, bool>> Build();
    }
}
