using HandyWork.Common.Attributes;
using HandyWork.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Utility
{
    public static class MyUrlUtility
    {
        public static MyUrlAttribute GetMyUrlAttribute(Enum url)
        {
            Type type = url.GetType();
            var memInfo = type.GetMember(url.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(MyUrlAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return (MyUrlAttribute)attrs[0];
                }
            }
            return null;
        }
        /// <summary>
        /// 返回url地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static ActionLink GetActionLink(Enum url)
        {
            var myurl = GetMyUrlAttribute(url);
            return myurl == null ? null : myurl.Link;
        }
    }
}
