using HandyWork.DAL.Repository.Abstracts;
using HandyWork.DAL.Repository.Interfaces;
using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HandyWork.Common.EntityFramework.Elements;
using HandyWork.Common.EntityFramework.Lambdas;
using HandyWork.Common.Extensions;
using HandyWork.ViewModel.PCWeb.Query;

namespace HandyWork.DAL.Repository
{
    public class AuthRoleRepository : BaseRepository<AuthRole>, IAuthRoleRepository
    {
        public AuthRoleRepository(UnitOfWork unitOfWork)
            : base(unitOfWork, false)
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
            UnitOfWork.RemoveAndClear(entity.AuthPermissions);
            UnitOfWork.RemoveAndClear(entity.Users);
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
        
        public override Expression<Func<AuthRole, bool>> GetExpression(BaseQuery baseQuery)
        {
            Expression<Func<AuthRole, bool>> expression = null;
            AuthRoleQuery query = baseQuery as AuthRoleQuery;
            if (query != null)
            {
                expression = expression
                    .And(IsNotEmpty.For(query.NameLike), LikeLambda<AuthRole>.For(o => o.Name, query.NameLike));

            }
            return expression;
        }
    }
}
