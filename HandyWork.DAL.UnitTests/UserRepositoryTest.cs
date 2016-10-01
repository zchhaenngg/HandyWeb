using HandyWork.Model;
using HandyWork.ViewModel.PCWeb.Query;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
        public void AddUser1()
        {
            var entity = new AuthUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "test".PadLeft(51,'1'),
                Password = "123456",
                RealName = "测试1号"
            };
            UnitOfWork.UserRepository.Add(entity, null);
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
            foreach (var validationResults in errors)
            {
                foreach (var error in validationResults.ValidationErrors)
                {
                    string s = string.Format(
                                      "Entity Property: {0}, Error: {1}",
                                      error.PropertyName,
                                      error.ErrorMessage);
                    Assert.IsNotNull(s);
                }
            }
        }
        [TestMethod]
        public void AddUser2()
        {
            var entity = new AuthUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "cheng.zhang",
                Password = "123456",
                RealName = "测试1号"
            };
            UnitOfWork.UserRepository.Add(entity, null);
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
        }
    }
}
