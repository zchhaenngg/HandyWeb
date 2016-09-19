using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.DAL;
using HandyWork.Common.EntityFramework.Lambdas;
using HandyWork.Model;
using HandyWork.Model.Query;

namespace HandyWork.Commom.UnitTests
{
    [TestClass]
    public class LessThanLambdaTest
    {
        private UnitOfWork _unitOfWork;
        protected UnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork = new UnitOfWork());

        [TestMethod]
        public void LessThanLambda_Build()
        {
            var equalLambda = new LessThanLambda<User, int>(o => o.LoginFailedCount, 1);
            var expression = equalLambda.Build();
        }
    }
}
