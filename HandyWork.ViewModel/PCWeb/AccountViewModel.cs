using HandyWork.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.ViewModel.PCWeb
{
    public class LoginViewModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
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
        public string RealName { get; set; }

        [Display(Name = "联系电话")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Email { get; set; }
        /// <summary>
        /// 是否为域账户
        /// </summary>
        public bool IsDomain { get; set; }
        /// <summary>
        /// 直属领导
        /// </summary>
        public string LeaderId { get; set; }
    }

    //public class RegisterViewModel
    //{
    //    /// <summary>
    //    /// 用户名
    //    /// </summary>
    //    [Required]
    //    [Display(Name = "用户名")]
    //    [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
    //    public string UserName { get; set; }
    //    /// <summary>
    //    /// 密码
    //    /// </summary>
    //    [Required]
    //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "密码")]
    //    public string Password { get; set; }
    //    /// <summary>
    //    /// 确认密码
    //    /// </summary>
    //    [Required]
    //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "确认密码")]
    //    public string ConfirmPassword { get; set; }

    //    [Required]
    //    [Display(Name = "姓名")]
    //    [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
    //    public string RealName { get; set; }

    //    [Display(Name = "联系电话")]
    //    [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
    //    public string Phone { get; set; }
    //    /// <summary>
    //    /// 邮箱
    //    /// </summary>
    //    [Display(Name = "邮箱")]
    //    [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
    //    public string Email { get; set; }
    //    /// <summary>
    //    /// 是否为域账户
    //    /// </summary>
    //    public bool IsDomain { get; set; }
    //    /// <summary>
    //    /// 直属领导
    //    /// </summary>
    //    public string LeaderId { get; set; }
    //}

    //public class ResetPasswordViewModel
    //{
    //    [Required]
    //    [Display(Name = "UserName")]
    //    [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
    //    public string UserName { get; set; }
    //    [Required]
    //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Password")]
    //    public string Password { get; set; }
    //    [Required]
    //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Password")]
    //    public string ConfirmPassword { get; set; }
    //}

    /// <summary>
    /// 用户列表
    /// </summary>
    //public class UserViewModel
    //{
    //    public string Id { get; set; }
    //    public string RealName { get; set; }
    //    public string UserName { get; set; }
    //    public string Email { get; set; }
    //    public bool IsDomain { get; set; }
    //    public bool IsValid { get; set; }
    //    public bool IsLocked { get; set; }
    //    public string IsDomainStr
    //    {
    //        get
    //        {
    //            return IsDomain ? LocalizedResource.YES : LocalizedResource.NO;
    //        }
    //    }
    //    public string IsValidStr
    //    {
    //        get
    //        {
    //            return IsValid ? LocalizedResource.YES : LocalizedResource.NO;
    //        }
    //    }
    //    public string IsLockedStr
    //    {
    //        get
    //        {
    //            return IsLocked ? LocalizedResource.YES : LocalizedResource.NO;
    //        }
    //    }
    //}

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
