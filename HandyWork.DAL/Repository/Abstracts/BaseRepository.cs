using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using HandyWork.Common.Extensions;
using HandyWork.ViewModel.PCWeb.Query;

namespace HandyWork.DAL.Repository.Abstracts
{
    public abstract class BaseRepository<T> : BaseRecordDataHistory<T>
        where T : class
    {
        private DbSet<T> _source; 
        public DbSet<T> Source => _source ?? (_source = UnitOfWork.EntityContext.Set<T>());

        public BaseRepository(UnitOfWork unitOfWork, bool isRecordDataChange)
            :base(unitOfWork, isRecordDataChange)
        {
        }
        
        protected abstract void OnBeforeAdd(T entity, string operatorId);
        protected abstract void OnBeforeUpdate(T entity, string operatorId);
        
        public virtual T Add(T entity, string operatorId)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(T).Name);
            }
            Validate(entity);
            OnBeforeAdd(entity, operatorId);
            T t = Source.Add(entity);
            RecordData(entity, operatorId);
            return t;
        }
        public virtual T Update(T entity, string operatorId)
        {
            Validate(entity);
            if (EntityState.Modified == UnitOfWork.EntityContext.Entry(entity).State)
            {
                OnBeforeUpdate(entity, operatorId);
                RecordData(entity, operatorId);
            }
            return entity;
        }
        
        public virtual T Remove(T entity)
        {
            return Source.Remove(entity);
        }

        public abstract T Find(T entity);

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

        public virtual void Validate(T entity)
        {

        }

        public virtual List<T> FindAllByQuery(BaseQuery query)
        {
            var expression = GetExpression(query);
            var queryable = expression == null ? Source : Source.Where(expression);
            return queryable.ToList();
        }
        
    }
}
