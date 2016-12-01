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
    public class AuthPermissionRepository : BaseRepository<hy_auth_permission>, IAuthPermissionRepository
    {
        public AuthPermissionRepository(UnitOfWork unitOfWork)
           : base(unitOfWork)
        {
        }
        
        public hy_auth_permission Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            return Source.Find(id);
        }
        
        public hy_auth_permission FindByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return null;
            }
            return Source.Where(o => o.Code == code).FirstOrDefault();
        }
        public hy_auth_permission FindByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            return Source.Where(o => o.Name == name).FirstOrDefault();
        }
    }
}
