﻿using HandyWork.Common.Model;
using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL.Repository
{
    public class AuthRoleRepository : BaseRepository<AuthRole>
    {
        public AuthRoleRepository(DbContext context, List<ErrorInfo> errorInfos)
            : base(context, errorInfos)
        {
        }
        protected override void OnBeforeAdd(AuthRole entity)
        {
            entity.CreatedById = LoginId;
            entity.CreatedTime = DateTime.Now;
            entity.LastModifiedById = LoginId;
            entity.LastModifiedTime = DateTime.Now;
        }

        protected override void OnBeforeUpdate(AuthRole entity)
        {
            entity.LastModifiedById = LoginId;
            entity.LastModifiedTime = DateTime.Now;
        }
        
        public void Remove(string id)
        {
            var entity = Find(id);
            entity.AuthPermission.Clear();
            entity.User.Clear();
            Remove(entity);
        }

        public AuthRole Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            return Source.Find(id);
        }

        public override AuthRole Find(AuthRole entity)
        {
            if (entity == null)
            {
                return null;
            }
            return Find(entity.Id);
        }

        public AuthRole FindByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            return Source.Where(o => o.Name == name).FirstOrDefault();
        }

        public override void Validate(AuthRole entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("role");
            }
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                ErrorInfos.Add(Errors.InvalidUserName);
            }
            else
            {
                var owner = FindByName(entity.Name);
                if (owner != null &&
                    !string.Equals(entity.Id, owner.Id))
                {
                    ErrorInfos.Add(Errors.DuplicateRole);
                }
            }
            base.Validate(entity);
        }

        public override string[] OnBeforeRecordHistory(AuthRole entity, DataHistory history)
        {
            throw new NotImplementedException();
        }
    }
}