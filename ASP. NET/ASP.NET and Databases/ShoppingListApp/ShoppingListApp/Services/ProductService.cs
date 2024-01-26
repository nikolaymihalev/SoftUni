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

        public async Task<ProductViewModel> GetByIdAsync(int id)
        {
            var entity = await context.Products.FindAsync(id);

            if (entity == null) 
            {
                throw new ArgumentException("Invalid product");
            }

            return new ProductViewModel() 
            {
                Id = id,
                Name = entity.Name
            };
        }

        public async Task UpdateProductAsync(ProductViewModel product)
        {
            var entity = await context.Products.FindAsync(product.Id);

            if (entity == null)
            {
                throw new ArgumentException("Invalid product");
            }

            entity.Name = product.Name;

            await context.SaveChangesAsync();
        }
    }
}
