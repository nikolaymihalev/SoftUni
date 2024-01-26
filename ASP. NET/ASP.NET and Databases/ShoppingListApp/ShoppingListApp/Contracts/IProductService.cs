using ShoppingListApp.Models;

namespace ShoppingListApp.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllAsync();
        Task<ProductViewModel> GetByIdAsync(int id);
        Task AddProductAsync(ProductViewModel product);
        Task UpdateProductAsync(ProductViewModel product);
        Task DeleteProductAsync(int id);
    }
}
