using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Model;

namespace HandyWork.UnitTests.Model
{
    [TestClass]
    public class EntityContextTest
    {
        [TestMethod]
        public void EntityContext_Initial()
        {
            using (var context = new EntityContext())
            {
                context.Database.Initialize(false);
            }
        }
    }
}
