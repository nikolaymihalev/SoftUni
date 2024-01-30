﻿using ForumApp.Core.Contracts;
using ForumApp.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forum_App.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService _postService)
        {
            postService = _postService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<PostDto> model = await postService.GetAllPostsAsync();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add() 
        {
            var model = new PostDto();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostDto model) 
        {
            if (ModelState.IsValid == false) 
            {
                return View(model);
            }

            await postService.AddAsync(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
