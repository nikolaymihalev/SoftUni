using Contacts.Data;
using Contacts.Data.Models;
using Contacts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Contacts.Controllers
{
    [Authorize]
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
                .Select(x => new ContactInfoViewModel(
                    x.Id,
                    x.FirstName,
                    x.LastName,
                    x.Email,
                    x.PhoneNumber,
                    x.Address,
                    x.Website)).ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToTeam(int contactId)
        {
            var contact = await context.Contacts
                .Where(x => x.Id == contactId)
                .Include(x => x.ApplicationUsersContacts)
                .FirstOrDefaultAsync();

            if (contact == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (!contact.ApplicationUsersContacts.Any(x => x.ApplicationUserId == userId))
            {
                contact.ApplicationUsersContacts.Add(new ApplicationUserContact()
                {
                    ApplicationUserId = userId,
                    ContactId = contactId
                });

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromTeam(int contactId)
        {
            var contact = await context.Contacts
                .Where(x => x.Id == contactId)
                .Include(x => x.ApplicationUsersContacts)
                .FirstOrDefaultAsync();

            if (contact == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            var auc = contact.ApplicationUsersContacts.FirstOrDefault(x => x.ApplicationUserId == userId);

            if (auc is null)
            {
                return BadRequest();
            }

            contact.ApplicationUsersContacts.Remove(auc);

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Team));
        }

        [HttpGet]
        public async Task<IActionResult> Team() 
        {
            string userId = GetUserId();

            var model = await context.ApplicationUsersContacts
                .AsNoTracking()
                .Where(x=>x.ApplicationUserId==userId)
                .Select(x=> new ContactInfoViewModel(
                    x.Contact.Id,
                    x.Contact.FirstName,
                    x.Contact.LastName,
                    x.Contact.Email,
                    x.Contact.PhoneNumber,
                    x.Contact.Address,
                    x.Contact.Website))
                .ToListAsync();

            return View(model);
        }

        private string GetUserId() 
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty; 
        }
    }
}
