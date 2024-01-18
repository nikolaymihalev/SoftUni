using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIntro.Controllers
{
    public class IntroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetNumber(int number) 
        {
            ViewBag.Title = "GetNumber";
            return View("Number",number);
        }
    }
}
