using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.ViewModel.Query.Interface
{
    public interface Hy_IQuery
    {
        string SortColumn { get; set; }
        bool IsAsc { get; set; }
        int PageIndex { get; set; }
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}
