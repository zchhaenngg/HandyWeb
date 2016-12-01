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
    public class AuthRoleRepository : BaseRepository<hy_auth_role>, IAuthRoleRepository
    {
        public AuthRoleRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public hy_auth_role Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            return Source.Find(id);
        }
        
        public hy_auth_role FindByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            return Source.Where(o => o.name == name).FirstOrDefault();
        }
    }
}
