//数据对象
function MsgReplyResult() {
    this.Msg = {
        MsgId: 0,
        RuleName: "",
        ReplyMode:"random_one"
    };
    this.Keys = [];
    this.Contents =[];
}
var dataList = [];
var newsList;
var imgList;
function initRuleInfo() {
    $.post("AjaxHandler/MsgReplyHandler.ashx/GetAllRule", function(result) {
        if (result.state) {
            for (var i = 0; i < result.data.length; i++) {
                createOneRule(result.data[i]);
            }
            $(".empty_tips").hide();
            $(".keywords_rule_item").removeClass("open");
        } 
        else {
            $(".empty_tips").show();
        }
        
    });
}
function initData() {
    layer.load(0, { shade: false });
    //初始化素材库数据
    $.post("AjaxHandler/MpHandler.ashx/GetMediaList",function (result) {
        if (result.state) {
            newsList = result.newsList;
            imgList = result.imgList;
            $("#box_news").html("").html(createNewsItem(newsList));
            $("#box_img").html("").html(msgimgHtml(imgList));
        } 
        initRuleInfo();
        layer.closeAll('loading');
    });
}
function createOneRule(obj) {
    if (!obj) {
        return;
    }
    var index = dataList.push(obj) - 1;
    var domObj = $("#Js_ruleItem_0").clone();
    domObj.attr('id', index).attr('msgId', obj.Msg.Id);
    domObj.find(".keywords_rule_num").html("规则"+(index+1)+"："+obj.Msg.RuleName);
    domObj.find(".frm_checkbox").prop("checked", obj.Msg.ReplyMode !== "random_one");
    domObj.addClass("open");
    if (obj.Keys != undefined && obj.Keys.length > 0) {
        for (var i = 0; i < obj.Keys.length; i++) {
            createOneKey(domObj, i, obj.Keys[i]);
        }
    } else {
        obj.Keys = [];
    }
    if (obj.Contents != undefined && obj.Contents.length > 0) {
        for (var i = 0; i < obj.Contents.length; i++) {
            createOneContent(domObj, i, obj.Contents[i]);
        }
    } else {
        obj.Contents = [];
    }

    $("#Js_ruleList").prepend(domObj.prop("outerHTML"));
    $("#" + index).find("input").val(obj.Msg.RuleName);
    $("#" + index).find(".Js_reply_all").prop("checked", obj.Msg.ReplyMode =="reply_all");
    ruleoverView(index);
}
function createOneKey(ruleObj,index,key) {
    var newItem = $("#keyTempale").clone();
    newItem.removeAttr("id").attr("kIndex", index);
    newItem.find("strong").html(key.KeyVal);
    newItem.find(".Js_keyword_mode").attr("data-mode", key.MatchMode).text(key.MatchMode =="contain"?"未全匹配":"已全匹配");
    ruleObj.find(".keybox").append(newItem.prop("outerHTML"));
}
function createOneContent(ruleObj, index, content) {
    var contentItem = $("#contentTempale").clone();
    contentItem.removeAttr("id").attr("cIndex", index);
    //判断类型
    if (content.ReplyType == "text") {
         contentItem.find("strong").html(decodeURIComponent(content.ReplyContent));
    }
    if (content.ReplyType == "img") {
        var imgItem = $("#div_img").find(".pic-wp[mval='" + content.ReplyContent + "']");
        contentItem.find(".desc").html(imgItem.prop("outerHTML"));
        contentItem.find(".edit_replyContent").remove();
    }
    if (content.ReplyType== "news") {
        var newsItem = $("#box_news").find(".js-list-select[mval='" + content.ReplyContent + "']");
        contentItem.find(".desc").html(newsItem.prop("outerHTML"));
        contentItem.find(".list-col-mask").remove();
        contentItem.find(".edit_replyContent").remove();
    }
    ruleObj.find(".contentbox").append(contentItem.prop("outerHTML"));
}
function ruleoverView(index) {
    var rule = dataList[index];
    var obj = $("#" + index);
    var keylab = "";
    var count = 0;
    var textcount = 0;
    var imgcount = 0;
    var newscount = 0;
    if (rule.Keys != undefined&&rule.Keys.length>0) {
        for (var i = 0; i < rule.Keys.length; i++) {
            if (rule.Keys[i].IsDel != 1) {
                keylab += '<li>'+rule.Keys[i].KeyVal+'</li>';
            }
        }
    }
    if (rule.Contents != undefined && rule.Contents.length > 0) {
        for (var j= 0; j < rule.Contents.length; j++) {
            if (rule.Contents[j].IsDel != 1) {
                count++;
                if (rule.Contents[j].ReplyType == "text") {
                    textcount++;
                }
                if (rule.Contents[j].ReplyType == "img") {
                    imgcount++;
                }
                if (rule.Contents[j].ReplyType == "news") {
                    newscount++;
                }
            }
        }
    }
    obj.find(".overview_keywords_list").html(keylab);
    obj.find(".total").text(count);
    obj.find(".textnum").text(textcount);
    obj.find(".imgnum").text(imgcount);
    obj.find(".newsnum").text(newscount);
    obj.find(".media_stat").find("em[data-type='1']").text(textcount);
    obj.find(".media_stat").find("em[data-type='2']").text(imgcount);
    obj.find(".media_stat").find("em[data-type='5']").text(newscount);
}

//剩余字数
function checkCharLength(max, item) {
    var maxLength = max || 30;
    var imgLength = item.find('img').length * 4;
    var txt = item.text();
    var tips = item.siblings(".editor_toolbar").find(".editor_tip")
    if (txt.length+imgLength > maxLength) {
        var overflowChar = (txt.length + imgLength) - maxLength;
        tips.html("<strong>已超出" + overflowChar + "个文字</strong>")
    } else {
        var currTxt = maxLength - (txt.length + imgLength) ;
        tips.html("还可以输入<b>" + currTxt + "</b>字")
    }
    return (txt.length + imgLength);
}
function boxEvent() {
    //折叠规则
    $("#Js_ruleList").on("click", ".keywords_rule_hd", function () {
        ruleoverView($(this).parents(".keywords_rule_item").attr("id"));
        $(this).parent().toggleClass("open");
    })
    //关键字编辑弹窗
    $("#Js_ruleList").on("click", ".Js_keyword_add", function () {
        $(this).popPanel({ popMain: ".key-panel" });
        $("#saytext").attr('edit_id', $(this).parents(".keywords_rule_item").attr("id"));
        $("#saytext").attr('edit_key', -1).html("");
        checkCharLength(30, $("#saytext"));
    })
    $("#saytext").on("keyup", function () {
        var $this = $(this)
        checkCharLength(30, $this)
    })
    //文字回复弹窗
    $("#Js_ruleList").on("click", ".tab_text", function () {
        $(this).popPanel({ popMain: ".txt-panel" });
        $("#edittext").html("");
        $("#edittext").attr('edit_id', $(this).parents(".keywords_rule_item").attr("id"));
        $("#edittext").attr('edit_content', -1).html("");
        $("#edittext").emoji({
            button: "#txt-emoji",
            showTab: false,
            animation: 'fade',
            icons: [{
                name: "QQ表情",
                path: "../img/face/",
                maxNum: 99,
                alias: emojidata,
                file: ".png"
            }]
        });
        checkCharLength(600, $("#edittext"));
    })
    $("#edittext").on("keyup", function () {
        var $this = $(this)
        checkCharLength(600, $this)
    })
    //图片回复弹窗
    $("#Js_ruleList").on("click", ".tab_img", function () {
        $(this).popPanel({ popMain: ".pic-panel" });
        //图片缩略图
        $(".pic-wp .pic-view").each(function (i) {
            var $this = $(this)
            var pic = $this.find("img")
            var picW = pic.width();
            var picH = pic.height();
            var picratio = picW / picH
            if (picratio > 1) {
                pic.css("height", "100%")
            } else {
                pic.css("width", "100%")
            }
        })
        $("#div_img").attr('edit_id', $(this).parents(".keywords_rule_item").attr("id"));
        $("#div_img").attr('edit_content', -1);
        $("#box_img").find(".pic-wp").removeClass("on");
    })
    //图文回复弹窗
    $("#Js_ruleList").on("click", ".tab_appmsg", function () {
        $(this).popPanel({ popMain: ".txt-pic-panel" });
        $("#box_news").attr('edit_id', $(this).parents(".keywords_rule_item").attr("id"));
        $("#box_news").attr('edit_content', -1);
        $("#box_news").find(".js-list-select").removeClass("on");
    })
    //选择标记
    $(".select-item-wp").on("click", ".select-item", function () {
        $("#box_img").find(".pic-wp").removeClass("on").removeAttr("state");
        $(this).toggleClass("on").attr("state",1);

    })
    $("#box_news").on("click", ".js-list-select", function () {
        $("#box_news").find(".js-list-select").removeClass("on").removeAttr("state");
        $(this).addClass("on").attr("state",1);
    });
   
}
function editEvent() {
    //删除规则
    $("#Js_ruleList").on("click", ".Js_rule_del", function () {
        if (confirm('确定要删除规则吗')) {
            var parentObj = $(this).parents(".keywords_rule_item");
            var msgId = parseInt(parentObj.attr('msgid'));
            var index = parentObj.attr('id');
            if (msgId > 0) {
                $.post("AjaxHandler/MsgReplyHandler.ashx/DeleteMsgRule", { msgId: msgId }, function (result) {

                });
            }
            parentObj.remove();
            if ($("#Js_ruleList").find(".keywords_rule_item").length == 0) {
                $(".empty_tips").show();
            }
        }
    })
    //编辑规则名
    $("#Js_ruleList").on("blur", ".frm_input", function () {
        var parentObj = $(this).parents(".keywords_rule_item");
        var index = parentObj.attr("id");
        dataList[index].Msg.RuleName = $(this).val().trim();
    });
    //编辑回复模式
    $("#Js_ruleList").on("click", ".frm_checkbox", function() {
        var parentObj = $(this).parents(".keywords_rule_item");
        var index = parentObj.attr("id");
        var mode = $(this).prop("checked") ? "reply_all" : "random_one";
        dataList[index].Msg.ReplyMode = mode ;
    });
    //新建关键字
    $("#sub_key").click(function () {
        var key = $("#saytext").html().trim();
        if (!key) {
            layer.msg("文字必须为1到30个字");
            return false;
        }
        var index = parseInt($("#saytext").attr("edit_id"));
        var kindex = parseInt($("#saytext").attr("edit_key"));
        if (kindex>=0) {
            dataList[index].Keys[kindex].KeyVal = key;
            $("#" + index).find('.keybox').find('.keyItem[kindex="' + kindex + '"]').find("strong").html(key);
        } else {
            var newKey = {
                KeyId: 0,
                MsgId: dataList[index].Msg.MsgId,
                KeyVal: key,
                MatchMode: 'contain'
            };
            var keyIndex = dataList[index].Keys.push(newKey) - 1;
            createOneKey($("#" + index), keyIndex, newKey);
            $("#" + index).find(".overview_keywords_list").append("<li>" + decodeURIComponent(key) + "</li>");

        }
        $("#esc_editkey").click();
        $("#saytext").html("");
        checkCharLength(30, $("#saytext"));
    });
    //编辑关键字
    $("#Js_ruleList").on("click", ".Js_keyword_edit", function (event) {
        event.stopPropagation();
        var kindex = $(this).parents(".keyItem").attr("kindex");
        var mindex = $(this).parents(".keywords_rule_item").attr("id");
        $(this).popPanel({ popMain: ".key-panel" });
        $("#saytext").attr('edit_id', mindex).attr("edit_key", kindex);
        $("#saytext").html(dataList[mindex].Keys[kindex].KeyVal);
        checkCharLength(30, $("#saytext"));
    });
    //编辑关键字匹配类型
    $("#Js_ruleList").on("click", ".Js_keyword_mode", function (event) {
        event.stopPropagation();
        var kindex=$(this).parents(".keyItem").attr("kindex");
        var mindex = $(this).parents(".keywords_rule_item").attr("id");
        var mode= dataList[mindex].Keys[kindex].MatchMode== "contain" ? "equal" :"contain";
        $(this).text(mode == "contain" ? "未全匹配" : "已全匹配");
        dataList[mindex].Keys[kindex].MatchMode = mode;
    });
    //删除关键字
    $("#Js_ruleList").on("click", ".Js_keyword_del", function (event) {
        event.stopPropagation();
        var kindex = $(this).parents(".keyItem").attr("kindex");
        var mindex = $(this).parents(".keywords_rule_item").attr("id");
        dataList[mindex].Keys[kindex].IsDel = 1;
        $(this).parents(".keyItem").remove();
        ruleoverView(mindex);
    });
    //编辑文字回复内容
    $("#btn_replyText").click(function () {
        var content = $("#edittext").html().trim();
        if (content==""||content.length>600) {
            layer.msg("文字必须为1到600个字");
            return false;
        }
        var index = parseInt($("#edittext").attr("edit_id"));
        var cindex = parseInt($("#edittext").attr("edit_content"));
        if (cindex >= 0) {
            dataList[index].Contents[cindex].ReplyContent = encodeURIComponent(content);
            $("#" + index).find('.contentbox').find('.replyItem[cIndex="' + cindex + '"]').find("strong").html(content);
        } else {
            var cIndex = dataList[index].Contents.push({
                ReplyId: 0,
                MsgId: dataList[index].Msg.MsgId,
                ReplyType: "text",
                ReplyContent: encodeURIComponent(content)
            }) - 1;
            var textItem = $("#contentTempale").clone();
            textItem.removeAttr("id").attr("cIndex", cIndex);
            textItem.find("strong").html(content);
            $("#" + index).find(".contentbox").append(textItem.prop("outerHTML"));
        }
        
        $("#esc_editText").click();
        ruleoverView(index);
        checkCharLength(600, $("#edittext"));
    });
    $("#Js_ruleList").on("click", ".edit_replyContent", function() {
        var cindex = $(this).parents(".replyItem").attr("cIndex");
        var mindex = $(this).parents(".keywords_rule_item").attr("id");
        $(this).popPanel({ popMain: ".txt-panel" });
        $("#edittext").emoji({
            button: "#txt-emoji",
            showTab: false,
            animation: 'fade',
            icons: [{
                name: "QQ表情",
                path: "../img/face/",
                maxNum: 99,
                alias: emojidata,
                file: ".png"
            }]
        });
        $("#edittext").attr('edit_id', mindex).attr("edit_content", cindex);
        $("#edittext").html(decodeURIComponent(dataList[mindex].Contents[cindex].ReplyContent));
        checkCharLength(600, $("#edittext"));
    });
    $("#Js_ruleList").on("click", ".del_replyContent", function (event) {
        event.stopPropagation();
        var cindex = $(this).parents(".replyItem").attr("cIndex");
        var mindex = $(this).parents(".keywords_rule_item").attr("id");
        dataList[mindex].Contents[cindex].IsDel = 1;
        $(this).parents(".replyItem").remove();
    });
    //编辑图片回复内容
    $("#btn_selectimg").click(function() {
        var index = parseInt($("#div_img").attr("edit_id"));
        var imgItem = $("#div_img").find('.pic-wp[state="1"]');
        var cIndex = dataList[index].Contents.push({
            ReplyId: 0,
            MsgId: dataList[index].Msg.MsgId,
            ReplyType: "img",
            ReplyContent: imgItem.attr("mval")
        }) - 1;
        var domItem = $("#contentTempale").clone();
        domItem.removeAttr("id").attr("cIndex", cIndex);
        domItem.find(".edit_replyContent").remove();
        domItem.find(".desc").html(imgItem.prop("outerHTML"));
        $("#" + index).find(".contentbox").append(domItem.prop("outerHTML"));
        $(".pic-wp .pic-view").each(function (i) {
            var $this = $(this)
            var pic = $this.find("img")
            var picW = pic.width();
            var picH = pic.height();
            var picratio = picW / picH
            if (picratio > 1) {
                pic.css("height", "100%")
            } else {
                pic.css("width", "100%")
            }
        })
        $(".js-close").click();
        ruleoverView(index);
    });
    //编辑图文回复内容
    $("#btn_selectnews").click(function() {
        var index = parseInt($("#box_news").attr("edit_id"));
        var newsItem = $("#box_news").find('.js-list-select[state="1"]');
        var cIndex = dataList[index].Contents.push({
            ReplyId: 0,
            MsgId: dataList[index].Msg.MsgId,
            ReplyType: "news",
            ReplyContent: newsItem.attr("mval")
        }) - 1;
        var domItem = $("#contentTempale").clone();
        domItem.removeAttr("id").attr("cIndex", cIndex);
        domItem.find(".edit_replyContent").remove();
        domItem.find(".desc").html(newsItem.prop("outerHTML"));
        domItem.find(".list-col-mask").remove();
        $("#" + index).find(".contentbox").append(domItem.prop("outerHTML"));
        $(".js-close").click();
        ruleoverView(index);
    });
}
$(function () {
    
    initData();
    //添加新规则
    $("#Js_rule_add").click(function () {
        var index = dataList.length - 1;
        if (dataList[index]!=undefined&&dataList[index].Msg.MsgId == 0) {
            $("#" + index).remove();
            dataList.pop();
            return false;
        }
        var newrule = new MsgReplyResult();
        newrule.Msg.RuleName = '新规则';
        createOneRule(newrule);
        
        $(".empty_tips").hide();
    });
    //保存
    $("#Js_ruleList").on("click", ".Js_rule_save", function() {
        var parentObj = $(this).parents(".keywords_rule_item");
        var index = parentObj.attr('id');
        var item = dataList[index];
        if (item.Msg.RuleName=="") {
            layer.msg("规则名称不能为空");
            return false;
        }
        if (item.Msg.RuleName.length > 60) {
            layer.msg("规则名称最多60字");
            return false;
        }
        if (parentObj.find(".keyItem").length == 0) {
            layer.msg("至少输入1个关键词");
            return false;
        }
        if (parentObj.find(".replyItem").length == 0) {
            layer.msg("至少输入1个回复");
            return false;
        }
        var itemJson = JSON.stringify(item);
        $.post("AjaxHandler/MsgReplyHandler.ashx/SaveRule", { ruleInfo: itemJson }, function (result) {
            if (result.state) {
                layer.msg("保存成功",
                    {time:1500}, function() {
                    window.location.reload();
                });
            }
        });
    });
    $(".sync-btn").click(function() {
        layer.load(1);
        //初始化素材库数据
        $.post("AjaxHandler/MpHandler.ashx/GetMediaList",
            {mode:true},function (result) {
            if (result.state) {
                newsList = result.newsList;
                imgList = result.imgList;
                $("#box_news").html("").html(createNewsItem(newsList));
                $("#box_img").html("").html(msgimgHtml(imgList));
            }
            layer.closeAll('loading');
        });
    });
    editEvent();
    boxEvent();
});