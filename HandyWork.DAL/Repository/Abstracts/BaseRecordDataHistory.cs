using HandyWork.DAL.Cache;
using HandyWork.DAL.Repository.Interfaces;
using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL.Repository.Abstracts
{
    public class BaseRecordDataHistory<T>
        where T : class
    {
        private bool _isRecordDataChange;
        protected DbContext Context { get; }
        private IDataHistoryRepository _dataRepository;

        public BaseRecordDataHistory(IDataHistoryRepository repository, DbContext context, bool isRecordDataChange)
        {
            this._dataRepository = repository;
            this.Context = context;
            this._isRecordDataChange = isRecordDataChange;
        }

        protected virtual void RecordData(T entity, string operatorId)
        {
            if (!_isRecordDataChange)
            {
                return;
            }
            var history = new DataHistory()
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

            var entry = Context.Entry(entity);
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
            _dataRepository.Add(history, operatorId);
        }

        /// <summary>
        /// 返回值为不需要记录在历史变更中的字段。同时需要对history的foreign key id和统计字段Keep1,Keep2等赋值
        /// </summary>
        protected virtual string[] OnBeforeRecordHistory(T entity, DataHistory history) => null;
    }
}
