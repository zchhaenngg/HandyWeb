namespace HandyWork.DAL
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Data.Entity.Infrastructure;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using Localization;
    using Model.Entity;
    using System.Threading.Tasks;
    using System.Threading;
    using System.Text;
    using Common.Helper;
    using Common.Exceptions;
    using HandyContext;
    using HandyModel.Entity;

    public partial class HyContext : HistoryDbContext<hy_data_history>
    {
        public HyContext() : base("name=MyConnection") { }

        public HyContext(string loginId)
            : base("name=MyConnection")
        {
            LoginId = loginId;
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
}

