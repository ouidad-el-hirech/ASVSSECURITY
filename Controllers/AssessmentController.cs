using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASVSSECURITYAUDITOR.Infrastructure.Data;
using ASVSSECURITYAUDITOR.Core.Entities;

namespace ASVSSECURITYAUDITOR.Controllers
{
    public class AssessmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssessmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            var assessment = new Assessment
            {
                Name = name
            };

            _context.Assessments.Add(assessment);

            await _context.SaveChangesAsync();

            var requirements = await _context
                .ASVSRequirements
                .ToListAsync();

            foreach (var requirement in requirements)
            {
                _context.AssessmentItems.Add(new AssessmentItem
                {
                    AssessmentId = assessment.Id,
                    ASVSRequirementId = requirement.Id,
                    Status = AssessmentStatus.Pending,
                    Notes = "" 
                });
                
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = assessment.Id });
        }

        public async Task<IActionResult> Details(int id)
        {
            var assessment = await _context
                .Assessments
                .Include(x => x.Items)
                .ThenInclude(x => x.ASVSRequirement)
                .FirstOrDefaultAsync(x => x.Id == id);

            return View(assessment);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(
            int itemId,
            AssessmentStatus status,
            string notes)
        {
            var item = await _context
                .AssessmentItems
                .FirstOrDefaultAsync(x => x.Id == itemId);

            if (item != null)
            {
                item.Status = status;
                item.Notes = notes;

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(
                "Details",
                new { id = item.AssessmentId });
        }
    }
}