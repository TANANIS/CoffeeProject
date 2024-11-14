using CN;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using PROJECT.Models;
using System.Data;


namespace PROJECT.Controllers
{
    public class ProductController : Controller
    {
        // 建立物件 設定連線
        DBCN _dbcn = new DBCN("Server=127.0.0.1;Database=PROJECT;User ID=sa;Password=12345;TrustServerCertificate=true");
        private readonly ProjectContext _context;

        public ProductController(ProjectContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            string sql = "SELECT * FROM PRODUCT";
            DataTable dt = _dbcn.SQL(sql);
            List<Product> DLProduct = new List<Product>();
            foreach (DataRow row in dt.Rows)
            {
                DLProduct.Add(new Product
                {
                    ProductId = row["ProductId"].ToString(),
                    ProductName = row["ProductName"].ToString()
                });
            }
            return View(DLProduct);
            return View(DLProduct);
        }
        public IActionResult commodity()
        {
            string sql = "SELECT * FROM PRODUCT";
            DataTable dt = _dbcn.SQL(sql);
            List<Product> DLProduct = new List<Product>();
            foreach (DataRow row in dt.Rows)
            {
                DLProduct.Add(new Product
                {
                    ProductId = row["ProductId"].ToString(),
                    ProductName = row["ProductName"].ToString(),
                    Price = (short)row["Price"]

                });
            }
            return View(DLProduct);
        }
        [HttpGet]
        public IActionResult Payment()
        {
            return View();
        }
        [HttpPost]
        [Route("Product/Payment")]
        public IActionResult Payment(IFormCollection form)
        {
            ViewBag.ID = form["itemNO"].ToList();
            ViewBag.ProductID = form["itemId"].ToList();
            ViewBag.Name = form["itemNane"].ToList();
            ViewBag.QTY = form["itemQTY"].ToList();
            ViewBag.Price = form["itemprice"].ToList();
            ViewBag.itemtotle = form["itemtotal"].ToList();
            ViewBag.Alltotle = form["totalprice"];
            // 取得所有form
            return View();
        }
        public IActionResult shopping_cart()
        {
            return View();
        }

        [HttpPost]
        [Route("Product/OrderFormData")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderFormData(IFormCollection form)
        {
            // 異步操作
            await Task.Run(() =>
            {
                string? OrderNO = _dbcn.ItemNO("O");
                string sql = $"INSERT INTO ORDERHEADER(OrderId,OrderDate,Total) VALUES ('{OrderNO}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{form["totle"]}')";
                _dbcn.Insert(sql);

                for (int i = 0; i < Request.Form["itemNO"].Count; i++)
                {
                    // 依項目總數決定執行次數，優先級
                    sql = $"INSERT INTO ORDERDETAIL(OrderId,OrderItem,ProductId,Qty,UnitPrice,Totle,CreateDate,UpdateDate) VALUES ('{OrderNO}','{form["itemNO"][i]}','{form["itemProuductID"][i]}','{form["itemQTY"][i]}','{form["itemPrice"][i]}','{form["itemTotle"][i]}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' )";
                    _dbcn.Insert(sql);
                }
            });
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //[Route("Product/OrderFormData")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> OrderFormData(AddOrderJson order)
        //{
        //    string? OrderNO = _dbcn.ItemNO("O");
        //    var orderTitle = new Orderheader
        //    {
        //        OrderId = OrderNO,
        //        OrderDate = DateTime.Now,
        //        Status = "Y"
        //    };
        //    _context.Orderheaders.Add(orderTitle);
        //    foreach (var item in order.od)
        //    {
        //        var orderDetail = new Orderdetail
        //        {
        //            OrderId = OrderNO,
        //            OrderItem = item.OrderItem,
        //            ProductId = item.ProductId,
        //            Qty = item.Qty,
        //            UnitPrice = item.UnitPrice,
        //            Totle = item.Qty * item.UnitPrice,
        //        };
        //        _context.Orderdetails.Add(orderDetail);
        //    }
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}



        [HttpPost]
        [Route("Product/LOING")]
        public async Task<IActionResult> LOING(IFormCollection form)
        {
            var newcus = new Customer
            {
                CustomerId = form["login"],
                UserId = form["pass"]
            };
            _context.Customers.Add(newcus);
            await _context.SaveChangesAsync();

            return RedirectToAction("TRY");
        }

        // 使用者登入 Post一份到資料庫比較
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartJson cart)
        {
            // 尋找帳戶使否存在，狀態是否為 Y
            var CartHeader = _context.Cartheaters
                .FirstOrDefault(ch => ch.UserId == cart.Hmodel.UserId && ch.Status == "Y");
            string cartId;
            if (CartHeader != null)
            {
                // 若CartHeader存在 使用現在使用者 並更新以下
                cartId = CartHeader.CartId;
            }
            else
            {
                // 若不存在，建立新的購物車標題
                cartId = _dbcn.ItemNO("C");
                var cartTitle = new Cartheater
                {
                    CartId = cartId,
                    UserId = cart.Hmodel.UserId,
                    CreateDate = DateTime.Now,
                    Status = "Y"
                };
                _context.Cartheaters.Add(cartTitle);
            }
            // 取得 DB 購物車 存成清單
            var EXCartItems = _context.Cartdetails
                .Where(cd => cd.CartId == cartId)
                .ToList();
            foreach (var item in cart.Dmodels)
            {
                // 檢查是否有重複商品
                var CartItem = EXCartItems
                    .FirstOrDefault(cd => cd.ProductId == item.productID);
                if (CartItem != null)
                {
                    // 更新該產品數量等
                    CartItem.Qty += item.Qty;
                    CartItem.TotalPrice = CartItem.Qty * CartItem.UnitPrice;
                }
                else
                {
                    // 新增一筆產品
                    var newCartItem = new Cartdetail
                    {
                        CartId = cartId,
                        CartItemId = (short)(EXCartItems.Count + 1), // 最大item + 1
                        ProductId = item.productID,
                        Qty = item.Qty,
                        UnitPrice = item.UnitPrice,
                        TotalPrice = item.Qty * item.UnitPrice,
                        CreateDate = DateTime.Now,
                        Status = "Y"
                    };
                    _context.Cartdetails.Add(newCartItem);
                    EXCartItems.Add(newCartItem); // 更新清單
                }
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        // JS按鈕觸發單項狀態更改為 N
        [HttpPost]
        [Route("/Product/DeleteCartItem")]
        public async Task<IActionResult> DeleteCartItem([FromBody] SingelCartJson cart)
        {
            var cartheader = _context.Cartheaters
                .FirstOrDefault(ch => ch.UserId == cart.Hmodel.UserId && ch.Status == "Y");
            if (cartheader != null)
            {
                string cartId = cartheader.CartId;
                var CartItem = _context.Cartdetails
                    .FirstOrDefault(cd => cd.CartId == cartId && cd.ProductId == cart.Dmodels.ProductId && cd.Status == "Y");
                if (CartItem != null) 
                {
                    CartItem.Status = "N";
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("shopping_cart","Product");
        }


        [HttpGet]
        [Route("/Product/get")]
        public IActionResult GetCart(string userId)
        {
            // 查詢購物車標題
            var cartHeader = _context.Cartheaters
                .FirstOrDefault(ch => ch.UserId == userId && ch.Status == "Y");
            // 查詢購物車詳細項目
            var cartItems = _context.VCartProducts
                .Where(cd => cd.CartId == cartHeader.CartId && cd.DStatus == "Y")
                .Select(cd => new CartItemDto
                {
                    ProductId = cd.ProductId,
                    CartItemId = cd.CartItemId,
                    name = cd.ProductName,
                    qty = (short)cd.Qty,
                    price = (decimal)cd.UnitPrice,
                    total = cd.TotalPrice
                })
                .ToList();
            // 確認 cartItems 是否有內容
            if (cartItems == null)
            {
                return Ok(new { success = false, message = "購物車為空", Items = new List<object>() });
            }
            // 傳回 JSON
            return Ok(new
            {
                success = true,
                CartId = cartHeader.CartId,
                UserId = cartHeader.UserId,
                items = cartItems
            });
        }

        // JS清空購物車觸發標題狀態更改為 N
        [HttpGet]
        [Route("/Product/remove/{userId}")]
        public async Task<IActionResult> RemoveCart(string userId)
        {
            var removecart = _context.Cartheaters
                .FirstOrDefault(ch => ch.UserId == userId && ch.Status == "Y");
            if (removecart != null) 
            {
                removecart.Status = "N";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("shopping_cart", "Product"); // 假設購物車頁面是 Product 控制器的 Index 方法

        }



        // --------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------
        // 處理購物車資料庫 當用戶在登入時合併購物車(localstorage 與 DB 的融合，一樣的累加)
        //[HttpPost]
        //public async Task<IActionResult> AddToCart([FromBody] AddToCartJson cart)
        //{
        //    string CartIDDB = _dbcn.ItemNO("C");
        //    var cartTitle = new Cartheater
        //    {
        //        CartId = CartIDDB,
        //        UserId = cart.Hmodel.UserId,
        //        CreateDate = DateTime.Now,
        //        Status = "Y"
        //    };
        //    _context.Cartheaters.Add(cartTitle);

        //    // 加入判斷 ->
        //    // 使用者ID
        //    // 購物車狀態(Y/N) 只能有一筆為 Y 的購物車標題
        //    // 產品ID是否存在，存在加總為一筆  
        //    short itemNO = 1;
        //    foreach (var item in cart.Dmodels)
        //    {
        //        var cartItem = new Cartdetail
        //        {
        //            CartId = CartIDDB,
        //            CartItemId = itemNO,
        //            ProductId = item.ProductId,
        //            Qty = item.Qty,
        //            UnitPrice = item.UnitPrice,
        //            TotalPrice = item.Qty * item.UnitPrice, 
        //            CreateDate = DateTime.Now
        //        };
        //        _context.Cartdetails.Add(cartItem);
        //        itemNO += 1;
        //    }

        //    // 4. 保存變更至資料庫
        //    await _context.SaveChangesAsync();

        //    return Ok(new { success = true, message = "成功加入購物車!" });
        //}
        // --------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------




        public IActionResult TRY()
        {
            ViewBag.aaa = _dbcn.ItemNO("C");
            return View();
        }


    }
    public class SingelCartJson 
    {
        public Cartheater? Hmodel { get; set; }
        public Cartdetail? Dmodels { get; set; }
    }



    public class AddOrderJson
    {
        public Orderheader oh { get; set; }
        public List<Orderdetail> od { get; set; }
    }



    public class AddToCartJson
    {
        public Cartheater Hmodel { get; set; }             // 購物車標題資料
        public List<CartdetailDTO> Dmodels { get; set; }   // 多筆購物車明細資料
    }

    public class CartdetailDTO
    {
        public string productID { get; set; }              // 商品 ID
        public short Qty { get; set; }                     // 數量
        public short UnitPrice { get; set; }               // 單價
    }
    public class CartItemDto
    {
        public string ProductId { get; set; }
        public short CartItemId { get; set; }
        public string name { get; set; }
        public int qty { get; set; }
        public decimal price { get; set; }
        public int? total { get; set; }
    }



}
