using ForumApp.Core.Contracts;
using ForumApp.Core.Models;
using ForumApp.Infrastructure.Data;
using ForumApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger logger;

        public PostService(
            ForumDbContext _context,
            ILogger<PostService> _logger)
        {
            context = _context;
            logger = _logger;
        }

        public async Task AddAsync(PostDto model)
        {
            var entity = new Post()
            {
                Title = model.Title,
                Content = model.Content,
            };

            try
            {
                await context.AddAsync<Post>(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "PostService.AddAsync");
                throw new ApplicationException("Operation failed. Try again!");
            }
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
