using HandyWork.Common;
using HandyWork.Common.Model;
using HandyWork.Model.Query;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Common.Extensions;
using HandyWork.Model;
using HandyWork.DAL.Cache;
using HandyWork.DAL.Repository.Interfaces;

namespace HandyWork.DAL.Repository.Abstracts
{
    public abstract class BaseRepository<T> : BaseRecordDataHistory<T>
        where T : class
    {
        protected UnitOfWork UnitOfWork { get; set; }
        public DbSet<T> Source { get; }//在其他repository中做联合查询时需要,所以限定修饰符为public
        
        public BaseRepository(UnitOfWork unitOfWork, DbContext context, bool isRecordDataChange)
            :base(unitOfWork.DataHistoryRepository, context, isRecordDataChange)
        {
            this.Source = context.Set<T>();
            this.UnitOfWork = unitOfWork;
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
            if (EntityState.Modified == Context.Entry(entity).State)
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

        /// <summary>
        /// 返回该页所有数据和所有页总数据量
        /// </summary>
        public virtual Tuple<List<T>, int> GetPage(BaseQuery query, Expression<Func<T, bool>> where = null)
        {
            IQueryable<T> queryable = Source;
            if (where == null)
            {

            }
            else
            {
                queryable = Source.Where(where);
            }
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
    }
}
