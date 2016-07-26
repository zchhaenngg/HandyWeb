using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UIBusiness.Ext
{
    public static class IPrincipalExt
    {
        public static string GetLoginId(this IPrincipal principal)
        {
            GatherPrincipal u = principal as GatherPrincipal;
            return u == null ? null : u.LoginId;
        }
        public static string GetLoginName(this IPrincipal principal)
        {
            GatherPrincipal u = principal as GatherPrincipal;
            return u == null ? null : u.LoginName;
        }
        public static string GetLoginRealName(this IPrincipal principal)
        {
            GatherPrincipal u = principal as GatherPrincipal;
            return u == null ? null : u.LoginRealName;
        }
        public static bool IsInPermission(this IPrincipal principal, string permissionCode)
        {
            GatherPrincipal u = principal as GatherPrincipal;
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
