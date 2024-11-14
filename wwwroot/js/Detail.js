//-------------------------------味道分布-極座標圖
const ctx = $("#polarAreaChart")[0].getContext("2d");
const polarAreaChart = new Chart(ctx, {
    type: 'polarArea',
    data: {
        labels: ['香氣', '甜味', '苦味', '酸度', '厚實度'],
        datasets: [{
            label: '咖啡豆風味等級',
            data: [4, 3, 2, 5, 4], // 每個屬性的等級 (範圍 1-5)
            backgroundColor: [
                'rgba(41, 52, 80, 0.6)',   // 深藍灰色
                'rgba(74, 144, 226, 0.6)', // 明亮藍色
                'rgba(43, 214, 109, 0.6)', // 柔亮綠色
                'rgba(127, 239, 189, 0.6)', // 薄荷綠色
                'rgba(166, 206, 57, 0.6)'  // 橄欖綠色
            ],
            borderColor: [
                'rgba(41, 52, 80, 0.6)',   // 深藍灰色
                'rgba(74, 144, 226, 0.6)', // 明亮藍色
                'rgba(43, 214, 109, 0.6)', // 柔亮綠色
                'rgba(127, 239, 189, 0.6)', // 薄荷綠色
                'rgba(166, 206, 57, 0.6)'  // 橄欖綠色
            ],
            borderWidth: 1
        }]
    },
    options: {
        scales: {                               //圖表的坐標軸和刻度
            r: {                                //圖的半徑
                beginAtZero: true,              //刻度從零開始 
                ticks: {                        //刻度的相關屬性
                    stepSize: 1,                //每個刻度 1
                    max: 5                      //刻度最大值5
                },
                angleLines: {                   //輻射線的屬性
                    display: true               // 顯示輻射線
                },
                grid: {
                    circular: true              //設置網格為圓形
                }
            }
        },
        plugins: {                              //設置圖表的插件選項
            tooltip: {                          //提示框的屬性
                callbacks: {                    //提示框中顯示的內容
                    label: function (context) { //label 函數，會在顯示提示框時被調用
                        return `${context.label}: ${context.raw}`; // 當前數據點的標籤跟原始數值
                    }
                }
            },
            legend: {
                labels: {
                    font: {
                        size: 16   // 調整圖例的文字大小
                    },
                    color: '#333'   // 圖例文字顏色（可選）
                }
            },
        }
    }
});


/* 咖啡沖泡計算機-參數設定區 */
function Select_Choose() {
    if ($('.cooffee-drink-select').val() === "other") {
        $('.coffee-drink-input').val('');
        $('.coffee-drink-input').prop('placeholder', '請輸入');
        $('.coffee-drink-input').prop('disabled', false);
    } else {
        $('.coffee-drink-input').val($('.cooffee-drink-select').val());
        $('.coffee-drink-input').prop('disabled', true)
    }
}
/**------------------------------------------------------------------------------------------------------------------ */
var chartData = [
    { unit: "毫升" },
    { unit: "克" },
    { unit: "分鐘" },
    { unit: "°C" },
    { unit: "毫升" },
    { unit: "秒" }
];

// 設置上下限拿來計算百分比
var limits = [
    { min: 0, max: 660 },    // 水量上下限
    { min: 0, max: 44 },     // 咖啡粉上下限
    { min: 90, max: 180 },      // 沖煮時間上下限（秒）
    { min: 90, max: 96 },   // 水溫上下限
    { min: 0, max: 88 },    // 悶蒸水量上下限
    { min: 20, max: 60 }     // 悶蒸時間上下限（秒）
];

var elements = document.querySelectorAll('.gaugeChart');
var charts = {}; // 儲存每個圖表物件，方便後續更新

elements.forEach(function (element, index) {
    var data = chartData[index];

    var options = {
        chart: {
            height: 180,
            type: "radialBar",
            offsetY: 20
        },
        series: [0],  // 初始時設為0
        colors: ["#20E647"],
        plotOptions: {
            radialBar: {
                hollow: {
                    margin: 0,
                    size: "66%",
                    background: "#293450"
                },
                track: {
                    dropShadow: {
                        enabled: true,
                        top: 2,
                        left: 0,
                        blur: 4,
                        opacity: 0.15
                    }
                },
                dataLabels: {
                    name: {
                        show: false  // 不顯示標籤名稱
                    },
                    value: {
                        show: false  // 不顯示數值
                    }
                }
            }
        },
        fill: {
            type: "gradient",
            gradient: {
                shade: "dark",
                type: "vertical",
                gradientToColors: ["#87D4F9"],
                stops: [0, 100]
            }
        },
        stroke: {
            lineCap: "round"
        },

    };

    var chart = new ApexCharts(element, options);
    chart.render();

    charts["chart" + index] = chart;  // 儲存圖表物件，方便後續更新
});


/* 咖啡沖泡計算機-計算結果區 */
function calculateParameters() {
    const baking_data = [
        { "baking": "淺焙", "watertemperature": "90-92", "brewtime": "2:30-3:00"},
        { "baking": "中淺焙", "watertemperature": "91-93", "brewtime": "2:15-2:45"},
        { "baking": "中焙", "watertemperature": "92-94", "brewtime": "2:00-2:30"},
        { "baking": "中深焙", "watertemperature": "93-95", "brewtime": "1:45-2:15"},
        { "baking": "深焙", "watertemperature": "94-96", "brewtime": "1:30-2:00"},
    ];

    // 使用者輸入的咖啡飲用量
    const coffee_drink_input = $('.coffee-drink-input').val();
    // 粉水比 1:15
    const coffee_water_ratio = 15;
    // 水量
    var water = coffee_drink_input;
    // 咖啡粉重量
    var coffee_weight = (coffee_drink_input / coffee_water_ratio).toFixed(0);
    // 悶蒸水量
    var simmer_water = (coffee_weight * 2).toFixed(0);
    // 悶蒸時間
    var simmer_time = "30-60";
    // 沖煮時間  先用class="thisanswer"等之後資料庫連線在看用什麼方法抓到
    var brew_time = baking_data.find(s => s.baking === $('.thisanswer').text()).brewtime;
    // 水溫
    var water_temperature = baking_data.find(s => s.baking === $('.thisanswer').text()).watertemperature;
    // 每個計算結果的陣列
    const calculate_data = [water, coffee_weight, brew_time, water_temperature, simmer_water, simmer_time]

    //--------------------------更新數據圖表--------------------------//                    
    chartData.forEach(function (data, index) {
        const limit = limits[index];
        const value = calculate_data[index];
        /*console.log(value);*/
        let min_value;
        if (data.unit === "分鐘") {
            const [star, end] = value.split('-');
            const [m1, s1] = star.split(':').map(Number);
            const [m2, s2] = end.split(':').map(Number);
            min_value = ((m1 + m2) * 60 + s1 + s2) / 2;
        } else if (data.unit === "°C" || data.unit === "秒") {
            const [star, end] = value.split('-').map(Number);
            min_value = (star + end) / 2;
        } else {
            min_value = Number(value);
        }
        const percentage = ((min_value - limit.min) / (limit.max - limit.min)) * 100;

        // 更新圖表
        charts["chart" + index].updateOptions({
            series: [percentage],
            plotOptions: {
                radialBar: {
                    dataLabels: {
                        value: {
                            color: "#fff",
                            fontSize: "20px",
                            show: true,
                            offsetY: -20,
                            formatter: function () {
                                return value;
                            }
                        },
                        name: {
                            color: "#fff",
                            fontSize: "15px",
                            show: true,
                            offsetY: 20,
                            formatter: function () {
                                return data.unit;
                            }
                        }
                    }
                }
            }
        });
    });

    

    // 取得 烘焙程度 拿來判斷 研磨粗細、萃取係數、注水方式
    var roast_level = $('.roast-level-answer').text();
    
    
    // 發送 AJAX 請求到後端 GET改成用POST 取得對應的 研磨粗細、萃取係數、注水方式
    // Grind_Size_Extraction_Conditions_Add_Water 
    $.ajax({
        url: '/Product/Grind_Size',
        type: 'POST',
        data: { roast_level: roast_level },
        success: function (response) {
            // 更新研磨粗細顯示結果
            $('.grind-size-result').text(response.grindsize);
            $('.extraction-conditions-result').text(response.extractionconditions);
            $('.add-water-result').text(response.addwater);
        },
        error: function () {
            // 顯示錯誤訊息
            $('#grindSizeResult').text('無法獲取資料');
        }
    });
}

