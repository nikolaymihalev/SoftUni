using Microsoft.AspNetCore.Mvc;
using TaskBoard.Data;

namespace TaskBoard.Controllers
{
    public class BoardController : Controller
    {
        private readonly TaskBoardAppDbContext data;

        public BoardController(TaskBoardAppDbContext _data)
        {
            data = _data;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
