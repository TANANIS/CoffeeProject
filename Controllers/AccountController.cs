using Microsoft.AspNetCore.Mvc;

namespace PROJECT.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
