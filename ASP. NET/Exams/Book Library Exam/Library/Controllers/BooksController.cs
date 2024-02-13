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

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
