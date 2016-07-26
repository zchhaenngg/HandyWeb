using HandyWork.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HandyWork.PCWeb.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            string message = string.Empty;
            var model = this.ViewData.Model as HandleErrorInfo;
            if (model != null)
            {
                if (model.Exception != null)
                {
                    if (model.Exception.Message.Contains("Unable to open connection to"))
                    {
                        message = "该异常可能是由于服务器宕机等原因所致，请联系管理员";
                    }
                    else
                    {
                        message = model.Exception.Message;
                    }
                }
            }
            ViewBag.ErrorMessage = message;
            return View();
        }

        public ActionResult NotFound()
        {
            string message = string.Empty;
            var model = this.ViewData.Model as HandleErrorInfo;
            if (model != null)
            {
                if (model.Exception != null)
                {
                    message = model.Exception.Message;
                }
            }
            ViewBag.ErrorMessage = message;
            return View("NotFound");
        }

        public ActionResult AccessDenied()
        {
            string message = string.Empty;
            var model = this.ViewData.Model as HandleErrorInfo;
            if (model != null)
            {
                if (model.Exception != null)
                {
                    message = model.Exception.Message;
                }
            }
            ViewBag.ErrorMessage = message;
            return View("AccessDenied");
        }

        public ActionResult AjaxGlobalError()
        {
            var isEasyUIListPage = !string.IsNullOrWhiteSpace(Request["rows"]) && !string.IsNullOrWhiteSpace(Request["page"]);
            string message = string.Empty;
            var model = this.ViewData.Model as HandleErrorInfo;
            if (model != null)
            {
                if (model.Exception != null)
                {
                    if (model.Exception is UIAjaxException)
                    {
                        message = model.Exception.Message;
                    }
                    else if (model.Exception.Message.Contains("Unable to open connection to"))
                    {
                        message = "该异常可能是由于服务器宕机等原因所致，请联系管理员";
                    }
                    else
                    {
                        message = model.Exception.Message;
                    }
                }
            }
            if (Request.RequestType == "GET")
            {
                return Content("<h2>小伙伴出错啦...</h2><div>" + message + "</div>");
            }
            if (string.IsNullOrWhiteSpace(message))
            {
                message = "未知错误";
            }
            if (isEasyUIListPage)
            {
                List<string> emptyList = new List<string>();
                if (model != null && model.Exception != null && model.Exception is System.TimeoutException)
                {
                    return new JsonResult { ContentType = "text/html", Data = new { IsSuccess = false, Message = "查询内容过多，请缩小查询范围", rows = emptyList } };
                }
                if (message.ToUpper().StartsWith("Timeout expired".ToUpper()) || message.ToUpper().StartsWith("Unable to read data from the transport connection".ToUpper()))
                {
                    return new JsonResult { ContentType = "text/html", Data = new { IsSuccess = false, Message = "查询内容过多，请缩小查询范围", rows = emptyList } };
                }
                return new JsonResult { ContentType = "text/html", Data = new { IsSuccess = false, Message = message, rows = emptyList } };
            }
            return new JsonResult { ContentType = "text/html", Data = new { IsSuccess = false, Message = message } };
        }
    }
}