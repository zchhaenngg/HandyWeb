using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.DAL;
using HandyWork.Common.EntityFramwork.Lambdas;
using HandyWork.Model;
using HandyWork.Model.Query;

namespace HandyWork.Commom.UnitTests
{
    [TestClass]
    public class LessThanOrEqualLambdaTest
    {
        private UnitOfWork _unitOfWork;
        protected UnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork = new UnitOfWork());

        [TestMethod]
        public void LessThanOrEqualLambda_Build()
        {
            var equalLambda = new LessThanOrEqualLambda<User, int>(o => o.LoginFailedCount, 1);
            var expression = equalLambda.Build();
        }
    }
}
