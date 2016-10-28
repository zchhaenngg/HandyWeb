using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Common.Extensions;
using HandyWork.Common.Utility;
using System.Globalization;

namespace HandyWork.Common.EntityFramework.Lambdas
{
    /// <summary>
    /// 数组、集合。  如果是关于string的请使用LikeLambda
    /// </summary>
    public class ContainLambda : BaseLambda
    {
        public ContainLambda(Type propertyType, string peopertyName, object value) : base(propertyType, peopertyName, value)
        {
        }
        
        public virtual MethodInfo GetContainMethodInfo()
        {
            var method = typeof(List<>).MakeGenericType(PropertyType).GetMethod("Contains");
            return method;
        }
        
        protected override object ConvertToPropertyType(object value)
        {
            var coll = value as ICollection;
            if (coll == null)
            {
                return null;
            }
            else
            {
                if (!PropertyType.IsValueType)
                {
                    return BasicTypeUtility.ToList<object>(coll);
                }
                else
                {
                    if (PropertyType.IsShortOrNullable())
                    {
                        if (PropertyType.IsNullable())
                        {
                            return BasicTypeUtility.ToList<short?>(coll);
                        }
                        else
                        {
                            return BasicTypeUtility.ToList<short>(coll);
                        }
                    }
                    else if (PropertyType.IsIntOrNullable())
                    {
                        if (PropertyType.IsNullable())
                        {
                            return BasicTypeUtility.ToList<int?>(coll);
                        }
                        else
                        {
                            return BasicTypeUtility.ToList<int>(coll);
                        }
                    }
                    else if (PropertyType.IsLongOrNullable())
                    {
                        if (PropertyType.IsNullable())
                        {
                            return BasicTypeUtility.ToList<long?>(coll);
                        }
                        else
                        {
                            return BasicTypeUtility.ToList<long>(coll);
                        }
                    }
                    else if (PropertyType.IsCharOrNullable())
                    {
                        if (PropertyType.IsNullable())
                        {
                            return BasicTypeUtility.ToList<char?>(coll);
                        }
                        else
                        {
                            return BasicTypeUtility.ToList<char>(coll);
                        }
                    }
                    else if (PropertyType.IsByteOrNullable())
                    {
                        if (PropertyType.IsNullable())
                        {
                            return BasicTypeUtility.ToList<byte?>(coll);
                        }
                        else
                        {
                            return BasicTypeUtility.ToList<byte>(coll);
                        }
                    }
                    else if (PropertyType.IsFloatOrNullable())
                    {
                        if (PropertyType.IsNullable())
                        {
                            return BasicTypeUtility.ToList<float?>(coll);
                        }
                        else
                        {
                            return BasicTypeUtility.ToList<float>(coll);
                        }
                    }
                    else if (PropertyType.IsDoubleOrNullable())
                    {
                        if (PropertyType.IsNullable())
                        {
                            return BasicTypeUtility.ToList<double?>(coll);
                        }
                        else
                        {
                            return BasicTypeUtility.ToList<double>(coll);
                        }
                    }
                    else if (PropertyType.IsDecimalOrNullable())
                    {
                        if (PropertyType.IsNullable())
                        {
                            return BasicTypeUtility.ToList<decimal?>(coll);
                        }
                        else
                        {
                            return BasicTypeUtility.ToList<decimal>(coll);
                        }
                    }
                    else if (PropertyType.IsBoolOrNullable())
                    {
                        if (PropertyType.IsNullable())
                        {
                            return BasicTypeUtility.ToList<bool?>(coll);
                        }
                        else
                        {
                            return BasicTypeUtility.ToList<bool>(coll);
                        }
                    }
                    else if (PropertyType.IsDateTimeOrNullable())
                    {
                        if (PropertyType.IsNullable())
                        {
                            return BasicTypeUtility.ToList<DateTime?>(coll);
                        }
                        else
                        {
                            return BasicTypeUtility.ToList<DateTime>(coll);
                        }
                    }
                    else
                    {
                        throw new NotSupportedException(string.Format("不支持值类型 {0}", PropertyType.FullName));
                    }
                }
            }
        }
    }
}
