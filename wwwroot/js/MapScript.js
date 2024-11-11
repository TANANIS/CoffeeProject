// 初始化地圖
var map = L.map('map').setView([20.0, 0.0], 2); // 預設地圖範圍

L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 18,
    attribution: '© OpenStreetMap'
}).addTo(map);

// 使用 jQuery 載入 JSON 資料
$.getJSON('/json/coffeeData.json', function (countries) {
    // 點擊咖啡產地標註時顯示詳細資料
    countries.forEach(function (country) {
        var marker = L.marker(country.coords).addTo(map);
        marker.bindPopup('<strong>' + country.name + '</strong>');

        marker.on('click', function () {
            // 顯示咖啡產地詳細信息
            document.getElementById('countryName').innerText = country.name;
            document.getElementById('countryDescription').innerText = country.description;

            // 顯示該產地的咖啡品種與特色
            var varietiesHtml = '';
            country.varieties.forEach(function (variety, index) {
                varietiesHtml += `
                    <div class="coffee-variety">
                        <p class="coffee-variety-btn" data-index="${index}">查看 ${variety.name} 的詳細資料</p>
                        <div class="coffee-variety-details" id="variety-${index}" style="display: none;">
                            <strong>風味：</strong> ${variety.flavor}<br>
                            <strong>沖泡方式：</strong> ${variety.brew}
                        </div>
                    </div>
                `;
            });
            document.getElementById('coffeeVarieties').innerHTML = varietiesHtml;

            // 設定商品頁面鏈接
            document.getElementById('productLink').href = `/language/Product/Details?id=${country.code}`;

            // 顯示詳細區域
            document.getElementById('coffeeDetails').style.display = 'block';

            // 顯示商品鏈接
            document.getElementById('productLink').style.display = 'inline-block'; // 顯示查看商品按鈕

            // 添加折疊/展開效果
            $('.coffee-variety-btn').on('click', function () {
                var index = $(this).data('index');
                var detailsDiv = $('#variety-' + index);
                detailsDiv.toggle(); // 切換顯示狀態
            });
        });
    });
});

////////////////////////////////////////////////////////////////
// 加載 JSON 資料並生成列表
$.getJSON('/json/coffeeBeans.json', function (data) {

        // 點擊按鈕事件
        $('button').on('click', function () {
            var category = $(this).data('category');  // 取得分類類型（例如: "origin", "processing", "roasting"）
            var index = $(this).data('index');  // 取得所選索引（例如: 0, 1, 2）

            var item = null;

            // 根據分類選擇正確的資料
            if (category === "origin") {
                item = data.origins[index];
            } else if (category === "processing") {
                item = data.processingMethods[index];
            } else if (category === "roasting") {
                item = data.roastingLevels[index];
            }

            // 顯示詳細資訊
            if (item) {
                $('#detailDescription').text(item.description);
                $('#detailBeans').empty();

                // 顯示相關豆類的選項
                item.beans.forEach(function (bean) {
                    var beanButton = $('<button class="list-group-item list-group-item-action">' + bean + '</button>');
                    $('#detailBeans').append(beanButton);
                });

                // 顯示詳細區域
                $('#detailSection').slideDown();
            }
        });
    });