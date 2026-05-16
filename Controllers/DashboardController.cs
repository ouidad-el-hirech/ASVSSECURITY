using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASVSSECURITYAUDITOR.Infrastructure.Data;
using ASVSSECURITYAUDITOR.Models;
using ASVSSECURITYAUDITOR.Core.Entities;

namespace ASVSSECURITYAUDITOR.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            var assessment = await _context
                .Assessments
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (assessment == null)
            {
                return NotFound();
            }

            var total = assessment.Items.Count;

            var valid = assessment.Items
                .Count(x => x.Status == AssessmentStatus.Valid);

            var notValid = assessment.Items
                .Count(x => x.Status == AssessmentStatus.NotValid);

            var pending = assessment.Items
                .Count(x => x.Status == AssessmentStatus.Pending);

            var notApplicable = assessment.Items
                .Count(x => x.Status == AssessmentStatus.NotApplicable);

            double percentage = total == 0
                ? 0
                : ((double)valid / total) * 100;

            string riskLevel;

            if (percentage >= 80)
            {
                riskLevel = "Low";
            }
            else if (percentage >= 50)
            {
                riskLevel = "Medium";
            }
            else
            {
                riskLevel = "High";
            }

            var model = new DashboardViewModel
            {
                TotalRequirements = total,
                ValidCount = valid,
                NotValidCount = notValid,
                PendingCount = pending,
                NotApplicableCount = notApplicable,
                CompliancePercentage = Math.Round(percentage, 2),
                RiskLevel = riskLevel
            };

            return View(model);
        }
    }
}