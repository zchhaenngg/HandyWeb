using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Common.Extensions;

namespace HandyWork.UnitTests.Common.Extensions
{
    [TestClass]
    public class DateTimeTest
    {
        [TestMethod]
        public void DateTimeExtension_Test()
        {
            {//dt-dt2
                DateTime? d1 = DateTime.Now;
                DateTime? d2 = d1.Value.AddDays(-2).AddMinutes(1);
                var m = d1.SubtractForHours(d2);
                var m1 = (d2 - d1).Value.Hours;
                Assert.IsTrue(m == 48);

                d2 = d1.Value.AddDays(-2).AddMinutes(-30);
                m = d1.SubtractForHours(d2);
                m1 = (d2 - d1).Value.Hours;
                Assert.IsTrue(m == 49);
            }
            {
                DateTime t1 = Convert.ToDateTime("2016-10-01 15:21:35");
                Assert.IsTrue(t1.ToDayMax() == Convert.ToDateTime("2016-10-01 23:59:59.999999"));
                DateTime? t2 = null;
                Assert.IsTrue(t2.ToDayMax() == null);
            }
            {
                var now = DateTime.Now;
                Assert.IsTrue(now.ToUTC(480) == now.ToUniversalTime());
            }
            {
                var utcnow = DateTime.UtcNow;
                Assert.IsTrue(utcnow.ToLocalTime() == utcnow.ToLogic(480));
            }
        }
    }
}
