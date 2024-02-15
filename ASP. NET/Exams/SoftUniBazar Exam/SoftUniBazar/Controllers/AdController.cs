using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
using SoftUniBazar.Data.Constants;
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
                    x.CreatedOn.ToString(ValidationConstants.DateFormat),
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
                    x.Ad.CreatedOn.ToString(ValidationConstants.DateFormat),
                    x.Ad.Category.Name,
                    x.Ad.Description,
                    x.Ad.Price.ToString("f2"),
                    x.Ad.Owner.UserName))
                .ToListAsync();


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add() 
        {
            var model = new AdFormViewModel();
            model.Categories = await GetCategories();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdFormViewModel model) 
        {
            if (!ModelState.IsValid) 
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            string userId = GetUserId();

            if (userId is null) 
            {
                return Unauthorized();
            }

            var entity = new Ad()
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                CategoryId = model.CategoryId,
                CreatedOn = DateTime.Now,
                OwnerId = userId
            };

            await context.Ads.AddAsync(entity);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        {
            var model = await context.Ads.FindAsync(id);

            if(model is null) 
            {
                return BadRequest();
            }

            var ad = new AdFormViewModel()
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                CategoryId = model.CategoryId,
                Categories = await GetCategories()
            };

            return View(ad);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdFormViewModel model, int id) 
        {
            var ad = await context.Ads.FindAsync(id);

            if (ad is null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid) 
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            ad.Name = model.Name;
            ad.Description = model.Description;
            ad.ImageUrl = model.ImageUrl;
            ad.Price = model.Price;
            ad.CategoryId = model.CategoryId;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        private async Task<IEnumerable<Category>> GetCategories() 
        {
            return await context.Categories.AsNoTracking().ToListAsync();
        }
        private string GetUserId() 
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
