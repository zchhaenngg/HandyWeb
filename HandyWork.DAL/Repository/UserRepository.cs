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
    public partial class UserRepository : BaseRepository<AuthUser>, IUserRepository
    {
        public UserRepository(UnitOfWork unitOfWork)
            :base(unitOfWork)
        {
        }

        public AuthUser Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            return Source.Find(id);
        }

        public AuthUser FindByUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return null;
            }
            return Source.Where(o => o.UserName == userName).FirstOrDefault();
        }

        public ICollection<AuthPermission> GetPermissionsByUserGrant(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }
            return Source.Find(userId).Permissions;
        }

        public DbSqlQuery<AuthPermission> GetPermissionByRoleGrant(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }

            return UnitOfWork.PermissionRepository.Source.SqlQuery(SQL.Permission4RoleUser, userId);
        }

        public IEnumerable<AuthPermission> GetAllPermissions(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }
            return GetPermissionByRoleGrant(userId).Union(GetPermissionsByUserGrant(userId));
        }
    }
}
