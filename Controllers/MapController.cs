using Microsoft.AspNetCore.Mvc;

namespace CoffeeMap20241106.Controllers
{
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
