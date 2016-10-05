using HandyWork.Common.Authority;
using HandyWork.DAL;
using HandyWork.UIBusiness.Manager;
using HandyWork.UIBusiness.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using HandyWork.UIBusiness.Extensions;
using System.Web;

namespace HandyWork.UIBusiness
{
    public class UnitOfManager : IDisposable
    {
        public string LoginId { get; }
        public UnitOfManager(string loginId)
        {
            LoginId = loginId;
        }

        private UnitOfWork _unitOfWork;
        internal UnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork =  new UnitOfWork(LoginId));

        private IAccountManager _accountManager;
        public IAccountManager AccountManager => _accountManager ?? (_accountManager = new AccountManager(this));

        private OwinManager _owinManager;
        public OwinManager OwinManager => _owinManager ?? (_owinManager = new OwinManager(this));

        public void Dispose()
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.Dispose();
            }
        } 
    }
}
