using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Model;
using HandyWork.Common.EntityFramwork.Lambdas;
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
        }
    }
}
