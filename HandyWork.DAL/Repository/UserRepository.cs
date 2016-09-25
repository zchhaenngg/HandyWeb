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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(UnitOfWork unitOfWork)
            :base(unitOfWork, unitOfWork.UserEntities, true)
        {
        }

        protected override void OnBeforeAdd(User entity, string operatorId)
        {
            entity.CreatedById = operatorId;
            entity.CreatedTime = DateTime.Now;
            entity.LastModifiedById = operatorId;
            entity.LastModifiedTime = DateTime.Now;
        }

        protected override void OnBeforeUpdate(User entity, string operatorId)
        {
            entity.LastModifiedById = operatorId;
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
                UnitOfWork.Errors.Add(Errors.InvalidUserName);
            }
            else
            {
                var owner = FindByUserName(entity.UserName);
                if (owner != null &&
                    !string.Equals(entity.Id, owner.Id))
                {
                    UnitOfWork.Errors.Add(Errors.DuplicateUserName);
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

            var roles = UnitOfWork.AuthRoleRepository.Source.Where(o => o.User.Any(u => u.Id == userId)).Include(r => r.AuthPermission).ToList();
            var list = new List<AuthPermission>();
            roles.ForEach(r => 
            {
                list.AddRange(r.AuthPermission);
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

        protected override string[] OnBeforeRecordHistory(User entity, DataHistory history)
        {
            history.ForeignId = entity.Id;
         //   history.Keep1  = 统计数据
            return new string[] { nameof(User.Id), nameof(User.Password) };
        }

        public override Expression<Func<User, bool>> GetExpression(BaseQuery baseQuery)
        {
            Expression<Func<User, bool>> expression = null;
            UserQuery query = baseQuery as UserQuery;
            if (query != null)
            {
                expression = expression
                    .And(IsNotEmpty.For(query.UserNameLike), LikeLambda<User>.For(o => o.UserName, query.UserNameLike))
                    .And(IsNotEmpty.For(query.UserNameEqual), EqualLambda<User, string>.For(o => o.UserName, query.UserNameEqual))
                    .And(IsNotEmpty.For(query.RealNameLike), LikeLambda<User>.For(o => o.RealName, query.RealNameLike))
                    .And(IsNotNull.For(query.IsValid), EqualLambda<User, bool>.For(o => o.IsValid, query.IsValid))
                    .And(IsNotNull.For(query.IsLocked), EqualLambda<User, bool>.For(o => o.IsLocked, query.IsLocked));
                    
            }
            return expression;
        }
        
    }
}
