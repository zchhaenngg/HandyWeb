﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramwork.Elements
{
    /// <summary>
    /// 数组、集合、值类型、string
    /// </summary>
    public class IsEmpty : BaseTag
    {
        public IsEmpty(object property) : base(property)
        {
        }

        public static IsEmpty For(object property)
        {
            return new IsEmpty(property);
        }

        public override bool IsPassed()
        {
            if (Value == null)
            {
                return true;
            }
            if (Value is ValueType)
            {
                return Value == null;
            }
            else if (Value is string)
            {
                return string.IsNullOrWhiteSpace(Value as string);
            }
            else if (Value is Array)
            {
                return (Value as Array).Length == 0;
            }
            else if (Value is ICollection)
            {
                return (Value as ICollection).Count == 0;
            }
            else
            {
                throw new NotSupportedException(string.Format("isEmpty不支持类型 {0}", Value.GetType().Name));
            }
        }
    }
}
