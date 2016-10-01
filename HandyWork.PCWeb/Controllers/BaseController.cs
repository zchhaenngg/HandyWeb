using HandyWork.Common.Authority;
using HandyWork.Localization;
using HandyWork.UIBusiness;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HandyWork.UIBusiness.Extensions;

namespace HandyWork.PCWeb.Controllers
{
    public abstract class BaseController : Controller
    {
        private UnitOfManager _unitOfManager;
        protected UnitOfManager UnitOfManager
        {
            get
            {
                if (_unitOfManager == null)
                {
                    _unitOfManager = new UnitOfManager(User.GetLoginId());
                }
                return _unitOfManager;
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_unitOfManager != null)
                {
                    _unitOfManager.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        
        protected ActionResult RedirectToLocal(string returnUrl = null)
        {
            if (returnUrl != null)
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        protected JsonResult GetJsonResult(string message, bool isSuccess = true)
        {
            return new JsonResult()
            {
                ContentType = "text/html",
                Data = new
                {
                    IsSuccess = isSuccess,
                    Message = message
                }
            };
        }
        
        protected JsonResult JsonResult4ModelState
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        builder.AppendLine(modelError.ErrorMessage);
                        if (modelError.Exception != null)
                        {
                            builder.AppendLine(modelError.Exception.Message);
                        }
                    }
                }
                string errorMsg = builder.ToString();
                return GetJsonResult(errorMsg, false);
            }
        }

        protected JsonResult GetJsonResult<T>(List<T> rows, int count, bool isSuccess = true)
        {
            return new JsonResult
            {
                Data = new
                {
                    IsSuccess = isSuccess,
                    rows = rows,
                    total = count
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        protected bool HasPermission(string permissionCode, string userId = null)
        {
            return HasPermission(permissionCode, userId);
        }

        public ActionResult RedirectToLocal<T>(Action<T> action, T model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    action(model);
                    return RedirectToLocal();
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
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult GetJsonResult<T>(Action<T> action, T model, string message)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    action(model);
                    return GetJsonResult(message);
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