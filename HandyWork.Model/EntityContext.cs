namespace HandyWork.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

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
        public virtual DbSet<User> Users { get; set; }

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
    }
}
