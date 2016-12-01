using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.ViewModel.PCWeb.Query;
using HandyWork.DAL.Queryable;
using HandyWork.Model;
using HandyWork.Model.Entity;

namespace HandyWork.UnitTests.Queryable
{
    [TestClass]
    public class MappingTest
    {
        [TestMethod]
        public void Mapping_GetExpression()
        {
            var query = new UserQuery();
            query.UserNameLike = "cheng";
            query.RealNameLike = "成";
            var where = Mapping.GetExpression<hy_user, UserQuery>(query);
            var where2 = Mapping.GetExpression<hy_user>(query);
            Assert.IsTrue(true);
        }
    }
}
