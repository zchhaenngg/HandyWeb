namespace HandyWork.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            AuthPermissions = new HashSet<AuthPermission>();
            AuthRoles = new HashSet<AuthRole>();
        }

        [StringLength(40)]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string RealName { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(250)]
        public string Phone { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public bool IsDomain { get; set; }

        public bool IsValid { get; set; }

        public bool IsLocked { get; set; }

        public int LoginFailedCount { get; set; }

        public DateTime? LastLoginFailedTime { get; set; }

        [StringLength(40)]
        public string CreatedById { get; set; }

        [StringLength(40)]
        public string LastModifiedById { get; set; }

        public DateTime? CreatedTime { get; set; }

        public DateTime? LastModifiedTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AuthPermission> AuthPermissions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AuthRole> AuthRoles { get; set; }
    }
}
