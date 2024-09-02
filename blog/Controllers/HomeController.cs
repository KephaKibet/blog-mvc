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
    }
}
