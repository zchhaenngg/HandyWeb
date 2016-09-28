using System.Linq;
using System.Security.Principal;
using HandyWork.Common.Authority;
using System.Web.Security;

namespace HandyWork.UIBusiness
{
    public class HandyPrincipal : IPrincipal
    {
        private string[] _roles;
        private Cookie _cookie;
        public IIdentity Identity { get; }
        public HandyPrincipal(IIdentity identity)
        {
            Identity = identity;
        }

        public Cookie Cookie  => _cookie ?? (_cookie = Cookie.Decoder((Identity as FormsIdentity)?.Ticket.UserData));

        /// <summary>
        /// 必须先登录授权。
        /// </summary>
        public string[] Roles
        {
            get
            {
                if (_roles == null)
                {
                    using (var manager = new UnitOfManager())
                    {
                        _roles = manager.AccountManager.GetAllPermissions4Code(Cookie.Id);
                    }
                }
                return _roles;
            }
        }
        
        public bool IsInRole(string role) => Roles?.FirstOrDefault(r => string.Equals(role, r)) != null;
    }
}
