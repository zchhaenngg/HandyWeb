using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Extensions
{
    /// <summary>
    /// 参考http://www.cnblogs.com/FlyEdward/archive/2010/12/06/Linq_ExpressionTree7.html
    /// </summary>
    public class ExpressionVisitorReplacer : ExpressionVisitor
    {
        public ExpressionVisitorReplacer(ParameterExpression parameterExpression)
        {
            ParameterExpression = parameterExpression;
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
    public static class ExpressionExtension
    {
        /// <summary>
        /// left为null,直接返回right
        /// </summary>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left,
                                              Expression<Func<T, bool>> right)
        {
            if (left == null)
            {
                return right;
            }
            var parameterExpression = Expression.Parameter(typeof(T), "o");
            var replacer = new ExpressionVisitorReplacer(parameterExpression);
            
            var newBody = Expression.OrElse(replacer.Replace(left.Body), replacer.Replace(right.Body));
            return Expression.Lambda<Func<T, bool>>(newBody, parameterExpression);
        }

        /// <summary>
        /// left为null,直接返回right
        /// </summary>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left,
                                                       Expression<Func<T, bool>> right)
        {
            if (left == null)
            {
                return right;
            }
            //此参数不会与left和right表达式中自定义的参数弄混淆，所以名字任意。因为所有表达式中的参数已被正确解释
            var parameterExpression = Expression.Parameter(typeof(T), "o");
            var replacer = new ExpressionVisitorReplacer(parameterExpression);
            
            var binary = Expression.AndAlso(replacer.Replace(left.Body), replacer.Replace(right.Body));
            return Expression.Lambda<Func<T, bool>>(binary, parameterExpression);
        }
        
    }
}
