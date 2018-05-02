using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace BulletRay.Export
{
    public class ExcelManager
    {
        public static byte[] CreateExcelFileToByteArray<T>(IEnumerable<T> list, Hashtable head, string fileName)
        {
            try
            {
                var workbook = new XSSFWorkbook();
                using (var ms = new NpoiMemoryStream())
                {
                    ms.AllowClose = false;
                    var sheet = workbook.CreateSheet() as XSSFSheet;
                    var headerRow = sheet.CreateRow(0) as XSSFRow;
                    var h = false;
                    var j = 1;
                    var type = typeof(T);
                    var properties = type.GetProperties();
                    foreach (var item in list)
                    {
                        var dataRow = sheet.CreateRow(j) as XSSFRow;
                        var i = 0;
                        foreach (var column in properties)
                        {
                            if (!h)
                            {
                                headerRow.CreateCell(i).SetCellValue(head[column.Name] == null ? column.Name : head[column.Name].ToString());
                                dataRow.CreateCell(i).SetCellValue(column.GetValue(item, null) == null ? "" : column.GetValue(item, null).ToString());
                            }
                            else
                            {
                                dataRow.CreateCell(i).SetCellValue(column.GetValue(item, null) == null ? "" : column.GetValue(item, null).ToString());
                            }
                            i++;
                        }
                        h = true;
                        j++;
                    }
                    workbook.Write(ms);
                    ms.Flush();
                    ms.Position = 0;
                    ms.AllowClose = true;
                    sheet = null;
                    headerRow = null;
                    workbook = null;
                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //新建类 重写NPOI流方法
        public class NpoiMemoryStream : MemoryStream
        {
            public NpoiMemoryStream()
            {
                AllowClose = true;
            }
            public bool AllowClose { get; set; }

            public override void Close()
            {
                if (AllowClose)
                    base.Close();
            }
        }
    }
}
