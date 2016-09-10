using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UIBusiness.Extensions
{
    public static class IPrincipalExtension
    {
        public static string GetLoginId(this IPrincipal principal)
        {
            HandyPrincipal u = principal as HandyPrincipal;
            return u == null ? null : u.LoginId;
        }
        public static string GetLoginName(this IPrincipal principal)
        {
            HandyPrincipal u = principal as HandyPrincipal;
            return u == null ? null : u.LoginName;
        }
        public static string GetLoginRealName(this IPrincipal principal)
        {
            HandyPrincipal u = principal as HandyPrincipal;
            return u == null ? null : u.LoginRealName;
        }
        public static bool IsInPermission(this IPrincipal principal, string permissionCode)
        {
            HandyPrincipal u = principal as HandyPrincipal;
            if (u == null)
            {
                return false;
            }
            else
            {
                return u.IsInPermission(permissionCode);
            }
        }
    }
}
