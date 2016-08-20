﻿using HandyWork.Common.Model;
using HandyWork.DAL.Repository.Interfaces;
using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL.Repository
{
    internal class DataHistoryRepository : BaseRepository<DataHistory>, IDataHistoryRepository
    {
        public DataHistoryRepository(UnitOfWork unitOfWork)
            : base(unitOfWork.HistoryEntities)
        {
            IsRecordHistory = false;
        }
        protected override void OnBeforeAdd(DataHistory entity)
        {
            entity.CreatedById = LoginId;
            entity.CreatedTime = DateTime.Now;
            entity.LastModifiedById = LoginId;
            entity.LastModifiedTime = DateTime.Now;
        }

        protected override void OnBeforeUpdate(DataHistory entity)
        {
            entity.LastModifiedById = LoginId;
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

        public override DataHistory Find(DataHistory entity)
        {
            if (entity == null)
            {
                return null;
            }
            return Find(entity.Id);
        }

        public override void Validate(DataHistory entity)
        {

        }

        public override string[] OnBeforeRecordHistory(DataHistory entity, DataHistory history)
        {
            throw new NotImplementedException();
        }
    }
}
