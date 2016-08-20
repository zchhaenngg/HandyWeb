using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL.Repository.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User Find(string id);
        User FindByUserName(string userName);
        List<AuthPermission> GetPermissionsByUserGrant(string userId);
        List<AuthPermission> GetPermissionByRoleGrant(string userId);
        List<AuthPermission> GetAllPermissions(string userId);
    }
}
