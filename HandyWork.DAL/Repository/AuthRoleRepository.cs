using HandyWork.Common.Model;
using HandyWork.DAL.Repository.Abstracts;
using HandyWork.DAL.Repository.Interfaces;
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
    public class AuthRoleRepository : BaseRepository<AuthRole>, IAuthRoleRepository
    {
        public AuthRoleRepository(UnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.UserEntities, false)
        {
        }
        protected override void OnBeforeAdd(AuthRole entity, string operatorId)
        {
            entity.CreatedById = operatorId;
            entity.CreatedTime = DateTime.Now;
            entity.LastModifiedById = operatorId;
            entity.LastModifiedTime = DateTime.Now;
        }

        protected override void OnBeforeUpdate(AuthRole entity, string operatorId)
        {
            entity.LastModifiedById = operatorId;
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

        public List<AuthRole> GetAll()
        {
            return Source.ToList();
        }

        public override void Validate(AuthRole entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("role");
            }
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                UnitOfWork.Errors.Add(Errors.InvalidUserName);
            }
            else
            {
                var owner = FindByName(entity.Name);
                if (owner != null &&
                    !string.Equals(entity.Id, owner.Id))
                {
                    UnitOfWork.Errors.Add(Errors.DuplicateRole);
                }
            }
            base.Validate(entity);
        }

        protected override string[] OnBeforeRecordHistory(AuthRole entity, DataHistory history)
        {
            throw new NotImplementedException();
        }
    }
}
