using ForumApp.Core.Contracts;
using ForumApp.Core.Models;
using ForumApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Core.Services
{
    public class PostService : IPostService
    {
        private readonly ForumDbContext context;

        public PostService(ForumDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            return await context.Posts                
                .Select(p=> new PostDto()
                { 
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
