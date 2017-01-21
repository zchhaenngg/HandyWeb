using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Common.Extensions;

namespace HandyWork.Services.Extensions
{
    public static class IPrincipalExtension
    {
        public static string GetLoginId(this IPrincipal principal)
        {
            return (principal as ClaimsPrincipal)?.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
        }
        public static string GetLoginName(this IPrincipal principal)
        {
            return (principal as ClaimsPrincipal)?.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value;
        }
        public static string GetLoginNickName(this IPrincipal principal)
        {
            return (principal as ClaimsPrincipal)?.Claims.FirstOrDefault(o => o.Type == "NickName")?.Value;
        }

        /// <summary>
        /// 登陆时查询出的Email地址
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetEmailAddress(this IPrincipal principal)
        {
            return (principal as ClaimsPrincipal)?.Claims.FirstOrDefault(o => o.Type == "EmailAddress")?.Value;
        }
        public static int TimezoneOffsetInMinute(this IPrincipal principal)
        {
            return (principal as ClaimsPrincipal)?.Claims.FirstOrDefault(o => o.Type == "GreaterThanUTCInMinute")?.Value.ToInt() ?? 480;
        }
    }
}
