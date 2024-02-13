using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}
