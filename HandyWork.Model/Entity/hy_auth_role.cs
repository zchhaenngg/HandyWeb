namespace HandyWork.Model.Entity
{
    using Abstracts;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hy_auth_role")]
    public partial class hy_auth_role : hy_Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hy_auth_role()
        {
            hy_auth_permissions = new HashSet<hy_auth_permission>();
            hy_users = new HashSet<hy_user>();
        }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [StringLength(500)]
        public string description { get; set; }
       
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hy_auth_permission> hy_auth_permissions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hy_user> hy_users { get; set; }
    }
}
