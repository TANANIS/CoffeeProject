using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.NetworkInformation;
using WebApplication1_1105_TSET_member5.Models;
using WebApplication1_1105_TSET_member5.Views.Home;

namespace WebApplication1_1105_TSET_member5.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private readonly ProjectContext _context;

        public HomeController(ProjectContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Privacy()
        {
			var loginId = HttpContext.Session.GetString("loginId");
            TempData["id2"] = loginId;
            if (loginId == null)
            {
                ViewBag.Message = "會員尚未登入"; // 如果沒有登入，顯示此訊息
            }
            else
            {
                var member = _context.Customers.FirstOrDefault(u => u.CustomerId == loginId);

                if (member == null)
                {
                    ViewBag.Message = "此會員尚未設定真實姓名";
                }
                else
                {
                    ViewBag.Message = $"{member.UserId} 你好"; // 設定顯示訊息
                }
            }
            return View();
        }


        // 模擬的測試資料
        public IActionResult GetTestJson()
        {
            // 測試用的 JSON 資料
            var testData = new List<ShipStatus>
            {
                new ShipStatus { OrderId = "O2410280001", Status = "已送達" },
                new ShipStatus { OrderId = "O2410280002", Status = "未送達" }
            };

            // 這裡返回 JSON 格式的資料
            return Json(testData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
