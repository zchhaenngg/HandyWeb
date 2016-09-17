using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Common.EntityFramwork.Elements;
using System.Collections.Generic;

namespace HandyWork.Commom.UnitTests
{
    [TestClass]
    public class IsEmptyTest
    {
        [TestMethod]
        public void IsCondition()
        {
            //数组、集合、值类型、string
            var intArr = new int[] { 1,2,3,4,5 };
            var list = new List<short> { 1, 2, 3, 4, 5, 6 };
            var now = DateTime.Now;
            string str = "abcd";

            var isEmpty = new IsEmpty(intArr);
            var testResult = isEmpty.IsPassed();
            Assert.IsFalse(testResult);

            var isEmpty2 = new IsEmpty(list);
            testResult = isEmpty2.IsPassed();
            Assert.IsFalse(testResult);

            var isEmpty3 = new IsEmpty(now);
            testResult = isEmpty3.IsPassed();
            Assert.IsFalse(testResult);

            var isEmpty4 = new IsEmpty(str);
            testResult = isEmpty4.IsPassed();
            Assert.IsFalse(testResult);

            var isEmpty5 = new IsEmpty(null);
            testResult = isEmpty5.IsPassed();
            Assert.IsTrue(testResult);

            var isEmpty6 = new IsEmpty(new object());
            try
            {
                isEmpty6.IsPassed();
                Assert.IsTrue(false);
            }
            catch (NotSupportedException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
