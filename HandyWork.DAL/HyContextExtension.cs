using HandyWork.DAL.Queryable;
using HandyWork.ViewModel.PCWeb.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Common.Extensions;

namespace HandyWork.DAL
{
    public static class HyContextExtension
    {
        public static IQueryable<TEntity> FindByQuery<TEntity, TQuery>(this IQueryable<TEntity> queryable, TQuery query)
        {
            var where = Mapping.GetExpression<TEntity, TQuery>(query);
            return queryable.Where(where);
        }
        public static IQueryable<T> GetPage<T, TQuery>(this IQueryable<T> queryable, TQuery query) where TQuery : BaseQuery
        {
            var source = queryable.FindByQuery(query).OrderBy(query.SortColumn, query.IsAsc);
            return source.GetPage(query.PageIndex, query.PageSize);
        }
        public static IQueryable<T> GetPage<T, TQuery>(this IQueryable<T> queryable, TQuery query, out int iTotal) where TQuery : BaseQuery
        {
            var source = queryable.FindByQuery(query);
            iTotal = source.Count();
            return source.OrderBy(query.SortColumn, query.IsAsc).GetPage(query.PageIndex, query.PageSize);
        }
    }
}
