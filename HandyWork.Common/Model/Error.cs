using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Authority
{
    public class Error
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public Error() { }
        public Error(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Code))
            {
                return Description;
            }
            else
            {
                return string.Format("相关模块-{0},描述-{1}", Code, Description);
            }
        }
    }
}
