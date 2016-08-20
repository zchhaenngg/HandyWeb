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

namespace HandyWork.DAL.Repository
{
    public abstract class BaseRepository<T> : CurrentHttpContext
        where T : class
    {
        protected DbContext _Context;
        
        public bool IsRecordHistory { get; set; } = true;
        internal IDataHistoryRepository HistoryRepository { get; set; }//待注入
        public List<ErrorInfo> ErrorInfos { get; set; }//待注入

        public BaseRepository(DbContext context)
        {
            _Context = context;
            Source = context.Set<T>();
        }

        public DbSet<T> Source
        {
            get;
            private set;
        }
        
        public virtual T Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(T).Name);
            }
            Validate(entity);
            OnBeforeAdd(entity);
            T t = Source.Add(entity);
            RecordHistory(entity);
            return t;
        }

        protected abstract void OnBeforeAdd(T entity);

        public virtual T Update(T entity)
        {
            Validate(entity);
            if (EntityState.Modified == _Context.Entry(entity).State)
            {
                OnBeforeUpdate(entity);
                RecordHistory(entity);
            }
            return entity;
        }

        protected abstract void OnBeforeUpdate(T entity);
        
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
        
        protected virtual void RecordHistory(T entity)
        {
            if (IsRecordHistory)
            {
                if (HistoryRepository == null)
                {
                    throw new Exception("HistoryRepository对象为空，无法记录历史！请联系管理员！");
                }
            }
            else
            {
                return;
            }
            DataHistory history = new DataHistory()
            {
                Id = Guid.NewGuid().ToString(),
                CreatedById = LoginId,
                LastModifiedById = LoginId,
                CreatedTime = DateTime.Now,
                LastModifiedTime = DateTime.Now,
                Category = typeof(T).Name
                //Keep1 = 统计数据
            };
            string[] ignorePropertyNames = OnBeforeRecordHistory(entity, history);
            
            DbEntityEntry<T> entry = _Context.Entry(entity); 
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
            HistoryRepository.Add(history);
        }

        /// <summary>
        /// 返回值为不需要记录在历史变更中的字段。同时需要对history的foreign key id和统计字段Keep1,Keep2等赋值
        /// </summary>
        public abstract string[] OnBeforeRecordHistory(T entity, DataHistory history);
    }
}
