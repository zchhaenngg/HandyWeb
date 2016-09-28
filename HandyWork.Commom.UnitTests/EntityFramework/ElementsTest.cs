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
            int i = Convert.ToInt16(true);
            int i2 = Convert.ToInt16(false);

            int i3 = Convert.ToInt32(true);
            int i4 = Convert.ToInt32(false);

            bool i5 = Convert.ToBoolean(1);
            bool i6 = Convert.ToBoolean(0);
            bool i7 = Convert.ToBoolean(2);

            //数组、集合、值类型、string
            Assert.IsFalse(IsEmpty.For(new int[] { 1, 2, 3, 4, 5 }).IsPassed);
            Assert.IsFalse(IsEmpty.For(new List<short> { 1, 2, 3, 4, 5, 6 }).IsPassed);
            
            Assert.IsFalse(IsEmpty.For(DateTime.Now).IsPassed);
            Assert.IsFalse(IsEmpty.For("abcd").IsPassed);
            Assert.IsTrue(IsEmpty.For(null).IsPassed);
            Assert.IsFalse(IsEmpty.For(new object()).IsPassed);
            Assert.IsTrue(IsEmpty.For(0).IsPassed);
            Assert.IsTrue(IsEmpty.For(DateTime.MinValue).IsPassed);

            Assert.IsFalse(IsNotEmpty.For(DateTime.MinValue).IsPassed);

            Assert.IsFalse(IsEmpty.For(false).IsPassed);
            Assert.IsTrue(IsNotEmpty.For(false).IsPassed);

            Assert.IsTrue(IsEmpty.For("").IsPassed);
            Assert.IsFalse(IsNotEmpty.For("").IsPassed);
        }
    }
}
