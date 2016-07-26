using HandyWork.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace HandyWork.Common
{
    public class CurrentHttpContext
    {
        public HttpRequest Request
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }

        public IPrincipal CurrentUser
        {
            get
            {
                return HttpContext.Current.User;
            }
        }
        public IIdentity Identity
        {
            get
            {
                return CurrentUser.Identity;
            }
        }

        private UserCookieData _userCookie;
        protected UserCookieData UserCookie
        {
            get
            {
                if (Identity as FormsIdentity == null)
                {
                    return null;
                }
                if (_userCookie == null)
                {
                    _userCookie = UserCookieData.Decoder((Identity as FormsIdentity).Ticket.UserData);
                }
                return _userCookie;
            }
        }

        protected string[] Roles { get { return UserCookie.Roles; } }

        public string LoginName { get { return UserCookie.Name; } }
        public string LoginRealName { get { return UserCookie.RealName; } }

        public string LoginId
        {
            get
            {
                return UserCookie == null ? null : UserCookie.Id;
            }
        }
        public bool IsInRole(string role)
        {
            if (Roles == null)
            {
                return false;
            }
            return Roles.FirstOrDefault(r => string.Equals(role, r)) != null;
        }

    }
}
