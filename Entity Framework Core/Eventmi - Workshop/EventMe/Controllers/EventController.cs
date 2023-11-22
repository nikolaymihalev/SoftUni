using Microsoft.AspNetCore.Mvc;

namespace EventMe.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
