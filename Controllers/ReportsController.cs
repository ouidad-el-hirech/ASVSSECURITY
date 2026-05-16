using Microsoft.AspNetCore.Mvc;
using ASVSSECURITYAUDITOR.Core.Interfaces;

namespace ASVSSECURITYAUDITOR.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IPdfReportService _pdfService;

        public ReportsController(
            IPdfReportService pdfService)
        {
            _pdfService = pdfService;
        }

        public IActionResult ExportPdf(int id)
        {
            var pdf = _pdfService
                .GenerateAssessmentReport(id);

            return File(
                pdf,
                "application/pdf",
                "ASVS_Report.pdf");
        }
    }
}