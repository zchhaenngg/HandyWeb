using HandyWork.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Common.Extensions;
using System.Threading;

namespace HandyWork.Common.Helper
{
    public static class EnumHelper
    {
        public static string GetString<TEnum>(TEnum value)
        {
            var resourceKey = string.Format("{0}_{1}", typeof(TEnum).Name, value.ToString());
            var property = typeof(EnumResource).GetProperty(resourceKey);
            if (property != null)
            {
                return ((ResourceManager)property.GetValue(null, null)).GetString(resourceKey);
            }
            else
            {
                return null;
            }
        }

        public static string GetString<TEnum>(TEnum? value)
            where TEnum : struct
        {
            return value == null ? null : GetString(value.Value);
        }
    }
}
