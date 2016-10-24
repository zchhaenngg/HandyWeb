using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace HandyWork.ViewModel.Web
{
    public class ActionLink
    {
        public ActionLink(string actionName, string controllerName, string linkText)
        {
            ActionName = actionName;
            ControllerName = controllerName;
            LinkText = linkText;
        }

        public string LinkText { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public object RouteValues { get; set; }
        public object HtmlAttributes { get; set; }
        
    }
}
