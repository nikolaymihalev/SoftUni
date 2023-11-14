using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext context = new ProductShopContext();
        }

        //01. Import users
        public static string ImportUsers(ProductShopContext context, string inputJson) 
        {
            var users = JsonConvert.DeserializeObject<User[]>(inputJson);
            context.Users.AddRange(users);
            context.SaveChanges();
            return $"Successfullty imported {users.Length}";
        }
        
        //02. Import products
        public static string ImportProducts(ProductShopContext context, string inputJson) 
        {
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);
            if (products != null) 
            {
                context.Products.AddRange(products);
                context.SaveChanges();
            }
            return $"Successfullty imported {products.Length}";
        }
        
        //03. Import categories
        public static string ImportCategories(ProductShopContext context, string inputJson) 
        {
            var categories = JsonConvert.DeserializeObject<Category[]>(inputJson);
            var validCategories = categories.Where(c => c.Name is not null).ToArray();

            if (validCategories != null) 
            {
                context.Categories.AddRange(validCategories);
                context.SaveChanges();
            }
            return $"Successfullty imported {validCategories.Length}";
        }
        
        //04. Import category products
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson) 
        {
            var categoryProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);
            if (categoryProducts != null) 
            {
                context.CategoriesProducts.AddRange(categoryProducts);
                context.SaveChanges();
            }
            return $"Successfullty imported {categoryProducts.Length}";
        }
    }
}