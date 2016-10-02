using HandyWork.DAL;
using Newtonsoft.Json;
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HandyWork.DAL.UnitTests
{
    public class BaseTest
    {
        private UnitOfWork _unitOfWork;
        protected UnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork = new UnitOfWork("-1"));

        public BaseTest()
        {
            MiniProfiler.Settings.ProfilerProvider = new SingletonProfilerProvider();
            MiniProfilerEF6.Initialize();
        }
        
        /// <summary>
        /// produce a profiling report.
        /// </summary>
        public static CustomTimingsJson ReportSqls()
        {
            try
            {
                return JsonConvert.DeserializeObject<CustomTimingsJson>(MiniProfiler.Current.Root.CustomTimingsJson);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
        public static string ReportPlainText()
        {
            return MiniProfiler.Current.RenderPlainText();
        }
        
    }
}
