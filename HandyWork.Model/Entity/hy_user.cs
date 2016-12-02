namespace HandyWork.Model.Entity
{
    using Abstracts;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("hy_user")]
    public partial class hy_user : hy_Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hy_user()
        {
            hy_auth_permissions = new HashSet<hy_auth_permission>();
            hy_auth_roles = new HashSet<hy_auth_role>();
        }
        
        [Display(Name = "尝试失败次数")]
        public int access_failed_times { get; set; }
        
        /// <summary>
        /// 如Admin等账号是永远不被锁定的
        /// </summary>
        [Display(Name = "检查账号锁定")]
        public bool is_lockout_enable { get; set; }

        [Display(Name = "账号解锁时间")]
        public DateTime? lockout_end_time { get; set; }

        [Required]
        public string password_hash { get; set; }
        [Required]
        public string security_stamp { get; set; }

        [Display(Name = "联系电话")]
        [StringLength(250)]
        public string phone_number { get; set; }
        [Display(Name = "确认用户电话")]
        public bool phone_number_confirmed { get; set; }
        
        [Display(Name = "启用其他登陆方式")]
        public bool two_factor_enabled { get; set; }

        [Display(Name = "确认用户邮箱")]
        public bool email_confirmed { get; set; }
        
        [Index("user_name_index", IsUnique = true)]
        [Required]
        [StringLength(50)]
        public string user_name { get; set; }

        [Required]
        [StringLength(50)]
        public string nick_name { get; set; }

        [Index("email_index", IsUnique = true)]
        [StringLength(256)]
        public string email { get; set; }

        [Display(Name = "账号启用")]
        public bool is_valid { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<hy_auth_permission> hy_auth_permissions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<hy_auth_role> hy_auth_roles { get; set; }
    }
}
