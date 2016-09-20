using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HandyWork.Common.EntityFramework.Elements;

namespace HandyWork.Commom.UnitTests.EntityFramework
{
    [TestClass]
    public class ElementsTest
    {
        [TestMethod]
        public void ElementsTest_IsEmpty()
        {
            //数组、集合、值类型、string
            var isEmpty = new IsEmpty(new int[] { 1, 2, 3, 4, 5 });
            Assert.IsFalse(isEmpty.IsPassed());

            var isEmpty2 = new IsEmpty(new List<short> { 1, 2, 3, 4, 5, 6 });
            Assert.IsFalse(isEmpty2.IsPassed());

            var isEmpty3 = new IsEmpty(DateTime.Now);
            Assert.IsFalse(isEmpty3.IsPassed());

            var isEmpty4 = new IsEmpty("abcd");
            Assert.IsFalse(isEmpty4.IsPassed());

            var isEmpty5 = new IsEmpty(null);
            Assert.IsTrue(isEmpty5.IsPassed());
            
            var isEmpty6 = new IsEmpty(new object());
            Assert.IsFalse(isEmpty6.IsPassed());

            var isEmpty7 = new IsEmpty(0);
            Assert.IsTrue(isEmpty7.IsPassed());

            var isEmpty8 = new IsEmpty(DateTime.MinValue);
            Assert.IsTrue(isEmpty7.IsPassed());
        }
    }
}
