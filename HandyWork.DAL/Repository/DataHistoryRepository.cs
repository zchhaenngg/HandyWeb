using HandyWork.Common.Model;
using HandyWork.DAL.Repository.Abstracts;
using HandyWork.DAL.Repository.Interfaces;
using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Model.Query;
using System.Linq.Expressions;
using HandyWork.Common.Ext;

namespace HandyWork.DAL.Repository
{
    internal sealed class DataHistoryRepository : IDataHistoryRepository
    {
        private UnitOfWork _unitOfWork;

        public DbSet<DataHistory> Source { get; }

        public DataHistoryRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Source = unitOfWork.HistoryEntities.Set<DataHistory>();
        }
        private void OnBeforeAdd(DataHistory entity, string operatorId)
        {
            entity.CreatedById = operatorId;
            entity.CreatedTime = DateTime.Now;
            entity.LastModifiedById = operatorId;
            entity.LastModifiedTime = DateTime.Now;
        }

        private void OnBeforeUpdate(DataHistory entity, string operatorId)
        {
            entity.LastModifiedById = operatorId;
            entity.LastModifiedTime = DateTime.Now;
        }
        
        public DataHistory Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            return Source.Find(id);
        }

        public DataHistory Find(DataHistory entity)
        {
            if (entity == null)
            {
                return null;
            }
            return Find(entity.Id);
        }

        public DataHistory Add(DataHistory entity, string operatorId)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(DataHistory));
            }
            Validate(entity);
            OnBeforeAdd(entity, operatorId);
            var t = Source.Add(entity);
            return t;
        }

        public DataHistory Update(DataHistory entity, string operatorId)
        {
            Validate(entity);
            if (EntityState.Modified == _unitOfWork.HistoryEntities.Entry(entity).State)
            {
                OnBeforeUpdate(entity, operatorId);
            }
            return entity;
        }

        public DataHistory Remove(DataHistory entity)
        {
            return Source.Remove(entity);
        }

        public Tuple<List<DataHistory>, int> GetPage(BaseQuery query, Expression<Func<DataHistory, bool>> where = null)
        {
            IQueryable<DataHistory> queryable = Source;
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
            List<DataHistory> list = queryable.GetPage(query.PageIndex, query.PageSize).ToList();
            return new Tuple<List<DataHistory>, int>(list, count);
        }

        public void Validate(DataHistory entity)
        {

        }
    }
}
