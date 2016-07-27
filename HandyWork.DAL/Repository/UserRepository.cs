using HandyWork.Common.Model;
using HandyWork.DAL.Cache;
using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(DbContext context, List<ErrorInfo> errorInfos, DataHistoryRepository historyRepository)
            : base(context, errorInfos, historyRepository)
        {
        }

        protected override void OnBeforeAdd(User entity)
        {
            entity.CreatedById = LoginId;
            entity.CreatedTime = DateTime.Now;
            entity.LastModifiedById = LoginId;
            entity.LastModifiedTime = DateTime.Now;
        }

        protected override void OnBeforeUpdate(User entity)
        {
            entity.LastModifiedById = LoginId;
            entity.LastModifiedTime = DateTime.Now;
        }
        
        public override User Remove(User entity)
        {
            entity.AuthPermission.Clear();
            entity.AuthRole.Clear();
            return base.Remove(entity);
        }

        public User Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            return Source.Find(id);
        }

        public override User Find(User entity)
        {
            if (entity == null || string.IsNullOrWhiteSpace(entity.Id))
            {
                return null;
            }
            return Find(entity.Id);
        }

        public User FindByUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return null;
            }
            return Source.Where(o => o.UserName == userName).FirstOrDefault();
        }

        public override void Validate(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(User));
            }
            if (string.IsNullOrWhiteSpace(entity.UserName))
            {
                ErrorInfos.Add(Errors.InvalidUserName);
            }
            else
            {
                var owner = FindByUserName(entity.UserName);
                if (owner != null &&
                    !string.Equals(entity.Id, owner.Id))
                {
                    ErrorInfos.Add(Errors.DuplicateUserName);
                }
            }
            base.Validate(entity);
        }
        
        public List<AuthPermission> GetPermissionsByUserGrant(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }
            User user = Find(userId);
            return user.AuthPermission.ToList();
        }

        public List<AuthPermission> GetPermissionByRoleGrant(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }
            SqlRepository sqlRepository = new SqlRepository(_Context);
            return sqlRepository.GetList<AuthPermission>(SQL.Permission4RoleUser, new SqlParameter("@UserId", userId));
        }

        public List<AuthPermission> GetAllPermissions(string userId)
        {
            List<AuthPermission> userPermissions = GetPermissionsByUserGrant(userId);
            List<AuthPermission> rolePermissions = GetPermissionByRoleGrant(userId);
            userPermissions.AddRange(rolePermissions);
            return userPermissions;
        }

        public override string[] OnBeforeRecordHistory(User entity, DataHistory history)
        {
            history.ForeignId = entity.Id;
         //   history.Keep1  = 统计数据
            return new string[] { nameof(User.Id), nameof(User.Password) };
        }
    }
}
