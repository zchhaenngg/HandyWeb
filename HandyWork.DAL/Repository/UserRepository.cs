using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using HandyWork.DAL.Repository.Interfaces;
using HandyWork.DAL.Repository.Abstracts;
using HandyWork.Common.Extensions;
using HandyWork.Common.EntityFramework.Elements;
using HandyWork.Common.EntityFramework.Lambdas;
using HandyWork.ViewModel.PCWeb.Query;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace HandyWork.DAL.Repository
{
    public partial class UserRepository : BaseRepository<hy_user>, IUserRepository
    {
        public UserRepository(UnitOfWork unitOfWork)
            :base(unitOfWork)
        {
        }

        public hy_user Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            return Source.Find(id);
        }

        public hy_user FindByUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return null;
            }
            return Source.Where(o => o.UserName == userName).FirstOrDefault();
        }
        
        public hy_user FindByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }
            return Source.Where(o => o.Email == email).FirstOrDefault();
        }

        public ICollection<hy_auth_permission> GetPermissionsByUserGrant(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }
            return Source.Find(userId).Permissions;
        }

        public DbSqlQuery<hy_auth_permission> GetPermissionByRoleGrant(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }

            return UnitOfWork.PermissionRepository.Source.SqlQuery(SQL.Permission4RoleUser, userId);
        }

        public IEnumerable<hy_auth_permission> GetAllPermissions(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }
            return GetPermissionByRoleGrant(userId).Union(GetPermissionsByUserGrant(userId));
        }
    }
}
