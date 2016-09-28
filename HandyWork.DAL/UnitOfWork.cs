using HandyWork.Common.Authority;
using HandyWork.DAL.Repository;
using HandyWork.DAL.Repository.Interfaces;
using HandyWork.Model;
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
        private EntityContext _entityContext;
        private IUserRepository _userRepository;
        private IAuthPermissionRepository _authPermissionRepository;
        private IAuthRoleRepository _authRoleRepository;
        private IDataHistoryRepository _dataHistoryRepository;

        internal EntityContext EntityContext => _entityContext ?? (_entityContext = new EntityContext());
        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(this));
        public IAuthPermissionRepository AuthPermissionRepository => _authPermissionRepository ?? (_authPermissionRepository = new AuthPermissionRepository(this));
        public IAuthRoleRepository AuthRoleRepository => _authRoleRepository ?? (_authRoleRepository = new AuthRoleRepository(this));
        internal IDataHistoryRepository DataHistoryRepository => _dataHistoryRepository ?? (_dataHistoryRepository = new DataHistoryRepository(this));

        public List<Error> Errors { get; } = new List<Error>();
        public void RemoveAndClear<TEntity>(ICollection<TEntity> entities)
            where TEntity : class
        {
            EntityContext.Set<TEntity>().RemoveRange(entities);
            entities.Clear();
        }
        public void SaveChanges()
        {
            if (Errors.Count > 0)
            {
                return;
            }
            try
            {
                if (this._entityContext != null)
                {
                    this._entityContext.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                Errors.Add(new Error("", ex.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage));
            }
        }
        public void Dispose()
        {
            if (this._entityContext != null)
            {
                this._entityContext.Dispose();
            }
        }
    }
}
