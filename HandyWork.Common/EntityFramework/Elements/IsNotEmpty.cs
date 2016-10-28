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
    /// 非空对象，非空数组、非空集合、非空值、非空格
    /// </summary>
    public class IsNotEmpty : BaseTag
    {

        public static IsNotEmpty For(object property)
        {
            return new IsNotEmpty { Value = property };
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
                else if (Value.GetType().Name.Equals(typeof(HashSet<>).Name))
                {
                    foreach (var item in Value as IEnumerable)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    if (Value.GetType().IsString())
                    {
                        return !string.IsNullOrWhiteSpace(Value as string);
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
    }
}
