//***********************************
// 方法：向本页面后台发送异步请求数据
//***********************************
function getData(startDate, endDate) {
    $.getJSON('?r=' + Math.random(), { start: startDate, end: endDate }, function (data) {
        if (data.msg === 'ok') {
            $('#totalOrdersCount').text(data.TotalOrdersCount); // 订单总数
            $('#dealCompletedOrdersCount').text(data.DealCompletedOrdersCount); // 成交订单数
            $('#totalDealMoney').text(data.TotalDealMoney); // 成交金额

            // 饼状图
            on_offlineDataBind(getPieChartData([data.OnlineOrdersCount, data.OfflineOrdersCount], ["线上", "线下"]));
            platformsDataBind(getPieChartData([data.PcOrdersCount, data.MobileOrdersCount, data.AppOrdersCount], ["官网", "M端", "APP"]));
            reservationsDataBind(getPieChartData([data.ReservationOrdersCount, data.NoneReservationsCount], ["预约单", "非预约"]));
            dealDataBind(getPieChartData([data.DealCompletedOrdersCount, data.NotDealOrdersCount], ["成交", "未成交"]));

            // 线性图
            dealMoneyDataBind(data.RelateOrders);
            salesRankingsDataBind(data.SalesRankings);
        } else {
            alert(data.msg.split(':')[1]);
        }
    });
}

var onlineOfflineContext = document.getElementById("online-offline").getContext("2d");
//**********************************
// 方法：为线上/线下饼状图绑定数据
//**********************************
function on_offlineDataBind(data) {
    window.myPieChart = new Chart(onlineOfflineContext).Pie(data, { responsive: true });
}

var platformsContext = document.getElementById("platforms").getContext("2d");
//**********************************
// 方法：为官网/M端/APP饼状图绑定数据
//**********************************
function platformsDataBind(data) {
    window.myDoughnutChart = new Chart(platformsContext).Doughnut(data, { responsive: true });
}

var reservationsContext = document.getElementById("reservations").getContext("2d");
//**********************************
// 方法：为预约/非预约饼状图绑定数据
//**********************************
function reservationsDataBind(data) {
    window.myDoughnutChart = new Chart(reservationsContext).Doughnut(data, { responsive: true });
}

var dealContext = document.getElementById("deal-complete").getContext("2d");
//**********************************
// 方法：为成交/未成交饼状图绑定数据
//**********************************
function dealDataBind(data) {
    window.myPieChart = new Chart(dealContext).Pie(data, { responsive: true });
}

//**********************************
// 方法：遍历后台传过来的数据列表
//       获取相应的参数
//**********************************
function loopDatas(data, name) {
    var labels = [];
    var datas = [];
    var i = 0;

    for (var i = 0; i < data.length; i++) {
        labels[i] = data[i]["Date"].replace("T", " ");
        datas[i] = data[i][name];
    }

    return { labels: labels, datas: datas };
}

//**********************************
// 方法：遍历销售排行，筛选线性图展示所需的数据
//**********************************
function loopSalesRankings(data) {
    var labels = [];
    var datas = [];

    for (var i = 0; i < data.length; i++) {
        labels[i] = data[i].ProductName;
        datas[i] = data[i].SaleCount;
    }

    return { labels:labels, datas:datas };
}

var dealMoneyContext = document.getElementById("dealMoney").getContext("2d");
//**********************************
// 方法：为成交金额线性图绑定数据
//**********************************
function dealMoneyDataBind(data) {
    // colors = ["rgba(220,220,220)", "rgba(151,187,205)", "rgba(223,240,216)"];
    var colors = ["rgba(220,220,220)", "rgba(151,187,205)"];
    var result = loopDatas(data, "TotalDealMoney");
    var labels = result.labels;
    var datas = [];

    datas[0] = result.datas;
    window.myLine = new Chart(dealMoneyContext).Line(getLineChartData(labels, colors, datas), {
        responsive: true
    });
}

var salesRankingsContext = document.getElementById("salesRankings").getContext("2d");
//**********************************
// 方法：为成交金额线性图绑定数据
//**********************************
function salesRankingsDataBind(data) {
    // colors = ["rgba(220,220,220)", "rgba(151,187,205)", "rgba(223,240,216)"];
    var colors = ["rgba(220,220,220)", "rgba(151,187,205)"];
    var result = loopSalesRankings(data);
    var labels = result.labels;
    var datas = [];

    datas[0] = result.datas;
    window.myBar = new Chart(salesRankingsContext).Bar(getLineChartData(labels, colors, datas), {
        responsive: true
    });
}