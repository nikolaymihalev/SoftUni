using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
using SoftUniBazar.Models;

namespace SoftUniBazar.Controllers
{
    [Authorize]
    public class AdController : Controller
    {
        private readonly BazarDbContext context;

        public AdController(BazarDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult>All()
        {
            var model = await context.Ads
                .AsNoTracking()
                .Select(x => new AdInfoViewModel(
                    x.Id,
                    x.Name,
                    x.ImageUrl,
                    x.CreatedOn.ToString(),
                    x.Category.Name,
                    x.Description,
                    x.Price.ToString("f2"),
                    x.Owner.UserName))
                .ToListAsync();                

            return View(model);
        }
    }
}
