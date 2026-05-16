using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASVSSECURITYAUDITOR.Infrastructure.Data;

namespace ASVSSecurityAuditor.Controllers
{
    public class RequirementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequirementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var requirements = await _context.ASVSRequirements.ToListAsync();

            return View(requirements);
        }
    }
}