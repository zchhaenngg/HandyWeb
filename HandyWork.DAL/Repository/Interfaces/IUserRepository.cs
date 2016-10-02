using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL.Repository.Interfaces
{
    public interface IUserRepository : IBaseRepository<AuthUser>
    {
        AuthUser Find(string id);
        AuthUser FindByUserName(string userName);
        ICollection<AuthPermission> GetPermissionsByUserGrant(string userId);
        DbSqlQuery<AuthPermission> GetPermissionByRoleGrant(string userId);
        IEnumerable<AuthPermission> GetAllPermissions(string userId);
    }
}
