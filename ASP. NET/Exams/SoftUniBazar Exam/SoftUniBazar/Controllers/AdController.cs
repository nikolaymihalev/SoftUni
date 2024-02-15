using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
using SoftUniBazar.Data.Models;
using SoftUniBazar.Models;
using System.Linq;
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
                var buyer= new AdBuyer()
                {
                    BuyerId = userId,
                    AdId = id
                };

                model.AdBuyers.Add(buyer);

                await context.AdsBuyers.AddAsync(buyer);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id) 
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

            var user = model.AdBuyers.FirstOrDefault(x => x.BuyerId == userId);

            if (user is null) 
            {
                return Unauthorized();
            }

            model.AdBuyers.Remove(user);

            context.AdsBuyers.Remove(user);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Cart() 
        {
            string userId = GetUserId();

            var model = await context.AdsBuyers
                .AsNoTracking()
                .Where(x=>x.BuyerId==userId)
                .Select(x => new AdInfoViewModel(
                    x.AdId,
                    x.Ad.Name,
                    x.Ad.ImageUrl,
                    x.Ad.CreatedOn.ToString(),
                    x.Ad.Category.Name,
                    x.Ad.Description,
                    x.Ad.Price.ToString("f2"),
                    x.Ad.Owner.UserName))
                .ToListAsync();


            return View(model);
        }

        private string GetUserId() 
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
