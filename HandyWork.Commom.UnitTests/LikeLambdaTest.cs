using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Model;
using HandyWork.Common.EntityFramwork.Lambdas;
using HandyWork.Model.Query;
using HandyWork.DAL;

namespace HandyWork.Commom.UnitTests
{
    [TestClass]
    public class LikeLambdaTest
    {
        private UnitOfWork _unitOfWork;
        protected UnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork = new UnitOfWork());

        [TestMethod]
        public void LikeLambda_Build()
        {
            var lambda = new LikeLambda<User>(o => o.UserName, "cheng");
            var expression = lambda.Build();
        }
    }
}
