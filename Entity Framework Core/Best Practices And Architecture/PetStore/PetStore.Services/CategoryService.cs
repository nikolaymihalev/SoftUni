using Microsoft.EntityFrameworkCore;
using PetStore.Data;
using PetStore.Data.Models;
using PetStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Services
{
    public class CategoryService:ICategoryService
    {
        readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAll() 
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
