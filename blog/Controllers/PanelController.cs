﻿using System.Runtime.Intrinsics.X86;
using blog.Data.FileManager;
using blog.Data.Repository;
using blog.Models;
using blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
        //[Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private IRepository _repo;
        private IFileManager _fileManager;
		public PanelController(
            IRepository repo,
            IFileManager fileManager
            )
        {
            _repo = repo;
            _fileManager = fileManager;

		}

        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(new PostViewModel());
            }

            else
            {
                var post = _repo.GetPost((int)id);
                return View(new PostViewModel
                {
                    Id = post.Id,
                    Tittle = post.Tittle,
                    Body = post.Body,
                    CurrentImage = post.Image,
                    Description = post.Description,
                    Category = post.Category,
                    Tags = post.Tags,

                });
            }

        }
       
        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {
            var post = new Post
            {
                Id = vm.Id,
                Tittle = vm.Tittle,
                Body = vm.Body,
				Description = vm.Description,
				Category = vm.Category,
				Tags = vm.Tags,
            };

            if (vm.Image == null)
                post.Image = vm.CurrentImage;

            else
				post.Image = await _fileManager.SaveImage(vm.Image);

			if (post.Id > 0)
                _repo.UpdatePost(post);
            else
                _repo.AddPost(post);


            if (await _repo.SaveChangesAsync())
            {
                return RedirectToAction("Index", "Panel");
            }
            else
                return View(post);
        }

		[HttpGet]
		public async Task<IActionResult> Remove(int id)
        {
            _repo.RemovePost(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}


