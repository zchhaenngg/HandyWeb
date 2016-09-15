using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramwork.Elements
{
    public class IsNull : BaseTag
    {
        public IsNull(object property) : base(property)
        {
        }

        public override bool IsCondition()
        {
            return Value == null;
        }
    }
}
