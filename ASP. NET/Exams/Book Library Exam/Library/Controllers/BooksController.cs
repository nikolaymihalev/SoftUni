﻿using Library.Data;
using Library.Data.Models;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace Library.Controllers
{
    [Authorize]
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
                    x.Rating.ToString("f2"),
                    x.Category.Name,
                    x.Description))
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
                var aub = new ApplicationUserBook()
                {
                    ApplicationUserId = userId,
                    BookId = bookId
                };
                model.ApplicationUsersBooks.Add(aub);

                await context.ApplicationUsersBooks.AddAsync(aub);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Mine));

        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int bookId) 
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

            var aui = model.ApplicationUsersBooks.FirstOrDefault(x => x.ApplicationUserId == userId);

            if (aui is null) 
            {
                return BadRequest();
            }

            model.ApplicationUsersBooks.Remove(aui);

            context.ApplicationUsersBooks.Remove(aui);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Mine() 
        {
            string userId = GetUserId();
            var model = await context.ApplicationUsersBooks
                .Where(x => x.ApplicationUserId == userId)
                .Select(x=>new BookInfoViewModel(
                    x.Book.Id,
                    x.Book.Title,
                    x.Book.Author,
                    x.Book.ImageUrl,
                    x.Book.Rating.ToString("f2"),
                    x.Book.Category.Name,
                    x.Book.Description))
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add() 
        {
            var model = new BookFormViewModel();
            model.Categories = await GetCategories();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookFormViewModel model) 
        {
            if (!ModelState.IsValid) 
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            var book = new Book() 
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                CategoryId = model.CategoryId
            };

            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        public async Task<IEnumerable<Category>> GetCategories() 
        {
            return await context.Categories.AsNoTracking().ToListAsync();
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
