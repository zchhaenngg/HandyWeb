using HandyWork.Common.Extensions;
using HandyWork.DAL.Repository.Interfaces;
using HandyWork.Model;
using HandyWork.ViewModel.PCWeb.Query;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HandyWork.DAL.Repository
{
    public sealed class DataHistoryRepository : IDataHistoryRepository
    {
        private UnitOfWork _unitOfWork;
        public DbSet<DataHistory> Source { get; set; }

        public DataHistoryRepository(UnitOfWork unitOfWork, DbSet<DataHistory> source)
        {
            _unitOfWork = unitOfWork;
            Source = source;
        }
        
        public DataHistory Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            return Source.Find(id);
        }
        
        public Expression<Func<DataHistory, bool>> GetExpression(BaseQuery baseQuery)
        {
            return null;
        }

        public Tuple<List<DataHistory>, int> GetPage(BaseQuery query)
        {
            Expression<Func<DataHistory, bool>> where = GetExpression(query);
            IQueryable<DataHistory> queryable = where == null ? Source : Source.Where(where);
            int count = queryable.Count();
            if (!string.IsNullOrWhiteSpace(query.SortColumn))
            {
                queryable = queryable.OrderBy(query.SortColumn, query.IsAsc);
            }
            List<DataHistory> list = queryable.GetPage(query.PageIndex, query.PageSize).ToList();
            return new Tuple<List<DataHistory>, int>(list, count);
        }
        
        public List<DataHistory> FindAllByQuery(BaseQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
