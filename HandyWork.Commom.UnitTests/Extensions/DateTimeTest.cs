using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Common.Extensions;

namespace HandyWork.Commom.UnitTests.Extensions
{
    [TestClass]
    public class DateTimeTest
    {
        [TestMethod]
        public void DateTimeExtension_Test()
        {
            //IsToday
            {
                Assert.IsTrue(DateTime.Now.IsToday());
                Assert.IsFalse(DateTime.Now.AddDays(1).IsToday());
            }
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
        }
    }
}
