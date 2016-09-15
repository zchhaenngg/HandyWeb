using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramwork.Elements
{
    public abstract class BaseTag
    {
        public BaseTag(object property)
        {
            Value = property;
        }

        public object Value { get; set; }

        public abstract bool IsCondition();
    }
}
