using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBoard.Data;
using TaskBoard.Models;

namespace TaskBoard.Controllers
{
    public class BoardController : Controller
    {
        private readonly TaskBoardAppDbContext data;

        public BoardController(TaskBoardAppDbContext _data)
        {
            data = _data;
        }

        public async Task<IActionResult> Index()
        {
            var boards = await data.Boards
                .Select(b => new BoardViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Tasks = b.Tasks.Select(t => new TaskViewModel()
                    {
                        Id = t.Id,
                        Title = t.Title,
                        CreatedOn = DateTime.Now,
                        Description = t.Description,
                        Owner = t.Owner.UserName,
                    })
                })
                .ToListAsync();


            return View(boards);
        }
    }
}
