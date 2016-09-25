using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HandyWork.Model.UnitTest
{
    [TestClass]
    public class EntityContextTest
    {
        [TestMethod]
        public void EntityContext_Initial()
        {
            using (EntityContext context = new EntityContext())
            {
                context.Database.Initialize(false);
            }
        }
    }
}
