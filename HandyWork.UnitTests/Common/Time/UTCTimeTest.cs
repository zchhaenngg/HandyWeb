using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Common.Time;

namespace HandyWork.UnitTests.Common.Time
{
    [TestClass]
    public class UTCTimeTest
    {
        [TestMethod]
        public void UTCTime_Test()
        {
            {//1.test是否为值类型 
                UTCTime time = new UTCTime { Value = DateTime.Now };
                UTCTime time2 = time;

                time2.Value = DateTime.Now;
                time2.Value = DateTime.MinValue;
                Assert.IsTrue(time.Value != time2.Value);
            }

            {//2.test可空类型
                UTCTime? t = null;
                Assert.IsFalse(t.HasValue);
                t = DateTime.Now;
                Assert.IsTrue(t.HasValue);
            }

            {//3.test非可识别的时间类型转换成UTC时间应报错。
                try
                {
                    UTCTime t2 = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    Assert.IsTrue(false);
                }
                catch (InvalidCastException)
                {
                    Assert.IsTrue(true);
                }
            }
            {//4.test初试时间的时间类型
                UTCTime ut = new UTCTime();
                Assert.IsTrue(ut.Value.Kind == DateTimeKind.Utc);
            }

            {//5.datetime->utctime
                UTCTime ut = DateTime.Now;
                UTCTime? ut2 = DateTime.Now;

                DateTime? time = DateTime.Now;
                UTCTime ut3 = (UTCTime)time;

                try
                {
                    UTCTime ut4 = (UTCTime)null;
                    Assert.IsTrue(false);
                }
                catch (InvalidCastException)
                {
                    Assert.IsTrue(true);
                }
            }
            {//6.utctime->datetime
                DateTime dt = UTCTime.Now;
                DateTime? dt2 = UTCTime.Now;
                UTCTime? ut = UTCTime.Now;
                DateTime dt3 = (DateTime)ut;

                try
                {
                    ut = null;
                    DateTime dt4 = (DateTime)ut;
                }
                catch (InvalidCastException)
                {
                    Assert.IsTrue(true);
                }
            }

        }

        [TestMethod]
        public void UTCTime_Parse()
        {
            string s = "2016-09-27";
            DateTime t;
            if (DateTime.TryParse(s, out t))
            {
                string tstr = t.ToString(Formats.ToDayMax);
                DateTime t2 = Convert.ToDateTime(tstr);

            }
        }
    }
}
