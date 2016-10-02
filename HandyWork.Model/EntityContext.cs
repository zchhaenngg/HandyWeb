namespace HandyWork.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Infrastructure;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using Localization;

    public partial class EntityContext : DbContext
    {
        public EntityContext()
            : base("name=EntityContext")
        {
        }

        public virtual DbSet<AppConfiguration> AppConfigurations { get; set; }
        public virtual DbSet<AuthPermission> Permissions { get; set; }
        public virtual DbSet<AuthRole> Roles { get; set; }
        public virtual DbSet<DataHistory> Histories { get; set; }
        public virtual DbSet<AuthUser> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthPermission>()
                .HasMany(e => e.AuthRoles)
                .WithMany(e => e.AuthPermissions)
                .Map(m => m.ToTable("AuthRolePermission").MapLeftKey("PermissionId").MapRightKey("RoleId"));

            modelBuilder.Entity<AuthPermission>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Permissions)
                .Map(m => m.ToTable("AuthUserPermission").MapLeftKey("PermissionId").MapRightKey("UserId"));
            
            modelBuilder.Entity<AuthRole>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .Map(m => m.ToTable("AuthUserRole").MapLeftKey("RoleId").MapRightKey("UserId"));
            
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            var result = base.ValidateEntity(entityEntry, items);
            if (result.IsValid)
            {
                if (entityEntry.Entity is AuthUser)
                {
                    ValidateUser(entityEntry, ref result);
                }
                else if (entityEntry.Entity is AuthPermission)
                {
                    ValidatePermission(entityEntry, ref result);
                }
                else if (entityEntry.Entity is AuthRole)
                {
                    ValidateRole(entityEntry, ref result);
                }
            }
            return result;
        }

        private void ValidateRole(DbEntityEntry entityEntry, ref DbEntityValidationResult result)
        {
            var role = entityEntry.Entity as AuthRole;
            if (Roles.Any(p => p.Name == role.Name && p.Id != role.Id))
            {
                result.ValidationErrors.Add(
                        new DbValidationError(nameof(AuthRole.Name), ValidatorResource.Role_Duplicate_Name));
            }
        }

        private void ValidatePermission(DbEntityEntry entityEntry, ref DbEntityValidationResult result)
        {
            var permission = entityEntry.Entity as AuthPermission;
            if (Permissions.Any(p => p.Code == permission.Code && p.Id != permission.Id))
            {
                result.ValidationErrors.Add(
                        new DbValidationError(nameof(AuthPermission.Code), ValidatorResource.Permission_Duplicate_Code));
            }
            else if (Permissions.Any(p => p.Name == permission.Name && p.Id != permission.Id))
            {
                result.ValidationErrors.Add(
                      new DbValidationError(nameof(AuthPermission.Name), ValidatorResource.Permission_Duplicate_Name));
            }
        }

        private void ValidateUser(DbEntityEntry entityEntry, ref DbEntityValidationResult result)
        {
            var user = entityEntry.Entity as AuthUser;
            if (Users.Any(p => p.UserName == user.UserName && p.Id!=user.Id))
            {
                result.ValidationErrors.Add(
                        new DbValidationError(nameof(AuthUser.UserName), ValidatorResource.User_Duplicate_UserName));
            }
        }
    }
}
