using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandyWork.Common.ExternLibs.OpenXml;
using HandyWork.Common.ExternLibs.OpenXml.Extensions;

namespace HandyWork.Commom.UnitTests
{
    [TestClass]
    public class WordUtilityTest
    {
        [TestMethod]
        public void OpenPackage()
        {

            WordUtility.OpenPackage(@"D:\handyUpload\个人简历.docx", @"D:\handyUpload\testOpenPackage.docx", document =>
            {
                var body = document.MainDocumentPart;
                
            });
        }
    }
}
