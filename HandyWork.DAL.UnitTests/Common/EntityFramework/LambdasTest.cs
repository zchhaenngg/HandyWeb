using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Model;
using HandyWork.Common.EntityFramework.Lambdas;
using HandyWork.DAL;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HandyWork.UnitTests.Common.EntityFramework
{
    /// <summary>
    /// LambdasTest 的摘要说明
    /// </summary>
    [TestClass]
    public class LambdasTest
    {
        private UnitOfWork _unitOfWork;
        protected UnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork = new UnitOfWork("-1"));

        [TestMethod]
        public void Lambda_Contain()
        {
            new ContainLambda<AuthUser, string>(o => o.UserName, new List<string> { "cheng.zhang" }).Build();

            new ContainLambda<AuthUser, string>(o => o.UserName, new int?[] { 1, null, 2 }).Build();

            var expression = new ContainLambda<AuthUser, int>(o => o.AccessFailedCount, new int?[] { 1, null, 2 }).Build();
        }

        [TestMethod]
        public void Lambda_Equal()
        {
            new EqualLambda<AuthUser, string>(o => o.UserName, "cheng.zhang").Build();
            new EqualLambda<AuthUser, DateTime?>(o => o.LockoutEndDateUtc, DateTime.Now).Build();
        }

        [TestMethod]
        public void Lambda_GreaterThan()
        {
            Expression<Func<AuthUser, bool>> expression = o => o.CreatedTime > DateTime.Now;

            new GreaterThanLambda<AuthUser, int>(o => o.AccessFailedCount, 1).Build();
            new GreaterThanLambda<AuthUser, DateTime?>(o => o.CreatedTime, DateTime.Now).Build();
        }

        [TestMethod]
        public void Lambda_GreaterThanOrEqual()
        {
            var equalLambda = new GreaterThanOrEqualLambda<AuthUser, int>(o => o.AccessFailedCount, 1);
            var expression = equalLambda.Build();
        }

        [TestMethod]
        public void Lambda_LessThan()
        {
            var equalLambda = new LessThanLambda<AuthUser, int>(o => o.AccessFailedCount, 1);
            var expression = equalLambda.Build();
        }

        [TestMethod]
        public void LambdaLessThanOrEqual()
        {
            var equalLambda = new LessThanOrEqualLambda<AuthUser, int>(o => o.AccessFailedCount, 1);
            var expression = equalLambda.Build();
        }

        [TestMethod]
        public void Lambda_Like()
        {
            var lambda = new LikeLambda<AuthUser>(o => o.UserName, "cheng");
            var expression = lambda.Build();
        }

        [TestMethod]
        public void Lambda_NotEqual()
        {
            var lambda = new NotEqualLambda<AuthUser, string>(o => o.UserName, "cheng.zhang");
            var expression = lambda.Build();

            var lambda2 = new NotEqualLambda<AuthUser, DateTime?>(o => o.LockoutEndDateUtc, null);
            expression = lambda2.Build();
        }
    }
}
