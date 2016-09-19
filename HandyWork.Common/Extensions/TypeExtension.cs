using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Extensions
{
    public static class TypeExtension
    {
        public static bool IsString(this Type type) => type == typeof(string);

        public static bool IsShort(this Type type) => type == typeof(short);
        public static bool IsShortNullable(this Type type) => type == typeof(short?);
        public static bool IsShortOrNullable(this Type type) => type.IsShort() || type.IsShortNullable();

        public static bool IsInt(this Type type) => type == typeof(int);
        public static bool IsIntNullable(this Type type) => type == typeof(int?);
        public static bool IsIntOrNullable(this Type type) => type.IsInt() || type.IsIntNullable();

        public static bool IsDouble(this Type type) => type == typeof(double);
        public static bool IsDoubleNullable(this Type type) => type == typeof(double?);
        public static bool IsDoubleOrNullable(this Type type) => type.IsDouble() || type.IsDoubleNullable();

        public static bool IsFloat(this Type type) => type == typeof(float);
        public static bool IsFloatNullable(this Type type) => type == typeof(float?);
        public static bool IsFloatOrNullable(this Type type) => type.IsDouble() || type.IsDoubleNullable();

    }
}
