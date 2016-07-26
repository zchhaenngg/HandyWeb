using HandyWork.Common.Model;
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
    public class AuthPermissionRepository : BaseRepository<AuthPermission>
    {
        public AuthPermissionRepository(DbContext context, List<ErrorInfo> errorInfos)
           : base(context, errorInfos)
        {
        }
        protected override void OnBeforeAdd(AuthPermission entity)
        {
            entity.CreatedById = LoginId;
            entity.CreatedTime = DateTime.Now;
            entity.LastModifiedById = LoginId;
            entity.LastModifiedTime = DateTime.Now;
        }

        protected override void OnBeforeUpdate(AuthPermission entity)
        {
            entity.LastModifiedById = LoginId;
            entity.LastModifiedTime = DateTime.Now;
        }
        
        public override AuthPermission Remove(AuthPermission entity)
        {
            entity.AuthRole.Clear();
            entity.User.Clear();
            return base.Remove(entity);
        }

        public AuthPermission Remove(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }
            AuthPermission entity = Find(id);
            return Remove(entity);
        }

        public AuthPermission Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            return Source.Find(id);
        }

        public override AuthPermission Find(AuthPermission entity)
        {
            if (entity == null)
            {
                return null;
            }
            return Find(entity.Id);
        }

        public AuthPermission FindByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return null;
            }
            return Source.Where(o => o.Code == code).FirstOrDefault();
        }
        public AuthPermission FindByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            return Source.Where(o => o.Name == name).FirstOrDefault();
        }
        public override void Validate(AuthPermission entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                ErrorInfos.Add(Errors.InvalidPermission);
            }
            else
            {
                var owner = FindByCode(entity.Code);
                if (owner != null &&
                    !string.Equals(entity.Id, owner.Id))
                {
                    ErrorInfos.Add(Errors.DuplicatePermission);
                }
                else
                {
                    var owner2 = FindByName(entity.Name);
                    if (owner2 != null &&
                        !string.Equals(entity.Id, owner2.Id))
                    {
                        ErrorInfos.Add(Errors.DuplicatePermission);
                    }
                }
            }
            base.Validate(entity);
        }
        
        public override string[] OnBeforeRecordHistory(AuthPermission entity, DataHistory history)
        {
            throw new NotImplementedException();
        }
    }
}
