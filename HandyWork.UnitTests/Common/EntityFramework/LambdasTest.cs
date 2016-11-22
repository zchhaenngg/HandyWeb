using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Model;
using HandyWork.Common.EntityFramework.Lambdas;
using HandyWork.DAL;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using HandyWork.Common.EntityFramework.Query;
using HandyWork.Common.EntityFramework.Elements;

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
         
            {
                var factory = new LambdaFactory<AuthUser>().IfContain(IsTrue.Init, o => o.UserName, new List<string> { "cheng.zhang" });
                var exp = factory.ToExpression();

            }
            {
                var factory = new LambdaFactory<AuthUser>().IfContain(IsTrue.Init, o => o.UserName, new int?[] { 1, null, 2 });
                var exp = factory.ToExpression();
            }
            {
                var factory = new LambdaFactory<AuthUser>().IfContain(IsTrue.Init, o => o.AccessFailedCount, new int?[] { 1, null, 2 });
                var exp = factory.ToExpression();
            }
        }

        [TestMethod]
        public void Lambda_Equal()
        {
            {
                var factory = new LambdaFactory<AuthUser>().IfEqual(IsTrue.Init, o => o.UserName, "cheng.zhang");
                var exp = factory.ToExpression();
            }
            {
                var factory = new LambdaFactory<AuthUser>().IfEqual(IsTrue.Init, o => o.LockoutEndDateUtc, DateTime.Now);
                var exp = factory.ToExpression();
            }
            {
                var factory = new LambdaFactory<AuthUser>().IfEqual(IsTrue.Init, o => o.LockoutEndDateUtc, null);
                var exp = factory.ToExpression();
            }
        }

        [TestMethod]
        public void Lambda_GreaterThan()
        {
            {
                var factory = new LambdaFactory<AuthUser>().IfGreaterThan(IsTrue.Init, o => o.AccessFailedCount, 1);
                var exp = factory.ToExpression();
            }
            {
                var factory = new LambdaFactory<AuthUser>().IfGreaterThan(IsTrue.Init, o => o.CreatedTime, DateTime.Now);
                var exp = factory.ToExpression();
            }
        }

        [TestMethod]
        public void Lambda_GreaterThanOrEqual()
        {
            {
                var factory = new LambdaFactory<AuthUser>().IfGreaterThanOrEqual(IsTrue.Init, o => o.AccessFailedCount, 1);
                var exp = factory.ToExpression();
            }
        }

        [TestMethod]
        public void Lambda_LessThan()
        {
            {
                var factory = new LambdaFactory<AuthUser>().IfLessThan(IsTrue.Init, o => o.AccessFailedCount, 1);
                var exp = factory.ToExpression();
            }
        }

        [TestMethod]
        public void LambdaLessThanOrEqual()
        {
            {
                var factory = new LambdaFactory<AuthUser>().IfLessThanOrEqual(IsTrue.Init, o => o.AccessFailedCount, 1);
                var exp = factory.ToExpression();
            }
        }

        [TestMethod]
        public void Lambda_Like()
        {
            {
                var factory = new LambdaFactory<AuthUser>().IfLike(IsTrue.Init, o => o.UserName, "cheng");
                var exp = factory.ToExpression();
            }
        }

        [TestMethod]
        public void Lambda_NotEqual()
        {
            {
                var factory = new LambdaFactory<AuthUser>().IfEqual(IsTrue.Init, o => o.UserName, "cheng");
                var exp = factory.ToExpression();
            }
        }
    }
}
