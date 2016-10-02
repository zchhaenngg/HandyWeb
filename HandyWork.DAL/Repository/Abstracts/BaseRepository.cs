using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using HandyWork.Common.Extensions;
using HandyWork.ViewModel.PCWeb.Query;

namespace HandyWork.DAL.Repository.Abstracts
{
    public abstract class BaseRepository<T>
        where T : class
    {
        protected UnitOfWork UnitOfWork { get; }
        public DbSet<T> Source { get; }

        public BaseRepository(UnitOfWork unitOfWork, DbSet<T> source)
        {
            UnitOfWork = unitOfWork;
            Source = source;
        }

        public abstract Expression<Func<T, bool>> GetExpression(BaseQuery baseQuery);

        /// <summary>
        /// 返回该页所有数据和所有页总数据量
        /// </summary>
        public virtual Tuple<List<T>, int> GetPage(BaseQuery query)
        {
            Expression<Func<T, bool>> where = GetExpression(query);
            IQueryable<T> queryable = where == null ? Source : Source.Where(where);
            int count = queryable.Count();
            if (!string.IsNullOrWhiteSpace(query.SortColumn))
            {
                queryable = queryable.OrderBy(query.SortColumn, query.IsAsc);
            }
            List<T> list = queryable.GetPage(query.PageIndex, query.PageSize).ToList();
            return new Tuple<List<T>, int>(list, count);
        }
        
        public virtual IQueryable<T> FindAllByQuery(BaseQuery query)
        {
            var expression = GetExpression(query);
            var queryable = expression == null ? Source : Source.Where(expression);
            return queryable;
        }
        
    }
}
