using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Common.Extensions;

namespace HandyWork.UnitTests.Common.Extensions
{
    [TestClass]
    public class TypeTest
    {
        [TestMethod]
        public void Type_IsNullable()
        {
            Assert.IsTrue(typeof(DateTime?).IsNullable());
            Assert.IsTrue(typeof(object).IsNullable());
            Assert.IsFalse(typeof(DateTime).IsNullable());
        }
    }
}
