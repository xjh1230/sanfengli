
//向jQuery对象中注册通用事件
; (function ($) {

    var regexs = {
        mobile: /^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|4|5|6|7|8|9]|17[0-9])\d{8}$/,//电话
        number: /^\d+$/,//数字
        email: /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/,//邮箱
        idcard: /(^\d{15}$)|(^\d{17}([0-9]|X)$)/, //身份证
        pwd: /^[\@A-Za-z0-9\!\#\$\%\^\&\*\.\~]{6,18}$/, //密码6-18位
        url: /^(\w+:\/\/)?\w+(\.\w+)+.*$/,//网址
        name: /^([\u4E00-\u9FA5]+|[a-zA-Z]+)$/,
        hasvalue: /.+/,
        zhengshu: /^[1-9]\d*/
    };
    //表单验证
    $.fn.regexValidate = function () {
        var value = this.val();
        var pattern = this.data("pattern");
        var validatetype = this.data("validatetype");
        if (pattern) {
            regex = new RegExp(pattern);
            return regex.test(value);
        } else {
            if (regexs[validatetype]) {
                return regexs[validatetype].test(value);
            } else {
                return true;
            }
        }

    };

    // 显示错误提示信息
    $.fn.showTips = function () {
        this.parents('.form-field').find('.tips-group').show();
        return this;
    };
    // 隐藏错误提示信息
    $.fn.hideTips = function () {
        this.parents('.form-field').find('.tips-group').hide();
        return this;
    };

    // 输入框获得焦点及失去焦点事件
    $(document).on('focus', 'input,textarea,password,select', function () {
        if (!$(this).regexValidate()) {
            $(this).showTips();
        }
    }).on('blur', 'input,textarea,password,select', function () {
        if (!$(this).regexValidate()) {
            $(this).hideTips();
        }
    }).on('keyup', 'input,textarea,password,select', function () {
        if (!$(this).regexValidate()) {
            $(this).showTips();
            var value = $(this).val();
            value = value.substring(0, value.length - 1);
            $(this).val(value);
        } else {
            $(this).hideTips();
        }
    }).on('change', 'input,textarea,password,select', function () {
        if ($(this).regexValidate()) {
            $(this).hideTips();
        }
    });

})(jQuery);






