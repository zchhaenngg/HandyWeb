using HandyWork.Services.Service;
using HandyWork.Services.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HandyWork.Common.Extensions;
using HandyWork.ViewModel.Common;
using HandyWork.Services.Extensions;

namespace HandyWork.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        private IAccountService _accountService;
        private IOWinService _oWinService;

        protected IAccountService AccountService => _accountService ?? (_accountService = new AccountService(User.GetLoginId()));
        protected IOWinService OwinService => _oWinService ?? (_oWinService = new OWinService());

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (returnUrl != null)
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        protected ActionResult RedirectToLocal<T>(T model, Action action, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                action();
                return RedirectToLocal(returnUrl);
            }
            return View(model);
        }
        protected JsonResult Json(Func<JsonViewModel> function)
        {
            if (ModelState.IsValid)
            {
                return Json(function());
            }
            return Json(new JsonViewModel
            {
                IsSuccess = false,
                Message = GetModelStateString()
            });
        }
        protected JsonResult Json<T>(Func<JsonPageViewModel<T>> function)
        {
            if (ModelState.IsValid)
            {
                return Json(function());
            }
            return Json(new JsonPageViewModel<T>
            {
                rows = new List<T>(),
                total = 0
            });
        }

        private string GetModelStateString()
        {
            return string.Join("<br/>", ModelState.Select(o => o.Value).SelectMany(o => o.Errors).Select(o => o.ErrorMessage + o.Exception?.Message));
        }
    }
}