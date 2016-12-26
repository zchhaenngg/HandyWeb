using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;
using HandyWork.Common.Attributes;
using HandyWork.ViewModel.Web;
using HandyWork.Common.Utility;

namespace HandyWork.UIBusiness.Utility
{
    public enum Urls
    {
        [MyUrl("Home","Index", "主页")]
        HomeIndex = 1,

        [MyUrl("Account", "UserIndex", "用户列表")]
        UserIndex = 2
    }
    
    

    public static class UrlUtility
    {
        public static IList<ActionLink> GetLinks4HomeIndex()
        {
            var list = new List<ActionLink>
            {
                new ActionLink("Modal", "Bootstrap", "弹出模态框")
            };
            for (int i = 0; i < 20; i++)
            {
                var link = MyUrlUtility.GetActionLink(Urls.UserIndex);
                if (link != null)
                {
                    link.HtmlAttributes = new { style = "display:none" };
                    list.Add(link);
                }
            }
            return list;
        }
    }
}
