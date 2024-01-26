using Microsoft.EntityFrameworkCore;
using ShoppingListApp.Contracts;
using ShoppingListApp.Data;
using ShoppingListApp.Data.Models;
using ShoppingListApp.Models;

namespace ShoppingListApp.Services
{
    public class ProductService : IProductService
    {
        private readonly ShoppingListDbContext context;
        
        public ProductService(ShoppingListDbContext _context)
        {
            context = _context;
        }

        public async Task AddProductAsync(ProductViewModel product)
        {
            var entity = new Product()
            {
                Name = product.Name,
            };

            await context.Products.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            return await context.Products.AsNoTracking()
                .Select(p=> new ProductViewModel() 
                { 
                    Id = p.Id, 
                    Name= p.Name})
                .ToListAsync();
        }

        public Task<ProductViewModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(ProductViewModel product)
        {
            throw new NotImplementedException();
        }
    }
}
