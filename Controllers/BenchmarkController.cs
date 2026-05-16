using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASVSSECURITYAUDITOR.Infrastructure.Data;
using ASVSSECURITYAUDITOR.Models;
using ASVSSECURITYAUDITOR.Core.Entities;

namespace ASVSSECURITYAUDITOR.Controllers
{ 
    public class BenchmarkController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BenchmarkController(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            var assessment = await _context
                .Assessments
                .Include(x => x.Items)
                .ThenInclude(x => x.ASVSRequirement)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (assessment == null)
            {
                return NotFound();
            }

            var total = assessment.Items.Count;

            var valid = assessment.Items
                .Count(x => x.Status == AssessmentStatus.Valid);

            double compliance =
                total == 0
                ? 0
                : ((double)valid / total) * 100;

            string risk =
                compliance >= 80 ? "Low" :
                compliance >= 50 ? "Medium" :
                "High";

            string maturity =
                compliance >= 85 ? "Advanced" :
                compliance >= 60 ? "Intermediate" :
                "Basic";

            var chapters = assessment.Items
                .GroupBy(x => x.ASVSRequirement.Chapter)
                .Select(g =>
                {
                    var chapterTotal = g.Count();

                    var chapterValid = g.Count(x =>
                        x.Status == AssessmentStatus.Valid);

                    double percentage =
                        chapterTotal == 0
                        ? 0
                        : ((double)chapterValid / chapterTotal) * 100;

                    string recommendation =
                        percentage < 50
                        ? "Immediate remediation required."
                        : percentage < 80
                        ? "Needs improvement."
                        : "Good security posture.";

                    return new ChapterScore
                    {
                        Chapter = g.Key,
                        Total = chapterTotal,
                        Valid = chapterValid,
                        Percentage = Math.Round(percentage, 2),
                        Recommendation = recommendation
                    };
                })
                .ToList();

            var model = new BenchmarkViewModel
            {
                CompliancePercentage =
                    Math.Round(compliance, 2),

                RiskLevel = risk,

                MaturityLevel = maturity,

                Chapters = chapters
            };

            return View(model);
        }
    }
}