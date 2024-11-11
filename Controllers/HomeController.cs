using CoffeeMap20241106.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoffeeMap20241106.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("language/coffeeMap")]
        public class MapController : Controller
        {
            [Route("")]
            public IActionResult CoffeeMap()
            {
                return View();
            }
        }
    }
}
