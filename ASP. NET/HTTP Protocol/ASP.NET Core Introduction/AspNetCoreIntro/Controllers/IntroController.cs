using AspNetCoreIntro.Models;
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

        public IActionResult GetStudentData() 
        {
            ViewBag.Title = "GetStudentData";

            var student = new Student
            {
                Id =1,
                Name = "Jhon",
                Email= "jhon@gmail.com"
            };

            return View(student);
        }
    }
}
