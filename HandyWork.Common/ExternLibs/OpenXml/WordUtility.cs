using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.ExternLibs.OpenXml
{
    public static class WordUtility
    {
        public static void OpenPackage(string templateDocPath, string docPath, Action<WordprocessingDocument> write)
        {
            File.Copy(templateDocPath, docPath, true);
            new FileInfo(docPath).IsReadOnly = false;
            using (WordprocessingDocument wdDoc = WordprocessingDocument.Open(docPath, true))
            {
                write(wdDoc);
            }
        }

        /// <summary>
        /// beforeDelete  会删除根据templateDocPath生成的word文件
        /// </summary>
        public static void OpenPackage(string templatePath, Action<WordprocessingDocument> write, Action<string> beforeDelete)
        {
            string deleteFile = Path.Combine(@"D:\handyUpload", Guid.NewGuid().ToString() + ".docx");

            OpenPackage(templatePath, deleteFile, write);
            beforeDelete(deleteFile);
            File.Delete(deleteFile);
        }
    }
}
