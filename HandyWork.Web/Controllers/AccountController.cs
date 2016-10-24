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
using HandyWork.UIBusiness.Controllers;
using HandyWork.UIBusiness.Manager;
using HandyWork.ViewModel.Web;
using HandyWork.UIBusiness.Enums;
using System.Collections.Generic;

namespace HandyWork.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        public AccountController()
        {
        }

        public ActionResult UserIndex()
        {
            var page = UnitOfManager.AccountManager.GetPage4UserViewModel();
            ViewBag.Count = page.Item2;
            return View(page.Item1);
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrWhiteSpace(returnUrl))
                {
                    return RedirectToAction("index", "home");
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 这不会计入到为执行帐户锁定而统计的登录失败次数中
            // 若要在多次输入错误密码的情况下触发帐户锁定，请更改为 shouldLockout: true
            var result = UnitOfManager.OwinManager.SignIn(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInResult.Success:
                   
                    return RedirectToLocal(returnUrl);
                case SignInResult.LockedOut:
                    return View("Lockout");
                case SignInResult.UserNameError:
                    ModelState.AddModelError("", Localization.LocalizedResource.NOTEXIST_USERNAME);
                    return View(model);
                case SignInResult.PasswordError:
                    ModelState.AddModelError("", "密码不正确");
                    return View(model);
                case SignInResult.Invalid:
                    ModelState.AddModelError("", "账号已禁用");
                    return View(model);
                case SignInResult.SuccessRehashNeeded:
                    return View("ResetPassword");
                default:
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // 要求用户已通过使用用户名/密码或外部登录名登录
            //if (!await SignInManager.HasBeenVerifiedAsync())
            //{
            //    return View("Error");
            //}
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 以下代码可以防范双重身份验证代码遭到暴力破解攻击。
            // 如果用户输入错误代码的次数达到指定的次数，则会将
            // 该用户帐户锁定指定的时间。
            // 可以在 IdentityConfig 中配置帐户锁定设置
            //var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(model.ReturnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "代码无效。");
            //        return View(model);
            //}
            throw new NotImplementedException();
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            return RedirectToLocal(model, () => 
            {
                var user = new OwinViewModel();
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.RealName = model.RealName;
                UnitOfManager.OwinManager.Register(user, model.Password);
                
            },"/Account/Login");
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            //var result = await UserManager.ConfirmEmailAsync(userId, code);
            //return View(result.Succeeded ? "ConfirmEmail" : "Error");
            throw new NotImplementedException();
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = await UserManager.FindByNameAsync(model.Email);
                //if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                //{
                //    // 请不要显示该用户不存在或者未经确认
                //    return View("ForgotPasswordConfirmation");
                //}

                // 有关如何启用帐户确认和密码重置的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=320771
                // 发送包含此链接的电子邮件
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "重置密码", "请通过单击 <a href=\"" + callbackUrl + "\">此处</a>来重置你的密码");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //var user = await UserManager.FindByNameAsync(model.Email);
            //if (user == null)
            //{
            //    // 请不要显示该用户不存在
            //    return RedirectToAction("ResetPasswordConfirmation", "Account");
            //}
            //var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            //if (result.Succeeded)
            //{
            //    return RedirectToAction("ResetPasswordConfirmation", "Account");
            //}
            //AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // 请求重定向到外部登录提供程序
            //return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
            throw new NotImplementedException();
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            //var userId = await SignInManager.GetVerifiedUserIdAsync();
            //if (userId == null)
            //{
            //    return View("Error");
            //}
            //var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            //var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            //return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
            throw new NotImplementedException();
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}

            //// 生成令牌并发送该令牌
            //if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            //{
            //    return View("Error");
            //}
            //return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
            throw new NotImplementedException();
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            //var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            //if (loginInfo == null)
            //{
            //    return RedirectToAction("Login");
            //}

            //// 如果用户已具有登录名，则使用此外部登录提供程序将该用户登录
            //var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
            //    case SignInStatus.Failure:
            //    default:
            //        // 如果用户没有帐户，则提示该用户创建帐户
            //        ViewBag.ReturnUrl = returnUrl;
            //        ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
            //        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            //}
            throw new NotImplementedException();
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            throw new NotImplementedException();
            //if (User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Index", "Manage");
            //}

            //if (ModelState.IsValid)
            //{
            //    // 从外部登录提供程序获取有关用户的信息
            //    var info = await AuthenticationManager.GetExternalLoginInfoAsync();
            //    if (info == null)
            //    {
            //        return View("ExternalLoginFailure");
            //    }
            //    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            //    var result = await UserManager.CreateAsync(user);
            //    if (result.Succeeded)
            //    {
            //        result = await UserManager.AddLoginAsync(user.Id, info.Login);
            //        if (result.Succeeded)
            //        {
            //            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            //            return RedirectToLocal(returnUrl);
            //        }
            //    }
            //    AddErrors(result);
            //}

            //ViewBag.ReturnUrl = returnUrl;
            //return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            UnitOfManager.OwinManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }
    }
}