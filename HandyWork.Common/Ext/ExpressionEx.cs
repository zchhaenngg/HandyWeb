using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Ext
{
    public class ParameterReplacer : ExpressionVisitor
    {
        public ParameterReplacer(ParameterExpression paramExpr)
        {
            ParameterExpression = paramExpr;
        }

        public ParameterExpression ParameterExpression { get; private set; }

        public Expression Replace(Expression expr)
        {
            return Visit(expr);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            return ParameterExpression;
        }
    }

    public static class ExpressionEx
    {
        /// <summary>
        /// expression1为null,直接返回expression2
        /// </summary>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expression1,
                                              Expression<Func<T, bool>> expression2)
        {
            if (expression1 == null)
            {
                return expression2;
            }
            ParameterExpression candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            Expression left = parameterReplacer.Replace(expression1.Body);
            Expression right = parameterReplacer.Replace(expression2.Body);
            BinaryExpression body = Expression.Or(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

        /// <summary>
        /// expression1为null,直接返回expression2
        /// </summary>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression1,
                                                       Expression<Func<T, bool>> expression2)
        {
            if (expression1 == null)
            {
                return expression2;
            }
            ParameterExpression candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            Expression left = parameterReplacer.Replace(expression1.Body);
            Expression right = parameterReplacer.Replace(expression2.Body);
            BinaryExpression body = Expression.And(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }
    }
}
