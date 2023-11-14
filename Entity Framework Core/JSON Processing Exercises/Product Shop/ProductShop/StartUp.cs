using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;
using System.Runtime.CompilerServices;

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
        
        //05. Export products in range
        public static string GetProductsInRange(ProductShopContext context) 
        {
            var produtctsInRange = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p=>new 
                {
                    name = p.Name,
                    price = p.Price,
                    seller = p.Seller.FirstName + " " + p.Seller.LastName
                })
                .OrderBy(p=>p.price)
                .ToArray();

            var json = JsonConvert.SerializeObject(produtctsInRange, Formatting.Indented);

            return json;
        }
        
        //06. Export sold products
        public static string GetSoldProducts(ProductShopContext context) 
        {
            var usersWithSoldProducts = context.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .OrderBy(u=>u.LastName)
                    .ThenBy(u=>u.FirstName)
                .Select(u=> new 
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,  
                    soldProducts = u.ProductsSold
                                    .Where(p=>p.BuyerId != null)
                                    .Select(p=>new 
                                    {
                                        name = p.Name,
                                        price = p.Price,
                                        buyerFirstName = p.Buyer.FirstName,
                                        buyerLastName = p.Buyer.LastName
                                    })
                })
                .ToArray();

            var json = JsonConvert.SerializeObject(usersWithSoldProducts, Formatting.Indented);
            return json;
        }
        
        //07. Export categories by products count
        public static string GetCategoriesByProductsCount(ProductShopContext context) 
        {
            var categoriesByProductsCount = context.Categories
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoriesProducts.Count(),
                    averagePrice = c.CategoriesProducts.Average(cp => cp.Product.Price).ToString("f2"),
                    totalRevenue = c.CategoriesProducts.Sum(cp=>cp.Product.Price).ToString("f2")
                })
                .OrderByDescending(x=>x.productsCount)
                .ToArray();
            
            var json = JsonConvert.SerializeObject(categoriesByProductsCount, Formatting.Indented);
            return json;
        }

        //08. Export users and products
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var usersWithProducts = context.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = u.ProductsSold.Where(p => p.BuyerId != null)
                                                 .Select(p => new
                                                 {
                                                     name = p.Name,
                                                     price = p.Price
                                                 })
                                                 .ToArray()
                })
                .OrderByDescending(u => u.soldProducts.Count())
                .ToArray();

            var output = new
            {
                usersCount = usersWithProducts.Count(),
                users = usersWithProducts.Select(u => new
                {
                    u.firstName,
                    u.lastName,
                    u.age,
                    soldProducts = new
                    {
                        count = u.soldProducts.Count(),
                        products = u.soldProducts
                    }
                })
            };

            string json = JsonConvert.SerializeObject(output, new JsonSerializerSettings 
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            });

            return json;
        }
    }
}