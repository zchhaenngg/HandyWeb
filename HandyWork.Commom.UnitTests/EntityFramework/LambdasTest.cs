using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Model;
using HandyWork.Common.EntityFramework.Lambdas;
using HandyWork.Model.Query;
using HandyWork.DAL;
using System.Collections.Generic;
using System.Linq.Expressions;
using HandyWork.Common.Extensions;

namespace HandyWork.Commom.UnitTests.EntityFramework
{
    /// <summary>
    /// LambdasTest 的摘要说明
    /// </summary>
    [TestClass]
    public class LambdasTest
    {
        private UnitOfWork _unitOfWork;
        protected UnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork = new UnitOfWork());

        [TestMethod]
        public void Lambda_Contain()
        {
            new ContainLambda<User, string>(o => o.UserName, new List<string> { "cheng.zhang" }).Build();

            new ContainLambda<User, string>(o => o.UserName, new int?[] { 1, null, 2 }).Build();

            var expression = new ContainLambda<User, int>(o => o.LoginFailedCount, new int?[] { 1, null, 2 }).Build();
        }

        [TestMethod]
        public void Lambda_Equal()
        {
            new EqualLambda<User, string>(o => o.UserName, "cheng.zhang").Build();
            new EqualLambda<User, DateTime?>(o => o.LastLoginFailedTime, DateTime.Now).Build();
        }

        [TestMethod]
        public void Lambda_GreaterThan()
        {
            Expression<Func<User,bool>> expression = o => o.CreatedTime> DateTime.Now;

            new GreaterThanLambda<User, int>(o => o.LoginFailedCount, 1).Build();
            new GreaterThanLambda<User, DateTime?>(o => o.CreatedTime, DateTime.Now).Build();
        }

        [TestMethod]
        public void Lambda_GreaterThanOrEqual()
        {
            var equalLambda = new GreaterThanOrEqualLambda<User, int>(o => o.LoginFailedCount, 1);
            var expression = equalLambda.Build();
        }

        [TestMethod]
        public void Lambda_LessThan()
        {
            var equalLambda = new LessThanLambda<User, int>(o => o.LoginFailedCount, 1);
            var expression = equalLambda.Build();
        }

        [TestMethod]
        public void LambdaLessThanOrEqual()
        {
            var equalLambda = new LessThanOrEqualLambda<User, int>(o => o.LoginFailedCount, 1);
            var expression = equalLambda.Build();
        }

        [TestMethod]
        public void Lambda_Like()
        {
            var lambda = new LikeLambda<User>(o => o.UserName, "cheng");
            var expression = lambda.Build();
        }

        [TestMethod]
        public void Lambda_NotEqual()
        {
            var lambda = new NotEqualLambda<User, string>(o => o.UserName, "cheng.zhang");
            var expression = lambda.Build();

            var lambda2 = new NotEqualLambda<User, DateTime?>(o => o.LastLoginFailedTime, null);
            expression = lambda2.Build();
        }
    }
}
