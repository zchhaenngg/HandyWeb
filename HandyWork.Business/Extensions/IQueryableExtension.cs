using HandyWork.Business.HyQuery;
using HandyWork.ViewModel.Query.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Common.Extensions;

namespace HandyWork.Business.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryable<TEntity> FindByQuery<TEntity, TQuery>(this IQueryable<TEntity> queryable, TQuery query)
        {
            var where = MapInitializer.Container.GetExpression<TEntity, TQuery>(query);
            return queryable.Where(where);
        }
        public static IQueryable<T> GetPage<T, TQuery>(this IQueryable<T> queryable, TQuery query) where TQuery : Hy_IQuery
        {
            var source = queryable.FindByQuery(query).OrderBy(query.SortColumn, query.IsAsc);
            return source.GetPage(query.PageIndex, query.PageSize);
        }
        public static IQueryable<T> GetPage<T, TQuery>(this IQueryable<T> queryable, TQuery query, out int iTotal) where TQuery : Hy_IQuery
        {
            var source = queryable.FindByQuery(query);
            iTotal = source.Count();
            return source.OrderBy(query.SortColumn, query.IsAsc).GetPage(query.PageIndex, query.PageSize);
        }
    }
}
