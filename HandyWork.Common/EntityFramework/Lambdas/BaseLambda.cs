using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Common.Extensions;
using System.Collections;

namespace HandyWork.Common.EntityFramework.Lambdas
{
    public abstract partial class BaseLambda<TEntity>
    {
        /// <summary>
        /// TEntity.TProperty's Type
        /// </summary>
        protected Type PropertyType { get; set; }
        protected string PropertyName { get; set; }
        protected object Value { get; set; }

        protected ExpressionType ExpressionType { get; set; }

        protected Expression<Func<TEntity, object>> PropertyExpression { get; set; }

        public BaseLambda(Type propertyType, string peopertyName, object entityValue)
        {
            PropertyType = propertyType;
            PropertyName = peopertyName;
            SetValue(entityValue);
        }
        
        public virtual Expression<Func<TEntity, bool>> Build()
        {
            var parameter = Expression.Parameter(typeof(TEntity), "o");
            var member = Expression.Property(parameter, PropertyName);

            if (PropertyType.IsNullable())
            {
                var binary = Expression.MakeBinary(ExpressionType, member, Expression.Convert(Expression.Constant(Value), PropertyType));
                return Expression.Lambda<Func<TEntity, bool>>(binary, parameter);
            }
            else
            {
                var binary = Expression.MakeBinary(ExpressionType, member, Expression.Constant(Value));
                return Expression.Lambda<Func<TEntity, bool>>(binary, parameter);
            }
        }

        protected object ConvertToTProperty(object item)
        {
            if (item == null)
            {
                CheckNotNull();
                return PropertyType.GetDefaultValue();
            }
            else
            {
                if (PropertyType.IsShortOrNullable())
                {
                    return Convert.ToInt16(item);
                }
                else if (PropertyType.IsIntOrNullable())
                {
                    return Convert.ToInt32(item);
                }
                else if (PropertyType.IsLongOrNullable())
                {
                    return Convert.ToInt64(item);
                }
                else if (PropertyType.IsCharOrNullable())
                {
                    return Convert.ToChar(item);
                }
                else if (PropertyType.IsByteOrNullable())
                {
                    return Convert.ToByte(item);
                }
                else if (PropertyType.IsFloatOrNullable())
                {
                    return Convert.ToSingle(item);
                }
                else if (PropertyType.IsDoubleOrNullable())
                {
                    return Convert.ToDouble(item);
                }
                else if (PropertyType.IsDecimalOrNullable())
                {
                    return Convert.ToDecimal(item);
                }
                else if (PropertyType.IsBoolOrNullable())
                {
                    return Convert.ToBoolean(item);
                }
                else if (PropertyType.IsDateTimeOrNullable())
                {
                    if (!item.GetType().IsDateTimeOrNullable())
                    {
                        throw new ArgumentException("ConvertToTProperty,不支持转DateTime--需要考虑UTC时间");
                    }
                    else
                    {
                        return item;
                    }
                }
                else if (PropertyType.IsString())
                {
                    return item;
                }
                else
                {
                    throw new NotSupportedException("ConvertToTProperty，不支持转换成类型" + PropertyType.Name);
                }
            }
        }

        private void CheckNotNull()
        {
            if (!PropertyType.IsNullable())
            {
                throw new ArgumentException("TProperty不可为空，entityValue也不能为空");
            }
        }

        private void SetValue(object entityValue)
        {
            if (entityValue != null)
            {
                var coll = entityValue as ICollection;
                if (coll != null)
                {
                    List<object> list = new List<object>();
                    var isPropertyNullable = PropertyType.IsNullable();
                    foreach (var item in coll)
                    {
                        if (isPropertyNullable)
                        {
                            var value = ConvertToTProperty(item);
                            list.Add(value);
                        }
                        else
                        {
                            if (item != null)
                            {
                                var value = ConvertToTProperty(item);
                                if (value != null)
                                {
                                    list.Add(value);
                                }
                            }
                        }
                    }
                    Value = list;
                }
                else
                {
                    Value = ConvertToTProperty(entityValue);
                }
            }
        }
    }
}
