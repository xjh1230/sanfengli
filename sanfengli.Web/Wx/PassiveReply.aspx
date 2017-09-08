<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PassiveReply.aspx.cs" Inherits="sanfengli.Web.Wx.PassiveReply" %>

<!DOCTYPE html>
<html>
<head lang="en">
    <meta charset="UTF-8">
    <meta name="referrer" content="never">
    <title>被添加自动回复</title>
    <link rel="stylesheet" href="../css/basic.css"/>
    <link rel="stylesheet" href="../css/emojis.css"/>
</head>
<body>
    <div class="wx-page">
        <div class="main_hd">
            <h2>被添加自动回复</h2>
        </div>
        <div class="main_bd">
            <div class="content_wrap">
                <div class="tab_navs clearfix">
                    <ul class="clearfix">
                        <li class="on"><a href="PassiveReply.aspx">被添加自动回复</a></li>
                        <li><a href="AutoReply.aspx">消息自动回复</a></li>
                        <li><a href="MsgReply.aspx">关键词自动回复</a></li>
                    </ul>
                </div>
                <div class="msg_sender">
                    <div class="msg_tab">
                        <div class="tab_navs_panel">
                            <ul class="clearfix">
                                <li id="li_text" class="on"><i class="icon icon-txt"></i>文字</li>
                                <li id="li_img"><i class="icon icon-pic"></i>图片</li>
                            </ul>
                        </div>
                        <div class="tab_panel">
                            <div class="tab_content">
                                <div class="emotion_editor">
                                    <div class="edit_area js_editorArea" contenteditable="true" id="saytext"></div>
                                    <div class="editor_toolbar clearfix">
                                        <a href="javascript:void(0);" class="icon icon-face emotion js_switch f-left"></a>
                                        <p class="editor_tip js_editorTip f-right">还可以输入<b>600</b>字</p>
                                    </div>
                                </div>
                            </div>
                            <div class="tab_content clearfix upload-pic">
                                
                                <div id="div_selectImg">
                                    <span class="icon icon-upload"></span>
                                    <p>从素材库中选择</p>
                                </div>
                                <div id="div_img" style="display: none">
                                    <div class="del-pic icon"></div>
                                    <img id="imgItem" src="" alt="">
                                </div>
                               <%-- <div>
                                    <span class="icon icon-upload"></span>
                                    <p>上传图片</p>
                                    <input type="file" class="upload-file">
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tool_bar">
                    <span id="js_save" class="btn btn_primary btn_input">保存</span>
                    <span id="js_del" class="btn btn_primary btn_input" style="display: none">删除回复</span>
                </div>
            </div>
        </div>
    </div>
     <!--选择图片-->
    <div class="pop-panel pic-panel">
	    <div class="popTit">选择图片</div>
        <div class="pop-con clearfix">
            <div class="btn sync-btn">同步素材库</div>
            <div class="pic-msg select-item-wp clearfix" id="panel_img">
               
            </div>
        </div>
	    <div class="opt-order">
            <span class="btn" id="select_img">确定</span>
            <span class="btn btn_default js-close">取消</span>
        </div>
        <div class="js-close close-btn icon icon-close">关闭</div>
    </div>
    <!--选择图片end-->
    <div class="popover pos_right">
        <div class="popover_inner">
            <div class="popover_content jsPopOverContent">删除后，关注该公众号的用户将不再接收该回复，确定删除？</div>
            <div class="popover_bar"><a href="javascript:;" class="btn btn_primary jsPopoverBt" id="btn_del">确定</a>&nbsp;<a href="javascript:;" class="btn btn_default jsPopoverBt jsCancel">取消</a></div>
        </div>
        <i class="popover_arrow popover_arrow_out"></i>
        <i class="popover_arrow popover_arrow_in"></i> 
    </div>
</body>
<script src="../Scripts/jquery-1.10.2.min.js"></script>
<script src="../Scripts/jquery.emoji.min.js" type="text/javascript"></script>
<script src="../Scripts/msgReply/common.js"></script>
<script src="../Scripts/layer/layer.js"></script>
<script>
    function getmediaImg() {
        layer.load(1);
        $.post("AjaxHandler/MpHandler.ashx/GetMediaList",{ type: 'img' }, function (result) {
            if (result.state && result.imgList.length > 0) {
                $("#panel_img").html("");
                for (var i = 0; i < result.imgList.length; i++) {
                    var temp = '<div class="select-item pic-wp" mediaId="' + result.imgList[i].MediaId + '"><p class="pic-view"><img src="' + result.imgList[i].MUrl + '" alt="' + result.imgList[i].MName + '"></p><div class="mytw_mask">√</div></div >';
                    $("#panel_img").append(temp);
                }
            }
            init();
            layer.closeAll('loading');
        });
    }
    var replyType = "text";
    var mid = "";
    var eventKey = "ych_subscribe";
    function init() {
        $.post("AjaxHandler/MsgReplyHandler.ashx/GetReplyEvent",{eventKey:eventKey},
            function(result) {
                if (result.state) {
                    replyType = result.data.ReplyType;
                    var replyContent = result.data.ReplyContent;
                    if (replyType== 'text') {
                        $("#li_text").click();
                        $("#saytext").html(decodeURIComponent(replyContent));
                        checkCharLength(600, $("#saytext"));
                    }
                    if (replyType == 'img') {
                        mid = replyContent;
                        $("#li_img").click();
                        $("#div_selectImg").hide();
                        $("#div_img").show();
                        $("#imgItem").attr('src', $(".pic-wp[mediaId='" + replyContent + "']").find('img')[0].src);
                    }
                    $("#js_del").show();
                }
                
            });
    }
    //剩余字数
    function checkCharLength(max, item) {
        var maxLength = max || 30;
        var txt = item.text();
        var tips = item.siblings(".editor_toolbar").find(".editor_tip")
        if (txt.length > maxLength) {
            var overflowChar = txt.length - maxLength;
            tips.html("<strong>已超出" + overflowChar + "个文字</strong>")
        } else {
            var currTxt = maxLength - txt.length;
            tips.html("还可以输入<b>" + currTxt + "</b>字")
        }

    }
    function convertText(obj) {
        var item=$(obj.clone());
        item.find('img').each(function() {
            var code = $(this).attr('data-code');
            $(this).after(code).remove();
        });
        return item.html().trim();
    }
    $(document).ready(function(){
        $("#saytext").emoji({
            button: ".emotion",
            showTab: false,
            animation: 'slide',
            icons: [{
                name: "QQ表情",
                path: "../img/face/",
                maxNum: 99,
                alias:emojidata,
                file: ".png"
            }]
        });
        
        $(".tab_navs_panel li").on("click",function(){
            var idx = $(this).index()
            $(".tab_navs_panel li").removeClass("on")
            $(this).addClass("on");
            $(".tab_panel .tab_content").hide();
            $(".tab_panel .tab_content").eq(idx).show();
            replyType = "img";
            if ($(this).attr("id") == "li_text") {
                replyType = "text";
            } 
        })
        
        $("#saytext").on("keyup",function(){
            var $this = $(this)
            checkCharLength(600,$this)
        });
       
        $("#div_selectImg").click(function () {
            $(this).popPanel({ popMain: ".pic-panel" });
            //图片缩略图
            $(".pic-wp .pic-view").each(function (i) {
                var $this = $(this)
                var pic = $this.find("img")
                var picW = pic.width();
                var picH = pic.height();
                console.log(picW)
                var picratio = picW / picH
                if (picratio > 1) {
                    pic.css("height", "100%")
                } else {
                    pic.css("width", "100%")
                }
            })
        })
        $("#panel_img").on("click", ".select-item", function () {
            $(".select-item").removeClass("on").removeAttr("state");
            $(this).addClass("on").attr("state",1);
        });
        $("#select_img").click(function() {
            $("#div_selectImg").hide();
            $("#div_img").show();
            $("#imgItem").attr('src', $(".select-item[state='1']").find("img")[0].src);
            mid = $(".select-item[state='1']").attr("mediaId");
            $(".js-close").click();
        });
        $(".del-pic").click(function() {
            mid = "";
            $("#div_selectImg").show();
            $("#div_img").hide();
        });
        //删除回复
        $("#js_del").click(function() {
            var posRight = $(this).offset().right
            var posTop = $(this).offset().top + $(this).height()
            $(".popover").show().css({
                "right": posRight,
                "top": posTop
            })
            $(".jsCancel").click(function() {
                $(this).parents(".popover").hide();
            })
        });
        $("#btn_del").click(function () {
            $(this).parents(".popover").hide();
            $.post("AjaxHandler/MsgReplyHandler.ashx/DeleteReplyEvent",
                { eventKey: eventKey }, function (result) {
                    if (result.state) {
                       layer.msg("删除成功",
                            { time: 1500 }, function () {
                                window.location.reload();
                            });
                    }
                });
        });
        $("#js_save").click(function () {
            var temp="";
            if (replyType == 'text') {
                if ($("#saytext").html().trim() == "" || $("#saytext").html().trim().length>600) {
                    layer.msg("文字必须为1到600个字");
                    return false;
                }
                temp = encodeURIComponent($("#saytext").html());
            }
            if (replyType == "img") {
                if (mid == "") {
                    layer.msg("请选择图片");
                    return false;
                }
                temp = mid;
            }
            $.post("AjaxHandler/MsgReplyHandler.ashx/SaveReplyEvent",
                { replyType: replyType, replyContent: temp, eventKey: eventKey, eventType:"subscribe" },
                function(result) {
                    if (result.state) {
                        layer.msg("保存成功",
                            { time: 1500 },function() {
                            window.location.reload();
                        });
                    }
                });
        });
        $(".sync-btn").click(function() {
            layer.load(1);
            $.post("AjaxHandler/MpHandler.ashx/GetMediaList", {mode:true, type: 'img' }, function (result) {
                if (result.state && result.imgList.length > 0) {
                    $("#panel_img").html("");
                    for (var i = 0; i < result.imgList.length; i++) {
                        var temp = '<div class="select-item pic-wp" mediaId="' + result.imgList[i].MediaId + '"><p class="pic-view"><img src="' + result.imgList[i].MUrl + '" alt="' + result.imgList[i].MName + '"></p><div class="mytw_mask">√</div></div >';
                        $("#panel_img").append(temp);
                    }
                }
                layer.closeAll('loading');
            });
        });
        getmediaImg();
    });
</script>
</html>