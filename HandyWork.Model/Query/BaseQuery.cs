using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Model.Query
{
    public class BaseQuery
    {
        private string _sortColumn;
        protected Dictionary<string, string> _sortColumnDic = new Dictionary<string, string>();

        public BaseQuery()
        {
            GenerateSortColumnDic();
        }
        protected virtual void GenerateSortColumnDic()
        {

        }

        public bool IsAsc { get; set; }//默认降序
        public int PageIndex { get; set; }
        public int PageNumber
        {
            set
            {
                PageIndex = value < 0 ? -1 : value - 1;
            }
        }
        public int PageSize { get; set; }

        public string SortColumn
        {
            set
            {
                if (value == null)
                {
                    _sortColumn = "LastModifiedById";
                }
                else
                {
                    if (_sortColumnDic.ContainsKey(value))
                    {
                        _sortColumn = _sortColumnDic[value];
                    }
                    else
                    {
                        _sortColumn = value.ToLower();
                    }
                }
            }
            get { return _sortColumn; }
        }
    }
}
