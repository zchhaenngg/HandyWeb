using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramework.Elements
{
    public class IsTrue : BaseTag
    {
        public static IsTrue Init { get; } = new IsTrue();

        public override bool IsPassed
        {
            get
            {
                return true;
            }
        }
    }
}
