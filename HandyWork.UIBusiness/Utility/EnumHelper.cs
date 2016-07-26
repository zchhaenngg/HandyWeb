using HandyWork.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UIBusiness.Utility
{
    public static class EnumHelper
    {
        /// <summary>
        /// Returns a dictionary that contains the description of the enum
        /// Key: enum value
        /// Value: enum value description
        /// </summary>
        /// <typeparam name="TEnum">Type of the enum</typeparam>
        /// <param name="resourceType">Type of the Resource</param>
        /// <returns>A dictionary that contains the description of the enum</returns>
        public static Dictionary<TEnum, string> GetEnumValues<TEnum>(Type resourceType = null)
        {
            bool isEnum = IsEnum<TEnum>();

            if (!isEnum)
                throw new ArgumentException("T must be an enumerated type");

            var enumDictionary = new Dictionary<TEnum, string>();

            Type enumType = GetUnderlyingType<TEnum>();
            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();

            foreach (var value in values)
            {
                string localizedString = GetLocalizedString<TEnum>(value);
                enumDictionary.Add(value, localizedString);
            }

            return enumDictionary;
        }

        /// <summary>
        /// Returns true if a type is an enumerable 
        /// </summary>
        /// <typeparam name="T">Generic type argument</typeparam>
        /// <returns></returns>
        public static bool IsEnum<T>()
        {
            return GetUnderlyingType<T>().IsEnum;
        }

        /// <summary>
        /// Returns the underlying type of the specified nullable type
        /// </summary>
        /// <typeparam name="T">The underlying value type</typeparam>
        public static Type GetUnderlyingType<T>()
        {
            Type realModelType = typeof(T);
            Type underlyingType = Nullable.GetUnderlyingType(realModelType);

            if (underlyingType != null)
                return underlyingType;

            return realModelType;
        }
        
        /// <summary>
        /// Gets the localized string for an enum value. 
        /// Format of the resource key: "EnumType.EnumValue".
        /// <example>
        ///	For enum value <c>DayOfWeek.Sunday</c>, the resource key will be "DayOfWeek.Sunday"
        /// </example>
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="value">The enum value.</param>
        /// <returns></returns>
        public static string GetLocalizedString<TEnum>(TEnum value, Type resourceType = null)
        {
            if (resourceType == null)
            {
                resourceType = typeof(EnumResource);
            }

            Type type = GetUnderlyingType<TEnum>();
            string resourceKey = string.Format("{0}_{1}", type.Name, value.ToString()); 
            return LookupResource(resourceType, resourceKey);
        }

        public static string GetLocalizedString<TEnum>(Nullable<TEnum> value, Type resourceType = null)
            where TEnum : struct
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                return GetLocalizedString(value.Value, resourceType);
            }
        }

        /// <summary>
        /// Lookups the resource.
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="resourceKey">The resource key.</param>
        /// <returns></returns>
        private static string LookupResource(Type resourceType, string resourceKey)
        {
            if (resourceType == null)
                return null;
            var PropertyInfos = resourceType.GetProperties();
            PropertyInfo resourceProperty = PropertyInfos.FirstOrDefault(p => p.PropertyType == typeof(ResourceManager));

            PropertyInfo cultureProperty = PropertyInfos.FirstOrDefault(p => p.PropertyType == typeof(CultureInfo));

            if (resourceProperty != null)
            {
                if (cultureProperty != null)
                {
                    CultureInfo cultureInfo = cultureProperty.GetValue(null, null) as CultureInfo;
                    return ((ResourceManager)resourceProperty.GetValue(null, null)).GetString(resourceKey, cultureInfo);
                }
                return ((ResourceManager)resourceProperty.GetValue(null, null)).GetString(resourceKey);
            }
            return null;
        }

    }
}
