using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASVSSECURITYAUDITOR.Infrastructure.Data;
using ASVSSECURITYAUDITOR.Core.Entities;

namespace ASVSSecurityAuditor.Controllers
{
    public class RequirementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequirementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // INDEX (SEARCH + FILTER)
        // =========================
        public async Task<IActionResult> Index(string search, string chapter)
        {
            var query = _context.ASVSRequirements.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x =>
                    x.Title.Contains(search) ||
                    x.Description.Contains(search) ||
                    x.RequirementCode.Contains(search));
            }

            if (!string.IsNullOrEmpty(chapter))
            {
                query = query.Where(x => x.Chapter == chapter);
            }

            ViewBag.Chapters = await _context.ASVSRequirements
                .Select(x => x.Chapter)
                .Distinct()
                .ToListAsync();

            var requirements = await query.ToListAsync();

            return View(requirements);
        }

        // =========================
        // EDIT (GET)
        // =========================
        public async Task<IActionResult> Edit(int id)
        {
            var requirement = await _context.ASVSRequirements
                .FindAsync(id);

            if (requirement == null)
            {
                return NotFound();
            }

            return View(requirement);
        }

        // =========================
        // EDIT (POST)
        // =========================
        [HttpPost]
        public async Task<IActionResult> Edit(ASVSRequirement requirement)
        {
            if (!ModelState.IsValid)
            {
                return View(requirement);
            }

            _context.Update(requirement);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // DELETE
        // =========================
        public async Task<IActionResult> Delete(int id)
        {
            var requirement = await _context.ASVSRequirements
                .FindAsync(id);

            if (requirement != null)
            {
                _context.ASVSRequirements.Remove(requirement);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}