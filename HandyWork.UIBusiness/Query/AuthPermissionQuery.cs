﻿using HandyWork.Model.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UIBusiness.Query
{
    public class AuthPermissionQuery : BaseQuery
    {
        public AuthPermissionQuery()
            : base()
        {
        }
        public string NameLike { get; set; }
        public string CodeLike { get; set; }
    }
}