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
    /// 空对象，空数组、空集合、空值、空格
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
                else if (Value.GetType().Name.Equals(typeof(HashSet<>).Name))
                {
                    foreach (var item in Value as IEnumerable)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    if (Value.GetType().IsString())
                    {
                        return string.IsNullOrWhiteSpace(Value as string);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
