﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramework.Elements
{
    public class IsNotNull : BaseTag
    {
        public static IsNotNull For(object property)
        {
            return new IsNotNull { Value = property };
        }

        public override bool IsPassed
        {
            get
            {
                return Value != null;
            }
        }
    }
}
