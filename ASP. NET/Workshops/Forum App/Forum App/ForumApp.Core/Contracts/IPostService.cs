using ForumApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Core.Contracts
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetAllPostsAsync();
        Task AddAsync(PostDto model);
        Task<PostDto?> GetByIdAsync(int id);
        Task EditAsync(PostDto model);
        Task DeleteAsync(int id);
    }
}
