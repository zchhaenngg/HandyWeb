using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramework.Query
{
    public class QueryItem
    {
        public string Field { get; set; }
        public object Value { get; set; }
        public QueryMethod Method { get; set; }
    }
}
