using OfficeOpenXml;

namespace IGDB_ExporterData.Export
{
    public class ExcelExporter : IExporter
    {
        public void WriteDataToFile<T>(IList<T> dataList, string fileName, bool prettyPrint = false) where T : IDataExport
        {
            // If you use EPPlus in a noncommercial context
            // according to the Polyform Noncommercial license:
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(fileName))
            {
                var sheet = package.Workbook.Worksheets.Add("Data");

                ExcelColumnFormat format = new ExcelColumnFormat();
                format.AutoFilter = true;
                format.IsBold = true;
                format.IsUnderline = true;

                WriteDataToColumns(ref sheet, dataList[0], 1, format);

                for (int i = 0; i < dataList.Count; i++)
                {
                    WriteDataToRows(ref sheet, dataList[i], i + 2, prettyPrint);
                }

                package.Save();
            }
        }
        private void WriteDataToColumns(ref ExcelWorksheet worksheet, IDataExport columnObject, int startingColumn = 1, ExcelColumnFormat? format = null)
        {
            var headerList = columnObject.GetPropertyHeaders();
            for (int i = 0; i < headerList.Count; i++)
            {
                worksheet.Cells[1, i + startingColumn].Value = headerList[i];

            }

            if (format != null)
            {
                worksheet.Cells[1, 1, 1, headerList.Count].Style.Font.Bold = format.Value.IsBold;
                worksheet.Cells[1, 1, 1, headerList.Count].Style.Font.Italic = format.Value.IsItalic;
                worksheet.Cells[1, 1, 1, headerList.Count].Style.Font.UnderLine = format.Value.IsUnderline;
                worksheet.Cells[1,1,1,headerList.Count].AutoFilter = format.Value.AutoFilter;
            }
        }

        private void WriteDataToRows(ref ExcelWorksheet worksheet, IDataExport rowData, int startingRow = 1, bool prettyPrint = false)
        {
            IList<string> dataList;

            if (prettyPrint)
                dataList = rowData.ExportToPrettyPrintStringList();
            else
                dataList = rowData.ExportToStringList();

            for (int j = 1; j < dataList.Count; j++)
            {
                worksheet.Cells[startingRow, j].Value = dataList[j - 1];
            }
        }

    }

    public struct ExcelColumnFormat
    {
        public bool IsBold { get; set; }
        public bool IsItalic { get; set; }
        public bool IsUnderline { get; set; }
        public bool AutoFilter { get; set; }
    }
}
