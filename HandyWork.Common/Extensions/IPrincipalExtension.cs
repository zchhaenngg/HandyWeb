using HandyWork.Common.Authority;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Extensions
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
            return principal.GetCookie()?.NickName;
        }
        public static int TimezoneOffsetInMinute(this IPrincipal principal)
        {
            return principal.GetCookie()?.GreaterThanUTCInMinute ?? 0;
        }
        public static HyCookie GetCookie(this IPrincipal principal)
        {
            return (principal as HyPrincipal)?.Cookie;
        }
    }
}
