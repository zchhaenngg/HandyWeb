using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Common.ExternLibs.EPPlus;

namespace HandyWork.Commom.UnitTests
{
    [TestClass]
    public class EPPlusUtilityTest
    {
        [TestMethod]
        public void Save()
        {
            EPPlusUtility.Save("d://aaa.xlsx", sheets => 
            {
                var sheet = sheets.Add("testSave");
                EPPlusUtility.WriteRow(sheet, 1, 1, DateTime.Now, DateTime.Now.AddDays(1));
                EPPlusUtility.WriteRow(sheet, 1, 2, DateTime.Now, DateTime.Now.AddDays(1), 13);
                sheet.Cells.AutoFitColumns(8, 18);
            });
        }
    }
}
