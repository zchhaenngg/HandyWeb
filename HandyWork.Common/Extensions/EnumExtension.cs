using HandyWork.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Extensions
{
    public static class EnumExtension
    {
        public static string ToLocalizedStr(this Enum value)
        {
            if (value == null)
            {
                return null;
            }
            string enumTypeName = value.GetType().Name;
            string resourceKey = string.Format("{0}_{1}", enumTypeName, value.ToString());
            return EnumResource.ResourceManager.GetString(resourceKey);
        }

    }
}
