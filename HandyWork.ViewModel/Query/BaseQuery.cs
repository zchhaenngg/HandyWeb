using HandyWork.ViewModel.Query.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.ViewModel.Query
{
    public class BaseQuery : Hy_IQuery
    {
        private string _sortColumn;
        protected Dictionary<string, string> ColumnDic { get; } = new Dictionary<string, string>();
        
        public bool IsAsc { get; set; }//默认降序
        public int PageIndex { get; set; }
        public int PageNumber
        {
            set
            {
                PageIndex = value < 0 ? -1 : value - 1;
            }
            get
            {
                return PageIndex + 1;
            }
        }
        public int PageSize { get; set; } = int.MaxValue;

        public string SortColumn
        {
            set
            {
                if (value != null)
                {
                    if (ColumnDic.ContainsKey(value))
                    {
                        _sortColumn = ColumnDic[value];
                    }
                    else
                    {
                        _sortColumn = value;
                    }
                }
            }
            get
            {
                return _sortColumn ?? "last_modified_by_id";
            }
        }
    }
}
