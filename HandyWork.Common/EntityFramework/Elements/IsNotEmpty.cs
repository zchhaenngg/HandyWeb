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
    /// 非null，非空数组、非空集合、非0
    /// </summary>
    public class IsNotEmpty : BaseTag
    {
        public IsNotEmpty(object property) : base(property)
        {
        }

        public static IsNotEmpty For(object property)
        {
            return new IsNotEmpty(property);
        }

        public override bool IsPassed
        {
            get
            {
                if (Value == null)
                {
                    return false;
                }
                else if (Value is ICollection)
                {
                    return (Value as ICollection).Count > 0;
                }
                else
                {
                    if (Value.GetType().IsBool())
                    {
                        return true;
                    }
                    else if (Value.GetType().IsString())
                    {
                        return !string.IsNullOrWhiteSpace(Value as string);
                    }
                    else
                    {
                        var defaultValue = Value.GetType().GetDefaultValue();
                        //数字、时间、其他非空对象
                        return !Value.Equals(defaultValue);
                    }
                }
            }
        }
    }
}
