using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HandyWork.Common.Authority;
using HandyWork.UIBusiness.Extensions;
using System.Data.Entity.Validation;

namespace HandyWork.UIBusiness.Controllers
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
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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
    }
}
