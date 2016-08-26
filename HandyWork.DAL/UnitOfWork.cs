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
        private IUserRepository _userRepository;
        private IAuthPermissionRepository _authPermissionRepository;
        private IAuthRoleRepository _authRoleRepository;
        private IDataHistoryRepository _dataHistoryRepository;

        public List<ErrorInfo> ErrorInfos { get; } = new List<ErrorInfo>();

        internal UserEntities UserEntities { get; } = new UserEntities();
        internal HistoryEntities HistoryEntities { get; } = new HistoryEntities();
        
        public IUserRepository UserRepository
            => _userRepository
                ?? (_userRepository = new UserRepository(this));
        
        public IAuthPermissionRepository AuthPermissionRepository 
            => _authPermissionRepository  
                ?? (_authPermissionRepository = new AuthPermissionRepository(this));

        public IAuthRoleRepository AuthRoleRepository 
            => _authRoleRepository
                ?? (_authRoleRepository = new AuthRoleRepository(this));
        
        internal IDataHistoryRepository DataHistoryRepository
            => _dataHistoryRepository
            ?? (_dataHistoryRepository = new DataHistoryRepository(this));

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
