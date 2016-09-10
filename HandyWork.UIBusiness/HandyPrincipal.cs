using HandyWork.DAL;
using HandyWork.DAL.Repository;
using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UIBusiness
{
    public class HandyPrincipal : IPrincipal
    {
        protected CurrentHttpContext _CurrentHttpContext;
        protected CurrentHttpContext CurrentHttpContext
        {
            get
            {
                if (_CurrentHttpContext == null)
                {
                    _CurrentHttpContext = new CurrentHttpContext();
                }
                return _CurrentHttpContext;
            }
        }
        public IIdentity Identity { get; private set; }
        public HandyPrincipal(IIdentity identity)
        {
            Identity = identity;
        }

        private List<AuthPermission> _permissions;

        protected List<AuthPermission> Permissions
        {
            get
            {
                if (_permissions == null)
                {
                    using (UnitOfWork unitOfWork = new UnitOfWork())
                    {
                        UserRepository userRepository = new UserRepository(unitOfWork);
                        _permissions = userRepository.GetAllPermissions(LoginId);
                    }
                }
                return _permissions;
            }
        }

        public bool IsInPermission(string permissionCode)
        {
            return Permissions.Exists(o => o.Code == permissionCode);
        }

        public bool IsInRole(string role)
        {
            return CurrentHttpContext.IsInRole(role);
        }

        public string LoginId { get { return CurrentHttpContext.LoginId; } }
        public string LoginName { get { return CurrentHttpContext.LoginName; } }
        public string LoginRealName { get { return CurrentHttpContext.LoginRealName; } }
    }
}
