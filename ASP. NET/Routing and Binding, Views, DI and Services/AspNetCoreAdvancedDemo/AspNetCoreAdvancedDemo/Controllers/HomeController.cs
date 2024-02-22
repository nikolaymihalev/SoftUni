using AspNetCoreAdvancedDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;

namespace AspNetCoreAdvancedDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upload() => View();

        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            var filePath = Path.GetTempFileName();
            foreach (var formFile in files.Where(f => f.Length > 0))
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            } 
            var bytes = files.Sum(f => f.Length);
            return Ok(new { count = files.Count, bytes, filePath });
        }

        public IActionResult Download(string fileName)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "Files");
            IFileProvider provider = new PhysicalFileProvider(filePath); 
            IFileInfo fileInfo = provider.GetFileInfo(fileName); 

            var readStream = fileInfo.CreateReadStream(); 
            var mimeType = "application/octet-stream"; 

            return File(readStream, mimeType, fileName); 
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
