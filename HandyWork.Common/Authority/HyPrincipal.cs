using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace HandyWork.Common.Authority
{
    public class HyPrincipal : IPrincipal
    {
        private string[] _roles;
        private Cookie _cookie;
        public IIdentity Identity { get; }
        public HyPrincipal(IIdentity identity)
        {
            Identity = identity;
        }

        public Cookie Cookie => _cookie ?? (_cookie = Cookie.Decoder((Identity as FormsIdentity)?.Ticket.UserData));

        /// <summary>
        /// 必须先登录授权。
        /// </summary>
        public string[] Roles
        {
            get
            {
                return Cookie.Roles;
            }
        }

        public bool IsInRole(string role) => Roles?.FirstOrDefault(r => string.Equals(role, r)) != null;
    }
}
