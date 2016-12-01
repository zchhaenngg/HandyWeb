using HandyWork.Common.Authority;
using HandyWork.Common.Utility;
using HandyWork.DAL.Queryable;
using HandyWork.DAL.Repository;
using HandyWork.DAL.Repository.Interfaces;
using HandyWork.Model;
using HandyWork.ViewModel.PCWeb.Query;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Common.Extensions;
using HandyWork.Model.Entity;

namespace HandyWork.DAL
{
    /// <summary>
    /// 添加表之后需要在这里做一些配置
    /// EntityContext的包装类.历史变更等
    /// </summary>
    public partial class UnitOfWork
    {
        //1.所有Repository都由GetRepository获取唯一实例
        private IUserRepository _userRepository;
        private IAuthPermissionRepository _authPermissionRepository;
        private IAuthRoleRepository _authRoleRepository;
        private IDataHistoryRepository _dataHistoryRepository;

        public IUserRepository UserRepository => GetRepository<hy_user>() as UserRepository;
        public IAuthPermissionRepository PermissionRepository => GetRepository<hy_auth_permission>() as AuthPermissionRepository;
        public IAuthRoleRepository RoleRepository => GetRepository<hy_auth_role>() as AuthRoleRepository;
        public IDataHistoryRepository HistoryRepository => GetRepository<hy_data_history>() as DataHistoryRepository;

        public IRepository GetRepository<TEntity>() where TEntity : class
        {
            if (typeof(TEntity).Equals(typeof(hy_user)))
            {
                return _userRepository ?? (_userRepository = new UserRepository(this));
            }
            else if (typeof(TEntity).Equals(typeof(hy_auth_permission)))
            {
                return _authPermissionRepository ?? (_authPermissionRepository = new AuthPermissionRepository(this));
            }
            else if (typeof(TEntity).Equals(typeof(hy_auth_role)))
            {
                return _authRoleRepository ?? (_authRoleRepository = new AuthRoleRepository(this));
            }
            else if (typeof(TEntity).Equals(typeof(hy_data_history)))
            {
                return _dataHistoryRepository ?? (_dataHistoryRepository = new DataHistoryRepository(this));
            }
            else
            {
                throw new NotSupportedException(string.Format("{0} 不支持 {1}", nameof(GetRepository), typeof(TEntity).Name));
            }
        }
        
    }
    public partial class UnitOfWork
    {
        public DbSet<TEntity> AsTracking<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>();
        }
        public DbQuery<TEntity> AsNoTracking<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>().AsNoTracking();
        }
    }
    public partial class UnitOfWork : IDisposable
    {
        public UnitOfWork(string loginId)
        {
            LoginId = loginId;
        }
        private HyContext _context;
        private HyContext Context => _context ?? (_context = new HyContext(LoginId));
        public string LoginId { get; }
        
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
