using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Data.Constants;
using SeminarHub.Data.Models;
using SeminarHub.Models;
using System.Globalization;
using System.Security.Claims;

namespace SeminarHub.Controllers
{
    [Authorize]
    public class SeminarController : Controller
    {
        private readonly SeminarHubDbContext context;

        public SeminarController(SeminarHubDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await context.Seminars
                .AsNoTracking()
                .Select(x => new SeminarInfoViewModel(
                    x.Id,
                    x.Topic,
                    x.Lecturer,
                    x.Category.Name,
                    x.DateAndTime.ToString(ValidationConstants.DateFormat),
                    x.Organizer.UserName))
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new SeminarFormViewModel();
            model.Categories = await GetCategories();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SeminarFormViewModel model)
        {
            DateTime date = DateTime.Now;

            if (!DateTime.TryParseExact(model.DateAndTime,
                ValidationConstants.DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None, out date))
            {
                ModelState.AddModelError(nameof(model.DateAndTime), $"Invalid date! Format must be: {ValidationConstants.DateFormat}");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            var seminar = new Seminar()
            {
                Topic = model.Topic,
                Lecturer = model.Lecturer,
                Details = model.Details,
                OrganizerId = GetUserId(),
                DateAndTime = date,
                Duration = model.Duration,
                CategoryId = model.CategoryId
            };

            await context.Seminars.AddAsync(seminar);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var model = await context.Seminars
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.SeminarsParticipants)
                .FirstOrDefaultAsync();

            if (model is null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (!model.SeminarsParticipants.Any(x => x.ParticipantId == userId))
            {
                var sp = new SeminarParticipant()
                {
                    ParticipantId = userId,
                    SeminarId = model.Id
                };

                model.SeminarsParticipants.Add(sp);

                await context.SeminarsParticipants.AddAsync(sp);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Joined));
            }

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var model = await context.Seminars
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.SeminarsParticipants)
                .FirstOrDefaultAsync();

            if (model is null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            var sp = model.SeminarsParticipants.FirstOrDefault(x => x.ParticipantId == userId);

            if (sp is null)
            {
                return BadRequest();
            }

            model.SeminarsParticipants.Remove(sp);

            context.SeminarsParticipants.Remove(sp);

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetUserId();
            var model = await context.SeminarsParticipants
                .Where(x => x.ParticipantId == userId)
                .Select(x => new SeminarInfoViewModel(
                    x.Seminar.Id,
                    x.Seminar.Topic,
                    x.Seminar.Lecturer,
                    x.Seminar.Category.Name,
                    x.Seminar.DateAndTime.ToString(ValidationConstants.DateFormat),
                    x.Seminar.Organizer.UserName))
                .ToListAsync();

            return View(model);
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
