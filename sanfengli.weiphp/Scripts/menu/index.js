var sortable;
var sortableArray;
var init_menubuttoncount = 1;
var init_submenubuttoncount = 1;
//菜单数据初始化
function initData(data) {
    for (var i = 0; i < data.length; i++) {
        var item = addMenu(data[i]);
        if (data[i].sub_button != undefined && data[i].sub_button.length > 0) {
            for (var j = 0; j < data[i].sub_button.length; j++) {
                addSubMenu(item, data[i].sub_button[j]);
            }
        }
    }
    
}
function addMenu(button) {
    var $this = $(".js-add-menu");
    var temp = ' <div class="menudata"><span class="mname">菜单名称</span ><span class="btype">click</span><span class="breplyType">news</span><span class="breplyContent"></span><span class="key">menu_' + init_menubuttoncount + '</span>';
    var mMhtml = '<li class="js-switchMenu mbtn" id="menu_' + init_menubuttoncount + '"><a href="javascript:;" class="mainMenu"><span>菜单名称</span></a><div class="subMenu-box"><ul><li class="js-add-menu2"><a href="javascript:;" class="subAdd"><i>&emsp;&ensp;</i></a></li></ul><i class="arrow-s arrow-outter"></i><i class="arrow-s arrow-inner"></i></div>' + temp + '</li>';
    if (button) {
        temp = ' <div class="menudata"><span class="mname">' + button.name + '</span ><span class="btype">' + button.type + '</span><span class="breplyType">' + button.replyType + '</span><span class="breplyContent">' +decodeURIComponent(button.replyContent) + '</span><span class="key">' + button.key + '</span>';
        mMhtml = '<li class="js-switchMenu mbtn" id="menu_' + init_menubuttoncount + '"><a href="javascript:;" class="mainMenu"><span>' + button.name + '</span></a><div class="subMenu-box"><ul><li class="js-add-menu2"><a href="javascript:;" class="subAdd"><i>&emsp;&ensp;</i></a></li></ul><i class="arrow-s arrow-outter"></i><i class="arrow-s arrow-inner"></i></div>' + temp + '</li>';
    }
    $(".js-edit-box").show();
    $(".js-edit").show();
    $(".js-tool").show();
    if (init_menubuttoncount == 1) {
        $this.removeClass("first").addClass("col2").find("span").hide();
        $this.before(mMhtml);
        $this.prev().addClass("col2 on onb");
        init_menubuttoncount = 2;
    } else if (init_menubuttoncount == 2) {
        $this.removeClass("col2").addClass("col3");
        $this.prev().removeClass("on onb");
        $this.before(mMhtml);
        $(".js-add-menu2").removeClass('col2').addClass('col3');
        $(".mbtn").removeClass('col2').addClass('col3');
        $this.prev().addClass("on onb");
        init_menubuttoncount = 3;
    } else if (init_menubuttoncount == 3) {
        $(".mbtn").removeClass("on onb");
        $this.before(mMhtml);
        $this.prev().addClass("col3 on onb");
        $this.hide().find("span").hide();
        init_menubuttoncount = 4;
    }
    $this.prev().click();
    return $this.prev().find('.js-add-menu2')[0];
}
function addSubMenu(obj, button) {
    var temp = ' <div class="menudata"><span class="mname">菜单名称</span ><span class="btype">click</span><span class="breplyType">news</span><span class="breplyContent"></span><span class="key">submenu_' + init_submenubuttoncount + '</span>';
    var sMhtml = '<li class="js-switchMenu2 mbtn"  id="submenu_' + init_submenubuttoncount + '"><a href="javascript:;" class="subMenu"><span>菜单名称</span></a>' + temp + '</li>';
    if (button) {
        temp = ' <div class="menudata"><span class="mname">' + button.name + '</span ><span class="btype">' + button.type + '</span><span class="breplyType">' + button.replyType + '</span><span class="breplyContent">' + decodeURIComponent(button.replyContent)+ '</span><span class="key">' + button.key + '</span>';
        sMhtml = '<li class="js-switchMenu2 mbtn" id="submenu_' + init_submenubuttoncount + '"><a href="javascript:;" class="subMenu"><span>' + button.name + '</span></a>' + temp + '</li>';
    }
    var $this = $(obj);
    $this.parent().prepend(sMhtml);
    $("#submenu_" + init_submenubuttoncount).click();
    var len = $this.parent().find("li").length;
    init_submenubuttoncount++;
    if (len == 6) {
        $this.hide();
    } else if (len == 2) {
        $this.parents("li.js-switchMenu").children("a").prepend('<i class="iMenu">&emsp;&ensp;</i>')
    };
}
function removeMenu(button) {
    button.remove();
    if (init_menubuttoncount == 2) {
        $(".js-add-menu").addClass("first").removeClass("col2").find('span').show();
        $(".js-edit-box").hide();
        $(".js-edit").hide();
        $(".js-tool").hide();
        init_menubuttoncount = 1;
    }
    if (init_menubuttoncount == 3) {
        $(".mbtn").removeClass("col3").addClass("col2");
        $(".js-add-menu").removeClass('col3').addClass('col2');
        $(".js-add-menu2").removeClass('col3').addClass('col2');
        init_menubuttoncount = 2;
    }
    if (init_menubuttoncount == 4) {
        $(".js-add-menu").addClass('col3').show();
        $(".js-add-menu2").addClass('col3');
        init_menubuttoncount = 3;
    }
    $(".mbtn").first().click();
}
function removeSubMenu(button) {
    var parentbtn = button.parent();
    var len = parentbtn.find(".js-switchMenu2").length;
    button.remove();
    init_submenubuttoncount--;
    if (len == 5) {
        parentbtn.find(".js-add-menu2").show();
        parentbtn.find(".js-switchMenu2").first().click();
    }
    else if (len == 1) {
        parentbtn.parents('.js-switchMenu').find('.iMenu').remove();
        parentbtn.parents('.js-switchMenu').click();
    }
    else {
        parentbtn.find(".js-switchMenu2").first().click();
    }

}
function publish() {
   
    var menu = {
        button: []
    }
    var state = true;
    $(".js-switchMenu").each(function () {
        var btn = $(this);
        var bname = btn.children('.menudata').find('.mname').text();
        var btype = btn.children('.menudata').find('.btype').text();
        var breplyType = btn.children('.menudata').find('.breplyType').text();
        var breplyContent = btn.children('.menudata').find('.breplyContent').html();
        var mode = btn.find(".js-switchMenu2").length > 0;
        var msg = checkForm(bname, btype, breplyType, breplyContent, btn,mode);
        if (msg != "") {
            state = false;
            layer.msg(msg);
            return;
        }
        var buttonItem= {
            name: bname
        }
        if (mode) {
            buttonItem.sub_button = [];
            btn.find(".js-switchMenu2").each(function() {
                var sbtn = $(this);
                var sbname = sbtn.children('.menudata').find('.mname').text();
                var sbtype = sbtn.children('.menudata').find('.btype').text();
                var sbreplyType = sbtn.children('.menudata').find('.breplyType').text();
                var sbreplyContent = sbtn.children('.menudata').find('.breplyContent').html();
                var msg = checkForm(sbname, sbtype, sbreplyType, sbreplyContent, sbtn,false);
                if (msg != "") {
                    state = false;
                    layer.msg(msg);
                    return;
                }
                var sbuttonItem = {
                    name: sbname,
                    type: sbtype
                }
                if (sbtype == "click") {
                    sbuttonItem.key = sbtn.attr('id');
                    sbuttonItem.replyType = sbreplyType;
                    sbuttonItem.replyContent = encodeURIComponent(sbreplyContent) ;
                }
                if (sbtype == "view") {
                    sbuttonItem.url = sbreplyContent;
                    sbuttonItem.replyContent = sbreplyContent;
                }
                buttonItem.sub_button.push(sbuttonItem);
            });
        } else {
            buttonItem.type = btype;
            if (btype == "click") {
                buttonItem.key = btn.attr('id');
                buttonItem.replyType = breplyType;
                buttonItem.replyContent = encodeURIComponent(breplyContent);
            }
            if (btype == "view") {
                buttonItem.url = breplyContent;
                buttonItem.replyContent = breplyContent;
            }
        }
        menu.button.push(buttonItem);
    });
    if (!state) {
        return;
    }
    var jsonMenu = JSON.stringify(menu);
    layer.load(0, { shade: false });
    $(".mask").show();
    $.post("AjaxHandler/MpHandler.ashx/PublishMenu",
        { menu: jsonMenu },
        function(result) {
            if (result.state) {
                layer.msg("发布成功");
                layer.closeAll('loading');
                $(".mask").hide();
            }
            if (result.error) {
                layer.msg("服务异常");
                layer.closeAll('loading');
                $(".mask").hide();
            }
        });
};
function checkForm(name, type, rtype, rcontent, btn,mode) {
    var alter = "";
    if (!name) {
        alter= "菜单名称不能为空";
    }
    if (mode) {
        return alter;
    }
    var match = /^((ht|f)tps?):\/\/([\w\-]+(\.[\w\-]+)*\/)*[\w\-]+(\.[\w\-]+)*\/?(\?([\w\-\.,@?^=%&:\/~\+#]*)+)?/;
    if (type == 'view') {
        if (!rcontent || !match.test(rcontent)) {
            alter = "请填写正确格式的跳转链接";
        }
    }
    if (type == 'click') {
        if (!rcontent) {
            if (rtype == 'news') {
                alter = "请选择图文消息";
            }
            if (rtype == 'img') {
                alter = "请选择图片";
            }
        }
    }
    
    if (alter != "") {
        btn.click();
    }
    return alter;
}
var TxtTool= {
    getStringRealLength: function(str) {
        var bytesCount = 0;
        if (str) {
            for (var i = 0; i < str.length; i++) {
                var c = str.charAt(i);
                if (/^[\u0000-\u00ff]$/.test(c))   //匹配双字节
                {
                    bytesCount += 1;
                }
                else {
                    bytesCount += 2;
                }
            }
        }
        return bytesCount;
    }
}
//剩余字数
function checkCharLength(max, item) {
    var maxLength = max || 30;
    var txt = item.text();
    var tips = item.siblings(".menu-emoji-toolbar").find(".menu-emoji-tip")
    item.attr('state', true);
    if (txt.length > maxLength) {
        var overflowChar = txt.length - maxLength;
        tips.html("<strong>已超出" + overflowChar + "个文字</strong>")
        item.attr('state', false);
    } else {
        var currTxt = maxLength - txt.length;
        tips.html("还可以输入<b>" + currTxt + "</b>字")
    }
};
//可视化编辑相关事件
function bindMenuEvent() {
    $(".js-add-menu").on('click', function (event) {
        event.stopPropagation();
        addMenu();
    });
    $('#lv1menu').on('click', '.js-add-menu2', function (event) {
        event.stopPropagation();
        addSubMenu($(this));
    });
    $('#lv1menu').on("click", '.mbtn', function (event) {
        event.stopPropagation();
        $(".mbtn").removeClass('on onb on2');
        if ($(this).hasClass('js-switchMenu')) {
            $(this).addClass('on onb');
        } else {
            $(this).addClass('on2');
            $(this).parents('.js-switchMenu').addClass('onb');

        }
        setEditContent($(this));
    });
    //开始排序
    $("#sort").click(function () {
        if (init_menubuttoncount == 1) {
            return false;
        }
        $(".js-edit").hide();
        $(".menu-rank").show();
        $(".js-add-menu").hide();
        $(".js-add-menu2").hide();
        $("#btn_submit").hide();
        if (init_menubuttoncount == 2) {
            $("#lv1menu li").removeClass("col2 on onb").addClass("col");
        }
        if (init_menubuttoncount == 3) {
            $("#lv1menu li").removeClass("col3 on onb").addClass("col2");
        }
        var el = document.getElementById('lv1menu');
        sortable = Sortable.create(el);
        var temparray = new Array();
        $("#lv1menu").find('ul').each(function () {
            var sortable = Sortable.create($(this)[0]);
            temparray.push(sortable);
        });
        sortableArray = temparray;
        $(".menusort").show();
        $(this).hide();
    });
    //完成排序
    $("#sortover").click(function () {
        sortable.destroy();
        for (var i = 0; i < sortableArray.length; i++) {
            sortableArray[i].destroy();
        }
        sortableArray = null;
        $(".menusort").show();
        $("#btn_submit").show();
        $(this).hide();
        $(".js-add-menu").show();
        $(".js-switchMenu").each(function () {
            var len = $(this).find(".js-switchMenu2").length;
            if (len < 5) {
                $(this).find(".js-add-menu2").show();
            }
        });
        $(".js-edit").show();
        $(".menu-rank").hide();
        if (init_menubuttoncount == 2) {
            $("#lv1menu li").removeClass("col").addClass("col2");
        }
        if (init_menubuttoncount == 3) {
            $("#lv1menu li").removeClass("col2").addClass("col3");
        }
        if (init_menubuttoncount == 4) {
            $(".js-add-menu").hide();
        }
    });
};
//属性编辑相关事件
function bindEditEvent() {
    $(".js-tabNav").click(function () {
        var $this = $(this);
        $(".edit_radio").removeAttr("checked");
        $this.find("input").prop("checked", "checked");
        $("div.js-tabCon[data-con=" + $this.attr("data-nav") + "]").show().siblings("div.js-tabCon").hide();
    });
    $(".js-tabNav2").click(function() {
        $(this).find("a").addClass("on").parent().siblings().find("a").removeClass("on");
        $("div.js-tabCon[data-con=" + $(this).attr("data-nav") + "]").show().siblings("div.js-tabCon").hide();
    });
    //删除菜单
    $("#edit_delbtn").click(function () {
        $("#alerttxt").text($("#edit_name").val());
        $("#box_delalert").show();
        $(".mask").show();
    });
    //确认删除
    $("#btn_delsure").click(function () {
        var id = $("#edit_currentId").val();
        var btn = $("#" + id);
        if (btn.hasClass("js-switchMenu")) {
            removeMenu(btn);
        } else {
            removeSubMenu(btn);
        }
        $("#box_delalert").hide();
        $(".mask").hide();
        return false;
    });
    //弹框关闭事件
    $(".close_alert").click(function () {
        $("#box_delalert").hide();
        $("#panel_news").hide();
        $("#panel_img").hide();
        $(".mask").hide();
    });
    //菜单名编辑事件
    document.getElementById("edit_name").onblur = function () {
        var newval = $("#edit_name").val();
        var len = TxtTool.getStringRealLength(newval);
        if (len > 16) {
            while (len>16) {
                newval = newval.substring(0, newval.length - 1);
                len = TxtTool.getStringRealLength(newval);
            }
        }
        $("#edit_name").val(newval);
        var btnid = $("#edit_currentId").val();
        $("#" + btnid).children('a').children('span').text(newval);
        $("#" + btnid).children('.menudata').children('.mname').text(newval);
        $("#edit_title").text(newval);
        $(".overNum").hide();
    }
    document.getElementById("edit_name").oninput= function() {
        var newval = $("#edit_name").val();
        var len = TxtTool.getStringRealLength(newval);
        if (len > 16) {
            $(".overNum").show();
        } else {
            $(".overNum").hide();
        }
    }
    //图片选择弹框
    $("#div_mediaimg").click(function() {
        $("#panel_img").show();
        $(".mask").show();
    });
    $("#box_img").on("click", ".img-wrap", function() {
        $(".img-wrap").removeClass("selected").removeAttr("state");
        $(this).addClass("selected").attr("state",1);
    });
    $("#btn_selectImg").click(function() {
        var item = $(".img-wrap[state='1']");
        var mId = item.attr('mval');
        var showsrc = item.find("img")[0].src;
        var btnid = $("#edit_currentId").val();
        $("#" + btnid).children('.menudata').children('.btype').text('click');
        $("#" + btnid).children('.menudata').children('.breplyType').text('img');
        $("#" + btnid).children('.menudata').children('.breplyContent').text(mId);
        $("#div_mediaimg").hide();
        $("#show_mediaimg").show();
        $("#img_show").attr('src',showsrc);
        $("#panel_img").hide();
        $(".mask").hide();
    });
    //图文选择弹框
    $("#div_medianews").click(function () {
        $("#panel_news").show();
        $(".mask").show();
    });
    $("#box_news").on('click', '.js-list-select', function () {
        $(".js-list-select").removeClass('selected').removeAttr('state');
        $(this).addClass('selected').attr('state', 1);
    });
    $("#btn_selectNews").click(function () {
        var item = $(".js-list-select[state='1']");
        var mId = item.attr('mval');
        var showHtml = item.find(".list-col-con").html();
        var btnid = $("#edit_currentId").val();
        $("#" + btnid).children('.menudata').children('.btype').text('click');
        $("#" + btnid).children('.menudata').children('.breplyType').text('news');
        $("#" + btnid).children('.menudata').children('.breplyContent').text(mId);
        $("#div_medianews").hide();
        $("#div_havenews").show();
        $("#show_news").html("").html(showHtml);
        $("#panel_news").hide();
        $(".mask").hide();
    });
    //跳转链接编辑事件
    $("#edit_url").blur(function() {
        var url = $(this).val().trim();
        var btnid = $("#edit_currentId").val();
        $("#" + btnid).children('.menudata').children('.btype').text('view');
        $("#" + btnid).children('.menudata').children('.breplyType').text('url');
        $("#" + btnid).children('.menudata').children('.breplyContent').text(url);
    });
  
    //文本信息编辑事件
    $("#saytext").on("keyup", function () {
        checkCharLength(600, $(this));
        });
    $("#saytext").blur(function () {
        var btnid = $("#edit_currentId").val();
        $("#" + btnid).children('.menudata').children('.btype').text('click');
        $("#" + btnid).children('.menudata').children('.breplyType').text('text');
        $("#" + btnid).children('.menudata').children('.breplyContent').html($(this).html());
    });
    //删除选中
    $("#del_imgItem").click(function() {
        event.stopPropagation();
        var bId = $("#edit_currentId").val();
        clearImg(bId);
    });
    $("#del_newsItem").click(function() {
        event.stopPropagation();
        var bId = $("#edit_currentId").val();
        clearNews(bId);
    });
}
//按钮编辑信息读取
function setEditContent(btn) {
    var name = btn.children('.menudata').find('.mname').text();
    var type = btn.children('.menudata').find('.btype').text();
    var replyType = btn.children('.menudata').find('.breplyType').text();
    var bval = btn.children('.menudata').find('.breplyContent').html();
    $("#edit_currentId").val(btn.attr('id'));
    $("#edit_url").val('');
    $("#saytext").html('');
    clearNews();
    clearImg();
    $("#edit_title").text(name);
    $("#edit_name").val(name);
    var rstyle;
    rstyle = $("#l_msg");
    if (type == "view") {
        rstyle = $("#l_url");
        $("#edit_url").val(bval);
    }
    $(".edit_radio").removeAttr("checked");
    rstyle.find("input").prop("checked", "checked");
    $("div.js-tabCon[data-con=" + rstyle.attr("data-nav") + "]").show().siblings("div.js-tabCon").hide();
    if (type == "click") {
        if (replyType == "news") {
            $("#tag_news").click();
            showNews(bval);
        } else if (replyType == "img") {
            $("#tag_img").click();
            showImg(bval);
        } else {
            $("#tag_text").click();
            showText(bval);
        }
    }
    //是否一级含有二级的菜单
    if (btn.find('.js-switchMenu2').length > 0) {
        $(".menu-oStyle").hide();
        $(".menu-oPart").hide();
    } else {
        $(".menu-oStyle").show();
        $(".menu-oPart").show();
    }
}
function showNews(mval) {
    if (!mval) {
        return;
    }
    var newsItem = $('.js-list-select[mval="' + mval + '"]').find(".list-col-con");
    if (newsItem.length==0) {
        return;
    }
    $("#div_medianews").hide();
    $("#div_havenews").show();
    var showHtml=newsItem.html();
    $("#show_news").html("").html(showHtml);
}
function showImg(mval) {
    if (!mval) {
        return;
    }
    var img = $('.img-wrap[mval="' + mval + '"]').find("img")[0];
    if (img == undefined) {
        return;
    }
    $("#div_mediaimg").hide();
    $("#show_mediaimg").show();
    var showsrc = img.src;
    $("#img_show").attr('src', showsrc);

}
function showText(text) {
    $("#saytext").html(text);
    checkCharLength(600, $("#saytext"));
}
function clearNews(bId) {
    $("#div_medianews").show();
    $("#div_havenews").hide();
    $("#show_news").html("");
    if (bId) {
        $("#" + bId).children('.menudata').find('.breplyContent').html("");
    }
}
function clearImg(bId) {
    $("#div_mediaimg").show();
    $("#show_mediaimg").hide();
    $("#img_show").attr('src', '');
    if (bId) {
        $("#" + bId).children('.menudata').find('.breplyContent').html("");
    }
}
var newsList;
var imgList;
function imgHtml(list) {
    var t = '';
    if (list!=undefined&&list.length > 0) {
        for (var i = 0; i < list.length; i++) {
            t += '<div class="img-wrap" mval="' + list[i].MediaId+'"><div class="img-box" ><img src="' + list[i].MUrl + '" alt="' + list[i].MName + '"></div><div class="img-mask"><i></i></div><p>' + list[i].MName + '</p></div>';
        }
        return t;
    }
}
$(document).ready(function () {
    bindMenuEvent();
    bindEditEvent();
    layer.load(0, { shade: false });
    $(".mask").show();
    //初始化素材库数据
    $.post("AjaxHandler/MpHandler.ashx/GetMediaList",function (result) {
        if (result.state) {
            newsList = result.newsList;
            imgList = result.imgList;
            $("#box_news").html("").html(createNewsItem(newsList));
            $("#box_img").html("").html(imgHtml(imgList));
        }
        $.post("AjaxHandler/MpHandler.ashx/GetMenu", function (result) {
            if (result.state) {
                initData(result.buttons.button);
            }
        });
        layer.closeAll('loading');
        $(".mask").hide();
    });
    $("#btn_submit").click(function() {
        publish();
    });
    $(".btn_sync").click(function () {
        layer.load(1);
        $.post("AjaxHandler/MpHandler.ashx/GetMediaList",
            {mode:true},function (result) {
            if (result.state) {
                newsList = result.newsList;
                imgList = result.imgList;
                $("#box_news").html("").html(createNewsItem(newsList));
                $("#box_img").html("").html(imgHtml(imgList));
            }
            layer.closeAll('loading');
        });
    });
});