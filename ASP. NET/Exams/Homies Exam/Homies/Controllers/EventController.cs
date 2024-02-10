﻿using Homies.Data;
using Homies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Homies.Data.Models;
using Microsoft.AspNetCore.Authorization;

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



        private string GetUserId() 
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
