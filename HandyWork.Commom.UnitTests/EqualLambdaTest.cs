using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Model;
using HandyWork.Common.EntityFramwork.Lambdas;
using HandyWork.Model.Query;
using HandyWork.DAL;

namespace HandyWork.Commom.UnitTests
{
    [TestClass]
    public class EqualLambdaTest
    {
        private UnitOfWork _unitOfWork;
        protected UnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork = new UnitOfWork());

        [TestMethod]
        public void Build()
        {
            var equalLambda = new EqualLambda<User, string>(o => o.UserName, "cheng.zhang");
            var expression = equalLambda.Build();
            var page = UnitOfWork.UserRepository.GetPage(new UserQuery(), expression);
            Assert.AreEqual(page.Item2, 1);

            var equalLambda2 = new EqualLambda<User, DateTime?>(o => o.LastLoginFailedTime, null);
            expression = equalLambda2.Build();
            page = UnitOfWork.UserRepository.GetPage(new UserQuery(), expression);
            Assert.IsTrue(page.Item2 > 0);
        }
    }
}
