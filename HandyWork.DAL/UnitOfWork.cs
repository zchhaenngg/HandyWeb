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
    public partial class UnitOfWork
    {//记录所有历史变更
        private DataHistory GetHistory<T>(T entity, params string[] ignoreProperties)
             where T : class
        {
            var entry = EntityContext.Entry(entity);
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
        /// <summary>
        ///将User的相关变更记录到历史表中等 
        /// </summary>
        private void ChangeUser(AuthUser entity)
        {
            var history = GetHistory(entity, nameof(entity.Id), nameof(entity.Password));
            history.ForeignId = entity.Id;
            EntityContext.Histories.Add(history);
            if (EntityState.Added == EntityContext.Entry(entity).State)
            {
                entity.CreatedById = LoginId;
                entity.LastModifiedById = LoginId;
                entity.CreatedTime = DateTime.UtcNow;
                entity.LastModifiedTime = DateTime.UtcNow;
            }
            else if (EntityState.Modified == EntityContext.Entry(entity).State)
            {
                entity.LastModifiedById = LoginId;
                entity.LastModifiedTime = DateTime.UtcNow;
            }
        }
    }
    /// <summary>
    /// EntityContext的包装类
    /// </summary>
    public partial class UnitOfWork : IDisposable
    {
        //private
        private EntityContext _entityContext;
        private IUserRepository _userRepository;
        private IAuthPermissionRepository _authPermissionRepository;
        private IAuthRoleRepository _authRoleRepository;
        private IDataHistoryRepository _dataHistoryRepository;
        //所有操作应当都有操作人员，默认已有系统管理员id为-1
        //待将该参数置入构造函数中
        public string LoginId { get; set; }
        //1.SaveChanges
        public int SaveChanges()
        {
            if (EntityContext.ChangeTracker.HasChanges())
            {
                foreach (var entry in EntityContext.ChangeTracker.Entries())
                {
                    if (entry.Entity is AuthUser)
                    {
                        var user = entry.Entity as AuthUser;
                        ChangeUser(user);
                    }     
                }
            }
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
        public IDataHistoryRepository HistoryRepository => GetRepository<DataHistory>() as DataHistoryRepository;
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
       
        //1私有该属性
        internal EntityContext EntityContext => _entityContext ?? (_entityContext = new EntityContext());
        
        public void Dispose()
        {
            if (this._entityContext != null)
            {
                this._entityContext.Dispose();
            }
        }
    }
}
