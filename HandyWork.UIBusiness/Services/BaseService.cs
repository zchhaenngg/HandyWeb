using HandyWork.Common;
using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UIBusiness.Services
{
    /// <summary>
    /// 包装了ServiceStore，以便共用ServiceStore的上下文
    /// </summary>
    public abstract class BaseService : CurrentHttpContext
    {
        public ServiceStore Store { get; private set; }

        public BaseService(ServiceStore store)
        {
            Store = store;
        }

        public bool HasPermission(string permissionCode, string userId = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                userId = LoginId;
            }

            AuthPermission permission = Store.AuthPermissionRepository.FindByCode(permissionCode);
            if (permission == null)
            {
                return false;
            }
            User usr = permission.User.FirstOrDefault(o => o.Id == userId);
            if (usr != null)
            {
                return true;
            }
            //permission.AuthRole  和
            foreach (AuthRole item in permission.AuthRole.ToList())
            {
                User ur = item.User.FirstOrDefault(o => o.Id == userId);
                if (ur != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
