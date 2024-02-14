﻿using ForumApp.Core.Contracts;
using ForumApp.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;

namespace ForumApp.Controllers
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
            IEnumerable<PostModel> models = await postService.GetAllPostsAsync();

            return View(models);
        }

        public IActionResult Add()
        {
            var model = new PostModel();

            return View(model);
        }

        public async Task<IActionResult> Add(PostModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await postService.AddAsync(model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            PostModel? model = await postService.GetByIdAsync(id);

            if (model == null)
            {
                ModelState.AddModelError("All", "Invalid post");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await postService.EditAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await postService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
