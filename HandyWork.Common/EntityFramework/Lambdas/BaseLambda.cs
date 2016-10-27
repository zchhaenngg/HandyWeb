using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Common.Extensions;
using System.Collections;
using HandyWork.Common.Utility;

namespace HandyWork.Common.EntityFramework.Lambdas
{
    public abstract partial class BaseLambda
    {
        /// <summary>
        /// TEntity.TProperty's Type
        /// </summary>
        protected Type PropertyType { get; set; }
        protected string PropertyName { get; set; }
        protected object Value { get; set; }

        protected ExpressionType ExpressionType { get; set; }
        
        public BaseLambda(Type propertyType, string peopertyName, object entityValue)
        {
            PropertyType = propertyType;
            PropertyName = peopertyName;
            SetValue(entityValue);
        }
        
        public virtual Expression<Func<TEntity, bool>> Build<TEntity>()
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
        
        private void SetValue(object entityValue)
        {
            if (entityValue != null)
            {
                var coll = entityValue as ICollection;
                if (coll != null)
                {
                    List<object> list = new List<object>();//不能使用object
                    var isPropertyNullable = PropertyType.IsNullable();
                    foreach (var item in coll)
                    {
                        if (isPropertyNullable)
                        {
                            var value = BasicTypeUtility.ConvertTo(item, PropertyType); 
                            list.Add(value);
                        }
                        else
                        {
                            if (item != null)
                            {
                                var value = BasicTypeUtility.ConvertTo(item, PropertyType);
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
                    Value = BasicTypeUtility.ConvertTo(entityValue, PropertyType);
                }
            }
        }
    }
}
