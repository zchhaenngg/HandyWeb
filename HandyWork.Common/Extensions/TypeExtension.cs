using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Extensions
{
    public enum SpecifiedType
    {
        Short = 1,
        NShort = 2,
        Int = 4,
        NInt = 8,
        Double = 16,
        NDouble = 32,
        Long = 64,
        NLong = 128,
        Float = 256,
        NFloat = 512,
        DateTime = 1024,
        NDateTime = 2048,
        String = 4096,
        Char = 8192,
        NChar = 16384,
        Byte = 32768,
        NByte = 65536,
        Decimal = 131072,
        NDecimal = 262144,
        Bool = 524288,
        NBool = 1048576
    }
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

        public static bool IsDateTime(this Type type) => type == typeof(DateTime);
        public static bool IsDateTimeNullable(this Type type) => type == typeof(DateTime?);
        public static bool IsDateTimeOrNullable(this Type type) => type.IsDateTime() || type.IsDateTimeNullable();

        public static bool IsLong(this Type type) => type == typeof(long);
        public static bool IsLongNullable(this Type type) => type == typeof(long?);
        public static bool IsLongOrNullable(this Type type) => type.IsLong() || type.IsLongNullable();

        public static bool IsChar(this Type type) => type == typeof(char);
        public static bool IsCharNullable(this Type type) => type == typeof(char?);
        public static bool IsCharOrNullable(this Type type) => type.IsChar() || type.IsCharNullable();

        public static bool IsByte(this Type type) => type == typeof(byte);
        public static bool IsByteNullable(this Type type) => type == typeof(byte?);
        public static bool IsByteOrNullable(this Type type) => type.IsByte() || type.IsByteNullable();

        public static bool IsDecimal(this Type type) => type == typeof(decimal);
        public static bool IsDecimalNullable(this Type type) => type == typeof(decimal?);
        public static bool IsDecimalOrNullable(this Type type) => type.IsDecimal() || type.IsDecimalNullable();

        public static bool IsBool(this Type type) => type == typeof(bool);
        public static bool IsBoolNullable(this Type type) => type == typeof(bool?);
        public static bool IsBoolOrNullable(this Type type) => type.IsBool() || type.IsBoolNullable();
        /// <summary>
        /// type.TrueIn(Short|NShort|Int)
        /// </summary>
        public static bool TrueIn(this Type type, SpecifiedType specifiedType)
        {
            return specifiedType.HasFlag(SpecifiedType.String) && type.IsString()
                || (specifiedType.HasFlag(SpecifiedType.Short) && type.IsShort())
                || (specifiedType.HasFlag(SpecifiedType.NShort) && type.IsShortNullable())
                || (specifiedType.HasFlag(SpecifiedType.Int) && type.IsInt())
                || (specifiedType.HasFlag(SpecifiedType.NInt) && type.IsIntNullable())
                || (specifiedType.HasFlag(SpecifiedType.Double) && type.IsDouble())
                || (specifiedType.HasFlag(SpecifiedType.NDouble) && type.IsDoubleNullable())
                || (specifiedType.HasFlag(SpecifiedType.Long) && type.IsLong())
                || (specifiedType.HasFlag(SpecifiedType.NLong) && type.IsLongNullable())
                || (specifiedType.HasFlag(SpecifiedType.Float) && type.IsFloat())
                || (specifiedType.HasFlag(SpecifiedType.NFloat) && type.IsFloatNullable())
                || (specifiedType.HasFlag(SpecifiedType.DateTime) && type.IsDateTime())
                || (specifiedType.HasFlag(SpecifiedType.NDateTime) && type.IsDateTimeNullable())
                || (specifiedType.HasFlag(SpecifiedType.Char) && type.IsChar())
                || (specifiedType.HasFlag(SpecifiedType.NChar) && type.IsCharNullable())
                || (specifiedType.HasFlag(SpecifiedType.Byte) && type.IsByte())
                || (specifiedType.HasFlag(SpecifiedType.NByte) && type.IsByteNullable())
                || (specifiedType.HasFlag(SpecifiedType.Decimal) && type.IsDecimal())
                || (specifiedType.HasFlag(SpecifiedType.NDecimal) && type.IsDecimalNullable())
                || (specifiedType.HasFlag(SpecifiedType.Bool) && type.IsBool())
                || (specifiedType.HasFlag(SpecifiedType.NBool) && type.IsBoolOrNullable());
        }

        public static object GetDefaultValue(this Type type)
        {
            if (type.IsShort())
            {
                return default(short);
            }
            else if (type.IsInt())
            {
                return default(int);
            }
            else if (type.IsDouble())
            {
                return default(double);
            }
            else if (type.IsLong())
            {
                return default(long);
            }
            else if (type.IsFloat())
            {
                return default(float);
            }
            else if (type.IsDateTime())
            {
                return default(DateTime);
            }
            else if (type.IsChar())
            {
                return default(char);
            }
            else if (type.IsByte())
            {
                return default(byte);
            }
            else if (type.IsDecimal())
            {
                return default(decimal);
            }
            else if (type.IsBool())
            {
                return default(bool);
            }
            else
            {
                return null;
            }
        }
        
        public static bool IsNullable(this Type type)
        {
            return !type.IsValueType || type.Name.Equals(typeof(Nullable<>).Name) ;
        }

        /// <summary>
        /// 获取可空类型的实际类型
        /// </summary>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public static Type GetUnNullableType(this Type type)
        {
            if (type.IsNullable())
            {
                return Nullable.GetUnderlyingType(type);
            }
            return type;
        }
    }
}
