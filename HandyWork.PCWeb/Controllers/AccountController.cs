using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HandyWork.UIBusiness.Enums;
using System.Web.Security;
using System.Collections.Generic;
using HandyWork.UIBusiness.Manager.Utility;
using HandyWork.ViewModel.PCWeb;
using System.Data.Entity.Validation;

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
            
            SignInResult result = UnitOfManager.AccountManager.SignIn(model.UserName, model.Password, model.GreaterThanUTCInMinute);
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
            return RedirectToLocal(UnitOfManager.AccountManager.Register, model);
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
            return RedirectToLocal(UnitOfManager.AccountManager.ResetPassword, model);
        }

        public ActionResult Index()
        {
            ViewBag.IsSelectList = SelectListUtility.IsSelectList();
            return View();
        }

        public ActionResult Create(string id)
        {
            ViewBag.IsSelectList = SelectListUtility.IsSelectList(useOptionLable: true, defaultValue: true);
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UnitOfManager.AccountManager.Register(model);
                return base.GetJsonResult("注册成功!");
            }
            return base.JsonResult4ModelState;
        }

        public ActionResult Edit(string id)
        {
            ViewBag.IsSelectList = SelectListUtility.IsSelectList(useOptionLable: true, defaultValue: true);
            UpdateUserViewModel model = UnitOfManager.AccountManager.GetUpdateUserViewModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UpdateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                UnitOfManager.AccountManager.UpdateUser(model);
                return base.GetJsonResult("更新成功");
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
            return GetJsonResult(UnitOfManager.AccountManager.CreatePermission, model, "操作成功");
        }

        public ActionResult EditPermission(string id)
        {
            PermissionViewModel model = UnitOfManager.AccountManager.GetPermissionViewModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPermission(PermissionViewModel model)
        {
            return GetJsonResult(UnitOfManager.AccountManager.EditPermission, model, "操作成功");
        }

        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(RoleViewModel model)
        {
            return GetJsonResult(UnitOfManager.AccountManager.CreateRole, model, "操作成功");
        }
        public ActionResult EditRole(string id)
        {
            RoleViewModel model = UnitOfManager.AccountManager.GetRoleViewModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(RoleViewModel model)
        {
            return GetJsonResult(UnitOfManager.AccountManager.EditRole, model, "操作成功");
        }

        public ActionResult RolePermissions(string id)
        {
            RoleViewModel model = UnitOfManager.AccountManager.GetRoleViewModel(id);
            return View(model);
        }
        public ActionResult UserPermissions(string id)
        {
            UpdateUserViewModel model = UnitOfManager.AccountManager.GetUpdateUserViewModel(id);
            return View(model);
        }
        public ActionResult UserRoles(string id)
        {
            UpdateUserViewModel model = UnitOfManager.AccountManager.GetUpdateUserViewModel(id);
            return View(model);
        }

        public ActionResult JsonDeleteRole(string id)
        {
            var tuple = UnitOfManager.AccountManager.DeleteRole(id);
            return base.GetJsonResult(tuple.Item2, tuple.Item1);
        }
        public ActionResult JsonGetUsers()
        {
            var tuple = UnitOfManager.AccountManager.GetPage4UserViewModel();
            return base.GetJsonResult(tuple.Item1, tuple.Item2);
        }
        public ActionResult JsonGetPermissions()
        {
            var tuple = UnitOfManager.AccountManager.GetPage4PermissionViewModel();
            return base.GetJsonResult(tuple.Item1, tuple.Item2);
        }
        public ActionResult JsonGetRoles()
        {
            var tuple = UnitOfManager.AccountManager.GetPage4RoleViewModel();
            return base.GetJsonResult(tuple.Item1, tuple.Item2);
        }
        public ActionResult JsonGetPermissionsByRoleId(string roleId)
        {
            string NameLike = (Request["NameLike"] ?? string.Empty).Trim();
            List<PermissionViewModel> list = UnitOfManager.AccountManager.GetPermissionViewModelsByRoleId(roleId, NameLike);
            return base.GetJsonResult(list, list.Count);
        }
        public ActionResult JsonGetPermissions4AddByRoleId(string roleId)
        {
            string NameLike = (Request["NameLike"] ?? string.Empty).Trim();
            List<PermissionViewModel> list = UnitOfManager.AccountManager.GetPermissionViewModels4AddByRoleId(roleId, NameLike);
            return base.GetJsonResult(list, list.Count);
        }
        public ActionResult JsonAddRolePermission(string roleId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string permissionId = Request["permissionId"];
                    UnitOfManager.AccountManager.AddRolePermission(roleId, permissionId);
                    return GetJsonResult("操作成功");
                }
                catch (DbEntityValidationException dbex)
                {
                    foreach (var error in dbex.EntityValidationErrors.First().ValidationErrors)
                    {
                        ModelState.AddModelError(error.PropertyName, string.Format("Entity Property: {0}, Error: {1}", error.PropertyName, error.ErrorMessage));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                return JsonResult4ModelState;
            }
            return JsonResult4ModelState;
        }
        public ActionResult JsonRemoveRolePermission(string roleId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string permissionId = Request["permissionId"];
                    UnitOfManager.AccountManager.RemoveRolePermission(roleId, permissionId);
                    return GetJsonResult("操作成功");
                }
                catch (DbEntityValidationException dbex)
                {
                    foreach (var error in dbex.EntityValidationErrors.First().ValidationErrors)
                    {
                        ModelState.AddModelError(error.PropertyName, string.Format("Entity Property: {0}, Error: {1}", error.PropertyName, error.ErrorMessage));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                return JsonResult4ModelState;
            }
            return JsonResult4ModelState;
        }

        public ActionResult JsonGetPermissionsByUserId(string userId)
        {
            string NameLike = (Request["NameLike"] ?? string.Empty).Trim();
            List<PermissionViewModel> list = UnitOfManager.AccountManager.GetPermissionViewModelsByUserId(userId, NameLike);
            return base.GetJsonResult(list, list.Count);
        }
        public ActionResult JsonGetPermissions4AddByUserId(string userId)
        {
            string NameLike = (Request["NameLike"] ?? string.Empty).Trim();
            List<PermissionViewModel> list = UnitOfManager.AccountManager.GetPermissionViewModels4AddByUserId(userId, NameLike);
            return base.GetJsonResult(list, list.Count);
        }
        public ActionResult JsonAddUserPermission(string userId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string permissionId = Request["permissionId"];
                    UnitOfManager.AccountManager.AddUserPermission(userId, permissionId);
                    return GetJsonResult("操作成功");
                }
                catch (DbEntityValidationException dbex)
                {
                    foreach (var error in dbex.EntityValidationErrors.First().ValidationErrors)
                    {
                        ModelState.AddModelError(error.PropertyName, string.Format("Entity Property: {0}, Error: {1}", error.PropertyName, error.ErrorMessage));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                return JsonResult4ModelState;
            }
            return JsonResult4ModelState;
        }
        public ActionResult JsonRemoveUserPermission(string userId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string permissionId = Request["permissionId"];
                    UnitOfManager.AccountManager.RemoveUserPermission(userId, permissionId);
                    return GetJsonResult("操作成功");
                }
                catch (DbEntityValidationException dbex)
                {
                    foreach (var error in dbex.EntityValidationErrors.First().ValidationErrors)
                    {
                        ModelState.AddModelError(error.PropertyName, string.Format("Entity Property: {0}, Error: {1}", error.PropertyName, error.ErrorMessage));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                return JsonResult4ModelState;
            }
            return JsonResult4ModelState;
        }

        public ActionResult JsonGetRolesByUserId(string userId)
        {
            List<RoleViewModel> list = UnitOfManager.AccountManager.GetRoleViewModelsByUserId(userId);
            return base.GetJsonResult(list, list.Count);
        }
        public ActionResult JsonGetRoles4AddByUserId(string userId)
        {
            List<RoleViewModel> list = UnitOfManager.AccountManager.GetRoleViewModels4AddByUserId(userId);
            return base.GetJsonResult(list, list.Count);
        }
        public ActionResult JsonAddUserRole(string userId, string roleId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UnitOfManager.AccountManager.AddUserRole(userId, roleId);
                    return GetJsonResult("操作成功");
                }
                catch (DbEntityValidationException dbex)
                {
                    foreach (var error in dbex.EntityValidationErrors.First().ValidationErrors)
                    {
                        ModelState.AddModelError(error.PropertyName, string.Format("Entity Property: {0}, Error: {1}", error.PropertyName, error.ErrorMessage));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                return JsonResult4ModelState;
            }
            return JsonResult4ModelState;
        }
        public ActionResult JsonRemoveUserRole(string userId, string roleId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UnitOfManager.AccountManager.RemoveUserRole(userId, roleId);
                    return GetJsonResult("操作成功");
                }
                catch (DbEntityValidationException dbex)
                {
                    foreach (var error in dbex.EntityValidationErrors.First().ValidationErrors)
                    {
                        ModelState.AddModelError(error.PropertyName, string.Format("Entity Property: {0}, Error: {1}", error.PropertyName, error.ErrorMessage));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                return JsonResult4ModelState;
            }
            return JsonResult4ModelState;
        }
        public ActionResult JsonSetUserValid(string userId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string succeedMessage = UnitOfManager.AccountManager.SetUserValid(userId);
                    return GetJsonResult(succeedMessage);
                }
                catch (DbEntityValidationException dbex)
                {
                    foreach (var error in dbex.EntityValidationErrors.First().ValidationErrors)
                    {
                        ModelState.AddModelError(error.PropertyName, string.Format("Entity Property: {0}, Error: {1}", error.PropertyName, error.ErrorMessage));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                return JsonResult4ModelState;
            }
            return JsonResult4ModelState;
        }
        public ActionResult JsonUnlockedById(string userId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UnitOfManager.AccountManager.SetUnlocked4User(userId);
                    return GetJsonResult("操作成功");
                }
                catch (DbEntityValidationException dbex)
                {
                    foreach (var error in dbex.EntityValidationErrors.First().ValidationErrors)
                    {
                        ModelState.AddModelError(error.PropertyName, string.Format("Entity Property: {0}, Error: {1}", error.PropertyName, error.ErrorMessage));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                return JsonResult4ModelState;
            }
            return JsonResult4ModelState;
        }
    }
}