using HandyWork.Model.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UIBusiness.Query
{
    public class UserQuery : BaseQuery
    {
        public UserQuery()
            : base()
        {
            _sortColumnDic.Add("IsValidStr", "IsValid");
        }

        public string UserNameLike { get; set; }
        public string RealNameLike { get; set; }
        public bool? IsValid { get; set; }
        public bool? IsLocked { get; set; }
    }
}
