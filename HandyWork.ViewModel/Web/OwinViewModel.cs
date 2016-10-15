using HandyWork.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.ViewModel.Web
{
    public class OwinViewModel
    {
        public OwinViewModel() { }

        public string Id { get; set; }

        [Display(Name = "尝试失败次数")]
        public int AccessFailedCount { get; set; }

        [Display(Name = "检查账号解锁时间")]
        public bool LockoutEnabled { get; set; }

        [Display(Name = "账号解锁时间")]
        public DateTime? LockoutEndDateUtc { get; set; }

        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }
        [Display(Name = "确认用户电话")]
        public bool PhoneNumberConfirmed { get; set; }

        [Display(Name = "启用其他登陆方式")]
        public bool TwoFactorEnabled { get; set; }

        [Display(Name = "确认用户邮箱")]
        public bool EmailConfirmed { get; set; }

        public string UserName { get; set; }

        public string RealName { get; set; }

        public string Email { get; set; }

        [Display(Name = "账号启用")]
        public bool IsValid { get; set; }

        public string CreatedById { get; set; }

        public string LastModifiedById { get; set; }

        public DateTime? CreatedTime { get; set; }

        public DateTime? LastModifiedTime { get; set; }
    }
}
