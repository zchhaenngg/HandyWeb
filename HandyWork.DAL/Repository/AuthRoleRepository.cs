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
using System.Data.Entity;

namespace HandyWork.DAL.Repository
{
    public class AuthRoleRepository : BaseRepository<AuthRole>, IAuthRoleRepository
    {
        public AuthRoleRepository(UnitOfWork unitOfWork, DbSet<AuthRole> source)
            : base(unitOfWork, source)
        {
        }

        public AuthRole Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            return Source.Find(id);
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
