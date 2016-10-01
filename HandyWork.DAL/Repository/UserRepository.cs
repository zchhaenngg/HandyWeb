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

namespace HandyWork.DAL.Repository
{
    public class UserRepository : BaseRepository<AuthUser>, IUserRepository
    {
        public UserRepository(UnitOfWork unitOfWork, DbSet<AuthUser> source)
            :base(unitOfWork, source)
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
        
        public List<AuthPermission> GetPermissionsByUserGrant(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }
            AuthUser user = Find(userId);
            return user.AuthPermissions.ToList();
        }

        public List<AuthPermission> GetPermissionByRoleGrant(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }

            var roles = UnitOfWork.RoleRepository.Source.Where(o => o.Users.Any(u => u.Id == userId)).Include(r => r.AuthPermissions).ToList();
            var list = new List<AuthPermission>();
            roles.ForEach(r => 
            {
                list.AddRange(r.AuthPermissions);
            });
            return list;
        }

        public List<AuthPermission> GetAllPermissions(string userId)
        {
            List<AuthPermission> userPermissions = GetPermissionsByUserGrant(userId);
            List<AuthPermission> rolePermissions = GetPermissionByRoleGrant(userId);
            userPermissions.AddRange(rolePermissions);
            return userPermissions;
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
