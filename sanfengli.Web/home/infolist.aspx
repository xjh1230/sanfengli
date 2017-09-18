<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="infolist.aspx.cs" Inherits="sanfengli.Web.home.infolist" %>

<%@ Import Namespace="sanfengli.Common.Enum" %>
<!DOCTYPE html>
<html lang="en" style="font-size: 28.9444px;">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta http-equiv="Cache-Control" content="no-transform">
    <meta http-equiv="Cache-Control" content="no-siteapp">
    <meta name="viewport" content="width=device-width, maximum-scale=1.0, user-scalable=no">
    <meta name="viewport" content="width=device-width, minimum-scale=0.1, maximum-scale=1.0, user-scalable=yes">
    <title>三丰里社区【<%=typename %>】</title>
    <link href="../Content/guo.css" rel="stylesheet" />
    <style type="text/css" abt="234"></style>
    <script src="../Scripts/jquery-1.10.2.min.js"></script>
    <style>
        .top-nav {
            position: absolute;
            right: 0;
            top: 0;
            height: 40px;
            width: 50px;
            font-size: 12px;
            background: url('/img/menu.png');
            background-position: 10px 0px;
            background-size: 30px 30px;
            background-repeat: no-repeat;
        }

            .top-nav span {
                position: absolute;
                left: 50%;
                margin-left: -10px;
                top: 50%;
                margin-top: -1px;
                width: 20px;
                height: 2px;
                background: #8dbdff;
            }

            .top-nav .animated {
                position: absolute;
                top: 45px;
                right: 10px;
                background: #eee;
                text-align: left;
                width: 110px;
                z-index: 100;
            }

            .top-nav dl a {
                color: #333;
                display: block;
                width: 100%;
                height: 40px;
                line-height: 40px;
                border-bottom: 1px solid #fff;
                margin-left: 10px;
            }

        .hide {
            display: none;
        }
    </style>
</head>
<body>

    <div class="bread-t" style="box-shadow: 0px 3px 3px #B7B1B1; margin: 0 0 0.475rem 0; padding: 0.375rem 0 0.4rem 0.375rem;">
        <div> <span style="color:#ccc"><%=typename%></span> &gt; </div>

        <%if (type == (int)InfoTypeEnum.文章列表)
            {%>
        <div class="top-nav">
            <dl class="animated  hide">
                <dt class="triangle-bottom"></dt>
                <%if (list_type != null && list_type.Count > 0)
                    {
                        foreach (var item in list_type)
                        {%>
                <dd><a href="<%=item.url %>"><i class="iconfont icon-home"></i><%=item.name %></a></dd>
                <%}
                    } %>
            </dl>
        </div>
    <%}%>
    </div>

    <div class="cont3 lxzs_cont">
        <div class="zszx" id="zszx">
            <div class="zhinan" style="display: block;">
                <ul id="box">
                    <%if (IsData)
                        {
                            foreach (var item in list)
                            {%>
                    <li>
                        <div class="li_list clearfix" style="padding: 0.25rem;">
                            <a href="<%=item.url %>"><img src="<%=item.img %>" alt="" class="img1 fl"></a>
                            <dl class="fl">
                                <dt>
                                    <p><a href="<%=item.url %>"><%=item.title %></a></p>
                                </dt>
                                <dd>
                                    <p><a href="<%=item.url %>"><%=item.desc %></a></p>
                                </dd>
                            </dl>
                            <div style="position: absolute; left: 4.4rem; bottom: 0; "><%=item.create_time %></div>
                           <%-- <div class="lxzx_ren"><i><%=item.count %></i></div>--%>
                        </div>
                    </li>
                    <%}
                        } %>
                    <li style="display:none;">
                        <div class="li_list clearfix" style="padding: 0.25rem;">
                            <img src="../img/app_no_pic.png" alt="" class="img1 fl">
                            <dl class="fl">
                                <dt>
                                    <p><a href="http://m.oxbridgedu.org/lxzx/ca/12037.html">最新加拿大最受学生欢迎大学排名出炉</a></p>
                                </dt>
                                <dd>
                                    <p>去加拿大留学择校确实是个技术活，要从学校的位置、名气、学</p>


                                </dd>
                            </dl>
                            <div style="position: absolute; left: 4.4rem; bottom: 0;">2017-09-15</div>
                            <div class="lxzx_ren"><i>0</i></div>
                        </div>
                    </li>
                </ul>

            </div>
        </div>
    </div>

    <!-- 底部 -->
    <%--<div id="none" class="foot" style="color: #FBD2D3; font-weight: bold; text-align: center; right: 6rem;">已经到头了哦！</div>
    <div class="hyh lxzs_b foot" id="more"><a href="?type=<%=type %>&pageIndex=<%=pageIndex+1 %>&pageSiz=<%=pageSize %>">点击查看更多&gt;&gt;</a></div>--%>
    <div class="foot" style="display: none;" id="yd-typeid">10</div>
    <div class="footer">

        <ul class="clearfix">

            <li><a href="?type=<%=(int)InfoTypeEnum.文章列表 %>">
                <img src="../img/info.png"></a>
                <a href="?type=<%=(int)InfoTypeEnum.文章列表 %>">

                    <p>文章</p>
                </a>
            </li>
            <li id="yd-qgfz" style="position: relative;"><a href="?type=<%=(int)InfoTypeEnum.投票列表 %>">
                <img src="../img/vote.png"></a> <a href="?type=<%=(int)InfoTypeEnum.投票列表 %>">

                    <p>投票</p>

                </a>
            </li>
            <li><a href="?type=<%=(int)InfoTypeEnum.活动列表 %>">
                <img src="../img/activity.png"></a> <a href="?type=<%=(int)InfoTypeEnum.活动列表 %>">

                    <p>活动</p>

                </a></li>
            <li><a href="?type=<%=(int)InfoTypeEnum.问卷列表 %>">
                <img src="../img/questionnaire.png" /></a> <a href="?type=<%=(int)InfoTypeEnum.问卷列表 %>">

                    <p>问卷</p>

                </a></li>

        </ul>

    </div>


    <script>
        $(function () {

                $('.top-nav').on('click', function () {
                    $('.animated').toggleClass('hide');
                })

                function change() {
                    var font = document.documentElement.clientWidth / 360 * 20;

                    document.documentElement.style.fontSize = font + 'px';

                };

                window.addEventListener('resize', change, false);

                change();
                <%if (IsData)
            {%>
                $('#none').hide();
                $('#more').show();
            <%}
            else
            {%>
                $('#none').show();
                $('#more').hide();
            <%}%>

        });

    </script>
</body>
</html>
