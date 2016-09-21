using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Common.Extensions;

namespace HandyWork.Common.EntityFramework.Elements
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

        public override bool IsPassed
        {
            get
            {
                if (Value == null)
                {
                    return true;
                }
                else if (Value is ICollection)
                {
                    return (Value as ICollection).Count == 0;
                }
                else
                {
                    var defaultValue = Value.GetType().GetDefaultValue();
                    //数字、时间、其他非空对象
                    return Value.Equals(defaultValue);
                }
            }
        }
    }
}
