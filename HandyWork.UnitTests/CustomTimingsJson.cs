using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UnitTests
{
    public class CustomTimingsJson
    {
        public List<CustomTimingJson> sql { get; set; }
    }

    public class CustomTimingJson
    {
        public string Id { get; set; }
        public string CommandString { get; set; }
        public string ExecuteType { get; set; }
        public string StackTraceSnippet { get; set; }
        public double StartMilliseconds { get; set; }
        public double DurationMilliseconds { get; set; }
        public double FirstFetchDurationMilliseconds { get; set; }
    }
}
