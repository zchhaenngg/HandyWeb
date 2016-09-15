using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Common.EntityFramwork.Lambdas;
using HandyWork.Model;
using HandyWork.Model.Query;
using HandyWork.DAL;

namespace HandyWork.Commom.UnitTests
{
    [TestClass]
    public class NotEqualLambdaTest
    {
        private UnitOfWork _unitOfWork;
        protected UnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork = new UnitOfWork());

        [TestMethod]
        public void NotEqualLambda_Build()
        {
            var lambda = new NotEqualLambda<User, string>(o => o.UserName, "cheng.zhang");
            var expression = lambda.Build();
            var page = UnitOfWork.UserRepository.GetPage(new UserQuery(), expression);
            Assert.IsTrue(page.Item1.TrueForAll(u => u.UserName != "cheng.zhang"));

            var lambda2 = new NotEqualLambda<User, DateTime?>(o => o.LastLoginFailedTime, null);
            expression = lambda2.Build();
            page = UnitOfWork.UserRepository.GetPage(new UserQuery(), expression);
            Assert.IsTrue(page.Item1.TrueForAll(u => u.LastLoginFailedTime!=null));
        }
    }
}
