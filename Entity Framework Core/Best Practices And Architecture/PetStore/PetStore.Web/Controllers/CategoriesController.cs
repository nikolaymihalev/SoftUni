using Microsoft.AspNetCore.Mvc;
using PetStore.Services.Interfaces;

namespace PetStore.Web.Controllers
{
    public class CategoriesController : Controller
    {
        readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var service = await _categoryService.GetAll();
            return View(service);
        }
    }
}
