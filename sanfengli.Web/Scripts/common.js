//显示日期控件
function ShowDatePickerMax(txtid) {
    WdatePicker({
        maxDate: '#F{$dp.$D(' + txtid + ')}',
        dateFmt: 'yyyy-MM-dd HH:mm:ss',
        lang: 'zh-cn',
        startDate: '%y-%M-%d 00:00:00',
        readOnly: true
    });
}
function ShowDatePickerMin(txtid) {
    WdatePicker({
        minDate: '#F{$dp.$D(' + txtid + ')}',
        dateFmt: 'yyyy-MM-dd HH:mm:ss',
        lang: 'zh-cn',
        startDate: '%y-%M-%d 23:59:59',
        readOnly: true
    });
}
//只能输入整数
function OnlyNum(obj) {
    obj.value = obj.value.replace(/\D/g, '');
}
//只能输入2位小数
function clearNoNum(obj) {
    obj.value = obj.value.replace(/[^\d.]/g, "");  //清除“数字”和“.”以外的字符  
    obj.value = obj.value.replace(/^\./g, "");  //验证第一个字符是数字而不是.  
    obj.value = obj.value.replace(/\.{2,}/g, "."); //只保留第一个. 清除多余的  
    obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
    obj.value = obj.value.replace(/^(\-)*(\d+)\.(\d\d).*$/, '$1$2.$3'); //只能输入两个小数  
}

//数组是否有重复元素
Array.prototype.IsRepeat = function () {
    var result = false;
    this.sort();
    var re = [this[0]];
    for (var i = 1; i < this.length; i++) {
        if (this[i] == re[re.length - 1]) {
            result = true;
            break;
        }
    }
    return result;
}

function AjaxPost(url, postBody, PostPalyCallbackName) {
    var myajax = new jQuery.ajax({
        type: 'post',
        url: url,
        async: true,
        data: postBody,
        success: function (data) {
            PostPalyCallbackName(data);
        }
    });
}
