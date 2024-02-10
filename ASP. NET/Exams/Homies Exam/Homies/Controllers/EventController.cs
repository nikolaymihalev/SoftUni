using Homies.Data;
using Homies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Homies.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Homies.Data.Constants;
using System.Globalization;

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
                .Select(e => new EventInfoViewModel(
                    e.Id, 
                    e.Name, 
                    e.Start, 
                    e.Type.Name, 
                    e.Organiser.UserName))
                .ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id) 
        {
            var ev = await context.Events
                .Where(e=>e.Id == id)
                .Include(e=>e.EventsParticipants)
                .FirstOrDefaultAsync(e=>e.Id == id);

            if (ev is null) 
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if(!ev.EventsParticipants.Any(p=>p.HelperId == userId))
            {
                ev.EventsParticipants.Add(new EventParticipant()
                {
                    HelperId = userId,
                    EventId = ev.Id
                });

                await context.SaveChangesAsync();
            }           

            return RedirectToAction(nameof(Joined));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id) 
        {
            var ev = await context.Events
                .Where(e => e.Id == id)
                .Include(e => e.EventsParticipants)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (ev is null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            var ep = ev.EventsParticipants.FirstOrDefault(e => e.HelperId == userId);

            if (ep is null) 
            {
                return BadRequest();
            }

            ev.EventsParticipants.Remove(ep);

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Joined() 
        {
            string userId = GetUserId();    
            var model = await context.EventsParticipants
                .Where(ep=>ep.HelperId == userId)
                .AsNoTracking()
                .Select(ep=>new EventInfoViewModel(
                    ep.EventId,
                    ep.Event.Name,
                    ep.Event.Start,
                    ep.Event.Type.Name,
                    ep.Event.Organiser.UserName))
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add() 
        {
            var model = new EventFormViewModel();
            model.Types = await GetTypes();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventFormViewModel model) 
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;

            if (!DateTime.TryParseExact(model.Start,
                ValidationConstants.DataFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out start)) 
            {
                ModelState.AddModelError(nameof(model.Start), $"Invalid date! Format must be: {ValidationConstants.DataFormat}");
            }

            if (!DateTime.TryParseExact(model.End,
                ValidationConstants.DataFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out end))
            {
                ModelState.AddModelError(nameof(model.End), $"Invalid date! Format must be: {ValidationConstants.DataFormat}");
            }

            if (!ModelState.IsValid) 
            {
                model.Types = await GetTypes();

                return View(model);
            }

            var entity = new Event() 
            {
                CreatedOn = DateTime.Now,
                Description = model.Description,
                Name = model.Name,
                OrganiserId = GetUserId(),
                TypeId = model.TypeId,
                Start = start,
                End = end,
            };

            await context.Events.AddAsync(entity);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        {
            var ev = await context.Events.FindAsync(id);

            if(ev is null) 
            {
                return BadRequest();
            }

            if (ev.OrganiserId != GetUserId()) 
            {
                return Unauthorized();
            }

            var model = new EventFormViewModel()
            {
                Description = ev.Description,
                Name = ev.Name,
                Start = ev.Start.ToString(ValidationConstants.DataFormat),
                End = ev.End.ToString(ValidationConstants.DataFormat),
                TypeId = ev.TypeId,
                Types = await GetTypes()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EventFormViewModel model, int id) 
        {
            var ev = await context.Events.FindAsync(id);

            if (ev is null)
            {
                return BadRequest();
            }

            if (ev.OrganiserId != GetUserId())
            {
                return Unauthorized();
            }

            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;

            if (!DateTime.TryParseExact(model.Start,
                ValidationConstants.DataFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out start))
            {
                ModelState.AddModelError(nameof(model.Start), $"Invalid date! Format must be: {ValidationConstants.DataFormat}");
            }

            if (!DateTime.TryParseExact(model.End,
                ValidationConstants.DataFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out end))
            {
                ModelState.AddModelError(nameof(model.End), $"Invalid date! Format must be: {ValidationConstants.DataFormat}");
            }

            if(!ModelState.IsValid) 
            {
                model.Types = await GetTypes();

                return View(model);
            }

            ev.Start = start;
            ev.End = end;
            ev.Description = model.Description;
            ev.Name = model.Name;
            ev.TypeId = model.TypeId;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        private string GetUserId() 
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        private async Task<IEnumerable<TypeViewModel>> GetTypes() 
        {
            return await context.Types
                .Select(t=>new TypeViewModel(t.Id, t.Name))
                .ToListAsync();
        }
    }
}
