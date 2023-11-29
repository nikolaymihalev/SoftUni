using Blog.Data;
using Blog.Models;
using Blog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services
{
    public class ArticleService : IArticleService
    {
        readonly BlogDBContext _context;

        public ArticleService(BlogDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> GetArticlesAsync() 
        {
            return await _context.Articles.AsNoTracking().ToListAsync();
        }
    }
}
