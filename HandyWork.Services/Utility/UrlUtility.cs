using HandyWork.Common.Attributes;
using HandyWork.Common.Utility;
using HandyWork.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Services.Utility
{
    public static class UrlUtility
    {
        public enum Urls
        {
            [MyUrl("Home", "Index", "主页")]
            HomeIndex = 1,

            [MyUrl("Account", "UserIndex", "用户列表")]
            UserIndex = 2
        }

        public static IList<MenuLink> GetLinks4Menu()
        {
            var list = new List<ActionLink>
            {
                MyUrlUtility.GetActionLink(Urls.HomeIndex)
            };
            var link = MyUrlUtility.GetActionLink(Urls.UserIndex);
            if (link != null)
            {
                list.Add(link);
            }
            return list.ConvertAll(MenuLink.GetByActionLink);
        }
    }
}
