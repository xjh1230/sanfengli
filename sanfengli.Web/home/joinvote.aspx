<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="joinvote.aspx.cs" Inherits="sanfengli.Web.home.joinvote" %>

<!DOCTYPE html>

<html class="js cssanimations">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <title>参加活动</title>
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
        
         .am-btn-danger{
            width:23.5rem;
        }
         .fixed_bg {
            position: fixed;
            z-index: -1;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            background: url(/img/bg2.jpg) no-repeat;
            background-size: 100% 100%;
        }
         .am-breadcrumb{
             margin-bottom:-1rem;
         }
    </style>
</head>
<body class="am-with-fixed-navbar">
    <div class="am-g">
        <div class="fixed_bg"></div>
        <section>
            <ol class="am-breadcrumb">
                <li class="am-active">参加投票</li>
            </ol>
            <div class="am-paragraph am-paragraph-default">
                <article class="am-article">
                   <%-- <div class="am-article-hd">
                        <h1 class="am-article-title">参加投票</h1>
                    </div>--%>
                    <hr data-am-widget="divider" class="am-divider am-divider-default am-no-layout">
                    <div class="am-article-bd  ">
                        <div class="am-form-group am-form-file">
                            <button type="button" class="am-btn am-btn-danger am-btn-sm">
                                <i class="am-icon-cloud-upload"></i>选择要上传的文件
			
                            </button>
                            <input id="upload" data-path="shopvote" type="file" name="fileImage" accept="image/*;" capture="camera">
                        </div>
                    </div>
                    <figure data-am-widget="figure" class="am am-figure am-figure-default am-no-layout">
                        <img class="img_wrap " style="display: none;">
                        <%--<img class="img_wrap1" style="display: none;">--%>
                    </figure>

                    <label class="item-label"><span class="need_flag">*</span>参赛宣言</label>
                    <div class="controls">
                        <input type="text" class="am-form-field" id="manifesto" />
                    </div>
                    <label class="item-label"><span class="need_flag">*</span>个人简介</label>
                    <div class="controls">
                        <input type="text" class="am-form-field" id="introduce" />
                    </div>


                    <input type="hidden" name="name" id="image_src" />
                    <div class="am-article-bd" style="margin-top: 10px;">
                        <div class="am-form-group am-form-file">
                            <button type="button" class="am-btn am-btn-danger am-btn-sm" id="subFeedback">
                                <i class="am-icon-upload"></i>提交
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
    <script src="../Scripts/spin.js"></script>
    <script src="../Scripts/messageBox.js"></script>
    <div style="display: none">
    </div>
    <div id="cli_dialog_div"></div>
</body>

<script>
    $(function () {
        $('#subFeedback').on('click', function () {
            var src = encodeURI($('#image_src').val());
            //个人简介
            var introduce = encodeURI($('#introduce').val());
            //参赛宣言
            var manifesto = encodeURI($('#manifesto').val());
            if (src == '' && content == '') {
                alert("请输入信息");
            } else {
                //$.post("ajax/uploadHandler.aspx?a=1", , function (res) {
                //    var data = $.parseJSON(res);
                //    alert(data.Msg);
                //})
                openShadow();
                setTimeout(function () { closeShadow() }, 3000);
                $.ajax({
                    url: "ajax/uploadHandler.aspx",
                    type: "POST",
                    data: { op: "joinvote", src: src, introduce: introduce, openId: '<%=openId%>', manifesto: manifesto, voteId:<%=voteId%>,token:'<%=model==null?"":model.token%>'},
                    dataType: "json",
                    success: function (data) {
                        $.messageBox(data.Msg);
                        closeShadow();
                    },
                });
            }
        })
    })
</script>
</html>
