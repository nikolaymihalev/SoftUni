using AspNetCoreAdvancedDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAdvancedDemo.ViewComponents
{
    public class HelloWorldViewComponent : ViewComponent
    {
        private readonly DataService _dataService;
        public HelloWorldViewComponent(DataService dataService)
         => _dataService = dataService;

        public async Task<IViewComponentResult> InvokeAsync(string name)
        {
            string helloMessage =
            await _dataService.GetHelloAsync();
            ViewData["Message"] = helloMessage;
            ViewData["Name"] = name;
            return View();
        }
    }

}
