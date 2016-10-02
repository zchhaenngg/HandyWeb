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
    public partial class UserRepository
    {
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

    public partial class UserRepository : BaseRepository<AuthUser>, IUserRepository
    {
        public UserRepository(UnitOfWork unitOfWork, DbSet<AuthUser> source)
            :base(unitOfWork, source)
        {
        }
        
        public override Expression<Func<AuthUser, bool>> GetExpression(BaseQuery baseQuery)
        {
            Expression<Func<AuthUser, bool>> expression = null;
            UserQuery query = baseQuery as UserQuery;
            if (query != null)
            {
                expression = expression
                    .And(IsNotEmpty.For(query.UserNameLike), LikeLambda<AuthUser>.For(o => o.UserName, query.UserNameLike))
                    .And(IsNotEmpty.For(query.UserNameEqual), EqualLambda<AuthUser, string>.For(o => o.UserName, query.UserNameEqual))
                    .And(IsNotEmpty.For(query.RealNameLike), LikeLambda<AuthUser>.For(o => o.RealName, query.RealNameLike))
                    .And(IsNotNull.For(query.IsValid), EqualLambda<AuthUser, bool>.For(o => o.IsValid, query.IsValid))
                    .And(IsNotNull.For(query.IsLocked), EqualLambda<AuthUser, bool>.For(o => o.IsLocked, query.IsLocked));
                    
            }
            return expression;
        }
    }
}
