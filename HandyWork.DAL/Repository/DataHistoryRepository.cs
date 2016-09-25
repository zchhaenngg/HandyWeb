﻿using HandyWork.Common.Extensions;
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
    internal sealed class DataHistoryRepository : IDataHistoryRepository
    {
        private UnitOfWork _unitOfWork;
        public DbSet<DataHistory> Source { get; set; }

        public DataHistoryRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Source = unitOfWork.EntityContext.Set<DataHistory>();
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
            if (EntityState.Modified == _unitOfWork.EntityContext.Entry(entity).State)
            {
                OnBeforeUpdate(entity, operatorId);
            }
            return entity;
        }

        public DataHistory Remove(DataHistory entity)
        {
            return Source.Remove(entity);
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

        public void Validate(DataHistory entity)
        {

        }

        public List<DataHistory> FindAllByQuery(BaseQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
