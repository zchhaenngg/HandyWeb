using HandyWork.Common;
using HandyWork.DAL;
using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HandyWork.UIBusiness.Extensions;
using HandyWork.Model.Entity;

namespace HandyWork.UIBusiness.Manager
{
    /// <summary>
    /// 包装了ServiceStore，以便共用ServiceStore的上下文
    /// </summary>
    public abstract class BaseManager
    {
        protected UnitOfManager UnitOfManager { get; }
        protected UnitOfWork UnitOfWork => UnitOfManager.UnitOfWork;
        protected string LoginId => HttpContext.Current.User.GetLoginId();
        protected HttpRequest Request => HttpContext.Current.Request;
        
        public BaseManager(UnitOfManager unitOfManager)
        {
            UnitOfManager = unitOfManager;
        }

        public bool HasPermission(string permissionCode, string userId = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                userId = LoginId;
            }
            hy_auth_permission permission = UnitOfWork.PermissionRepository.FindByCode(permissionCode);
            if (permission == null)
            {
                return false;
            }
            hy_user usr = permission.Users.FirstOrDefault(o => o.Id == userId);
            if (usr != null)
            {
                return true;
            }
            //permission.AuthRole  和
            foreach (hy_auth_role item in permission.AuthRoles.ToList())
            {
                hy_user ur = item.hy_users.FirstOrDefault(o => o.Id == userId);
                if (ur != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
