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
        public Type PropertyType { get; set; }
        public string PropertyName { get; set; }
        public object Value { get; }
        public ExpressionType ExpressionType { get; set; }

        protected virtual object ConvertToPropertyType(object value)
        {
            return BasicTypeUtility.ConvertTo(value, PropertyType);
        }

        public BaseLambda(Type propertyType, string peopertyName, object value)
        {
            PropertyType = propertyType;
            PropertyName = peopertyName;
            Value = ConvertToPropertyType(value);
        }
    }
}
