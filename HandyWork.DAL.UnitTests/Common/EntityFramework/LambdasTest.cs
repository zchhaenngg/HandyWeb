using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Model;
using HandyWork.Common.EntityFramework.Lambdas;
using HandyWork.DAL;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

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
            var parameter = Expression.Parameter(typeof(AuthUser), "o");
            var member = Expression.Property(parameter, "AccessFailedCount");
            List<int> list1 = new List<int> { 1, 2 };
            var body = Expression.Call(Expression.Constant(list1), list1.GetType().GetMethod("Contains"), member);
            var xx = Expression.Lambda<Func<AuthUser, bool>>(body, parameter);

            var list = new List<AuthUser> { new AuthUser { UserName = "cheng.zhang" } };

            LambdaUtility<AuthUser>.GetContainLambda(o => o.UserName, new List<string> { "cheng.zhang" }).Build<AuthUser>();

            LambdaUtility<AuthUser>.GetContainLambda(o => o.UserName, new int?[] { 1, null, 2 }).Build<AuthUser>();

            LambdaUtility<AuthUser>.GetContainLambda(o => o.AccessFailedCount, new int?[] { 1, null, 2 });

            LambdaUtility<AuthUser>.GetContainLambda(o => o.AccessFailedCount, new int?[] { 1, null, 2 }).Build<AuthUser>();
            {
                var exp = LambdaUtility<AuthUser>.GetContainLambda(o => o.UserName, new List<string> { "cheng.zhang" }).Build<AuthUser>();
                var entity = list.Where(exp.Compile()).First();
                Assert.IsTrue(entity.UserName == "cheng.zhang");
            }
        }

        [TestMethod]
        public void Lambda_Equal()
        {
            LambdaUtility<AuthUser>.GetEqualLambda(o => o.UserName, "cheng.zhang").Build<AuthUser>();
            LambdaUtility<AuthUser>.GetEqualLambda(o => o.LockoutEndDateUtc, DateTime.Now).Build<AuthUser>();
        }

        [TestMethod]
        public void Lambda_GreaterThan()
        {
            Expression<Func<AuthUser, bool>> expression = o => o.CreatedTime > DateTime.Now;

            LambdaUtility<AuthUser>.GetGreaterThanLambda(o => o.AccessFailedCount, 1).Build<AuthUser>();
            LambdaUtility<AuthUser>.GetGreaterThanLambda(o => o.CreatedTime, DateTime.Now).Build<AuthUser>();
        }

        [TestMethod]
        public void Lambda_GreaterThanOrEqual()
        {
            
            var equalLambda = LambdaUtility<AuthUser>.GetGreaterThanOrEqualLambda(o => o.AccessFailedCount, 1);
            var expression = equalLambda.Build<AuthUser>();
        }

        [TestMethod]
        public void Lambda_LessThan()
        {
            var equalLambda = LambdaUtility<AuthUser>.GetLessThanLambda(o => o.AccessFailedCount, 1);
            var expression = equalLambda.Build<AuthUser>();
        }

        [TestMethod]
        public void LambdaLessThanOrEqual()
        {
            var equalLambda = LambdaUtility<AuthUser>.GetLessThanOrEqualLambda(o => o.AccessFailedCount, 1);
            var expression = equalLambda.Build<AuthUser>();
        }

        [TestMethod]
        public void Lambda_Like()
        {
            var lambda = LambdaUtility<AuthUser>.GetLikeLambda(o => o.UserName, "cheng");
            var expression = lambda.Build<AuthUser>();
        }

        [TestMethod]
        public void Lambda_NotEqual()
        {
            
            var lambda = LambdaUtility<AuthUser>.GetNotEqualLambda(o => o.UserName, "cheng.zhang");
            var expression = lambda.Build<AuthUser>();

            var lambda2 = LambdaUtility<AuthUser>.GetNotEqualLambda(o => o.LockoutEndDateUtc, null);
            expression = lambda2.Build<AuthUser>();
        }
    }
}
