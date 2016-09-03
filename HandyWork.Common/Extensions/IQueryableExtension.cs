using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> GetPage<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            if (pageIndex < 0)
            {
                pageIndex = 0;
            }
            if (pageSize < 0)
            {
                pageSize = 0;
            }
            return query.Skip(pageIndex * pageSize).Take(pageSize);
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string sortColumn, bool isAsc = false)
        {
            string methodName = string.Format("OrderBy{0}", isAsc ? "" : "descending");
            //•Then, we construct the lambda-expression as p => p.Name 
            ParameterExpression parameter = Expression.Parameter(query.ElementType, "p");
            //•So we get a member of the class, which will be sorted. 
            //This code takes into account that a class can be a nested class. 
            //In this case, it's expected that they will be separated by a dot:
            MemberExpression memberAccess = null;
            foreach (string property in sortColumn.Split('.'))
            {
                memberAccess = MemberExpression.Property(memberAccess ?? (parameter as Expression), property);
            }
            //•The Lambda-expression is completed by the creation of the object which represents calling a method:
            LambdaExpression orderByLambda = Expression.Lambda(memberAccess, parameter);
            MethodCallExpression result = Expression.Call(
                                  typeof(Queryable),
                                  methodName,
                                  new[] { query.ElementType, memberAccess.Type },
                                  query.Expression,
                                  Expression.Quote(orderByLambda));
            //•Return IQuerable<T>:
            return query.Provider.CreateQuery<T>(result);
        }
    }
}
