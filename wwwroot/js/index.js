$(window).on('load', function () {
    if (window.innerWidth > 768) {
        forSwiper();
    }
    else {
        RWDprod();
    }

    // 監聽視窗寬度，依照對應的大小改變商品區的呈現狀況
    $(window).on("resize", function () {
        //console.log(window.innerWidth);

        if (!document.querySelector('.swiper') && window.innerWidth > 768) {
            $('#recommendProd').remove();

            $('.recommend').append(`
                <div class="swiper">
                <div class="swiper-wrapper">
                    <div class="swiper-slide"><a href="#"><img src="/img/coffee_1.jpg"></a></div>
                    <div class="swiper-slide"><a href="#"><img src="/img/coffee_2.jpg"></a></div>
                    <div class="swiper-slide"><a href="#"><img src="/img/coffee_3.jpg"></a></div>
                    <div class="swiper-slide"><a href="#"><img src="/img/coffee_4.jpg"></a></div>
                    <div class="swiper-slide"><a href="#"><img src="/img/coffee_5.jpg"></a></div>
                    <div class="swiper-slide"><a href="#"><img src="/img/coffee_6.jpg"></a></div>
                    <div class="swiper-slide"><a href="#"><img src="/img/coffee_7.jpg"></a></div>
                    <div class="swiper-slide"><a href="#"><img src="/img/coffee_8.jpg"></a></div>
                </div>
            </div>`);


            forSwiper();
        }

        else if (!document.getElementById("recommendProd") && window.innerWidth <= 768) {
            console.log("小於768");
            RWDprod();
        }


    })

    // map 互動
    $('.app-svg-map--world__name').on('mouseover', function () {
        console.log("OK");

        // 獲取當前元素的 data-title 屬性
        const countryTitle = $(this).data("title");

        // 新增 div，後續把資料庫帶出的資料放在這邊
        const newProd = $("<div class='newProd'>").html(`<p>${countryTitle}</p>`);

        // 將 div 加到 main 中
        $("main").append(newProd);

        let coordinate_x = $(this).offset().top - 30;
        let coordinate_y = $(this).offset().left - 10;

        // 設定 div 的 CSS
        $('.newProd').css({
            "position": "absolute",
            "top": coordinate_x,
            "left": coordinate_y,
            "background-color": "white",
            "width": "280px",
            "height": "auto",
            "border": "1px solid black",
            "font-size": "18px"
        })

    })

    $('.app-svg-map--world__name').on('mouseout', function () {
        $(".newProd").css('display', 'none');
    })

})

// Swiper 輪播設定
function forSwiper() {

    var swiper = new Swiper('.swiper', {
        direction: 'horizontal',
        slidesPerView: 4,
        loop: true,
        autoplay: {
            delay: 5000
        }

    });
}

// RWD 狀況下的商品區設定
function RWDprod() {
    $('.swiper').remove();

    $('.recommend').append(`<div id="recommendProd"></div>`);
    $('#recommendProd').append(`
        <div class="prodDiv"><a href="#"><img src="/img/coffee_1.jpg"></a></div>
        <div class="prodDiv"><a href="#"><img src="/img/coffee_2.jpg"></a></div>
        <div class="prodDiv"><a href="#"><img src="/img/coffee_3.jpg"></a></div>
        <div class="prodDiv"><a href="#"><img src="/img/coffee_4.jpg"></a></div>
        <div class="prodDiv"><a href="#"><img src="/img/coffee_5.jpg"></a></div>
        <div class="prodDiv"><a href="#"><img src="/img/coffee_6.jpg"></a></div>
        <div class="prodDiv"><a href="#"><img src="/img/coffee_7.jpg"></a></div>
        <div class="prodDiv"><a href="#"><img src="/img/coffee_8.jpg"></a></div>
        `);

    // CSS 設定
    $('#recommendProd').css({
        "display": "flex",
        "FlexDirection": "column",
        "JustifyContent": "center",
        "AlignItems": "center"
    });

    $('.prodDiv').css({
        "width": "280px",
        "height": "280px",
        "overflow": "hidden",
        "BorderRadius": "10px",
        "margin": "15px 0"
    });

    $('.prodDiv img').css({
        "width": "280px",
        "BorderRadius": "10px"
    });

    $('.prodDiv img').on('mouseenter', function () {
        $(this).css("transform", "scale(1.2, 1.2)")
    }).on("mouseleave", function () {
        $(this).css("transform", "scale(1, 1)")
    });
}

