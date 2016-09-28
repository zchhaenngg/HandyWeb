namespace HandyWork.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AuthPermission")]
    public partial class AuthPermission
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AuthPermission()
        {
            AuthRoles = new HashSet<AuthRole>();
            Users = new HashSet<AuthUser>();
        }

        [StringLength(40)]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(40)]
        public string CreatedById { get; set; }

        [StringLength(40)]
        public string LastModifiedById { get; set; }

        public DateTime? CreatedTime { get; set; }

        public DateTime? LastModifiedTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AuthRole> AuthRoles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AuthUser> Users { get; set; }
    }
}
