﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.ViewModel.Query
{
    public class AuthRoleQuery : BaseQuery
    {
        public AuthRoleQuery()
            : base()
        {

        }

        public string NameLike { get; set; }
    }
}
