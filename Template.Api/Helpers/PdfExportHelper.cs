using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ReportsBackend.Api.Helpers
{
    public static class PdfExportHelper
    {

        public static byte[] GenerateTablePdf<T>(
            IEnumerable<T> items,
            List<ColumnDefinition<T>> columns,
            string title = "Export")
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(11));
                    page.Header()
                        .Text(title)
                        .FontSize(22)
                        .Bold()
                        .FontColor(Colors.Blue.Medium)
                        .AlignCenter();


                    page.Content().Table(table =>
                    {
                        // Define columns
                        table.ColumnsDefinition(columnsDef =>
                        {
                            foreach (var _ in columns)
                                columnsDef.RelativeColumn();
                        });

                        // Header row
                        table.Header(header =>
                        {
                            foreach (var col in columns)
                            {
                                header.Cell().Element(CellStyle).Background(Colors.Grey.Lighten2).Text(col.Header).Bold();
                            }
                        });

                        // Data rows with alternating background
                        int rowIndex = 0;
                        foreach (var item in items)
                        {
                            var bgColor = rowIndex % 2 == 0 ? Colors.White : Colors.Grey.Lighten4;
                            foreach (var col in columns)
                            {
                                table.Cell().Element(c => CellStyle(c).Background(bgColor)).Text(col.ValueSelector(item) ?? "");
                            }
                            rowIndex++;
                        }

                        IContainer CellStyle(IContainer container) =>
                            container
                                .Border(1)
                                .BorderColor(Colors.Grey.Lighten1)
                                .PaddingVertical(4)
                                .PaddingHorizontal(6)
                                .AlignMiddle();
                    });
                });
            });

            return document.GeneratePdf();
        }


        public static byte[] GenerateDynamicTablePdf(
    List<Dictionary<string, object>> rows,
    List<string> headers,
    string title = "Export")
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(11));
                    page.Header()
                        .Text(title)
                        .FontSize(22)
                        .Bold()
                        .FontColor(Colors.Blue.Medium)
                        .AlignCenter();

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columnsDef =>
                        {
                            foreach (var _ in headers)
                                columnsDef.ConstantColumn(80);
                        });

                        // Header row
                        table.Header(header =>
                        {
                            foreach (var col in headers)
                                header.Cell().Element(CellStyle).Background(Colors.Grey.Lighten2).Text(col).Bold();
                        });

                        // Data rows
                        int rowIndex = 0;
                        foreach (var dataRow in rows)
                        {
                            var bgColor = rowIndex % 2 == 0 ? Colors.White : Colors.Grey.Lighten4;
                            foreach (var col in headers)
                            {
                                table.Cell().Element(c => CellStyle(c).Background(bgColor)).Text(dataRow.ContainsKey(col) ? dataRow[col]?.ToString() ?? "" : "");
                            }
                            rowIndex++;
                        }

                        IContainer CellStyle(IContainer container) =>
                            container
                                .Border(1)
                                .BorderColor(Colors.Grey.Lighten1)
                                .PaddingVertical(4)
                                .PaddingHorizontal(6)
                                .AlignMiddle();
                    });
                });
            });

            return document.GeneratePdf();
        }

    }
}
