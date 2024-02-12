using Contacts.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Contacts.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (User?.Identity != null && User.Identity.IsAuthenticated) 
            {
                return RedirectToAction("All", "Contacts");
            }
            return View();
        }
    }
}