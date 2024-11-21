// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// 使用者登入狀態
// var userID = @;

var V_userId = "C11070"; // 使用者登入後的ID

// 刪除購物車某項 DLitemNO: 某項編號，由編號帶過來
// 控制器: 標題 cartid && status => 項目 productId ==  cartitem == ?(int) && 
function Deletecartitem(ProductID) {
    $.ajax({
        url: "/Cart/DeleteCartItem",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            Hmodel:
            {
                UserId: V_userId
            },
            Dmodels:
            {
                ProductId: ProductID
            }
        })
    });
}


// 加入購物車觸發 自動加入一筆一筆
// 判斷產品編號是否存在 和 是否狀態為 Y
function JsonToDB(userId, productId, qty, price) {
    // 發送 AJAX 請求
    $.ajax({
        url: "/Cart/AddToCartOne",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            // Hmodel && Dmodel 為類別
            Hmodel: {
                UserId: userId
            },
            Dmodel: {
                ProductId: productId,
                Qty: qty,
                UnitPrice: price
            }
        })
    });
    //SendJsonToDB();
}


// ---------------------------------------------------------------
// 使用者登入後，更新購物車 POST
function PostCartData() {
    let cart = JSON.parse(localStorage.getItem("cart")) || [];
    // 轉成控制器期望的格式
    let DetailCart = cart.map(item => ({
        productID: item.productId,
        //productID: item.productID || "",
        qty: item.qty,
        unitPrice: item.price
    }));
    $.ajax({
        url: "/Cart/AddToCart",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            Hmodel: {
                UserId: V_userId
            },
            Dmodels: DetailCart
        })
    });
}
// 使用者登入後，取得 DB購物車資料 JSON ，並傳到 localStorage
function GetCartData() {
    $.ajax({
        url: `/Cart/get?userId=${V_userId}`,
        type: "GET",
        success: function (response) {
            // console.log("回應資料:", response);
            if (response.success) {
                localStorage.setItem("cart", JSON.stringify(response.items));
                //console.log(response.items);
                //alert("成功獲取資料 ! ");
            } else {
                alert("SERVER 回傳錯誤 !!!!! ");
            }
        }
    });
}
// 購物車POST & GET
function mergesCart() {
    setTimeout("PostCartData()", 1000);
    setTimeout("GetCartData()", 5000);
}
// ---------------------------------------------------------------























