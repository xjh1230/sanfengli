﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="upload.aspx.cs" Inherits="sanfengli.Web.home.upload" %>

<!DOCTYPE html>

<html class="js cssanimations">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <title>随手拍上传</title>
    <meta name="HandheldFriendly" content="True">
    <link href="../Scripts/upload/amazeui.min.css" rel="stylesheet" />
    <script src="../Scripts/upload/common.js"></script>
    <style type="text/css" abt="234"></style>

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
    </style>
</head>
<body class="am-with-fixed-navbar">
    <div class="am-g">
        <section>
            <ol class="am-breadcrumb">
                <li class="am-active">随手拍上传</li>
            </ol>
            <div class="am-paragraph am-paragraph-default">
                <article class="am-article">
                    <div class="am-article-hd">
                        <h1 class="am-article-title">随手拍上传</h1>
                    </div>
                    <hr data-am-widget="divider" class="am-divider am-divider-default am-no-layout">
                    <div class="am-article-bd  ">
                        <div class="am-form-group am-form-file">
                            <button type="button" class="am-btn am-btn-danger am-btn-sm">
                                <i class="am-icon-cloud-upload"></i>选择要上传的文件
			
                            </button>
                            <input id="upload" type="file" accept="image/*;" capture="camera">
                        </div>
                    </div>

                    <figure data-am-widget="figure" class="am am-figure am-figure-default am-no-layout">
                        <img class="img_wrap" style="display: none;">
                        <img class="img_wrap1" style="display: none;">
                    </figure>

                    <label class="item-label"><span class="need_flag">*</span>意见描述</label>
                    <div class="controls">
                        <label class="textarea input-large">
                            <textarea name="intro" id="feed_content"></textarea></label>
                    </div>


                    <input type="hidden" name="name" id="image_src" />
                    <button type="button" class="am-btn am-btn-danger am-btn-sm" id="subFeedback">
                        <i class="am-icon-cloud-upload"></i>提交
			
                    </button>
                </article>
            </div>

        </section>
        <input type="hidden" id="access_domain" value="http://www.54php.cn/">
    </div>
    <script src="../Scripts/jquery-1.8.3.min.js"></script>
    <script src="../Scripts/upload/amazeui.min.js"></script>
    <script src="../Scripts/upload/amazeui.ie8polyfill.min.js"></script>
    <script src="../Scripts/upload/amazeui.widgets.helper.min.js"></script>
    <script src="../Scripts/upload/h5_upload.js"></script>
    <div style="display: none">
    </div>
    <div id="cli_dialog_div"></div>
</body>

<script>
    $(function () {
        $('#subFeedback').on('click', function () {
            var src = encodeURI($('#image_src').val());
            var content = encodeURI($('#feed_content').val());
            if (src == '' && content == '') {
                alert("请输入");
            } else {
                $.post("ajax/uploadHandler.aspx?a=1", { src: src, content: content }, function (res) {
                    alert("成功");
                })
            }
        })
    })
</script>
</html>
