﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramework.Elements
{
    public class IsNull : BaseTag
    {
        public static IsNull For(object property)
        {
            return new IsNull { Value = property };
        }

        public override bool IsPassed
        {
            get
            {
                return Value == null;
            }
        }
    }
}
