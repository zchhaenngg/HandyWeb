using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Model;
using HandyWork.Common.EntityFramework.Lambdas;
using HandyWork.Model.Query;
using HandyWork.DAL;
using System.Collections.Generic;

namespace HandyWork.Commom.UnitTests
{
    [TestClass]
    public class ContainLambdaTest
    {
        private UnitOfWork _unitOfWork;
        protected UnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork = new UnitOfWork());

        [TestMethod]
        public void ContainLambda_Build()
        {
            var lambda = new ContainLambda<User, string>(o => o.UserName, new List<string> { "cheng.zhang" });
            var expression = lambda.Build();

            lambda = new ContainLambda<User, string>(o => o.UserName, new int?[] { 1,null,2 });
            expression = lambda.Build();

            var lambda3 = new ContainLambda<User, int>(o => o.LoginFailedCount, new int?[] { 1, null, 2 });
            expression = lambda3.Build();

            var str = expression.Name;
        }
    }
}
