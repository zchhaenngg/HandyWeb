using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Common.Extensions;

namespace HandyWork.Commom.UnitTests.Extensions
{
    [TestClass]
    public class StringTest
    {
        [TestMethod]
        public void String_ToUTCFromLogic()
        {
            string logic = "2016-10-12 08:15";
            var time = logic.ToUTCFromLogic(20);
            Assert.IsTrue(time.Value.Kind == DateTimeKind.Utc);
            Assert.IsTrue(time.Value == new DateTime(2016, 10, 12, 7, 55, 0));
            Assert.IsTrue("".ToUTCFromLogic(20) == null);
        }
    }
}
