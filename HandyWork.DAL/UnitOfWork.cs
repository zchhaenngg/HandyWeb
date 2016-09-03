using HandyWork.Common.Model;
using HandyWork.DAL.Repository;
using HandyWork.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL
{
    /// <summary>
    /// 共享上下文
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        

        private UserEntities _userEntities;
        private HistoryEntities _historyEntities;
        private IUserRepository _userRepository;
        private IAuthPermissionRepository _authPermissionRepository;
        private IAuthRoleRepository _authRoleRepository;
        private IDataHistoryRepository _dataHistoryRepository;
        
        internal UserEntities UserEntities => _userEntities ?? (_userEntities = new UserEntities());
        internal HistoryEntities HistoryEntities => _historyEntities ?? (_historyEntities = new HistoryEntities());
        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(this));
        public IAuthPermissionRepository AuthPermissionRepository => _authPermissionRepository ?? (_authPermissionRepository = new AuthPermissionRepository(this));
        public IAuthRoleRepository AuthRoleRepository => _authRoleRepository ?? (_authRoleRepository = new AuthRoleRepository(this));
        internal IDataHistoryRepository DataHistoryRepository => _dataHistoryRepository ?? (_dataHistoryRepository = new DataHistoryRepository(this));

        public List<ErrorInfo> Errors { get; } = new List<ErrorInfo>();
        public void SaveChanges()
        {
            if (Errors.Count > 0)
            {
                return;
            }
            try
            {
                if (this._userEntities != null)
                {
                    this._userEntities.SaveChanges();
                }
                if (this._historyEntities != null)
                {
                    this._historyEntities.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                Errors.Add(new ErrorInfo("", ex.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage));
            }
        }

        public void Dispose()
        {
            if (this._userEntities != null)
            {
                this._userEntities.Dispose();
            }
            if (this._historyEntities != null)
            {
                this._historyEntities.Dispose();
            }
        }
    }
}
