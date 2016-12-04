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
using HandyWork.Model.Entity;

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
                var factory = new LambdaFactory<hy_user>().IfContain(IsTrue.Init, o => o.user_name, new List<string> { "cheng.zhang" });
                var exp = factory.ToExpression();

            }
            {
                var factory = new LambdaFactory<hy_user>().IfContain(IsTrue.Init, o => o.user_name, new int?[] { 1, null, 2 });
                var exp = factory.ToExpression();
            }
            {
                var factory = new LambdaFactory<hy_user>().IfContain(IsTrue.Init, o => o.access_failed_times, new int?[] { 1, null, 2 });
                var exp = factory.ToExpression();
            }
        }

        [TestMethod]
        public void Lambda_Equal()
        {
            {
                var factory = new LambdaFactory<hy_user>().IfEqual(IsTrue.Init, o => o.user_name, "cheng.zhang");
                var exp = factory.ToExpression();
            }
            {
                var factory = new LambdaFactory<hy_user>().IfEqual(IsTrue.Init, o => o.locked_time, DateTime.Now);
                var exp = factory.ToExpression();
            }
            {
                var factory = new LambdaFactory<hy_user>().IfEqual(IsTrue.Init, o => o.locked_time, null);
                var exp = factory.ToExpression();
            }
        }

        [TestMethod]
        public void Lambda_GreaterThan()
        {
            {
                var factory = new LambdaFactory<hy_user>().IfGreaterThan(IsTrue.Init, o => o.access_failed_times, 1);
                var exp = factory.ToExpression();
            }
            {
                var factory = new LambdaFactory<hy_user>().IfGreaterThan(IsTrue.Init, o => o.created_time, DateTime.Now);
                var exp = factory.ToExpression();
            }
        }

        [TestMethod]
        public void Lambda_GreaterThanOrEqual()
        {
            {
                var factory = new LambdaFactory<hy_user>().IfGreaterThanOrEqual(IsTrue.Init, o => o.access_failed_times, 1);
                var exp = factory.ToExpression();
            }
        }

        [TestMethod]
        public void Lambda_LessThan()
        {
            {
                var factory = new LambdaFactory<hy_user>().IfLessThan(IsTrue.Init, o => o.access_failed_times, 1);
                var exp = factory.ToExpression();
            }
        }

        [TestMethod]
        public void LambdaLessThanOrEqual()
        {
            {
                var factory = new LambdaFactory<hy_user>().IfLessThanOrEqual(IsTrue.Init, o => o.access_failed_times, 1);
                var exp = factory.ToExpression();
            }
        }

        [TestMethod]
        public void Lambda_Like()
        {
            {
                var factory = new LambdaFactory<hy_user>().IfLike(IsTrue.Init, o => o.user_name, "cheng");
                var exp = factory.ToExpression();
            }
        }

        [TestMethod]
        public void Lambda_NotEqual()
        {
            {
                var factory = new LambdaFactory<hy_user>().IfEqual(IsTrue.Init, o => o.user_name, "cheng");
                var exp = factory.ToExpression();
            }
        }
    }
}
