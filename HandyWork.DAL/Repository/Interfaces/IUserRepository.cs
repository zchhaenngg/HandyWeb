using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL.Repository.Interfaces
{
    public interface IUserRepository : IBaseRepository<hy_user>
    {
        hy_user Find(string id);
        hy_user FindByUserName(string userName);
        hy_user FindByEmail(string email);
        ICollection<hy_auth_permission> GetPermissionsByUserGrant(string userId);
        DbSqlQuery<hy_auth_permission> GetPermissionByRoleGrant(string userId);
        IEnumerable<hy_auth_permission> GetAllPermissions(string userId);
    }
}
