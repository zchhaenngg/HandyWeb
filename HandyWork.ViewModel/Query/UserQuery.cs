using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.ViewModel.Query
{
    public class UserQuery : BaseQuery
    {
        public UserQuery()
        {
            ColumnDic.Add("IsValidStr", "IsValid");
        }

        public string UserNameEqual { get; set; }
        public string UserNameLike { get; set; }
        public string RealNameLike { get; set; }
        public bool? IsValid { get; set; }
        public bool? IsLocked { get; set; }
    }
}
