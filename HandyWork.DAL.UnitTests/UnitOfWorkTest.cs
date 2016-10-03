using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Model;
using System.Data.Entity.Validation;
using System.Linq;
using HandyWork.ViewModel.PCWeb.Query;
using System.Data.Entity;

namespace HandyWork.DAL.UnitTests
{
    [TestClass]
    public class UnitOfWorkTest : BaseTest
    {
        [TestMethod]
        public void UnitOfWork_SaveChanges()
        {
            {
                #region   校验未通过-UserName 长度超了
                var entity = new AuthUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "test".PadLeft(51, '1'),
                    Password = "123456",
                    RealName = "测试1号"
                };
                UnitOfWork.Add(entity);
                try
                {
                    int cnt = UnitOfWork.SaveChanges();
                    Assert.IsTrue(false);
                }
                catch (DbEntityValidationException)
                {
                    Assert.IsTrue(true);
                }

                var errors = UnitOfWork.ValidateEntities();
                errors.First().ValidationErrors.First();
                Assert.IsTrue(true);
                #endregion
            }
            {
                #region 校验未通过-UserName已存在
                var entity = new AuthUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "cheng.zhang",
                    Password = "123456",
                    RealName = "测试1号"
                };
                UnitOfWork.Add(entity);
                try
                {
                    int cnt = UnitOfWork.SaveChanges();
                    Assert.IsTrue(false);
                }
                catch (DbEntityValidationException ex)
                {
                    var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                    string s = string.Format(
                                         "Entity Property: {0}, Error: {1}",
                                         error.PropertyName,
                                         error.ErrorMessage);
                    Assert.IsNotNull(s);
                }

                var errors = UnitOfWork.ValidateEntities();
                Assert.IsTrue(errors.Count() > 0);
                #endregion
            }
        }

        [TestMethod]
        public void UnitOfWork_Tracking()
        {
            using (ReportOuput output = new ReportOuput())
            {//AsTracking,Entity的状态为EntityState.Unchanged
                var entity = UnitOfWork.AsTracking<AuthUser>().First();
                var state = UnitOfWork.GetEntityState(entity);
                Assert.IsTrue(state == EntityState.Unchanged);
            }
            
            using (ReportOuput output = new ReportOuput())
            {//AsNoTracking后,Entity的状态为EntityState.Detached
                var entity = UnitOfWork.AsNoTracking<AuthUser>().First();
                var state = UnitOfWork.GetEntityState(entity);
                Assert.IsTrue(state == EntityState.Detached);
            }
        }

        [TestMethod]
        public void UnitOfWork_GetPage()
        {
            var query = new UserQuery();
            query.UserNameLike = "cheng";
            query.RealNameLike = "成";
            using (ReportOuput output = new ReportOuput())
            {
                int iTotal;
                UnitOfWork.AsNoTracking<AuthUser>().GetPage(query, out iTotal).Select(o => new { o.RealName, o.UserName }).ToList();
                UnitOfWork.AsTracking<AuthUser>().GetPage(query, out iTotal).Select(o => new { o.RealName, o.UserName }).ToList();
            }
            Assert.IsTrue(true);
        }
    }
}
