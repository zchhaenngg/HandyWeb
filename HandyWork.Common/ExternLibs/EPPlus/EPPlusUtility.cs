using HandyWork.Common.Consts;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.ExternLibs.EPPlus
{
    /// <summary>
    /// 最小单元格rowNumber=1,columnNumber=1
    /// </summary>
    public static class EPPlusUtility
    {

        public static void WriteRow(ExcelWorksheet sheet, int rowNumber, int columnNumber, params object[] row)
        {
            foreach (var obj in row)
            {
                sheet.Cells[rowNumber, columnNumber].Value = obj;
                if (obj is DateTime)
                {
                    if ("General" == sheet.Cells[rowNumber, columnNumber].Style.Numberformat.Format)
                    {
                        sheet.Cells[rowNumber, columnNumber].Style.Numberformat.Format = Formats.ToSecond;
                    }
                }
                columnNumber ++;
            }
        }

        public static void WriteRows(ExcelWorksheet sheet, int rowNumber, int columnNumber, IEnumerable<IEnumerable<object>> rows)
        {
            foreach (var row in rows)
            {
                WriteRow(sheet, rowNumber, columnNumber, row);
                rowNumber++;
            }
        }

        /// <summary>
        ///  Opens a System.IO.FileStream on the Template path, with the Read access.Write data and save to the Save path.
        /// </summary>
        /// <param name="savePath">The file to save.</param>
        /// <param name="template">The template file to read。</param>
        /// <param name="write"></param>
        public static void Save(string savePath, string templatePath, Action<ExcelWorksheets> write)
        {
            using (Stream templateStream = File.Open(templatePath, FileMode.Open, FileAccess.Read))
            {
                using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    using (ExcelPackage package = new ExcelPackage(fs, templateStream))
                    {
                        ExcelWorksheets sheets = package.Workbook.Worksheets;
                        write(sheets);
                        package.Save();
                    }
                }
            }
        }

        /// <summary>
        /// Opens a System.IO.FileStream on the specified path, with the Read and Write access.Write data on the file.
        /// </summary>
        /// <param name="savePath">The file to save.</param>
        /// <param name="write"></param>
        public static void Save(string savePath, Action<ExcelWorksheets> write)
        {
            using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite))
            {
                using (ExcelPackage package = new ExcelPackage(fs))
                {
                    ExcelWorksheets sheets = package.Workbook.Worksheets;
                    write(sheets);
                    package.Save();
                }
            }
        }
    }
}
