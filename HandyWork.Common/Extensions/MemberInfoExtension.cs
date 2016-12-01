using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Extensions
{
    public static class MemberInfoExtension
    {
        public static TCustomerAttribute GetCustomerAttribute<TCustomerAttribute>(this MemberInfo memberInfo)
            where TCustomerAttribute : Attribute
        {
            return memberInfo.GetCustomAttributes(false).FirstOrDefault(c=>c is TCustomerAttribute) as TCustomerAttribute;
        }
    }
}
