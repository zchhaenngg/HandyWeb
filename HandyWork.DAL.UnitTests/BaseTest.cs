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

namespace HandyWork.UnitTests
{
    public class BaseTest
    {
        private UnitOfWork _unitOfWork;
        protected UnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork = new UnitOfWork("-1"));

        static BaseTest()
        {
            MiniProfiler.Settings.ProfilerProvider = new SingletonProfilerProvider();
            MiniProfilerEF6.Initialize();
        }
        public BaseTest()
        {
            //避免第一次运行ReportOutput会输出执行Migration的查询
            UnitOfWork.SaveChanges();
        }
    }
}
