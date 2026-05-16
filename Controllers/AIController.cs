using Microsoft.AspNetCore.Mvc;
using ASVSSECURITYAUDITOR.Core.Interfaces;

namespace ASVSSECURITYAUDITOR.Controllers
{
    public class AIController : Controller
    {
        private readonly IAIExplanationService _aiService;

        public AIController(
            IAIExplanationService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost]
        public async Task<IActionResult> Explain(
            string requirement)
        {
            var result = await _aiService
                .ExplainRequirement(requirement);

            return Json(new
            {
                explanation = result
            });
        }
    }
}