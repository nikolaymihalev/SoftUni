using HouseRentingSystem.Core.Models.Agent;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    public class AgentController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Become()
        {
            return View(new BecomeAgentFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentFormModel model)
        {
            return RedirectToAction(nameof(HouseController.All), "House");
        }
    }
}
