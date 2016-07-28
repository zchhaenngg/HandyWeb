using HandyWork.Localization;
using HandyWork.UIBusiness.IManager;
using HandyWork.UIBusiness.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HandyWork.UIBusiness.Manager
{
    public class SelectListManager : BaseManager, ISelectListManager
    {
        public const string TRUE = "True";
        public const string FALSE = "False";

        public SelectListManager():base(null) { }

        public SelectListManager(ManagerStore store)
            : base(store)
        {
        }

        #region 枚举
        internal IEnumerable<SelectListItem> GetSelectListItems<TEnum>()
        {
            Type resourceType = typeof(EnumResource);
            bool isEnum = EnumHelper.IsEnum<TEnum>();

            if (!isEnum)
                throw new ArgumentException("TEnum must be an enumerated type");

            IEnumerable<SelectListItem> values = EnumHelper.GetEnumValues<TEnum>(resourceType).Select(x =>
                new SelectListItem
                {
                    Text = x.Value,
                    Value = Convert.ToInt32(x.Key).ToString()
                }
                );

            return values;
        }

        public SelectList GetSelectList<TEnum>(bool useOptionLabel = true, object defaultValue = null, string[] specifiedValues = null)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in GetSelectListItems<TEnum>())
            {
                if (specifiedValues == null)
                {
                    items.Add(item);
                }
                else
                {
                    if (specifiedValues.Contains(item.Value))
                    {
                        items.Add(item);
                    }
                }
            }
            if (useOptionLabel)
            {
                items.Insert(0, new SelectListItem() { Value = null, Text = LocalizedResource.DROP_DOWN_LIST_OPTION_LABEL });
            }
            return new SelectList(items, "Value", "Text", selectedValue: defaultValue);
        }
        #endregion

        public SelectList IsSelectList(bool useOptionLable = true, Nullable<bool> defaultValue = null, string trueValue = null, string falseValue = null)
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem(){ Value = TRUE, Text = trueValue ?? LocalizedResource.YES },
                new SelectListItem(){ Value = FALSE, Text = falseValue ?? LocalizedResource.NO }
            };
            if (useOptionLable)
            {
                items.Insert(0, new SelectListItem() { Value = null, Text = LocalizedResource.DROP_DOWN_LIST_OPTION_LABEL });
            }
            return new SelectList(items, "Value", "Text", selectedValue: defaultValue);
        }
    }
}
