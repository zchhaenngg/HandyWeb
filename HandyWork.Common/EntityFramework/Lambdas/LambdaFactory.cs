using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using HandyWork.Common.EntityFramework.Query;
using HandyWork.Common.EntityFramework.Elements;
using HandyWork.Common.Extensions;

namespace HandyWork.Common.EntityFramework.Lambdas
{
    public class LambdaFactory<TEntity>
    {//1个LambdaFactory实例对应一个使用同一个ParameterExpression的Expression
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

        public bool IsAnd { get; }

        /// <summary>
        /// 1个LambdaFactory实例对应一个使用同一个ParameterExpression的Expression
        /// </summary>
        /// <param name="isAnd">Lambdas之间以And还是Or相连</param>
        public LambdaFactory(bool isAnd = true)
        {
            IsAnd = isAnd;
        }

        public List<BaseLambda> Lambdas { get; set; } = new List<BaseLambda>();

        public LambdaFactory<TEntity> If<TValue>(BaseTag ifCondition, QueryMethod method, Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            if (ifCondition.IsPassed)
            {
                var lambda = LambdaUtility.GetLambda(method, entityProperty, value);
                Lambdas.Add(lambda);
            }
            return this;
        }

        public LambdaFactory<TEntity> IfLike(BaseTag ifCondition, Expression<Func<TEntity, string>> entityProperty, object value)
        {
            return If(ifCondition, QueryMethod.Like, entityProperty, value);
        }
        public LambdaFactory<TEntity> IfContain<TValue>(BaseTag ifCondition, Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return If(ifCondition, QueryMethod.Contain, entityProperty, value);
        }
        public LambdaFactory<TEntity> IfEqual<TValue>(BaseTag ifCondition, Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return If(ifCondition, QueryMethod.Equal, entityProperty, value);
        }
        public LambdaFactory<TEntity> IfLessThan<TValue>(BaseTag ifCondition, Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return If(ifCondition, QueryMethod.LessThan, entityProperty, value);
        }
        public LambdaFactory<TEntity> IfGreaterThan<TValue>(BaseTag ifCondition, Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return If(ifCondition, QueryMethod.GreaterThan, entityProperty, value);
        }
        public LambdaFactory<TEntity> IfLessThanOrEqual<TValue>(BaseTag ifCondition, Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return If(ifCondition, QueryMethod.LessThanOrEqual, entityProperty, value);
        }
        public LambdaFactory<TEntity> IfGreaterThanOrEqual<TValue>(BaseTag ifCondition, Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return If(ifCondition, QueryMethod.GreaterThanOrEqual, entityProperty, value);
        }
        public LambdaFactory<TEntity> IfNotEqual<TValue>(BaseTag ifCondition, Expression<Func<TEntity, TValue>> entityProperty, object value)
        {
            return If(ifCondition, QueryMethod.NotEqual, entityProperty, value);
        }

        public LambdaFactory<TEntity> AddLambda(QueryItem item)
        {
            var lambda = LambdaUtility.GetLambda<TEntity>(item);
            Lambdas.Add(lambda);
            return this;
        }
        public LambdaFactory<TEntity> AddLambdas(IEnumerable<QueryItem> items)
        {
            if (items != null)
            {
                foreach (var item in items.ToList())
                {
                    AddLambda(item);
                }
            }
            return this;
        }
        public LambdaFactory<TEntity> AddLambdas(QueryModel model) => model == null ? this: AddLambdas(model.Items);


        public Expression<Func<TEntity, bool>> ToExpression()
        {
            if (Lambdas == null || !Lambdas.Any())
            {
                return null;
            }
            Expression<Func<TEntity, bool>> expression = null;//千万别赋值。expression = expression.Or(null)
            Lambdas.ForEach(lambda => 
            {
                if (IsAnd)
                {
                    expression = expression.And(LambdaUtility.ToExpression<TEntity>(ParameterExpression, lambda));
                }
                else
                {
                    expression = expression.Or(LambdaUtility.ToExpression<TEntity>(ParameterExpression, lambda));
                }
            });
            return expression;
        }
    }
}
