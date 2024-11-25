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
                ViewBag.Message = "�|���|���n�J"; // �p�G�S���n�J�A��ܦ��T��
            }
            else
            {
                var member = _context.Customers.FirstOrDefault(u => u.CustomerId == loginId);

                if (member == null)
                {
                    ViewBag.Message = "���|���|���]�w�u��m�W";
                }
                else
                {
                    ViewBag.Message = $"{member.UserId} �A�n"; // �]�w��ܰT��
                }
            }
            return View();
        }


        // ���������ո��
        public IActionResult GetTestJson()
        {
            // ���եΪ� JSON ���
            var testData = new List<ShipStatus>
            {
                new ShipStatus { OrderId = "O2410280001", Status = "�w�e�F" },
                new ShipStatus { OrderId = "O2410280002", Status = "���e�F" }
            };

            // �o�̪�^ JSON �榡�����
            return Json(testData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
