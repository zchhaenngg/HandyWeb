using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Model.Query;
using HandyWork.Common.Utility;
using HandyWork.Model;
using HandyWork.Common.Consts;
using HandyWork.DAL;
using HandyWork.Common.Extensions;
using System.Linq.Expressions;

namespace HandyWork.Commom.UnitTests
{
    [TestClass]
    public class ExpressionUtilityTest
    {
        private UnitOfWork _unitOfWork;
        protected UnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork = new UnitOfWork());

        [TestMethod]
        public void BuildEqual()
        {
            UserQuery query = new UserQuery
            {
                UserNameEqual = "cheng.zhang"
            };
            var expression = ExpressionUtility.Build<User,string>(o => o.UserName, query.UserNameEqual, QueryCondition.IsNotNullOrWhiteSpace, QueryMethod.Equal);
            var page = UnitOfWork.UserRepository.GetPage(query, expression);
            Assert.AreEqual(page.Item2, 1);
        }
    }
}
