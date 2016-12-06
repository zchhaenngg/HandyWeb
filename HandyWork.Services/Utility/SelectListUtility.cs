using HandyWork.Localization;
using HandyWork.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HandyWork.Common.Extensions;
using HandyWork.Common.Helper;

namespace HandyWork.Services.Utility
{
    public static class SelectListUtility
    {
        #region 枚举
        internal static IEnumerable<SelectListItem> GetSelectListItems<TEnum>()
            where TEnum : struct
        {
            return from TEnum e in Enum.GetValues(typeof(TEnum))
                       select new SelectListItem
                       {
                           Value = Convert.ToInt32(e).ToString(),
                           Text = EnumHelper.GetString(e)
                       };
        }

        public static SelectList GetSelectList<TEnum>(bool useOptionLabel = true, object defaultValue = null, string[] specifiedValues = null)
            where TEnum : struct
        {
            var items = GetSelectListItems<TEnum>().Where(o => specifiedValues == null || specifiedValues.Contains(o.Value)).ToList();
            if (useOptionLabel)
            {
                items.Insert(0, new SelectListItem() { Value = null, Text = LocalizedResource.DROP_DOWN_LIST_OPTION_LABEL });
            }
            return new SelectList(items, "Value", "Text", selectedValue: defaultValue);
        }
        #endregion

        #region Is
        public static SelectList IsSelectList(bool useOptionLable = true, bool? defaultValue = null, string trueValue = null, string falseValue = null)
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem(){ Value = true.ToString(), Text = trueValue ?? LocalizedResource.YES },
                new SelectListItem(){ Value = false.ToString(), Text = falseValue ?? LocalizedResource.NO }
            };
            if (useOptionLable)
            {
                items.Insert(0, new SelectListItem { Value = null, Text = LocalizedResource.DROP_DOWN_LIST_OPTION_LABEL });
            }
            return new SelectList(items, "Value", "Text", selectedValue: defaultValue);
        }
        #endregion
    }
}
