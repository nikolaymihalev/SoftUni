using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.Agent;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseRentingSystem.Controllers
{
    public class AgentController : BaseController
    {
        private readonly IAgentService agentService;

        public AgentController(IAgentService _agentService)
        {
            agentService = _agentService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            if(await agentService.ExistsByIdAsync(User.Id()))
            {
                return BadRequest();
            }
            return View(new BecomeAgentFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentFormModel model)
        {
            return RedirectToAction(nameof(HouseController.All), "House");
        }
    }
}
