using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Model
{
    public class ErrorInfo
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public ErrorInfo() { }
        public ErrorInfo(string code, string description)
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
                return string.Format("[{0}],{1}", Code, Description);
            }
        }
    }
}
