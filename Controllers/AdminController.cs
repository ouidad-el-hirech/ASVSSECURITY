using Microsoft.AspNetCore.Mvc;
using ASVSSECURITYAUDITOR.Core.Interfaces;

namespace ASVSSECURITYAUDITOR.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICsvImportService _csvService;

        public AdminController(ICsvImportService csvService)
        {
            _csvService = csvService;
        }

        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file != null)
            {
                await _csvService.ImportAsync(file);
            }

            ViewBag.Message = "CSV imported successfully";

            return View();
        }
    }
}