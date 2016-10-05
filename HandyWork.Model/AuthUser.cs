namespace HandyWork.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public partial class AuthUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AuthUser()
        {
            Permissions = new HashSet<AuthPermission>();
            Roles = new HashSet<AuthRole>();
        }

        [StringLength(40)]
        public string Id { get; set; }

        [Display(Name = "尝试失败次数")]
        public int AccessFailedCount { get; set; }
        
        [Display(Name = "检查账号解锁时间")]
        public bool LockoutEnabled { get; set; }

        [Display(Name = "账号解锁时间")]
        public DateTime? LockoutEndDateUtc { get; set; }

        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string SecurityStamp { get; set; }

        [StringLength(250)]
        public string PhoneNumber { get; set; }
        [Display(Name = "确认用户电话")]
        public bool PhoneNumberConfirmed { get; set; }
        
        [Display(Name = "启用其他登陆方式")]
        public bool TwoFactorEnabled { get; set; }

        [Display(Name = "确认用户邮箱")]
        public bool EmailConfirmed { get; set; }
        
        [Index("UserNameIndex", IsUnique = true)]
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string RealName { get; set; }

        [Index("EmailIndex", IsUnique = true)]
        [StringLength(256)]
        public string Email { get; set; }

        [Display(Name = "账号启用")]
        public bool IsValid { get; set; }
        
        [StringLength(40)]
        public string CreatedById { get; set; }

        [StringLength(40)]
        public string LastModifiedById { get; set; }

        public DateTime? CreatedTime { get; set; }

        public DateTime? LastModifiedTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<AuthPermission> Permissions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<AuthRole> Roles { get; set; }
    }
}
