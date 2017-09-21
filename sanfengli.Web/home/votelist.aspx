<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="votelist.aspx.cs" Inherits="sanfengli.Web.home.votelist" %>


<!DOCTYPE html>
<!-- saved from url=(0076)http://sanfengli.koalajoy.com/index.php?s=/w16/Vote/Wap/index/vote_id/4.html -->
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <title>世界选美大赛-比赛投票</title>

    <meta content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport">

    <meta content="no-cache,must-revalidate" http-equiv="Cache-Control">
    <meta content="no-cache" http-equiv="pragma">
    <meta content="0" http-equiv="expires">
    <meta content="telephone=no, address=no" name="format-detection">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent">
    <link href="../Content/mobile_module.css" rel="stylesheet" />
    <link href="../Content/shop_vote.css" rel="stylesheet" />

    <script src="../Scripts/jquery-1.10.2.min.js"></script>
    <script src="../Scripts/vote.js"></script>
    <script src="../Scripts/prefixfree.min.js"></script>
    <script src="../Scripts/dialog.js"></script>
    <script src="../Scripts/mobile_module.js"></script>
    <script src="../Scripts/jweixin-1.0.0.js"></script>
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

        #search {
            float: right;
            height: 22px;
            width: 40px;
            border-left: 1px solid #eee;
            margin-top: -32px;
            background: url(/img/search.png) no-repeat center center / 20px 20px;
        }

        .option_list {
            margin-bottom: 39px;
        }

        .btn_join {
            position: fixed;
            bottom: 0;
            width: 100%;
            margin-top: 10px;
            z-index:10;
        }
    </style>
</head>

<body>
    <div class="container">
        <div class="fixed_bg"></div>
        <div class="vote_wrap">

            <div class="vote_content">
                <p class="vote_title"><%= vote == null ? "比赛投票" : vote.title %></p>
                <div>
                    <div class="vote_remark" onclick="switchRemark(this)">
                        <p><%=vote==null?"":vote.remark %></p>
                        <a href="javascript:;" class="open"></a>
                    </div>
                </div>
                <br />
                <div>
                    <input type="text" class="am-form-field" placeholder="请输入参赛者编号" id="search_id" />
                    <a id="search"></a>
                </div>
            </div>
            <div class="option_list">
                <%if (list != null && list.Count > 0)
                    {
                        foreach (var item in list)
                        {%>
                <ul class="option_ul">
                    <li>
                        <div class="list_content">
                            <a href="votedetaile.aspx?option_id=<%=item.Id %>&amp;vote_id=<%=vote_id %>">
                                <img src="<%=item.ImagePath %>" style="height: 145px;">
                            </a>
                            <div class="name">
                                <p>
                                    <%=item.truename+"("+item.Id+")号" %>
                                </p>
                                <p class="opt_count"><span id="opt_count_10"><%=item.opt_count %> 票</span></p>
                            </div>
                            <div class="list_info">
                                <a class="detail_btn" href="votedetaile.aspx?option_id=<%=item.Id %>&vote_id=<%=vote_id %>">了解</a>
                                <%if (item.IsVoteCurrent)
                                    {%>
                                <a class='detail_btn has_vote'><em class="zan"></em>已投票</a>
                                <% }
                                    else if (item.IsVote)
                                    {%>
                                <a class='detail_btn over_btn'><em class="zan"></em>已投完</a>
                                <% }
                                    else
                                    {%>
                                <a class="detail_btn" onclick="vote_join(<%=item.Id %>,<%=item.vote_id %>,<%=user==null?0:user.Id %>,this);"><em class="zan"></em>投TA 一票</a>
                                <%} %>
                            </div>
                        </div>
                    </li>
                </ul>
                <%}
                    } %>
            </div>
            <%if (is_validity)
                {%>
            <div><a class="detail_btn btn_join" href="http://sanfengli.koalajoy.com/home/joinvote.aspx?voteId=<%=vote.Id %>">我要参加</a></div>
            <%} %>
        </div>
    </div>
    <script type="text/javascript">
        var is_verify = '0';


        function switchRemark(ele) {
            var remarkEle = $(ele);
            if (remarkEle.hasClass('vote_remark_open')) {
                remarkEle.removeClass('vote_remark_open')
            } else {
                remarkEle.addClass('vote_remark_open')
            }
        }

        $(function () {
            $('.list_content img').height($('.list_content img').width());
            $('#search').on('click', function () {
                search();
            })
        })
        function search() {
            var search_id = $("#search_id").val();
            if (isNaN(search_id)) {
                $.Dialog.fail("请输入有效编号");
                return false;
            } else {
                var href = "votedetaile.aspx?option_id=" + search_id + "&vote_id=<%=vote_id %>";
                window.location.href = href;
            }
        }
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('hideToolbar');
        });
        wx.ready(function () {
            var shareData = {
                title: '<%=vote==null?"比赛投票":vote.title%>', // 分享标题
                desc: '<%=vote==null?"比赛投票":vote.remark%>', // 分享描述
                link: "<%=url%>", //分享的链接地址
                imgUrl: "http://sanfengli.koalajoy.com/Addons/Vote/icon.png", // 分享图标
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

</body>
</html>
