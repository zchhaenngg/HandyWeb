using HandyWork.UIBusiness.IManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HandyWork.PCWeb.Controllers
{
    public class BusinessController : BaseController
    {
        public IAccountManager AccountManager { get; }
        public ISelectListManager SelectListManager { get; }
    }
}