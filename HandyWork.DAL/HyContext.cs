namespace HandyWork.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Infrastructure;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using Localization;
    using Model;
    using Model.Entity;
    using Model.Entity.Interfaces;
    using System.Threading.Tasks;
    using System.Threading;
    using System.Data.Entity.Core.Objects;
    using System.ComponentModel.DataAnnotations;
    using HandyWork.Common.Extensions;
    using System.Text;
    using Common.Helper;
    using Common.Exceptions;

    public partial class HyContext : DbContext
    {
        /// <summary>
        /// 操作人员
        /// </summary>
        public string LoginId { get; set; }

        public HyContext() : base("name=MyConnection") { }

        public HyContext(string loginId)
            : base("name=MyConnection")
        {
            LoginId = loginId;
            ChangedEvent = Changed;
            ChangedEvent += AddDataHistories;
        }
        
        public virtual DbSet<hy_configuration> hy_configurations { get; set; }
        public virtual DbSet<hy_auth_permission> hy_auth_permissions { get; set; }
        public virtual DbSet<hy_auth_role> hy_auth_roles { get; set; }
        public virtual DbSet<hy_data_history> hy_data_histories { get; set; }
        public virtual DbSet<hy_user> hy_users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
    /// <summary>
    /// 校验
    /// </summary>
    public partial class HyContext
    {
        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            var result = base.ValidateEntity(entityEntry, items);
            if (result.IsValid)
            {
                if (entityEntry.Entity is hy_user)
                {
                    ValidateUser(entityEntry, ref result);
                }
                else if (entityEntry.Entity is hy_auth_permission)
                {
                    ValidatePermission(entityEntry, ref result);
                }
                else if (entityEntry.Entity is hy_auth_role)
                {
                    ValidateRole(entityEntry, ref result);
                }
            }
            return result;
        }

        private void ValidateRole(DbEntityEntry entityEntry, ref DbEntityValidationResult result)
        {
            var role = entityEntry.Entity as hy_auth_role;
            if (hy_auth_roles.Any(p => p.name == role.name && p.id != role.id))
            {
                result.ValidationErrors.Add(
                        new DbValidationError(nameof(hy_auth_role.name), ValidatorResource.Role_Duplicate_Name));
            }
        }

        private void ValidatePermission(DbEntityEntry entityEntry, ref DbEntityValidationResult result)
        {
            var permission = entityEntry.Entity as hy_auth_permission;
            if (hy_auth_permissions.Any(p => p.code == permission.code && p.id != permission.id))
            {
                result.ValidationErrors.Add(
                        new DbValidationError(nameof(hy_auth_permission.code), ValidatorResource.Permission_Duplicate_Code));
            }
            else if (hy_auth_permissions.Any(p => p.name == permission.name && p.id != permission.id))
            {
                result.ValidationErrors.Add(
                      new DbValidationError(nameof(hy_auth_permission.name), ValidatorResource.Permission_Duplicate_Name));
            }
        }

        private void ValidateUser(DbEntityEntry entityEntry, ref DbEntityValidationResult result)
        {
            var user = entityEntry.Entity as hy_user;
            if (hy_users.Any(p => p.user_name == user.user_name && p.id != user.id))
            {
                result.ValidationErrors.Add(
                        new DbValidationError(nameof(hy_user.user_name), ValidatorResource.User_Duplicate_UserName));
            }
        }
    }
    /// <summary>
    /// SaveChanges
    /// </summary>
    public partial class HyContext
    {
        public event Action ChangedEvent;

        public override int SaveChanges()
        {
            ChangedEvent?.Invoke();
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException dbex)
            {
                var error = GetValidationExceptionString(dbex);
                LogHelper.ErrorLog.Error(error);
                throw new LogException(error, false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public override Task<int> SaveChangesAsync()
        {
            ChangedEvent?.Invoke();
            try
            {
                return base.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbex)
            {
                var error = GetValidationExceptionString(dbex);
                LogHelper.ErrorLog.Error(error);
                throw new LogException(error, false);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            ChangedEvent?.Invoke();
            try
            {
                return base.SaveChangesAsync(cancellationToken);
            }
            catch (DbEntityValidationException dbex)
            {
                var error = GetValidationExceptionString(dbex);
                LogHelper.ErrorLog.Error(error);
                throw new LogException(error, false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static string GetValidationExceptionString(DbEntityValidationException dbex)
        {
            var builder = new StringBuilder();
            foreach (var item in dbex.EntityValidationErrors)
            {
                foreach (var error in item.ValidationErrors)
                {
                    builder.Append(error.ErrorMessage);
                }
            }
            return builder.ToString();
        }
    }
    /// <summary>
    /// modified property like created_time,last_modified_by_id,etc
    /// </summary>
    public partial class HyContext
    {
        private void Changed()
        {
            if (ChangeTracker.HasChanges())
            {
                foreach (var entry in ChangeTracker.Entries())
                {
                    if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                    {
                        if (entry.Entity is hy_ICreator)
                        {
                            var entity = entry.Entity as hy_ICreator;
                            entity.created_by_id = LoginId;
                            entity.created_time = DateTime.UtcNow;
                        }
                        if (entry.Entity is hy_IModifier)
                        {
                            var entity = entry.Entity as hy_IModifier;
                            entity.last_modified_by_id = LoginId;
                            entity.last_modified_time = DateTime.UtcNow;
                        }
                    }
                }
            }
        }
    }
    /// <summary>
    /// 提交变更历史
    /// </summary>
    public partial class HyContext
    {
        private void AddDataHistories()
        {
            if (ChangeTracker.HasChanges())
            {
                foreach (var entry in ChangeTracker.Entries())
                {
                    if (entry.Entity is hy_data_history)
                    {
                        continue;
                    }
                    if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    {
                        continue;
                    }
                    WriteHistory(entry);
                }
            }
        }
        private void WriteHistory(DbEntityEntry entry)
        {
            var history = GetHistory(entry);
            history.category_name = history.entity_name;
            if (!string.IsNullOrWhiteSpace(history.description))
            {
                hy_data_histories.Add(history);
            }
        }
        private hy_data_history GetHistory(DbEntityEntry entry, params string[] ignores)
        {
            var data = new hy_data_history
            {
                id = Guid.NewGuid().ToString(),
                created_by_id = LoginId,
                created_time = DateTime.UtcNow,
                unique_key = GetPrimaryKeyValue(entry),
                entity_name = GetObjectType(entry.Entity.GetType()).Name,
                description = GetHistoryDescription(entry, ignores)
            };
            switch (entry.State)
            {
                case EntityState.Added:
                    data.operation = "新增";
                    break;
                case EntityState.Modified:
                    data.operation = "修改";
                    break;
                case EntityState.Deleted:
                    data.operation = "删除";
                    break;
                default:
                    break;
            }
            return data;
        }

        private string GetHistoryDescription(DbEntityEntry entry, params string[] ignores)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    return entry.CurrentValues.PropertyNames.Where(p => !ignores?.Contains(p) != null && IsToRecord(entry.CurrentValues[p], null))
                        .Select(p => string.Format("{0}:{1}", GetPropertyDescription(entry.Entity.GetType(), p), GetValueDescription(entry, p, true)))
                        .Aggregate((current, item) => { return current += item + "<br/>"; });
                case EntityState.Modified:
                    return entry.CurrentValues.PropertyNames.Where(p => !ignores?.Contains(p) != null && IsToRecord(entry.CurrentValues[p], entry.OriginalValues[p]))
                        .Select(p => string.Format("{0}:{1} -> {2}", GetPropertyDescription(entry.Entity.GetType(), p), GetValueDescription(entry, p, false), GetValueDescription(entry, p, true)))
                        .Aggregate((current, item) => { return current += item + "<br/>"; });
                case EntityState.Deleted:
                    return "已删除！";
                default:
                    return null;
            }
        }

        private bool IsToRecord(object currentValue, object orginalValue)
        {
            if (currentValue == null && orginalValue == null)
            {
                return false;
            }
            else if (currentValue?.GetType() == typeof(byte[]) || (orginalValue?.GetType() == typeof(byte[])))
            {
                return false;
            }
            return currentValue != orginalValue;
        }

        public string GetValueDescription(DbEntityEntry entry, string propertyName, bool isToGetCurrent)
        {
            var value = isToGetCurrent ? entry.CurrentValues[propertyName]: entry.OriginalValues[propertyName];
            if (entry.Entity is hy_ICreator && propertyName == nameof(hy_ICreator.created_by_id)
                ||(entry.Entity is hy_IModifier && propertyName == nameof(hy_IModifier.last_modified_by_id)))
            {
                var actualValue = (string)value;
                return hy_users.First(o => o.id == actualValue).nick_name;
            }
            return value.ToString();
        }
    }

    /// <summary>
    /// Improve Method
    /// </summary>
    public partial class HyContext
    {
        public EntityState GetEntityState<TEntity>(TEntity entity) where TEntity : class
        {
            return Entry(entity).State;
        }

        public TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            return Set<TEntity>().Add(entity);
        }
        public IEnumerable<TEntity> AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            return Set<TEntity>().AddRange(entities);
        }

        public void RemoveAndClear<TEntity>(ICollection<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().RemoveRange(entities);
            entities.Clear();
        }
        public TEntity Remove<TEntity>(TEntity entity) where TEntity : class
        {
            return Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// call this method for each entity in its cache whose state is not Unchanged
        /// </summary>
        public IEnumerable<DbEntityValidationResult> ValidateEntities()
        {
            return GetValidationErrors();
        }
        public Type GetObjectType(Type entityType)
        {
            return ObjectContext.GetObjectType(entityType);
        }

        public string GetPropertyDescription(Type entity, string propertyName)
        {
            var displayAttribute = entity.GetProperty(propertyName).GetCustomerAttribute<DisplayAttribute>();
            return displayAttribute?.Description ?? propertyName;
        }
        public string GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            var values = objectStateEntry.EntityKey.EntityKeyValues?.Select(kv => kv.Value);
            if (values == null)
            {
                return string.Empty;
            }
            else
            {
                return string.Join(",", values);
            }
        }
        public DbQuery<TEntity> AsNoTracking<TEntity>()
            where TEntity : class
        {
            return Set<TEntity>().AsNoTracking();
        }
        public DbSet<TEntity> AsTracking<TEntity>()
            where TEntity : class
        {
            return Set<TEntity>();
        }
    }
}

