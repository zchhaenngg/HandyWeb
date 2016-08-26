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
using HandyWork.Common.Ext;
using HandyWork.Model;
using HandyWork.DAL.Cache;
using HandyWork.DAL.Repository.Interfaces;

namespace HandyWork.DAL.Repository.Abstracts
{
    public abstract class BaseRepository<T>
        where T : class
    {
        protected UnitOfWork UnitOfWork { get; set; }
        private DbContext _context;
        public DbSet<T> Source { get; }//在其他repository中做联合查询时需要,所以限定修饰符为public
        private bool _isRecordDataChange;

        public BaseRepository(UnitOfWork unitOfWork, DbContext context, bool isRecordDataChange)
        {
            this.Source = context.Set<T>();
            this.UnitOfWork = unitOfWork;
            this._context = context;
            this._isRecordDataChange = isRecordDataChange;
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
            if (EntityState.Modified == _context.Entry(entity).State)
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
        
        protected virtual void RecordData(T entity, string operatorId)
        {
            if (!_isRecordDataChange)
            {
                return;
            }
            DataHistory history = new DataHistory()
            {
                Id = Guid.NewGuid().ToString(),
                CreatedById = operatorId,
                LastModifiedById = operatorId,
                CreatedTime = DateTime.Now,
                LastModifiedTime = DateTime.Now,
                Category = typeof(T).Name
                //Keep1 = 统计数据
            };
            string[] ignorePropertyNames = OnBeforeRecordHistory(entity, history);
            
            DbEntityEntry<T> entry = _context.Entry(entity); 
            string description = SysColumnsCache.CompareObject(typeof(T).Name, entity, (propName) =>
            {
                if (ignorePropertyNames != null)
                {
                    if (ignorePropertyNames.Contains(propName))
                    {
                        return null;
                    }
                }
                if (entry.State == EntityState.Added)
                {
                    return new Tuple<string, string>("", (entry.CurrentValues[propName] ?? "").ToString());
                }
                else
                {
                    return new Tuple<string, string>((entry.OriginalValues[propName] ?? "").ToString(), (entry.CurrentValues[propName] ?? "").ToString());
                }
            });
            history.Description = description;
            UnitOfWork.DataHistoryRepository.Add(history, operatorId);
        }

        /// <summary>
        /// 返回值为不需要记录在历史变更中的字段。同时需要对history的foreign key id和统计字段Keep1,Keep2等赋值
        /// </summary>
        protected virtual string[] OnBeforeRecordHistory(T entity, DataHistory history)
        {
            return null;
        }
    }
}
