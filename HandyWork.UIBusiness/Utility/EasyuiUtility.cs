using HandyWork.Common.Exceptions;
using HandyWork.Common.Helper;
using HandyWork.Model.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HandyWork.UIBusiness.Utility
{
    public static class EasyuiUtility
    {
        public static void FillPageQueryFromRequest(HttpRequest req, BaseQuery query)
        {
            int size = int.Parse(req["rows"]);
            if (size < query.PageSize)
            {
                query.PageSize = size;
            }
            else
            {
                throw new ErrorException(string.Format("客户端在尝试一次获取 {0} 条数据，当前实际限制为 {1}。", size, query.PageSize));
            }

            string pageStr = req["page"]; //当前页数
            query.PageNumber = string.IsNullOrEmpty(pageStr) ? 1 : int.Parse(pageStr);

            string sortStr = req["sort"]; //排序参数
            query.SortColumn = string.IsNullOrEmpty(sortStr) ? null : sortStr;

            string orderStr = req["order"]; //排序方法 ASC or DESC
            query.IsAsc = string.Equals(orderStr, "asc", StringComparison.OrdinalIgnoreCase);
        }
    }
}
