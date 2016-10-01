using HandyWork.Common.Authority;
using HandyWork.Common.Utility;
using HandyWork.DAL.Repository;
using HandyWork.DAL.Repository.Interfaces;
using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL
{
    /// <summary>
    /// EntityContext的包装类
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        //private
        private EntityContext _entityContext;
        private IUserRepository _userRepository;
        private IAuthPermissionRepository _authPermissionRepository;
        private IAuthRoleRepository _authRoleRepository;
        private IDataHistoryRepository _dataHistoryRepository;
        //所有操作应当都有操作人员，默认已有系统管理员id为-1
        //待将该参数置入构造函数中
        public int LoginId { get; set; }
        //1.SaveChanges
        public int SaveChanges()
        {
            return EntityContext.SaveChanges();
        }
        //2.扩展RemoveAndClear
        public void RemoveAndClear<TEntity>(ICollection<TEntity> entities)
            where TEntity : class
        {
            EntityContext.Set<TEntity>().RemoveRange(entities);
            entities.Clear();
        }
        //3.所有Repository都由GetRepository获取唯一实例
        public IUserRepository UserRepository => GetRepository<AuthUser>() as UserRepository;
        public IAuthPermissionRepository PermissionRepository => GetRepository<AuthPermission>() as AuthPermissionRepository;
        public IAuthRoleRepository RoleRepository => GetRepository<AuthRole>() as AuthRoleRepository;
        public IDataHistoryRepository DataHistoryRepository => GetRepository<DataHistory>() as DataHistoryRepository;
        public IRepository GetRepository<TEntity>() where TEntity : class
        {
            if (typeof(TEntity).Equals(typeof(AuthUser)))
            {
                return _userRepository ?? (_userRepository = new UserRepository(this));
            }
            else if (typeof(TEntity).Equals(typeof(AuthPermission)))
            {
                return _authPermissionRepository ?? (_authPermissionRepository = new AuthPermissionRepository(this));
            }
            else if (typeof(TEntity).Equals(typeof(AuthRole)))
            {
                return _authRoleRepository ?? (_authRoleRepository = new AuthRoleRepository(this));
            }
            else if (typeof(TEntity).Equals(typeof(DataHistory)))
            {
                return _dataHistoryRepository ?? (_dataHistoryRepository = new DataHistoryRepository(this));
            }
            else
            {
                throw new NotSupportedException(string.Format("{0} 不支持 {1}", nameof(GetRepository), typeof(TEntity).Name));
            }
        }
        //4.校验。call this method for each entity in its cache whose state is not Unchanged
        public IEnumerable<DbEntityValidationResult> ValidateEntities()
        {
            if (_entityContext == null)
            {
                return null;
            }
            else
            {
                return _entityContext.GetValidationErrors();
            }
        }
       

        //待删除
        internal EntityContext EntityContext => _entityContext ?? (_entityContext = new EntityContext());
        //待删除
        public List<Error> Errors { get; } = new List<Error>();
        
        public void Dispose()
        {
            if (this._entityContext != null)
            {
                this._entityContext.Dispose();
            }
        }
    }
}
