using HandyWork.Common.Utility;
using HandyWork.DAL.Queryable;
using HandyWork.Model;
using HandyWork.UIBusiness.Utility;
using HandyWork.ViewModel.PCWeb.Query;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Profiling;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
namespace HandyWork.UnitTests.Common.Utility
{
    [TestClass]
    public class MyUrlUtilityTest : BaseTest
    {
        [TestMethod]
        public void MyUrlUtility_GetActionLink()
        {
            Assert.IsTrue("主页" == MyUrlUtility.GetActionLink(Urls.HomeIndex).LinkText);
        }
    }
}
