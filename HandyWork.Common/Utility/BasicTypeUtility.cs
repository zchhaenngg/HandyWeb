using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Common.Extensions;
using System.Collections;

namespace HandyWork.Common.Utility
{
    /// <summary>
    /// short,int,long,char,byte,float,double,decimal,bool,datetime,string
    /// </summary>
    public class BasicTypeUtility
    {
        public static T ConvertTo<T>(object item)
        {
            return (T)ConvertTo(item, typeof(T));
        }
        public static object ConvertTo(object item, Type destination)
        {
            if (item == null)
            {
                if (!destination.IsNullable())
                {
                    
                    throw new InvalidCastException(string.Format("转换失败，空对象无法转换成不可空类型 {0}", destination.Name));
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (destination.FullName == "System.Object")
                {
                    return item;
                }
                if (destination.IsShortOrNullable())
                {
                    return Convert.ToInt16(item);
                }
                else if (destination.IsIntOrNullable())
                {
                    return Convert.ToInt32(item);
                }
                else if (destination.IsLongOrNullable())
                {
                    return Convert.ToInt64(item);
                }
                else if (destination.IsCharOrNullable())
                {
                    return Convert.ToChar(item);
                }
                else if (destination.IsByteOrNullable())
                {
                    return Convert.ToByte(item);
                }
                else if (destination.IsFloatOrNullable())
                {
                    return Convert.ToSingle(item);
                }
                else if (destination.IsDoubleOrNullable())
                {
                    return Convert.ToDouble(item);
                }
                else if (destination.IsDecimalOrNullable())
                {
                    return Convert.ToDecimal(item);
                }
                else if (destination.IsBoolOrNullable())
                {
                    return Convert.ToBoolean(item);
                }
                else if (destination.IsDateTimeOrNullable())
                {
                    if (!item.GetType().IsDateTimeOrNullable())
                    {
                        throw new InvalidCastException(string.Format("转换失败，不支持将类型 {0} 转换为 {1}", item.GetType().FullName, destination.FullName));
                    }
                    else
                    {
                        return item;
                    }
                }
                else if (destination.IsString())
                {
                    return item;
                }
                else
                {
                    throw new InvalidCastException(string.Format("转换失败，不支持将类型 {0} 转换为 {1}", item.GetType().FullName, destination.FullName));
                }
            }
        }

        public static List<TValue> ToList<TValue>(ICollection coll)
        {
            if (coll == null)
            {
                return null;
            }
            else
            {
                var list = new List<TValue>();
                var isPropertyTypeNullable = typeof(TValue).IsNullable();
                foreach (var item in coll)
                {
                    if (item == null)
                    {
                        if (isPropertyTypeNullable)
                        {
                            list.Add(default(TValue));
                        }
                    }
                    else
                    {
                        var t = ConvertTo<TValue>(item);
                        list.Add(t);
                    }
                }
                return list;
            }
        }

    }
}
