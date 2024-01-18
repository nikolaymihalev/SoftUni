using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIntro.Controllers
{
    public class IntroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
