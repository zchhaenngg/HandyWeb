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
            AuthPermission permission = UnitOfWork.AuthPermissionRepository.FindByCode(permissionCode);
            if (permission == null)
            {
                return false;
            }
            AuthUser usr = permission.Users.FirstOrDefault(o => o.Id == userId);
            if (usr != null)
            {
                return true;
            }
            //permission.AuthRole  和
            foreach (AuthRole item in permission.AuthRoles.ToList())
            {
                AuthUser ur = item.Users.FirstOrDefault(o => o.Id == userId);
                if (ur != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
