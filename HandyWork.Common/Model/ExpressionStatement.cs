using HandyWork.Common.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Model
{
    public class ExpressionStatement
    {
        public object Property { get; set; }
        public QueryCondition Condition { get; set; }
        public ExpressionStatement(object property, QueryCondition condition)
        {
            this.Property = property;
        }
    }
}
