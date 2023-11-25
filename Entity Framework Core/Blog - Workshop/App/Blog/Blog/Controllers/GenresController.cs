﻿using Blog.Models;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class GenresController:Controller
    {
        readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() 
        {
            var genres = await _genreService.GetGenresAsync();
            return View(genres);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Genre genre) 
        {
            if (ModelState.IsValid) 
            {
                await _genreService.CreateGenreAsync(genre);
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }
    }
}
