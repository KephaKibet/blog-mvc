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
			var posts = _repo.GetAllPosts();
            return View(posts);
        }
        public IActionResult Post(int id)
        {
            var post = _repo.GetPost(id);
            return View(post);
		}
	}
}
