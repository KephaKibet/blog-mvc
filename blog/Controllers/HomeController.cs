using System.Diagnostics;
using blog.Data.Repository;
using blog.Models;
using Blog.Data;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
    public class HomeController : Controller
    {
		private IRepository _repo;

		public HomeController(IRepository repo)
		{
			_repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Post()
        {
            return View();
        }
        public IActionResult Remove()
        {
            return View();
        }
        [HttpGet]
		public IActionResult Edit()
        {
            return View(new Post());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
			_repo.AddPost(post);

            if (await _repo.SaveChangesAsync())
            {

                return RedirectToAction("Index");
            }
            else 
                return View(post);
         }
    }
}
