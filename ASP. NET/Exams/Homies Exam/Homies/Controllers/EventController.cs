using Homies.Data;
using Homies.Data.Constants;
using Homies.Data.Models;
using Homies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Homies.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly HomiesDbContext context;

        public EventController(HomiesDbContext _context)
        {
            context = _context;   
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await context.Events
                .AsNoTracking()
                .Select(x => new EventInfoViewModel(
                    x.Id,
                    x.Name,
                    x.Start.ToString(ValidationConstants.DateFormat),
                    x.Type.Name,
                    x.Organiser.UserName))
                .ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id) 
        {
            var model = await context.Events
                .Where(x => x.Id == id)
                .AsNoTracking()
                .Include(x => x.EventsParticipants)
                .FirstOrDefaultAsync();

            string userId = GetUserId();

            if (!model.EventsParticipants.Any(x => x.HelperId == userId)) 
            {
                var ep = new EventParticipant()
                {
                    HelperId = userId,
                    EventId = id
                };

                model.EventsParticipants.Add(ep);

                await context.EventsParticipants.AddAsync(ep);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Joined));
        }

        private string GetUserId() 
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
