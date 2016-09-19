using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Common.EntityFramework.Lambdas;
using HandyWork.Model;
using HandyWork.Model.Query;
using HandyWork.DAL;

namespace HandyWork.Commom.UnitTests
{
    [TestClass]
    public class GreaterThanLambdaTest
    {
        private UnitOfWork _unitOfWork;
        protected UnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork = new UnitOfWork());

        [TestMethod]
        public void GreaterThanLambda_Build()
        {
            var equalLambda = new GreaterThanLambda<User, int>(o => o.LoginFailedCount, 1);
            var expression = equalLambda.Build();
        }
    }
}
