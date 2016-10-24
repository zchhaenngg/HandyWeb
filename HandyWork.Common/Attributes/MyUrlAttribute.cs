using HandyWork.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Attributes
{
    public class MyUrlAttribute : Attribute
    {
        public MyUrlAttribute(string controllerName, string actionName, string linkText)
        {
            Link = new ActionLink(actionName, controllerName, linkText);
        }
        public ActionLink Link { get; set; }

    }
}
