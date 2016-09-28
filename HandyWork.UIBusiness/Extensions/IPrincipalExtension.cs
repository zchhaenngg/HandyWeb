using HandyWork.Common.Authority;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace HandyWork.UIBusiness.Extensions
{
    public static class IPrincipalExtension
    {
        public static string GetLoginId(this IPrincipal principal)
        {
            return principal.GetCookie()?.Id;
        }
        public static string GetLoginName(this IPrincipal principal)
        {
            return principal.GetCookie()?.Name;
        }
        public static string GetLoginRealName(this IPrincipal principal)
        {
            return principal.GetCookie()?.RealName;
        }
        public static int TimezoneOffsetInMinute(this IPrincipal principal)
        {
            return principal.GetCookie()?.TimezoneOffsetInMinute ?? 0;
        }
        public static Cookie GetCookie(this IPrincipal principal)
        {
            return (principal as HandyPrincipal)?.Cookie;
        }
    }
}
