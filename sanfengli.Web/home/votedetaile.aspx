<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="votedetaile.aspx.cs" Inherits="sanfengli.Web.home.votedetaile" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <title><%=vote.title %></title>

    <meta content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport">

    <meta content="no-cache,must-revalidate" http-equiv="Cache-Control">
    <meta content="no-cache" http-equiv="pragma">
    <meta content="0" http-equiv="expires">
    <meta content="telephone=no, address=no" name="format-detection">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent">

    <style type="text/css" abt="234"></style>
    <link href="../Content/mobile_module.css" rel="stylesheet" />
    <link href="../Content/shop_vote.css" rel="stylesheet" />

    <script src="../Scripts/jquery-1.8.3.min.js"></script>
    <script src="../Scripts/prefixfree.min.js"></script>
    <script src="../Scripts/jweixin-1.0.0.js"></script>
    <script src="../Scripts/dialog.js"></script>
    <script src="../Scripts/mobile_module.js"></script>
    <script src="../Scripts/dialog.js"></script>
    <script src="../Scripts/vote.js"></script>
    <style type="text/css">
        object, embed {
            -webkit-animation-duration: .001s;
            -webkit-animation-name: playerInserted;
            -ms-animation-duration: .001s;
            -ms-animation-name: playerInserted;
            -o-animation-duration: .001s;
            -o-animation-name: playerInserted;
            animation-duration: .001s;
            animation-name: playerInserted;
        }

        @-webkit-keyframes playerInserted {
            from {
                opacity: 0.99;
            }

            to {
                opacity: 1;
            }
        }

        @-ms-keyframes playerInserted {
            from {
                opacity: 0.99;
            }

            to {
                opacity: 1;
            }
        }

        @-o-keyframes playerInserted {
            from {
                opacity: 0.99;
            }

            to {
                opacity: 1;
            }
        }

        @keyframes playerInserted {
            from {
                opacity: 0.99;
            }

            to {
                opacity: 1;
            }
        }
        body{text-align:center} 
        .none-info{
            margin:0 auto;
            color:#ff0000;
            font-size: 20px;
            margin-top: 179px;
        }
    </style>
</head>

<body>
    <div class="container">
        <div class="fixed_bg"></div>
        <div class="vote_wrap">
            <div class="common_header">
                <a class="back" href="javascript:;" onclick="history.back()"></a>
                <span>投票详情</span>
            </div>
            <%if (option != null&&option.option_status==1)
                {%>
            <div class="option_user">
                <img src="<%=option.ImagePath %>">
                <p>
                    姓名：
                   
                    <%=option.truename %>
                </p>
                <p>总票数:<%=option.opt_count %></p>

                <%if (option.IsVoteCurrent)
    {%>
                <a class='detail_btn has_vote'><em class="zan"></em>已投票</a>
                <% }
    else if (option.IsVote)
    {%>
                <a class='detail_btn over_btn'><em class="zan"></em>已投完</a>
                <% }
    else
    {%>
                <a class="detail_btn" onclick="vote_join(<%=option.Id %>,<%=option.vote_id %>,<%=uid %>,this);"><em class="zan"></em>投TA 一票</a>
                <%} %>
            </div>
            <div class="option_content">
                <h6>
                    <img src="../img/smile.png">微笑宣言</h6>
                <p><%=option.manifesto %></p>
                <h6>
                    <img src="../img/remark.png">选手介绍</h6>
                <p><%=option.introduce %></p>
            </div>
            <% }
                else if (option != null) {%>
            <div class="none-info">
            <p>投票正在审核中,请耐心等待<a onclick="history.back()">点击返回</a></p>
                </div>
                    <%}else{%>
            <div class="none-info">
            <p>未找到该选手信息<a onclick="history.back()">点击返回</a></p>
                </div>
               <% }%>
        </div>
    </div>

</body>
<script type="text/javascript">
    var currentDomin = '<%=sanfengli.Common.BaseClass.CurrentDomin%>';
    document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
        WeixinJSBridge.call('hideToolbar');
    });
    wx.ready(function () {
        var shareData = {
            title: '<%=vote.title%>', // 分享标题
            desc: '<%=vote.remark%>', // 分享描述
            link: "<%=url%>", //分享的链接地址
            imgUrl: currentDomin+"Addons/Vote/icon.png", // 分享图标
            type: 'link', // 分享类型,music、video或link，不填默认为link
            dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
            success: function () {
            },
            cancel: function () {
            }
        }
        wx.onMenuShareAppMessage(shareData);
        wx.onMenuShareTimeline(shareData);
        wx.onMenuShareQQ(shareData);
        wx.onMenuShareWeibo(shareData);
    });
</script>
</html>
