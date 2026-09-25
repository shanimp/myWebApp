using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace myProject02.Services.Pdf
{
    public class PdfReportService
    {
        public byte[] GeneratePdf<T>(
            string title,
            IEnumerable<T> data,
            Action<TableDescriptor, T> createRow)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);

                    page.Header()
                        .Text(title)
                        .FontSize(20)
                        .Bold()
                        .AlignCenter();

                    page.Content()
                        .PaddingTop(20)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(50);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(1.5f);
                            });

                            foreach (var item in data)
                            {
                                createRow(table, item);
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(text =>
                        {
                            text.Span("Generated on: ");
                            text.Span(
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                        });
                });
            });

            return document.GeneratePdf();
        }
    }
}