using ClosedXML.Excel;

namespace ReportsBackend.Api.Helpers
{
    public static class ExcelExportHelper
    {

        public static byte[] GenerateTableExcel<T>(IEnumerable<T> items, List<ColumnDefinition<T>> columns, string sheetName = "Export")
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(sheetName);

            // Header
            for (int i = 0; i < columns.Count; i++)
            {
                worksheet.Cell(1, i + 1).Value = columns[i].Header;
                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
            }

            // Data
            int row = 2;
            foreach (var item in items)
            {
                for (int col = 0; col < columns.Count; col++)
                {
                    worksheet.Cell(row, col + 1).Value = columns[col].ValueSelector(item) ?? "";
                }
                row++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
