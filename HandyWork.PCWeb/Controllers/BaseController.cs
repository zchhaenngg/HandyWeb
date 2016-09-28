using HandyWork.Common.Authority;
using HandyWork.Localization;
using HandyWork.UIBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

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
                    _unitOfManager = new UnitOfManager();
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

        protected void AddModelError()
        {
            foreach (var item in UnitOfManager.Errors)
            {
                ModelState.AddModelError("", item.ToString());
            }
        }

        protected bool HasErrorInfo
        {
            get
            {
                return UnitOfManager.Errors.Count > 0;
            }
        }

        protected string ErrorMessage
        {
            get
            {
                if (HasErrorInfo)
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (Error item in UnitOfManager.Errors)
                    {
                        builder.Append(item.ToString());
                    }
                    return builder.ToString();
                }
                return string.Empty;
            }
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

        protected JsonResult GetJsonResult(bool isSuccess, string message)
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

        protected JsonResult GetJsonResultByErrorInfos(string succeedMessage = null)
        {
            succeedMessage = string.IsNullOrWhiteSpace(succeedMessage) ? LocalizedResource.SUCCEED : succeedMessage;
            if (HasErrorInfo)
            {
                return GetJsonResult(false, ErrorMessage);
            }
            else
            {
                return GetJsonResult(true, succeedMessage);
            }
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
                return GetJsonResult(false, errorMsg);
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
        
    }
}