using HandyWork.Common.Model;
using HandyWork.PCWeb.Controllers;
using HandyWork.UIBusiness;
using HandyWork.UIBusiness.Manager;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace HandyWork.PCWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            LodingAssembly();
            SetupLogConfig();

            LogHelper.Log.Info("****Web应用-日志正常启动****");
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_AuthorizeRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;

            if (app.Context.User is GenericPrincipal)
            {
                return;
            }
            else//此时app.Context.User被转成了RolePrincipal，在MVC2开始的，所以要改回来
            {
                //获取身份验证的cookie
                addContextUser(app);
            }
        }

        void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;

            addContextUser(app);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                var httpContext = ((MvcApplication)sender).Context;
                Exception error = Server.GetLastError();

                string action = "Index";
                if (error is DbEntityValidationException)
                {
                    DbEntityValidationException ex = error as DbEntityValidationException;
                    LogHelper.ErrorLog.Error(ex.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage, ex);
                }
                else if (error is HttpException)
                {
                    var httpEx = error as HttpException;
                    switch (httpEx.GetHttpCode())
                    {
                        case 404:
                            action = "NotFound";
                            break;

                        case 401:
                            action = "AccessDenied";
                            break;
                    }
                }
                else
                {
                    if ((error as LogException)?.ShouldWriteLog != false)
                    {//左边表达式结果有  null,true,false.其中null和true都需要记录日志
                        LogHelper.ErrorLog.Error(error.Message, error);
                    }
                    if (httpContext.Request["is_ajax_call"] == "yes" || httpContext.Request.Headers["X-Requested-With"] != null && httpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        action = "AjaxGlobalError";
                    }
                }

                Server.ClearError();
                httpContext.ClearError();
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                httpContext.Response.TrySkipIisCustomErrors = true;
                var routeData = new RouteData();
                routeData.Values["controller"] = "Error";
                routeData.Values["action"] = action;

                var tuple = getTuple4ControllerAction(httpContext);
                var controller = new ErrorController();
                controller.ViewData.Model = new HandleErrorInfo(error, tuple.Item1, tuple.Item2);
                ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
                ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
            }
            catch (Exception ee)
            {
                LogHelper.ErrorLog.Error(ee.Message, ee);
            }
        }

        private Tuple<string, string> getTuple4ControllerAction(HttpContext httpContext)
        {
            string currentController = string.Empty;
            string currentAction = string.Empty;
            var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));
            if (currentRouteData != null)
            {
                var controllerName = currentRouteData.Values["controller"];
                var actionName = currentRouteData.Values["action"];
                if (controllerName != null)
                {
                    currentController = controllerName.ToString();
                }

                if (actionName != null)
                {
                    currentAction = actionName.ToString();
                }
            }
            return new Tuple<string, string>(currentController, currentAction);
        }

        private void addContextUser(HttpApplication app)
        {
            //获取身份验证的cookie
            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                string encryptedTicket = cookie.Value;
                //解密cookie中的票据信息
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(encryptedTicket);
                //创建用户标识
                FormsIdentity identity = new FormsIdentity(ticket);
                //var oldUser = app.Context.User;
                app.Context.User = new GatherPrincipal(identity);
            }
        }

        private void LodingAssembly()
        {
            //string path = Server.MapPath("log4net.dll");
            //Assembly.LoadFile(path);
        }
        private void SetupLogConfig()
        {
            string configFile = Server.MapPath("/App_Data/log4net.config");
            if (File.Exists(configFile))
            {
                XmlConfigurator.Configure(new FileInfo(configFile));
            }
        }
    }
}
