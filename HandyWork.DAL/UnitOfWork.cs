using HandyWork.Common.Authority;
using HandyWork.Common.Utility;
using HandyWork.DAL.Cache;
using HandyWork.DAL.Repository;
using HandyWork.DAL.Repository.Interfaces;
using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL
{
    /// <summary>
    /// 添加表之后需要在这里做一些配置
    /// </summary>
    public partial class UnitOfWork
    {
        //1.所有Repository都由GetRepository获取唯一实例
        private IUserRepository _userRepository;
        private IAuthPermissionRepository _authPermissionRepository;
        private IAuthRoleRepository _authRoleRepository;
        private IDataHistoryRepository _dataHistoryRepository;

        public IUserRepository UserRepository => GetRepository<AuthUser>() as UserRepository;
        public IAuthPermissionRepository PermissionRepository => GetRepository<AuthPermission>() as AuthPermissionRepository;
        public IAuthRoleRepository RoleRepository => GetRepository<AuthRole>() as AuthRoleRepository;
        public IDataHistoryRepository HistoryRepository => GetRepository<DataHistory>() as DataHistoryRepository;

        public IRepository GetRepository<TEntity>() where TEntity : class
        {
            if (typeof(TEntity).Equals(typeof(AuthUser)))
            {
                return _userRepository ?? (_userRepository = new UserRepository(this, Context.Users));
            }
            else if (typeof(TEntity).Equals(typeof(AuthPermission)))
            {
                return _authPermissionRepository ?? (_authPermissionRepository = new AuthPermissionRepository(this, Context.Permissions));
            }
            else if (typeof(TEntity).Equals(typeof(AuthRole)))
            {
                return _authRoleRepository ?? (_authRoleRepository = new AuthRoleRepository(this, Context.Roles));
            }
            else if (typeof(TEntity).Equals(typeof(DataHistory)))
            {
                return _dataHistoryRepository ?? (_dataHistoryRepository = new DataHistoryRepository(this, Context.Histories));
            }
            else
            {
                throw new NotSupportedException(string.Format("{0} 不支持 {1}", nameof(GetRepository), typeof(TEntity).Name));
            }
        }
        
        /// <summary>
        ///记录 AuthUser 历史变更情况
        /// </summary>
        private void SaveChangeSet(AuthUser entity)
        {
            var history = GetHistory(entity, nameof(entity.Id), nameof(entity.Password));
            history.ForeignId = entity.Id;
            Context.Histories.Add(history);
        }
        
        private void Modified(AuthUser entity)
        {
            var state = Context.Entry(entity).State;
            if (EntityState.Added == state)
            {
                entity.CreatedById = LoginId;
                entity.LastModifiedById = LoginId;
                entity.CreatedTime = DateTime.UtcNow;
                entity.LastModifiedTime = DateTime.UtcNow;
            }
            else if (EntityState.Modified == state)
            {
                entity.LastModifiedById = LoginId;
                entity.LastModifiedTime = DateTime.UtcNow;
            }
            else if (EntityState.Deleted == state)
            {
                RemoveAndClear(entity.AuthPermissions);
                RemoveAndClear(entity.AuthRoles);
            }
        }

        private void Modified(AuthRole entity)
        {
            var state = Context.Entry(entity).State;
            if (EntityState.Added == state)
            {
                entity.CreatedById = LoginId;
                entity.LastModifiedById = LoginId;
                entity.CreatedTime = DateTime.UtcNow;
                entity.LastModifiedTime = DateTime.UtcNow;
            }
            else if (EntityState.Modified == state)
            {
                entity.LastModifiedById = LoginId;
                entity.LastModifiedTime = DateTime.UtcNow;
            }
            else if (EntityState.Deleted == state)
            {
                RemoveAndClear(entity.AuthPermissions);
                RemoveAndClear(entity.Users);
            }
        }

        private void Modified(AuthPermission entity)
        {
            var state = Context.Entry(entity).State;
            if (EntityState.Added == state)
            {
                entity.CreatedById = LoginId;
                entity.LastModifiedById = LoginId;
                entity.CreatedTime = DateTime.UtcNow;
                entity.LastModifiedTime = DateTime.UtcNow;
            }
            else if (EntityState.Modified == state)
            {
                entity.LastModifiedById = LoginId;
                entity.LastModifiedTime = DateTime.UtcNow;
            }
            else if (EntityState.Deleted == state)
            {
                RemoveAndClear(entity.Users);
                RemoveAndClear(entity.AuthRoles);
            }
        }

        //1.SaveChanges
        public int SaveChanges()
        {
            if (Context.ChangeTracker.HasChanges())
            {
                foreach (var entry in Context.ChangeTracker.Entries())
                {
                    if (entry.Entity is AuthUser)
                    {
                        var entity = entry.Entity as AuthUser;
                        Modified(entity);
                        SaveChangeSet(entity);
                    }
                    else if (entry.Entity is AuthPermission)
                    {
                        var entity = entry.Entity as AuthPermission;
                        Modified(entity);
                    }
                    else if (entry.Entity is AuthRole)
                    {
                        var entity = entry.Entity as AuthRole;
                        Modified(entity);
                    }
                    else if (entry.Entity is DataHistory)
                    { }
                    else
                    {
                        //防止忘记!
                        throw new NotSupportedException(string.Format("{0} 不支持 {1}", nameof(SaveChanges), entry.Entity.GetType().Name));
                    }
                }
            }
            return Context.SaveChanges();
        }

       
    }
    /// <summary>
    /// EntityContext的包装类.历史变更等
    /// </summary>
    public partial class UnitOfWork : IDisposable
    {
        //private
        private EntityContext _context;
        //私有该属性
        private EntityContext Context => _context ?? (_context = new EntityContext());
        //所有操作应当都有操作人员，默认已有系统管理员id为-1
        //待将该参数置入构造函数中
        public string LoginId { get; set; }
        public UnitOfWork(string loginId)
        {
            LoginId = loginId;
        }
        // 1.Add
        public TEntity Add<TEntity>(TEntity entity)
            where TEntity : class
        {
            return Context.Set<TEntity>().Add(entity);

        }
        public IEnumerable<TEntity> AddRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {
            return Context.Set<TEntity>().AddRange(entities);
        }
        
        //2.RemoveAndClear
        public void RemoveAndClear<TEntity>(ICollection<TEntity> entities)
            where TEntity : class
        {
            Context.Set<TEntity>().RemoveRange(entities);
            entities.Clear();
        }
        public TEntity Remove<TEntity>(TEntity entity)
            where TEntity : class
        {
            return Context.Set<TEntity>().Remove(entity);
        }
        
        //3.Validation。call this method for each entity in its cache whose state is not Unchanged
        public IEnumerable<DbEntityValidationResult> ValidateEntities()
        {
            if (_context == null)
            {
                return null;
            }
            else
            {
                return _context.GetValidationErrors();
            }
        }

        private DataHistory GetHistory<T>(T entity, params string[] ignoreProperties)
             where T : class
        {
            var entry = Context.Entry(entity);
            var history = new DataHistory();
            history.Id = Guid.NewGuid().ToString();
            history.CreatedById = LoginId;
            history.LastModifiedById = LoginId;
            history.CreatedTime = DateTime.UtcNow;
            history.LastModifiedTime = DateTime.UtcNow;
            history.Category = typeof(T).Name;
            if (EntityState.Deleted == entry.State)
            {
                history.Description = "数据已被删除！";
            }
            else
            {
                history.Description = SysColumnsCache.CompareObject(typeof(T).Name, entity, (propName) =>
                {
                    if (ignoreProperties != null)
                    {
                        if (ignoreProperties.Contains(propName))
                        {
                            return null;
                        }
                    }
                    if (entry.State == EntityState.Added)
                    {
                        return new Tuple<string, string>("", (entry.CurrentValues[propName] ?? "").ToString());
                    }
                    else
                    {
                        return new Tuple<string, string>((entry.OriginalValues[propName] ?? "").ToString(), (entry.CurrentValues[propName] ?? "").ToString());
                    }
                });
            }
            return history;
        }
        public void Dispose()
        {
            if (this._context != null)
            {
                this._context.Dispose();
            }
        }
    }
}
