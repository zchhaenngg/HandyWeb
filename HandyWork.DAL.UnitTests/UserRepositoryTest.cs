using HandyWork.Model;
using HandyWork.ViewModel.PCWeb.Query;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Profiling;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace HandyWork.DAL.UnitTests
{
    [TestClass]
    public class UserRepositoryTest: BaseTest
    {
        [TestMethod]
        public void FindAllByQuery()
        {
            var expression1 = UnitOfWork.UserRepository.GetExpression(new UserQuery { UserNameLike = "h", IsLocked = false });
        }

        [TestMethod]
        public void Tracking()
        {
            MiniProfiler.Start();
            {
                var coll = UnitOfWork.UserRepository.GetAllPermissions("-1");
                var entity = coll.First();
                var state = UnitOfWork.GetEntityState(entity);
                Assert.IsTrue(state == EntityState.Unchanged);
            }
            var str = ReportPlainText();
            var timings = ReportSqls();
            foreach (var item in timings.sql)
            {
                var s = item.CommandString;
            }
            MiniProfiler.Stop();

            MiniProfiler.Start();
            {
                var entity = UnitOfWork.UserRepository.Source.AsNoTracking().Where(o => o.Id == "-1").FirstOrDefault();
                var state = UnitOfWork.GetEntityState(entity);
                Assert.IsTrue(state == EntityState.Detached);
            }

            str = ReportPlainText();
            timings = ReportSqls();
            foreach (var item in timings.sql)
            {
                var s = item.CommandString;
            }
            Assert.IsTrue(str != null);
            
        }
    }
}
