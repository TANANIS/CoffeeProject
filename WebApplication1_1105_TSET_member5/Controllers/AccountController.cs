using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using WebApplication1_1105_TSET_member5.Models;
using System.IO;
using WebApplication1_1105_TSET_member5.Views.Account;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace WebApplication1_1105_TSET_member5.Controllers
{
    public class AccountController : Controller
    {
        private readonly ProjectContext _context;

        public AccountController(ProjectContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var userId = _context.Customers.FirstOrDefault(u => u.UserId == model.UserId);
                if (userId != null)
                {
                    var login = _context.Customers.FirstOrDefault(u => u.UserId == model.UserId && u.Password == model.Password);
                    if (login != null)
                    {
                        // 登入成功，設定 Cookie 或 Session
                        HttpContext.Session.SetString("loginId", login.CustomerId);
                        TempData["id2"] = login.CustomerId;
                        return RedirectToAction("Index", "Account"); //登入後預設為Account的Index畫面
                    }
                }
                else { return View("Register"); }
            }
            return View(model);
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("loginId");
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Index()
        {
            var loginId = HttpContext.Session.GetString("loginId");
            if (loginId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            TempData["id2"] = loginId;

            //大頭照
            var avatar = await _context.Customers
                                  .FirstOrDefaultAsync(c => c.CustomerId == loginId);
            if (avatar != null && avatar.ImgSrc != null)
            {
                TempData["avatar2"] = avatar.ImgSrc;
            }
            else
            {
                // 若 avatar 或 ImgSrc 為 null，您可以設置一個預設值，或進行錯誤處理
                TempData["avatar2"] = "/img/Member/defaultAvatar.jpg"; // 設置預設圖片
            }

            // 查詢Orderheader資料表
            var orderHeaders = await _context.Orderheaders.Where(o => o.CustomerId == loginId).ToListAsync();
            // 為每個OrderHeader準備HeaderViewModel
            var headerViewModels = new List<HeaderViewModel>();

            foreach (var o in orderHeaders)
            {
                var headerViewModel = new HeaderViewModel
                {
                    OrderId = o.OrderId,
                    CustomerId = o.CustomerId,
                    OrderDate = o.OrderDate,
                    Payment = o.Payment,
                    Total = o.Total,
                    OrderStatus = o.OrderStatus,
                    ShipStatus = o.ShipStatus
                };
                // 查詢對應OrderId的OrderDetail資料
                var orderDetails = await _context.Orderdetails
                                       .Where(od => od.OrderId == o.OrderId)
                                       .ToListAsync();

                // 填充OrderDetails的產品名稱
                foreach (var d in orderDetails)
                {
                    var product = await _context.Products
                                      .FirstOrDefaultAsync(p => p.ProductId == d.ProductId);
                    if (product != null)
                    {
                        headerViewModel.Orderdetails.Add(new DetailViewModel
                        {
                            ProductId = d.ProductId,
                            ProductName = product.ProductName,
                            Qty = d.Qty,
                            UnitPrice = d.UnitPrice,
                            Totle = d.Totle
                        });
                    }
                }
                headerViewModels.Add(headerViewModel);
            }

            //// 使用 HttpClient 發送 GET 請求來獲取外部 JSON 資料
            //using (var client = new HttpClient())
            //{
            //    var response = await client.GetStringAsync("https://your-json-api-url.com");
            //    var jsonData = JsonConvert.DeserializeObject<List<ShipStatus>>(response);

            //    // 使用 JSON 資料來更新 headerViewModels 中的出貨狀態
            //    foreach (var order in headerViewModels)
            //    {
            //        var orderStatus = jsonData.FirstOrDefault(x => x.OrderId == order.OrderId);
            //        if (orderStatus != null)
            //        {
            //            order.ShipStatus = orderStatus.Status; // 更新 ShipStatus
            //        }
            //    }
            //}

            return View(headerViewModels);
        }

        public async Task<IActionResult> Details(string? id)
        {
            var loginId = HttpContext.Session.GetString("loginId");
            TempData["id2"] = loginId;

            if (id == null)
            {
                return NotFound();
            }

            //大頭照
            var avatar = await _context.Customers
                                  .FirstOrDefaultAsync(c => c.CustomerId == loginId);
            if (avatar != null && avatar.ImgSrc != null)
            {
                TempData["avatar2"] = avatar.ImgSrc;
            }
            else
            {
                // 若 avatar 或 ImgSrc 為 null，您可以設置一個預設值，或進行錯誤處理
                TempData["avatar2"] = "/img/Member/defaultAvatar.jpg"; // 設置預設圖片
            }


            //找OrderId
            var orderHeader = await _context.Orderheaders
                               .FirstOrDefaultAsync(o => o.OrderId == id);

            if (orderHeader == null)
            {
                return NotFound();
            }

            // 查找該OrderId的所有Orderdetail資料
            var orderDetails = await _context.Orderdetails
                                   .Where(od => od.OrderId == id)
                                   .ToListAsync();

            // 構建ViewModel
            var detailViewModels = new List<DetailViewModel>();

            foreach (var d in orderDetails)
            {
                var product = await _context.Products
                                      .FirstOrDefaultAsync(p => p.ProductId == d.ProductId);

                detailViewModels.Add(new DetailViewModel
                {
                    ProductId = d.ProductId,
                    ProductName = product?.ProductName,  // 確保ProductName是從Product資料表取得的
                    Qty = d.Qty,
                    Uom = d.Uom,
                    UnitPrice = d.UnitPrice,
                    Totle = d.Totle
                });
            }

            // 將OrderHeader與OrderDetails傳遞給視圖
            var viewModel = new HeaderViewModel
            {
                OrderId = orderHeader.OrderId,
                OrderDate = orderHeader.OrderDate,
                CustomerId = orderHeader.CustomerId,
                Total = orderHeader.Total,
                OrderStatus = orderHeader.OrderStatus,
                ShipStatus = orderHeader.ShipStatus,
                Orderdetails = detailViewModels
            };

            return View(viewModel);
        }

        public IActionResult Register()
        {
            return View();
        }

        private readonly string _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img/Member");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("CustomerId,UserId,Password,Email,Phone,Name,Gender,Birthday,Language,ReceiverAddress,CreateUser,UpdateUser,CreateDate,UpdateDate")] Customer customer, IFormFile? ImgSrc)
        {
            // 檢查是否有檔案上傳
            if (ImgSrc != null)
            {
                // 確保 上傳資料夾存在
                if (!Directory.Exists(_uploadsFolder))
                {
                    Directory.CreateDirectory(_uploadsFolder);
                }

                // 取得檔案名稱與副檔名
                var fileName = Path.GetFileName(ImgSrc.FileName);
                var filePath = Path.Combine(_uploadsFolder, fileName);

                // 儲存檔案到伺服器
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImgSrc.CopyToAsync(stream);  // 儲存上傳檔案到指定路徑
                }

                // 將檔案的相對路徑儲存到 ImgSrc 屬性
                customer.ImgSrc = "/img/Member/" + fileName;
            }

            // 查詢資料庫中最大的 CustomerId
            var lastCustomerId = await _context.Customers
                .OrderByDescending(c => c.CustomerId)  // 按照 CustomerId 進行降序排列
                .FirstOrDefaultAsync();

            // 計算新的 CustomerId
            string newCustomerId;
            // 提取最後一個 CustomerId 的數字部分
            string lastId = lastCustomerId!.CustomerId.Substring(4);  // 假設 CustomerId 格式為 CUST00000001
            int lastNumber = int.Parse(lastId);  // 將數字部分轉換為整數
            int newNumber = lastNumber + 1;  // 新的數字是上一個數字 + 1
            newCustomerId = "CUST" + newNumber.ToString("D8");  // 生成新的 CustomerId，並保證八位數字，不足位補零

            // 檢查新的 CustomerId 是否已經存在
            while (await _context.Customers.AnyAsync(c => c.CustomerId == newCustomerId))
            {
                newNumber++;
                newCustomerId = "CUST" + newNumber.ToString("D8");
            }

            customer.CustomerId = newCustomerId;  // 設置新的 CustomerId

            customer.CreateDate = DateTime.Now;
            customer.UpdateDate = DateTime.Now;
            customer.CreateUser = customer.UserId;
            customer.UpdateUser = customer.UserId;

            _context.Add(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Account/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            var loginId = HttpContext.Session.GetString("loginId");
            TempData["id2"] = loginId;

            if (id == null)
            {
                return NotFound();
            }
            //大頭照
            var avatar = await _context.Customers
                                  .FirstOrDefaultAsync(c => c.CustomerId == loginId);
            if (avatar != null && avatar.ImgSrc != null)
            {
                TempData["avatar2"] = avatar.ImgSrc;
            }
            else
            {
                // 若 avatar 或 ImgSrc 為 null，您可以設置一個預設值，或進行錯誤處理
                TempData["avatar2"] = "/img/Member/defaultAvatar.jpg"; // 設置預設圖片
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CustomerId,UserId,Password,Email,Phone,Name,Gender,Birthday,Language,ReceiverAddress,CreateUser,UpdateUser,CreateDate,UpdateDate")] Customer customer, IFormFile? ImgSrc)
        {
            var loginId = HttpContext.Session.GetString("loginId");
            TempData["id2"] = loginId;

            //大頭照
            if (customer.ImgSrc != null)
            {
                TempData["avatar2"] = customer.ImgSrc;
            }
            else
            {
                // 若 avatar 或 ImgSrc 為 null，您可以設置一個預設值，或進行錯誤處理
                TempData["avatar2"] = "/img/Member/defaultAvatar.jpg"; // 設置預設圖片
            }

            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            // 檢查是否有檔案上傳
            if (ImgSrc != null && ImgSrc.Length > 0)
            {
                // 確保 上傳資料夾存在
                if (!Directory.Exists(_uploadsFolder))
                {
                    Directory.CreateDirectory(_uploadsFolder);
                }

                // 取得檔案名稱與副檔名
                var fileName = Path.GetFileName(ImgSrc.FileName);
                var filePath = Path.Combine(_uploadsFolder, fileName);

                // 儲存檔案到伺服器
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImgSrc.CopyToAsync(stream);  // 儲存上傳檔案到指定路徑
                }

                // 將檔案的相對路徑儲存到 ImgSrc 屬性
                customer.ImgSrc = "/img/Member/" + fileName;
            }
            else
            {
                // 如果 imgSrc 為 null 或空，則保持原來的圖片路徑
                // 不改變 ImgSrc 欄位
                // 這樣可以防止把資料庫中的 ImgSrc 設為 null
                customer.ImgSrc = (string?)TempData["avatar2"];
            }


            try
            {
                // 更新時間戳與使用者
                customer.UpdateDate = DateTime.Now;
                customer.UpdateUser = customer.UserId;

                // 設置實體狀態為 Modified（強制更新所有屬性）
                _context.Entry(customer).State = EntityState.Modified;
                // 禁止更新 CustomerId 欄位
                _context.Entry(customer).Property(x => x.CustomerId).IsModified = false;
                // 禁止更新 Id 欄位（如果 Id 是識別屬性，應該不被修改）
                _context.Entry(customer).Property(x => x.Id).IsModified = false;

                // 儲存變更
                await _context.SaveChangesAsync();

                // 儲存完成後，將訊息放入 TempData 以便在視圖中顯示
                TempData["SaveSuccess"] = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.CustomerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            //return View(customer);
            return RedirectToAction("Edit", new { id = customer.CustomerId }); // 重導回 "ChangePassword" 頁面
        }
        private bool CustomerExists(string id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }

        // 顯示更改密碼的頁面
        public async Task<IActionResult> ChangePassword(string? id)
        {
            var loginId = HttpContext.Session.GetString("loginId");
            TempData["id2"] = loginId;

            if (id == null)
            {
                return NotFound();
            }
            //大頭照
            var avatar = await _context.Customers
                                  .FirstOrDefaultAsync(c => c.CustomerId == loginId);
            if (avatar != null && avatar.ImgSrc != null)
            {
                TempData["avatar2"] = avatar.ImgSrc;
            }
            else
            {
                // 若 avatar 或 ImgSrc 為 null，您可以設置一個預設值，或進行錯誤處理
                TempData["avatar2"] = "/img/Member/defaultAvatar.jpg"; // 設置預設圖片
            }


            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
            //return RedirectToAction("ChangePassword", new { id = customer.CustomerId }); // 重導回 "ChangePassword" 頁面

        }

        // POST: Account/ChangePassword/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string id, string newPassword, string confirmPassword)
        {
            var loginId = HttpContext.Session.GetString("loginId");
            TempData["id2"] = loginId;

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }


            // 更新密碼
            customer.Password = newPassword;

            try
            {
                // 更新時間戳與使用者
                customer.UpdateDate = DateTime.Now;
                customer.UpdateUser = customer.UserId;

                // 設置實體狀態為 Modified（強制更新所有屬性）
                _context.Entry(customer).State = EntityState.Modified;
                // 禁止更新 CustomerId 欄位
                _context.Entry(customer).Property(x => x.CustomerId).IsModified = false;
                // 禁止更新 Id 欄位（如果 Id 是識別屬性，應該不被修改）
                _context.Entry(customer).Property(x => x.Id).IsModified = false;

                // 儲存變更
                await _context.SaveChangesAsync();

                // 儲存完成後，顯示提示
                TempData["SaveSuccess"] = true;

                // 重新導向回 ChangePassword 頁面
                return RedirectToAction("ChangePassword", new { id = customer.CustomerId });

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.CustomerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
