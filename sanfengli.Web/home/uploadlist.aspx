<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uploadlist.aspx.cs" Inherits="sanfengli.Web.home.uploadlist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>我的反馈</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta http-equiv="Cache-Control" content="no-transform">
    <meta http-equiv="Cache-Control" content="no-siteapp">
    <meta name="viewport" content="width=device-width, maximum-scale=1.0, user-scalable=no">
    <meta name="viewport" content="width=device-width, minimum-scale=0.1, maximum-scale=1.0, user-scalable=yes">
    <style type="text/css" abt="234"></style>
    <meta name="HandheldFriendly" content="True">
    <link href="../Content/uploadlist.css" rel="stylesheet" />
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

        .am-article-hd img {
            margin-bottom: 15px;
            max-width: 98%;
            display: block;
            margin: 5px auto;
            border: 1px solid #eee;
            padding: 2px;
        }

        p {
            padding-left: 10px;
            font: 14px/28px PingFangSC-Regular,'-apple-system',Simsun;
            word-wrap: break-word;
            word-break: break-all;
            padding-right: 12px;
            margin-bottom: 10px;
            margin-top: 10px;
            text-indent: 2em;
        }

        .dr {
            position: absolute;
            top: -1rem;
            right: 1.5rem;
            color: #494949;
        }

        .dl {
            position: absolute;
            top: -1rem;
            left: 0.5rem;
            font-size: 0.8rem;
            color: #ed1c24;
            font-weight: bold;
        }
        li{
            margin-top: 10px;
        }
        h1{
            font-size: 1.7rem; margin-bottom:10px; color:#999;border-bottom:2px solid #eee;
        }
        ul{
                min-height: 350px;
        }
    </style>
</head>
<body>
    <div class="bread-t" style="box-shadow: 0px 3px 3px #B7B1B1; margin: 0 0 0.475rem 0; padding: 0.375rem 0 0.4rem 0.375rem;">

        <div class="cont3 lxzs_cont">
            <div class="zszx" id="zszx">
                <div class="zhinan" style="display: block;">
                    <div class="am-article-hd">
                        <!-- <h1 class="am-article-title">随手拍上传</h1>-->
                        <img style="margin-bottom: 15px;" src="../img/suishoupai-banner.png" />
                    </div>
                    <h1>我的反馈</h1>
                    <ul id="box">
                        <%if (HasData)
    {
        foreach (var item in list)
        {%>
                        <li >
                            <div class="li_list clearfix" style="padding: 0.25rem;">
                                <div class="dl"><%=item.typename %></div>
                                <div class="dr"><%=item.CreateOn %></div>
                                <img src="<%=item.Image %>"" alt="" class="img1 fl">
                                <div>
                                    <b style="color: #ccc; font-size: 1.2rem;border-left: 2px solid #ed1c24;">反馈内容：</b><p style="color: #303030;"><%=item.Content %></p>
                                </div>
                            </div>
                            <div style="margin-top: 20px;">
                                <dd>
                                    <b style="color: #ccc; font-size: 1rem;">回复意见：</b><p style="color: #8ba8c4;"><%=item.remark %></p>
                                </dd>
                            </div>
                        </li>
                        <%}
                            }
                            else {%>
                        <div style="    text-align: center;
    padding-top: 100px;"><p style="color:cadetblue;font-size:20px;    padding-left: 0px;">您还没有提交反馈信息~</p></div>
                            <%} %>
                    </ul>

                </div>
            </div>
        </div>
</body>
</html>
