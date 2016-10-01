namespace HandyWork.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Infrastructure;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;

    public partial class EntityContext : DbContext
    {
        public EntityContext()
            : base("name=EntityContext")
        {
        }

        public virtual DbSet<AppConfiguration> AppConfigurations { get; set; }
        public virtual DbSet<AuthPermission> AuthPermissions { get; set; }
        public virtual DbSet<AuthRole> AuthRoles { get; set; }
        public virtual DbSet<DataHistory> DataHistories { get; set; }
        public virtual DbSet<AuthUser> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthPermission>()
                .HasMany(e => e.AuthRoles)
                .WithMany(e => e.AuthPermissions)
                .Map(m => m.ToTable("AuthRolePermission").MapLeftKey("PermissionId").MapRightKey("RoleId"));

            modelBuilder.Entity<AuthPermission>()
                .HasMany(e => e.Users)
                .WithMany(e => e.AuthPermissions)
                .Map(m => m.ToTable("AuthUserPermission").MapLeftKey("PermissionId").MapRightKey("UserId"));
            
            modelBuilder.Entity<AuthRole>()
                .HasMany(e => e.Users)
                .WithMany(e => e.AuthRoles)
                .Map(m => m.ToTable("AuthUserRole").MapLeftKey("RoleId").MapRightKey("UserId"));
            
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            var result = base.ValidateEntity(entityEntry, items);
            if (result.IsValid)
            {
                if (entityEntry.Entity is AuthUser && entityEntry.State == EntityState.Added)
                {
                    ValidateUser(entityEntry, ref result);
                }
            }
            return result;
        }

        private void ValidateUser(DbEntityEntry entityEntry, ref DbEntityValidationResult result)
        {
            AuthUser user = entityEntry.Entity as AuthUser;
            if (Users.Any(p => p.UserName == user.UserName))
            {
                result.ValidationErrors.Add(
                        new DbValidationError("UserName", "UserName must be unique."));
            }
        }
    }
}
