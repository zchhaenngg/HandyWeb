using HandyWork.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace HandyWork.UIBusiness
{
    public class CurrentHttpContext
    {
        public HttpRequest Request => HttpContext.Current.Request;
        private UserCookie _userCookie;
        protected UserCookie UserCookie
        {
            get
            {
                if (_userCookie == null)
                {
                    _userCookie = UserCookie.Decoder((HttpContext.Current.User.Identity as FormsIdentity)?.Ticket.UserData);
                }
                return _userCookie;
            }
        }

        protected string[] Roles { get { return UserCookie.Roles; } }

        public string LoginName { get { return UserCookie.Name; } }
        public string LoginRealName { get { return UserCookie.RealName; } }

        public string LoginId => UserCookie?.Id;
        public bool IsInRole(string role)=> UserCookie?.Roles?.FirstOrDefault(r => string.Equals(role, r)) != null;

    }
}
