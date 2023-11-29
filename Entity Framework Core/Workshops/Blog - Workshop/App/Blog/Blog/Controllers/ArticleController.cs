using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class ArticleController : Controller
    {
        readonly IArticleService _articleService;
        readonly IGenreService _genreService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task<IActionResult> Index() 
        {
            var articles = await _articleService.GetArticlesAsync();
            return View(articles);
        }
        public async Task<IActionResult> Create()
        {
            var genres = await _genreService.GetGenresAsync();

            return View();
        }
    }
}
