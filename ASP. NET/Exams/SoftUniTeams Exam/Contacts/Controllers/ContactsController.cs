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

        [HttpGet]
        public async Task<IActionResult> Add() 
        {
            var model = new ContactFormViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ContactFormViewModel model) 
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            var contact = new Contact()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Website = model.Website,
            };

            await context.AddAsync(contact);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int contactId) 
        {
            var model = await context.Contacts.FindAsync(contactId);

            if (model is null) 
            {
                return BadRequest();
            }

            var contact = new ContactFormViewModel()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Website = model.Website,
            };

            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ContactFormViewModel model,int contactId) 
        {
            var contact = await context.Contacts.FindAsync(contactId);

            if (contact is null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            contact.FirstName = model.FirstName;
            contact.LastName = model.LastName;
            contact.Email = model.Email;
            contact.PhoneNumber = model.PhoneNumber;
            contact.Address = model.Address;
            contact.Website = model.Website;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
            
        }

        private string GetUserId() 
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty; 
        }
    }
}
