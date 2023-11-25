using Blog.Data;
using Blog.Models;
using Blog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services
{
    public class GenreService : IGenreService
    {
        readonly BlogDBContext _context;

        public GenreService(BlogDBContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> GetGenresAsync() 
        {
            return await _context.Genres.AsNoTracking().ToListAsync();
        }
        public async Task CreateGenreAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
        }
    }
}
