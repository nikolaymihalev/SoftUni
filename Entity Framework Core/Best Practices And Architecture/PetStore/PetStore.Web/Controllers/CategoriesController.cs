using Microsoft.AspNetCore.Mvc;

namespace PetStore.Web.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
