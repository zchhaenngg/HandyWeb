using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Model;
using HandyWork.Common.EntityFramework.Elements;
using HandyWork.Common.EntityFramework.Lambdas;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Common.Extensions;
using HandyWork.Common.Utility;

namespace HandyWork.UnitTests.Common.Utility
{
    [TestClass]
    public class ExpressionUtilityTest
    {
        [TestMethod]
        public void GetLambdaExpressionOfProperty()
        {
            Expression<Func<ExpressionUtilityTest_a, bool>> expression = c => c.b.name == "aaa";
            var expression2 = ExpressionUtility.GetLambdaExpressionOfProperty<ExpressionUtilityTest_a>("b.name");
            Assert.IsTrue(true);
        }
    }

    public class ExpressionUtilityTest_a
    {
        public ExpressionUtilityTest_b b { get; set; }

        public List<ExpressionUtilityTest_b> bs { get; set; }
    }
    public class ExpressionUtilityTest_b
    {
        public string name { get; set; }
    }
}
