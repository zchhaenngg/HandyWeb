using HandyWork.DAL.Repository.Abstracts;
using HandyWork.DAL.Repository.Interfaces;
using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using HandyWork.ViewModel.PCWeb.Query;
using System.Linq.Expressions;
using HandyWork.Common.EntityFramework.Elements;
using HandyWork.Common.EntityFramework.Lambdas;
using HandyWork.Common.Extensions;
using System.Data.Entity;

namespace HandyWork.DAL.Repository
{
    public class AuthPermissionRepository : BaseRepository<AuthPermission>, IAuthPermissionRepository
    {
        public AuthPermissionRepository(UnitOfWork unitOfWork, DbSet<AuthPermission> source)
           : base(unitOfWork, source)
        {
        }
        
        public AuthPermission Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            return Source.Find(id);
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
