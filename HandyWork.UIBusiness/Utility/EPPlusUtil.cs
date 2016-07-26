using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UIBusiness.Utility
{
    public static class EPPlusUtil
    {
        //public static void WriteRow(ExcelWorksheet sheet, List<object> row, int rowNum, int colNum)
        //{
        //    for (int i = 0; i < row.Count; i++)
        //    {
        //        sheet.Cells[rowNum, colNum + i].Value = row[i];
        //    }
        //}

        //public static void WriteTable(ExcelWorksheet sheet, List<List<Object>> lines, int rowNum, int colNum)
        //{
        //    for (int i = 0; i < lines.Count; i++)
        //    {
        //        WriteRow(sheet, lines[i], rowNum + i, colNum);
        //    }
        //}
        //public static void SaveXlsx(string file, string templateFile, Action<ExcelWorksheets> sheetsAction)
        //{
        //    using (Stream templateStream = File.Open(templateFile, FileMode.Open, FileAccess.Read))
        //    {
        //        using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.ReadWrite))
        //        {
        //            using (ExcelPackage package = new ExcelPackage(fs, templateStream))
        //            {
        //                ExcelWorksheets sheets = package.Workbook.Worksheets;
        //                sheetsAction(sheets);
        //                package.Save();
        //            }
        //        }
        //    }
        //}
    }
}
