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
    public class UnitOfWork : IDisposable
    {
        public List<ErrorInfo> ErrorInfos { get; } = new List<ErrorInfo>();

        internal UserEntities UserEntities { get; } = new UserEntities();
        internal HistoryEntities HistoryEntities { get; } = new HistoryEntities();

        private IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(this);
                }
                return _userRepository;
            }
        }

        private IAuthPermissionRepository _authPermissionRepository;
        public IAuthPermissionRepository AuthPermissionRepository
        {
            get
            {
                if (_authPermissionRepository == null)
                {
                    _authPermissionRepository = new AuthPermissionRepository(this);
                }
                return _authPermissionRepository;
            }
        }

        private IAuthRoleRepository _authRoleRepository;
        public IAuthRoleRepository AuthRoleRepository
        {
            get
            {
                if (_authRoleRepository == null)
                {
                    _authRoleRepository = new AuthRoleRepository(this);
                }
                return _authRoleRepository;
            }
        }

        private IDataHistoryRepository _dataHistoryRepository;
        internal IDataHistoryRepository DataHistoryRepository
        {
            get
            {
                if (_dataHistoryRepository == null)
                {
                    _dataHistoryRepository = new DataHistoryRepository(this);
                }
                return _dataHistoryRepository;
            }
        }

        public void SaveChanges()
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
