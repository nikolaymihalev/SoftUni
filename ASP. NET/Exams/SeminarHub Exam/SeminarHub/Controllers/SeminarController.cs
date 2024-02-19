using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Data.Constants;
using SeminarHub.Models;

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
    }
}
