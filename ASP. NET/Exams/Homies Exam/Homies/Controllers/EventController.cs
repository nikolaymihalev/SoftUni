using Homies.Data;
using Homies.Data.Constants;
using Homies.Data.Models;
using Homies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using Type = Homies.Data.Models.Type;

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

            if(model is null) 
            {
                return BadRequest();
            }

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

        [HttpPost]
        public async Task<IActionResult> Leave(int id) 
        {
            var model = await context.Events
                .Where(x => x.Id == id)
                .AsNoTracking()
                .Include(x => x.EventsParticipants)
                .FirstOrDefaultAsync();

            if (model is null)
            {
                return BadRequest();
            }

            string userId = GetUserId();


            var ep = model.EventsParticipants.FirstOrDefault(x => x.HelperId == userId);

            if(ep is null) 
            {
                return BadRequest();
            }

            model.EventsParticipants.Remove(ep);

            context.EventsParticipants.Remove(ep);

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Joined() 
        {
            string userId = GetUserId();

            var model = await context.EventsParticipants
                .AsNoTracking()
                .Where(x=>x.HelperId == userId)
                .Select(x => new EventInfoViewModel(
                    x.Event.Id,
                    x.Event.Name,
                    x.Event.Start.ToString(ValidationConstants.DateFormat),
                    x.Event.Type.Name,
                    x.Event.Organiser.UserName))
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add() 
        {
            var model = new EventFormViewModel();
            model.Types = await GetTypesAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventFormViewModel model) 
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;

            if (!DateTime.TryParseExact(model.Start,
                ValidationConstants.DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out start))
            {
                ModelState.AddModelError(nameof(model.Start), $"Invalid date! Format must be: {ValidationConstants.DateFormat}");
            }

            if (!DateTime.TryParseExact(model.End,
                ValidationConstants.DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out end))
            {
                ModelState.AddModelError(nameof(model.End), $"Invalid date! Format must be: {ValidationConstants.DateFormat}");
            }

            if (!ModelState.IsValid) 
            {
                model.Types = await GetTypesAsync();
                return View(model);
            }

            var even = new Event()
            {
                Name = model.Name,
                Description = model.Description,
                Start = start,
                End = end,
                TypeId = model.TypeId,
                OrganiserId = GetUserId()                
            };

            await context.Events.AddAsync(even);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        public async Task<IEnumerable<Type>> GetTypesAsync() 
        {
            return await context.Types.AsNoTracking().ToListAsync();
        }

        private string GetUserId() 
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
