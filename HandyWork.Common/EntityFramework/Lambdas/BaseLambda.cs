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
    public abstract class BaseLambda<TEntity, TProperty>
    {
        protected object Value { get; set; }

        protected ExpressionType ExpressionType { get; set; }

        protected Expression<Func<TEntity, TProperty>> PropertyExpression { get; set; }

        public BaseLambda(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue)
        {
            PropertyExpression = entityProperty;

            SetValue(entityValue);
        }
        
        public virtual Expression<Func<TEntity, bool>> Build()
        {
            var parameter = Expression.Parameter(typeof(TEntity), "o");
            var propertyName = (PropertyExpression.Body as MemberExpression).Member.Name;
            var member = Expression.Property(parameter, propertyName);

            if (typeof(TProperty).IsNullable())
            {
                var binary = Expression.MakeBinary(ExpressionType, member, Expression.Convert(Expression.Constant(Value), typeof(TProperty)));
                return Expression.Lambda<Func<TEntity, bool>>(binary, parameter);
            }
            else
            {
                var binary = Expression.MakeBinary(ExpressionType, member, Expression.Constant(Value));
                return Expression.Lambda<Func<TEntity, bool>>(binary, parameter);
            }
        }

        protected TProperty ConvertToTProperty(object item)
        {
            if (item == null)
            {
                CheckNotNull();
                return default(TProperty);
            }
            else
            {
                var propertyType = typeof(TProperty);
                if (propertyType.IsShortOrNullable())
                {
                    return (TProperty)(Convert.ToInt16(item) as object);
                }
                else if (propertyType.IsIntOrNullable())
                {
                    return (TProperty)(Convert.ToInt32(item) as object);
                }
                else if (propertyType.IsLongOrNullable())
                {
                    return (TProperty)(Convert.ToInt64(item) as object);
                }
                else if (propertyType.IsCharOrNullable())
                {
                    return (TProperty)(Convert.ToChar(item) as object);
                }
                else if (propertyType.IsByteOrNullable())
                {
                    return (TProperty)(Convert.ToByte(item) as object);
                }
                else if (propertyType.IsFloatOrNullable())
                {
                    return (TProperty)(Convert.ToSingle(item) as object);
                }
                else if (propertyType.IsDoubleOrNullable())
                {
                    return (TProperty)(Convert.ToDouble(item) as object);
                }
                else if (propertyType.IsDecimalOrNullable())
                {
                    return (TProperty)(Convert.ToDecimal(item) as object);
                }
                else if (propertyType.IsBoolOrNullable())
                {
                    return (TProperty)(Convert.ToBoolean(item) as object);
                }
                else if (propertyType.IsDateTimeOrNullable())
                {
                    if (!item.GetType().IsDateTimeOrNullable())
                    {
                        throw new ArgumentException("ConvertToTProperty,不支持转DateTime--需要考虑UTC时间");
                    }
                    else
                    {
                        return (TProperty)(item);
                    }
                }
                else if (propertyType.IsString())
                {
                    return (TProperty)(item.ToString() as object);
                }
                else
                {
                    throw new NotSupportedException("ConvertToTProperty，不支持转换成类型" + propertyType.Name);
                }
            }
        }

        private void CheckNotNull()
        {
            if (default(TProperty) != null)
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
                    List<TProperty> list = new List<TProperty>();
                    var isPropertyNullable = typeof(TProperty).IsNullable();
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
