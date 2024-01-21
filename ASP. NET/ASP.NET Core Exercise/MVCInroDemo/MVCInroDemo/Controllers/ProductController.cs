using Microsoft.AspNetCore.Mvc;
using MVCInroDemo.Models;

namespace MVCInroDemo.Controllers
{
    public class ProductController : Controller
    {
        private readonly List<ProductViewModel> products = new List<ProductViewModel>() 
        {
            new ProductViewModel()
            {
                Id = 1,
                Name = "Cheese",
                Price = 2.4
            },
            new ProductViewModel()
            {
                Id = 2,
                Name = "Ham",
                Price = 4.7
            }
        };

        public IActionResult Index()
        {
            return View(products);
        }

        public IActionResult ById(int id)
        {
            if (id < 0 || id >= products.Count) 
            {
                TempData["Error"] = "No such product";
                return RedirectToAction(nameof(Index));
            }

            return View(products[id]);
        }
    }
}
