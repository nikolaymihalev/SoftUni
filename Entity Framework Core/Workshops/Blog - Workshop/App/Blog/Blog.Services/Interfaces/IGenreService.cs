using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services.Interfaces
{
    public interface IGenreService
    {
        Task<List<Genre>> GetGenresAsync();
        Task CreateGenreAsync(Genre genre);
    }
}
