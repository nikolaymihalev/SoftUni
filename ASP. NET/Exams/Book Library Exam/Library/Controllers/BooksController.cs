using Library.Data;
using Library.Data.Models;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryDbContext context;

        public BooksController(LibraryDbContext _context)
        {
            context = _context;   
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await context.Books
                .AsNoTracking()
                .Select(x=> new BookInfoViewModel(
                    x.Id,
                    x.Title,
                    x.Author,
                    x.ImageUrl,
                    x.Rating.ToString(),
                    x.Category.Name))
                .ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int bookId) 
        {
            var model = await context.Books
                .AsNoTracking()
                .Where(x => x.Id == bookId)
                .Include(x => x.ApplicationUsersBooks)
                .FirstOrDefaultAsync();

            if (model is null) 
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (!model.ApplicationUsersBooks.Any(x => x.ApplicationUserId == userId)) 
            {
                model.ApplicationUsersBooks.Add(new ApplicationUserBook()
                {
                    ApplicationUserId = userId,
                    BookId = bookId
                });

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(All));

        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int bookId) 
        {
            var model = await context.Books
                .FindAsync(bookId);

            if (model is null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            var aui = model.ApplicationUsersBooks.FirstOrDefault(x => x.ApplicationUserId == userId);

            if (aui is null) 
            {
                return BadRequest();
            }

            model.ApplicationUsersBooks.Remove(aui);

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Mine));
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
