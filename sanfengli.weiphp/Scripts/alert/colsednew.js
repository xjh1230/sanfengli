// JavaScript Document


//关闭层 外部，内部都可调用
function HideTxt() {
    jQuery(".cd_osdbg").remove();
    jQuery(".cd_tck").remove();
}
//_PromptText 提示文本（必填）
//_BtnOkText 确认按钮文本
//_PromptText 确认按钮要执行的事件（必须是函数名）
//_BtnCancelText 取消按钮文本
//_BtnCancelEvent 取消按钮要执行的事件（必须是函数名）
//_InputText 文本框的标题
//_PromptText 文本框的非空验证显示文本
function ShowTxt(_PromptText, _BtnOkText, _BtnOkEvent, _BtnCancelText, _BtnCancelEvent, _InputText, _NotNullTitle) {
    if (arguments.length <= 0) {
        _PromptText = "<img src='../Images/loadingAnimation.gif'/><br />正在处理中,请稍后。。。";
    }
    var _appHTML = "<div class='cd_osdbg'></div>"; /**添加背景遮罩层**/
    _appHTML += "<div class='cd_tck'>";
    _appHTML += "<div class='cd_prd'>";
    //_appHTML += "<span class='cd_btn'></span>"; //显示关闭按钮
    if (_isNull(_PromptText)) {
        _appHTML += "<h1>" + _PromptText + "</h1>";
    }
    if (_isNull(_InputText)) {
        _appHTML += "<div class='cd_taipt'><span>" + _InputText + "</span><input /><p>" + _NotNullTitle + "</p></div>";
    }
    if (arguments.length > 1) {
        _appHTML += "<div class='span_btn'>";
        if (_isNull(_BtnOkText)) {
            _appHTML += "<a class='btn1_alink_btn'>" + _BtnOkText + "</a>";

        }
        if (arguments.length > 3) {
            if (_isNull(_BtnCancelText)) {
                _appHTML += "<a class='btn2_alink_btn'>" + _BtnCancelText + "</a>";
            }
        }
        _appHTML += "</div>";
    }
    _appHTML += "</div>";
    _appHTML += "</div>";
    //添加弹出框；
    jQuery("body").append(_appHTML);

    //绑定事件
    jQuery(".span_btn a").each(function (index) {
        jQuery(this).attr("id", jQuery(this).attr("class") + "_" + index);
        var domElement = document.getElementById(jQuery(this).attr("id"));
        if (window.addEventListener) { _bind(domElement, "click", HideTxt); }
        var _a_classname = jQuery(this).attr("class");
        //确定按钮添加事件
        if (_a_classname == "btn1_alink_btn") {
            if (_isNull(_BtnOkText)) {
                if (_isFunction(_BtnOkEvent)) {
                    _bind(domElement, "click", _BtnOkEvent);
                }
            }
        }
        //取消按钮添加事件
        if (_a_classname == "btn2_alink_btn") {
            if (_isNull(_BtnCancelText)) {
                if (_isFunction(_BtnOkEvent)) {
                    _bind(domElement, "click", _BtnCancelEvent);
                }
            }
        }
        if (window.attachEvent) { _bind(domElement, "click", HideTxt); }
    });
    //关闭；
    jQuery(".cd_btn").click(function () { HideTxt() }); //给那个右上角的关闭按钮加事件
}
function _isElement(obj) {
    return !!(obj && obj.nodeType == 1);
};
function _bind(el, name, fn) { //绑定事件
    if (_isElement(el)) {
        return el.addEventListener ? el.addEventListener(name, fn, false) : el.attachEvent('on' + name, fn);
    }
}

function _isNull(_obj) {
    if (_obj != null && typeof (_obj) != "undefined" && _obj != "")
        return true
    else
        return false;
}
function _isFunction(_fn) {
    if (typeof (_fn) == "function")
        return true
    else
        return false;
}

