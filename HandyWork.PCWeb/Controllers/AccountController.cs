using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using HandyWork.UIBusiness.ViewModel.PCWeb;
using HandyWork.UIBusiness.Enums;
using System.Web.Security;
using System.Collections.Generic;

namespace HandyWork.PCWeb.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            SignInResult result = Store.AccountService.SignIn(model.UserName, model.Password);
            switch (result)
            {
                case SignInResult.Success:
                    return RedirectToLocal(returnUrl);
                case SignInResult.LockedOut:
                    ModelState.AddModelError("", " 账号已被锁定, 如有疑问请联系管理员");
                    break;
                case SignInResult.Invalid:
                    ModelState.AddModelError("", "该账号已被禁用, 如有疑问请联系管理员");
                    break;
                case SignInResult.UserNameError:
                    ModelState.AddModelError("", "用户名不存在");
                    break;
                case SignInResult.PasswordError:
                    ModelState.AddModelError("", "密码错误");
                    break;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToLocal();
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Store.AccountService.Register(model);
                if (HasErrorInfo)
                {
                    AddModelError(Store.ErrorInfos);
                    return View(model);
                }
                else
                {
                    return RedirectToLocal();
                }
            }
            return View(model);
        }
        public ActionResult ResetPassword(string code)
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Store.AccountService.ResetPassword(model);
            if (HasErrorInfo)
            {
                AddModelError(Store.ErrorInfos);
            }
            else
            {
                return RedirectToLocal();
            }
            return View(model);
        }

        public ActionResult Index()
        {
            ViewBag.IsSelectList = Store.SelectListService.IsSelectList();
            return View();
        }

        public ActionResult Create(string id)
        {
            ViewBag.IsSelectList = Store.SelectListService.IsSelectList(useOptionLable: true, defaultValue: true);
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Store.AccountService.Register(model);
                return base.GetJsonResultByErrorInfos();
            }
            return base.JsonResult4ModelState;
        }

        public ActionResult Edit(string id)
        {
            ViewBag.IsSelectList = Store.SelectListService.IsSelectList(useOptionLable: true, defaultValue: true);
            UpdateUserViewModel model = Store.AccountService.GetUpdateUserViewModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UpdateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Store.AccountService.UpdateUser(model);
                return base.GetJsonResultByErrorInfos();
            }
            return base.JsonResult4ModelState;
        }

        public ActionResult RoleIndex()
        {
            return View();
        }

        public ActionResult PermissionIndex()
        {
            return View();
        }

        public ActionResult CreatePermission()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePermission(PermissionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Store.AccountService.CreatePermission(model);
                return base.GetJsonResultByErrorInfos();
            }
            return base.JsonResult4ModelState;
        }

        public ActionResult EditPermission(string id)
        {
            PermissionViewModel model = Store.AccountService.GetPermissionViewModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPermission(PermissionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Store.AccountService.EditPermission(model);
                return base.GetJsonResultByErrorInfos();
            }
            return base.JsonResult4ModelState;
        }

        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                Store.AccountService.CreateRole(model);
                return base.GetJsonResultByErrorInfos();
            }
            return base.JsonResult4ModelState;
        }
        public ActionResult EditRole(string id)
        {
            RoleViewModel model = Store.AccountService.GetRoleViewModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                Store.AccountService.EditRole(model);
                return base.GetJsonResultByErrorInfos();
            }
            return base.JsonResult4ModelState;
        }

        public ActionResult RolePermissions(string id)
        {
            RoleViewModel model = Store.AccountService.GetRoleViewModel(id);
            return View(model);
        }
        public ActionResult UserPermissions(string id)
        {
            UpdateUserViewModel model = Store.AccountService.GetUpdateUserViewModel(id);
            return View(model);
        }
        public ActionResult UserRoles(string id)
        {
            UpdateUserViewModel model = Store.AccountService.GetUpdateUserViewModel(id);
            return View(model);
        }

        public ActionResult JsonDeleteRole(string id)
        {
            var tuple = Store.AccountService.DeleteRole(id);
            return base.GetJsonResult(tuple.Item1, tuple.Item2);
        }
        public ActionResult JsonGetUsers()
        {
            var tuple = Store.AccountService.GetPage4UserViewModel();
            return base.GetJsonResult(tuple.Item1, tuple.Item2);
        }
        public ActionResult JsonGetPermissions()
        {
            var tuple = Store.AccountService.GetPage4PermissionViewModel();
            return base.GetJsonResult(tuple.Item1, tuple.Item2);
        }
        public ActionResult JsonGetRoles()
        {
            var tuple = Store.AccountService.GetPage4RoleViewModel();
            return base.GetJsonResult(tuple.Item1, tuple.Item2);
        }
        public ActionResult JsonGetPermissionsByRoleId(string roleId)
        {
            string NameLike = (Request["NameLike"] ?? string.Empty).Trim();
            List<PermissionViewModel> list = Store.AccountService.GetPermissionViewModelsByRoleId(roleId, NameLike);
            return base.GetJsonResult(list, list.Count);
        }
        public ActionResult JsonGetPermissions4AddByRoleId(string roleId)
        {
            string NameLike = (Request["NameLike"] ?? string.Empty).Trim();
            List<PermissionViewModel> list = Store.AccountService.GetPermissionViewModels4AddByRoleId(roleId, NameLike);
            return base.GetJsonResult(list, list.Count);
        }
        public ActionResult JsonAddRolePermission(string roleId)
        {
            string permissionId = Request["permissionId"];
            Store.AccountService.AddRolePermission(roleId, permissionId);
            return base.GetJsonResultByErrorInfos();
        }
        public ActionResult JsonRemoveRolePermission(string roleId)
        {
            string permissionId = Request["permissionId"];
            Store.AccountService.RemoveRolePermission(roleId, permissionId);
            return base.GetJsonResultByErrorInfos();
        }

        public ActionResult JsonGetPermissionsByUserId(string userId)
        {
            string NameLike = (Request["NameLike"] ?? string.Empty).Trim();
            List<PermissionViewModel> list = Store.AccountService.GetPermissionViewModelsByUserId(userId, NameLike);
            return base.GetJsonResult(list, list.Count);
        }
        public ActionResult JsonGetPermissions4AddByUserId(string userId)
        {
            string NameLike = (Request["NameLike"] ?? string.Empty).Trim();
            List<PermissionViewModel> list = Store.AccountService.GetPermissionViewModels4AddByUserId(userId, NameLike);
            return base.GetJsonResult(list, list.Count);
        }
        public ActionResult JsonAddUserPermission(string userId)
        {
            string permissionId = Request["permissionId"];
            Store.AccountService.AddUserPermission(userId, permissionId);
            return base.GetJsonResultByErrorInfos();
        }
        public ActionResult JsonRemoveUserPermission(string userId)
        {
            string permissionId = Request["permissionId"];
            Store.AccountService.RemoveUserPermission(userId, permissionId);
            return base.GetJsonResultByErrorInfos();
        }

        public ActionResult JsonGetRolesByUserId(string userId)
        {

            List<RoleViewModel> list = Store.AccountService.GetRoleViewModelsByUserId(userId);
            return base.GetJsonResult(list, list.Count);
        }
        public ActionResult JsonGetRoles4AddByUserId(string userId)
        {
            List<RoleViewModel> list = Store.AccountService.GetRoleViewModels4AddByUserId(userId);
            return base.GetJsonResult(list, list.Count);
        }
        public ActionResult JsonAddUserRole(string userId, string roleId)
        {
            Store.AccountService.AddUserRole(userId, roleId);
            return base.GetJsonResultByErrorInfos();
        }
        public ActionResult JsonRemoveUserRole(string userId, string roleId)
        {
            Store.AccountService.RemoveUserRole(userId, roleId);
            return base.GetJsonResultByErrorInfos();
        }
        public ActionResult JsonSetUserValid(string userId)
        {
            string succeedMessage = Store.AccountService.SetUserValid(userId);
            return base.GetJsonResultByErrorInfos(succeedMessage);
        }
        public ActionResult JsonUnlockedById(string userId)
        {
            Store.AccountService.SetUnlocked4User(userId);
            return base.GetJsonResultByErrorInfos();
        }
    }
}