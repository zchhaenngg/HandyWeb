using HandyWork.DAL.Repository.Abstracts;
using HandyWork.DAL.Repository.Interfaces;
using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using HandyWork.Model.Query;
using System.Linq.Expressions;
using HandyWork.Common.EntityFramwork.Elements;
using HandyWork.Common.EntityFramwork.Lambdas;
using HandyWork.Common.Extensions;

namespace HandyWork.DAL.Repository
{
    public class AuthPermissionRepository : BaseRepository<AuthPermission>, IAuthPermissionRepository
    {
        public AuthPermissionRepository(UnitOfWork unitOfWork)
           : base(unitOfWork, unitOfWork.UserEntities, false)
        {
        }
        protected override void OnBeforeAdd(AuthPermission entity, string operatorId)
        {
            entity.CreatedById = operatorId;
            entity.CreatedTime = DateTime.Now;
            entity.LastModifiedById = operatorId;
            entity.LastModifiedTime = DateTime.Now;
        }

        protected override void OnBeforeUpdate(AuthPermission entity, string operatorId)
        {
            entity.LastModifiedById = operatorId;
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

        public List<AuthPermission> GetAll()
        {
            return Source.ToList();
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
                throw new ArgumentNullException(nameof(entity));
            }
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                UnitOfWork.Errors.Add(Errors.InvalidPermission);
            }
            else
            {
                var owner = FindByCode(entity.Code);
                if (owner != null &&
                    !string.Equals(entity.Id, owner.Id))
                {
                    UnitOfWork.Errors.Add(Errors.DuplicatePermission);
                }
                else
                {
                    var owner2 = FindByName(entity.Name);
                    if (owner2 != null &&
                        !string.Equals(entity.Id, owner2.Id))
                    {
                        UnitOfWork.Errors.Add(Errors.DuplicatePermission);
                    }
                }
            }
        }

        public override Expression<Func<AuthPermission, bool>> GetExpression(BaseQuery baseQuery)
        {
            Expression<Func<AuthPermission, bool>> expression = null;
            var query = baseQuery as AuthPermissionQuery;
            if (query != null)
            {
                expression = expression
                    .And(IsNotEmpty.For(query.NameLike), LikeLambda<AuthPermission>.For(o => o.Name, query.NameLike))
                    .And(IsNotEmpty.For(query.CodeLike), EqualLambda<AuthPermission, string>.For(o => o.Code, query.CodeLike));

            }
            return expression;
        }
        
    }
}
