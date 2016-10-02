using HandyWork.Model;
using HandyWork.ViewModel.PCWeb.Query;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Profiling;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace HandyWork.DAL.UnitTests
{
    [TestClass]
    public class UserRepositoryTest: BaseTest
    {
        [TestMethod]
        public void Tracking()
        {
            using (ReportOuput output = new ReportOuput())
            {
                var coll = UnitOfWork.UserRepository.GetAllPermissions("-1");
                var entity = coll.First();
                var state = UnitOfWork.GetEntityState(entity);
                Assert.IsTrue(state == EntityState.Unchanged);
            }
            //AsNoTracking后,Entity的状态为EntityState.Detached
            using (ReportOuput output = new ReportOuput())
            {
                var entity = UnitOfWork.UserRepository.Source.AsNoTracking().Where(o => o.Id == "-1").FirstOrDefault();
                var state = UnitOfWork.GetEntityState(entity);
                Assert.IsTrue(state == EntityState.Detached);
            }
        }

        //如FindAll和FindAllByQuery不可直接返回所有数据应当由业务决定
        [TestMethod]
        public void test()
        {
            using (ReportOuput output = new ReportOuput())
            {
                UnitOfWork.UserRepository.Source.Where(o=>o.Id=="-1").Select(o => new { o.RealName, o.UserName }).ToList();
                UnitOfWork.UserRepository.Source.Select(o => new { o.RealName, o.UserName }).ToList();
                UnitOfWork.UserRepository.GetPermissionsByUserGrant("-1").Select(o => o.Name).ToList();
            }   
        }
    }
}
