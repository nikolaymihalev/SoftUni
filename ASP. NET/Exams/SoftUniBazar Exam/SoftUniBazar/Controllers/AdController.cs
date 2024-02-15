using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
using SoftUniBazar.Data.Models;
using SoftUniBazar.Models;
using System.Security.Claims;

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

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id) 
        {
            var model = await context.Ads
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.AdBuyers)
                .FirstOrDefaultAsync();

            if (model is null) 
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (!model.AdBuyers.Any(x => x.BuyerId == userId))
            {
                model.AdBuyers.Add(new AdBuyer()
                {
                    BuyerId = userId,
                    AdId = id
                });
                
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Cart));
        }

        private string GetUserId() 
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
