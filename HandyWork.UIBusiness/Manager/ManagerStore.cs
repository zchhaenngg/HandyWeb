using HandyWork.Common.Model;
using HandyWork.DAL;
using HandyWork.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UIBusiness.Manager
{
    /// <summary>
    /// 访问入口，保证使用统一的上下文
    /// </summary>
    public class ManagerStore : IDisposable
    {
        private AccountManager _accountManager;
        private SelectListManager _selectListManager;

        #region 用户及权限
        internal UserEntities UserEntities { get; private set; } = new UserEntities();
        private UserRepository _userRepository;
        private AuthPermissionRepository _authPermissionRepository;
        private AuthRoleRepository _authRoleRepository;
        #endregion

        #region 配置历史等
        internal HistoryEntities HistoryEntities { get; } = new HistoryEntities();
        private DataHistoryRepository _dataHistoryRepository;
        #endregion


        public ManagerStore()
        {

        }

        public List<ErrorInfo> ErrorInfos { get; }= new List<ErrorInfo>();

        #region Service

        public AccountManager AccountManager
        {
            get
            {
                if (_accountManager == null)
                {
                    _accountManager = new AccountManager(this);
                }
                return _accountManager;
            }
        }

        public SelectListManager SelectListManager
        {
            get
            {
                if (_selectListManager == null)
                {
                    _selectListManager = new SelectListManager(this);
                }
                return _selectListManager;
            }
        }
        #endregion

        #region Repository 用户及权限

        internal UserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(UserEntities, ErrorInfos, DataHistoryRepository);
                }
                return _userRepository;
            }
        }

        internal AuthPermissionRepository AuthPermissionRepository
        {
            get
            {
                if (_authPermissionRepository == null)
                {
                    _authPermissionRepository = new AuthPermissionRepository(UserEntities, ErrorInfos);
                }
                return _authPermissionRepository;
            }
        }

        internal AuthRoleRepository AuthRoleRepository
        {
            get
            {
                if (_authRoleRepository == null)
                {
                    _authRoleRepository = new AuthRoleRepository(UserEntities, ErrorInfos);
                }
                return _authRoleRepository;
            }
        }
        #endregion

        #region Repository 配置历史等
        internal DataHistoryRepository DataHistoryRepository
        {
            get
            {
                if (_dataHistoryRepository == null)
                {
                    _dataHistoryRepository = new DataHistoryRepository(HistoryEntities, ErrorInfos);
                }
                return _dataHistoryRepository;
            }
        }
        #endregion 

        /// <summary>
        /// -1 表明ErrorInfos中有错误记录因此不会提交变更。
        /// </summary>
        internal void SaveChanges()
        {
            if (ErrorInfos.Count > 0)
            {
                return;
            }
            try
            {
                this.UserEntities.SaveChanges();
                this.HistoryEntities.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                ErrorInfos.Add(new ErrorInfo("", ex.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage));
            }
        }

        public void Dispose()
        {
            this.UserEntities.Dispose();
            this.HistoryEntities.Dispose();
        }
    }
}
