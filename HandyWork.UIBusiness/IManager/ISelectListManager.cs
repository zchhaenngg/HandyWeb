using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HandyWork.UIBusiness.IManager
{
    public interface ISelectListManager
    {
        SelectList GetSelectList<TEnum>(bool useOptionLabel = true, object defaultValue = null, string[] specifiedValues = null);
        SelectList IsSelectList(bool useOptionLable = true, Nullable<bool> defaultValue = null, string trueValue = null, string falseValue = null);
    }
}
