using Newtonsoft.Json;
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL.UnitTests
{
    public class ReportOuput : IDisposable
    {
        public ReportOuput()
        {
            MiniProfiler.Start();
        }

        public void Dispose()
        {
            ReportSql();
            MiniProfiler.Stop();
        }

        /// <summary>
        /// produce a profiling report.
        /// </summary>
        private CustomTimingsJson GetReport()
        {
            try
            {
                return JsonConvert.DeserializeObject<CustomTimingsJson>(MiniProfiler.Current.Root.CustomTimingsJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string ReportPlainText()
        {
            return MiniProfiler.Current.RenderPlainText();
        }

        /// <summary>
        /// 菜单栏选择 视图-输出，在弹出窗口中选择输出来源 调试，会将sql信息打印在此处
        /// </summary>
        public void ReportSql()
        {
            Debug.WriteLine(ReportPlainText());
            var report = GetReport();
            foreach (var item in report.sql)
            {
                Debug.WriteLine(string.Format("duration {0} ms", item.DurationMilliseconds));
                Debug.WriteLine(item.CommandString);
            }
            Debug.WriteLine("=========================end========================");
        }

        public void WriteLine(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
