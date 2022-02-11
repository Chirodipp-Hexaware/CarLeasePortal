using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Spreadsheet
{
    public static class Export
    {
        #region Public Methods and Operators

        public static byte[] ExportRecords<T>(IReadOnlyCollection<T> records, string worksheetName = "Export")
        {
            try
            {
                const int maximumRecords = 1048500;
                var totalRows = records.Count;
                var totalSheets = Math.Ceiling(totalRows / (decimal) maximumRecords);

                var pis = typeof(T)
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => !Attribute.IsDefined(p, typeof(EpPlusIgnore)))
                    .ToArray();

                using (var ep = new ExcelPackage())
                {
                    for (var i = 1; i <= totalSheets; i++)
                    {
                        var ws = ep.Workbook.Worksheets.Add(totalSheets.Equals(1)
                            ? worksheetName
                            : $"{worksheetName} {i:F0}");
                        var skip = (i - 1) * maximumRecords;
                        var take = Math.Min(maximumRecords, totalRows - skip);
                        var theseRecords = records.Skip(skip).Take(take).ToList();

                        ws.Cells.LoadFromCollection(theseRecords, true, TableStyles.None,
                            BindingFlags.Instance | BindingFlags.Public, pis);

                        var firstDataRow = ws.Dimension.Start.Row + 1;
                        var lastDataRow = ws.Dimension.End.Row;
                        var lastDataColumn = ws.Dimension.End.Column;

                        ws.Cells[firstDataRow, 1, lastDataRow, lastDataColumn].Style.Numberformat.Format = "@";

                        for (var k = 0; k < pis.Length; k++)
                        {
                            var property = pis[k];

                            var customAttributes = property.GetCustomAttributes()
                                .Where(t => t.GetType() == typeof(EpPlusNumberFormat)).ToList();

                            foreach (var c in customAttributes)
                            {
                                var q = (EpPlusNumberFormat) c;
                                ws.Cells[firstDataRow, k + 1, lastDataRow, k + 1].Style.Numberformat.Format = q.Format;
                            }
                        }

                        var cellRange = ws.Cells[1, 1, 1, lastDataColumn];
                        cellRange.Style.Font.Bold = true;
                        cellRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        cellRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                        cellRange =
                            ws.Cells[
                                ws.Dimension.Start.Row, ws.Dimension.Start.Column, ws.Dimension.End.Row,
                                ws.Dimension.End.Column];
                        cellRange.AutoFilter = true;
                        cellRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        cellRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        cellRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        cellRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        cellRange.Style.Border.Bottom.Color.SetColor(Color.Black);
                        cellRange.Style.Border.Top.Color.SetColor(Color.Black);
                        cellRange.Style.Border.Left.Color.SetColor(Color.Black);
                        cellRange.Style.Border.Right.Color.SetColor(Color.Black);
                        cellRange.Style.Border.Right.Color.SetColor(Color.Black);
                        ws.View.FreezePanes(2, 1);
                        cellRange.AutoFitColumns();
                    }

                    return ep.GetAsByteArray();
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return null;
            }
        }

        #endregion
    }
}