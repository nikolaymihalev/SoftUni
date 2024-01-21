using Microsoft.AspNetCore.Mvc;

namespace MVCInroDemo.Controllers
{
    public class NumbersController : Controller
    {
        private readonly ILogger<NumbersController> logger;

        public NumbersController(ILogger<NumbersController> _logger)
        {
            logger=_logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
