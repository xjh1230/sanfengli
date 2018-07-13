<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="upload.aspx.cs" Inherits="sanfengli.Web.home.upload" %>

<!DOCTYPE html>

<html class="js cssanimations">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <title>随手拍上传</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta http-equiv="Cache-Control" content="no-transform">
    <meta http-equiv="Cache-Control" content="no-siteapp">
    <meta name="viewport" content="width=device-width, maximum-scale=1.0, user-scalable=no">
    <meta name="viewport" content="width=device-width, minimum-scale=0.1, maximum-scale=1.0, user-scalable=yes">
    <style type="text/css" abt="234"></style>
    <meta name="HandheldFriendly" content="True">
    <link href="../Scripts/upload/amazeui.min.css" rel="stylesheet" />

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

        .am-article-hd {
            margin-top: 14px;
        }

        .am-btn-danger {
            width: 98%;
            background: #fff;
            border-color: #EEEEEE;
            color: #777;
            height: 80px;
        }

        .item-label {
            color: #777777;
            font-weight: 100;
            margin-top: 10px;
        }
    </style>
</head>
<body class="am-with-fixed-navbar">
    <div class="am-g">
        <section>

            <div class="am-paragraph am-paragraph-default">
                <article class="am-article">
                    <div class="am-article-hd">
                        <!-- <h1 class="am-article-title">随手拍上传</h1>-->
                        <img style="margin-bottom: 15px;" src="../img/suishoupai-banner.png" />
                    </div>

                    <div class="am-article-bd  ">
                        <div class="am-form-group am-form-file">
                            <button type="button" class="am-btn am-btn-danger am-btn-sm">
                                <i class="am-icon-cloud-upload"></i>拍照上传
							
                            </button>
                            <input id="upload" data-path="feedback" type="file" name="fileImage" accept="image/*;" capture="camera">
                        </div>
                    </div>
                    <figure data-am-widget="figure" class="am am-figure am-figure-default am-no-layout">
                        <img class="img_wrap " style="display: none;">
                        <%--<img class="img_wrap1" style="display: none;">--%>
                    </figure>

                    <label class="item-label">所属分类</label>
                    <div class="controls">
                        <select id="type" style="width: 100%; height: 3.6rem;">
                            <%if (listType != null && listType.Count > 0)
                                {
                                    foreach (var item in listType)
                                    { %>
                            <option value="<%=item.Id %>"><%=item.Name %></option>
                            <% }
                                } %>
                        </select>
                    </div>
                    <label class="item-label">问题描述</label>
                    <div class="controls">
                        <textarea name="intro" class="am-form-field" id="feed_content"></textarea>
                    </div>
                    <label class="item-label">位置信息</label>
                    <div class="controls" style="text-align: right;">
                        <input type="text" class="am-form-field" id="postion" readonly="readonly" /><a onclick="qq_position()">获取定位信息</a>
                    </div>
                    <label class="item-label">具体位置</label>
                    <div class="controls">
                        <input type="text" class="am-form-field" id="addr" />
                    </div>
                    <label class="item-label">联系姓名</label>
                    <div class="controls">
                        <input type="text" class="am-form-field" id="name" />
                    </div>
                    <label class="item-label">联系电话</label>
                    <div class="controls">
                        <input type="tel" class="am-form-field" id="phone" data-validatetype="number" maxlength="11" />
                    </div>


                    <input type="hidden" name="name" id="image_src" />
                    <div class="am-article-bd" style="margin-top: 10px;">
                        <div class="am-form-group am-form-file">
                            <button type="button" class="am-btn am-btn-danger am-btn-sm" id="subFeedback" style="background: #fa9686; height: 50px; color: #fff;">
                                提交
			
                            </button>
                        </div>
                    </div>
                </article>
            </div>

        </section>
        <input type="hidden" id="access_domain" value="http://www.54php.cn/">
    </div>
    <script src="../Scripts/jquery-1.8.3.min.js"></script>
    <script src="../Scripts/upload/common.js"></script>
    <script src="../Scripts/upload/amazeui.min.js"></script>
    <script src="../Scripts/upload/amazeui.ie8polyfill.min.js"></script>
    <script src="../Scripts/upload/amazeui.widgets.helper.min.js"></script>
    <script src="../Scripts/upload/h5_upload.js"></script>
    <script src="../Scripts/messageBox.js"></script>
    <script src="../Scripts/spin.js"></script>
    <script src="../Scripts/validate.js"></script>
    <div style="display: none">
    </div>
    <div id="cli_dialog_div"></div>
</body>
<script type="text/javascript" src="https://3gimg.qq.com/lightmap/components/geolocation/geolocation.min.js"></script>
<script>
    function qq_position() {
        var geolocation = new qq.maps.Geolocation("OB4BZ-D4W3U-B7VVO-4PJWW-6TKDJ-WPB77", "myapp");
        if (geolocation) {
            var options = { timeout: 8000 };
            geolocation.getLocation(showPosition, showErr, options);
        } else {
            $.messageBox("定位尚未加载");
        }
    }
    function showPosition(position) {
        $('#postion').val(position.addr);
    };
    function showErr(err) {
        //所有可能的错误
        //alert(err);
        $.messageBox("未能获取到定位信息，请手动输入");
    };

    $(function () {
        $('.cs-select').select();

        $('#subFeedback').on('click', function () {
            var src = encodeURI($('#image_src').val());
            var content = encodeURI($('#feed_content').val());
            var name = encodeURI($('#name').val());
            var phone = encodeURI($('#phone').val());
            var postion = encodeURI($('#postion').val());
            var addr = encodeURI($('#addr').val());
            var typeId = encodeURI($('#type').val());
            var typeName = encodeURI($('#type').find("option:selected").text().trim());
            if (src == '' && content == '') {
                alert("请输入");
            } else {
                openShadow();
                setTimeout(function () { closeShadow() }, 3000);
                $.ajax({
                    url: "ajax/uploadHandler.aspx",
                    type: "POST",
                    data: { op: "save", src: src, content: content, openId: '<%=openId%>', name: name, phone: phone, postion: postion, addr: addr, typeId: typeId, typeName: typeName},
                    dataType: "json",
                    success: function (data) {
                        $.messageBox(data.Msg);
                        closeShadow();
                        window.location.href = 'uploadlist.aspx';
                    },
                });
            }
        })
    })
</script>
</html>
