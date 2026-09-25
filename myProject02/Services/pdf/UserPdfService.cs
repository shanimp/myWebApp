using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using myProject02.DTOs;

namespace myProject02.Services.Pdf
{
    public class UserPdfService
    {
        public byte[] GenerateUserPdf(List<UserDto> users)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);

                    page.Header()
                        .Text("System User Report")
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
                                columns.RelativeColumn(2);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(HeaderStyle).Text("ID");
                                header.Cell().Element(HeaderStyle).Text("Name");
                                header.Cell().Element(HeaderStyle).Text("Role ID");
                                header.Cell().Element(HeaderStyle).Text("Role");

                                static IContainer HeaderStyle(IContainer container)
                                {
                                    return container
                                        .Background(Colors.Grey.Lighten2)
                                        .Border(1)
                                        .Padding(5);
                                }
                            });

                            foreach (var user in users)
                            {
                                table.Cell().Element(CellStyle)
                                    .Text(user.Id.ToString());

                                table.Cell().Element(CellStyle)
                                    .Text(user.Name);

                                table.Cell().Element(CellStyle)
                                    .Text(user.RoleId.ToString());

                                table.Cell().Element(CellStyle)
                                    .Text(user.RoleName);

                                static IContainer CellStyle(IContainer container)
                                {
                                    return container
                                        .Border(1)
                                        .Padding(5);
                                }
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text($"Generated on {DateTime.Now:yyyy-MM-dd HH:mm}");
                });
            });

            return document.GeneratePdf();
        }
    }
}