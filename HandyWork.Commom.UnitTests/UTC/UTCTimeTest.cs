using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Common.UTC;

namespace HandyWork.Commom.UnitTests.UTC
{
    [TestClass]
    public class UTCTimeTest
    {
        [TestMethod]
        public void UTCTime_Test()
        {
            //UTCTime ut = null;  编译失败
            UTCTime time = new UTCTime();
            var a = time.Value;

            UTCTime time2 = time;
            time2.Value = DateTime.Now;
            //1.测试是否为值类型
            Assert.IsTrue(time.Value != time2.Value);
            //2.测试可空类型
            UTCTime? t = null;
            Assert.IsFalse(t.HasValue);
            t = DateTime.Now;
            Assert.IsTrue(t.HasValue);
            //3.测试非可识别的时间类型转换成UTC时间应报错。
            try
            {
                UTCTime t2 = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                Assert.IsTrue(false);
            }
            catch (InvalidCastException ex)
            {
                Assert.IsTrue(true);
            }
            //test初试时间的时间类型
            UTCTime ut = new UTCTime();
            Assert.IsTrue(ut.Value.Kind == DateTimeKind.Utc);
        }

        //[TestMethod]
        //public void test()
        //{
        //    var zones = TimeZoneInfo.GetSystemTimeZones();
        //}
    }
    
}
