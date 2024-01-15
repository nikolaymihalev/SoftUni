using Microsoft.AspNetCore.Mvc;

namespace HttpBasic.Controllers
{
    public class StoreListController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddItem() 
        {
            return View();
        }
    }
}
