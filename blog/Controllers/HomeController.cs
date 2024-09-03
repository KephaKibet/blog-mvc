using System.Diagnostics;
using blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Post()
        {
            return View();
        }
        public IActionResult Header()
        {
            return View();
        }
		[HttpGet]
		public IActionResult Edit()
        {
            return View(new Post());
        }
        [HttpPost]
        public IActionResult Edit(Post post)
        {
            return RedirectToAction("Index");
         }
    }
}
