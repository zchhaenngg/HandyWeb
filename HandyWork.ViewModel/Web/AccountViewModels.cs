using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.ViewModel.Web
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "代码")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "记住此浏览器?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
    }
   
    public class LoginViewModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
        public int GreaterThanUTCInMinute { set; get; }
    }
    public class UpdateUserViewModel
    {
        public string Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [Display(Name = "用户名")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "姓名")]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string NickName { get; set; }

        [Display(Name = "联系电话")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Email { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [Display(Name = "昵称")]
        public string NickName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
    }

    public class AuthUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "账号锁定")]
        public bool IsLocked { get; set; }

        [Display(Name = "账号解锁时间")]
        public DateTime? UnLockTime { get; set; }

        [Display(Name = "联系电话")]
        public string PhoneNumber { get; set; }

        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Display(Name = "姓名")]
        public string NickName { get; set; }

        [Display(Name = "邮箱")]
        public string Email { get; set; }
        [Display(Name = "邮箱确认")]
        public bool EmailConfirmed { get; set; }
    }

    public class PermissionViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "权限代码")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Code { get; set; }
        [Required]
        [Display(Name = "权限名称")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Name { get; set; }
        [Display(Name = "备注")]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string Description { get; set; }
    }

    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "角色名称")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Name { get; set; }
        [Display(Name = "备注")]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string Description { get; set; }
    }
}
