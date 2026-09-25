using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using myProject02.Models;

namespace myProject02.Services.Pdf
{
    public class VoterPdfService
    {
        private readonly PdfReportService _pdfReportService;

        public VoterPdfService(PdfReportService pdfReportService)
        {
            _pdfReportService = pdfReportService;
        }

        public byte[] GenerateVoterPdf(IEnumerable<Voter> voters)
        {
            return _pdfReportService.GeneratePdf(
                "Voter Report",
                voters,
                (table, voter) =>
                {
                    table.Cell()
                        .Border(1)
                        .Padding(5)
                        .Text(voter.Id.ToString());

                    table.Cell()
                        .Border(1)
                        .Padding(5)
                        .Text(voter.FullName ?? "");

                    table.Cell()
                        .Border(1)
                        .Padding(5)
                        .Text(voter.Email ?? "");

                    table.Cell()
                        .Border(1)
                        .Padding(5)
                        .Text(voter.District ?? "");
                });
        }
    }
}