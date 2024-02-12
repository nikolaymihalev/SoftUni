using Contacts.Data;
using Contacts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ContactsDbContext context;

        public ContactsController(ContactsDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await context.Contacts
                .AsNoTracking()
                .Select(x=> new ContactInfoViewModel(
                    x.Id,
                    x.FirstName,
                    x.LastName,
                    x.Email,
                    x.PhoneNumber,
                    x.Address,
                    x.Website)).ToListAsync();

            return View(model);
        }
    }
}
