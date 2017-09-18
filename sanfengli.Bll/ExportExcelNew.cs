using Bitauto.Mall.Aop;
using BitAuto.Utils;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll
{
    public static class ExportExcelNew
    {
        /// <summary>
        /// 生成excel（三个数组的个数应该相同）
        /// </summary>
        /// <param name="table">table</param>
        /// <param name="caption">列头数组</param>
        /// <param name="cols">列数组</param>
        /// <param name="characterNum">列宽数组（按字符个数）</param>
        /// <param name="sheetName">sheet名</param>
        /// <returns>流</returns>
        public static MemoryStream ExportDataTableToExcelWithTitle(DataTable table, string[] caption, string[] cols, int[] characterNum, string sheetName)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                //Stopwatch sw = new Stopwatch();
                //sw.Start();
                try
                {
                    List<int> dateTimeIndex = new List<int>();
                    //Create the worksheet
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add(sheetName);

                    for (int i = 0; i < caption.Length; i++)
                    {
                        ws.Cells[1, i + 1].Value = caption[i];
                        ws.Column(i + 1).Width = characterNum[i];
                    }
                    //for (int i = 0; i < cols.Length; i++)
                    //{
                    //    if (table.Columns[cols[i]].DataType == typeof(DateTime))
                    //    {
                    //        dateTimeIndex.Add(i);
                    //    }
                    //}
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        for (int j = 0; j < cols.Length; j++)
                        {
                            //ws.Cells[i + 2, j + 1].Value = table.Rows[i][cols[j]].ToString();
                            //if (dateTimeIndex.Contains(j))
                            if (table.Columns[cols[j]].DataType == typeof(DateTime))
                            {
                                ws.Cells[i + 2, j + 1].Value = table.Rows[i][cols[j]];
                                ws.Cells[i + 2, j + 1].Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss";
                                ws.Cells[i + 2, j + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            else
                            {
                                ws.Cells[i + 2, j + 1].Value = table.Rows[i][cols[j]].ToString();
                            }
                        }
                    }

                    //sw.Stop();
                    //var time = sw.ElapsedMilliseconds;
                    var temp = pck.GetAsByteArray();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ms.Write(temp, 0, temp.Length);
                        return ms;
                    }
                }
                catch (Exception ex)
                {

                    LogHandler.Error(ex);
                    return null;
                }
            }
        }

        /// <summary>
        /// 生成excel需要标记颜色（三个数组的个数应该相同）
        /// </summary>
        /// <param name="table">table(最后一列的值是是否标记颜色)</param>
        /// <param name="caption">列头数组</param>
        /// <param name="cols">列数组</param>
        /// <param name="characterNum">列宽数组（按字符个数）</param>
        /// <param name="sheetName">sheet名</param>
        /// <param name="index">需要标记颜色的列</param>
        /// <param name="color">颜色</param>
        /// <returns>流</returns>
        public static MemoryStream ExportDataTableToExcelWithTitleAndColor(DataTable table, string[] caption, string[] cols, int[] characterNum, string sheetName, int index, Color color)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {

                try
                {

                    //Create the worksheet
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add(sheetName);

                    for (int i = 0; i < caption.Length; i++)
                    {
                        ws.Cells[1, i + 1].Value = caption[i];
                        ws.Column(i + 1).Width = characterNum[i];
                    }
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        for (int j = 0; j < cols.Length; j++)
                        {
                            ws.Cells[i + 2, j + 1].Value = table.Rows[i][cols[j]].ToString();
                            if (j == index)
                            {
                                bool isBiaoJi = ConvertHelper.GetBoolean(table.Rows[i][table.Columns.Count - 1]);
                                if (isBiaoJi)
                                {
                                    ws.Cells[i + 2, j + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    ws.Cells[i + 2, j + 1].Style.Fill.BackgroundColor.SetColor(color);
                                }

                            }

                        }
                    }


                    var temp = pck.GetAsByteArray();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ms.Write(temp, 0, temp.Length);
                        return ms;
                    }
                }
                catch (Exception ex)
                {

                    LogHandler.Error(ex);
                    return null;
                }
            }
        }


        /// <summary>
        /// 生成excel（三个数组的个数应该相同）  流没有关闭 用完后关闭
        /// </summary>
        /// <param name="listTable">listTable</param>
        /// <param name="caption">列头数组</param>
        /// <param name="cols">列数组</param>
        /// <param name="characterNum">列宽数组（按字符个数）</param>
        /// <param name="sheetNames">sheet名</param>
        /// <returns>流</returns>
        public static MemoryStream ExportDataTableListToExcelWithTitle(List<DataTable> listTable, string[] caption, string[] cols, int[] characterNum, List<string> sheetNames)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {

                try
                {
                    int count = 0;
                    foreach (var table in listTable)
                    {
                        //Create the worksheet
                        ExcelWorksheet ws = pck.Workbook.Worksheets.Add(sheetNames[count++]);

                        for (int i = 0; i < caption.Length; i++)
                        {
                            ws.Cells[1, i + 1].Value = caption[i];
                            ws.Column(i + 1).Width = characterNum[i];
                        }
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            for (int j = 0; j < cols.Length; j++)
                            {
                                ws.Cells[i + 2, j + 1].Value = table.Rows[i][cols[j]].ToString();
                            }
                        }
                    }
                    var temp = pck.GetAsByteArray();
                    //using (MemoryStream ms = new MemoryStream())
                    //{
                    MemoryStream ms = new MemoryStream();
                    ms.Write(temp, 0, temp.Length);
                    return ms;
                    //}
                }
                catch (Exception ex)
                {

                    LogHandler.Error(ex);
                    return null;
                }
            }
        }


        public static MemoryStream ExportDataTableToExcelWithTitle(DataTable table, string sheetName)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                //Stopwatch sw = new Stopwatch();
                //sw.Start();
                try
                {
                  
                    //Create the worksheet
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add(sheetName);

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        ws.Cells[1, i + 1].Value = table.Columns[i].ColumnName;
                        ws.Column(i + 1).Width = 30;
                    }
                   
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            //ws.Cells[i + 2, j + 1].Value = table.Rows[i][cols[j]].ToString();
                            //if (dateTimeIndex.Contains(j))
                            if (table.Columns[j].DataType == typeof(DateTime))
                            {
                                ws.Cells[i + 2, j + 1].Value = table.Rows[i][j];
                                ws.Cells[i + 2, j + 1].Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss";
                                ws.Cells[i + 2, j + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            else
                            {
                                ws.Cells[i + 2, j + 1].Value = table.Rows[i][j].ToString();
                            }
                        }
                    }

                    //sw.Stop();
                    //var time = sw.ElapsedMilliseconds;
                    var temp = pck.GetAsByteArray();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ms.Write(temp, 0, temp.Length);
                        return ms;
                    }
                }
                catch (Exception ex)
                {

                    LogHandler.Error(ex);
                    return null;
                }
            }
        }
    }
}
