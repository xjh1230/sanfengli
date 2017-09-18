function vote_join(optionid, voteid, uid, _this) {
    if (optionid == 0) {
        $.Dialog.fail('获取不到投票选项');
        return false;
    }
    if (voteid == 0) {
        $.Dialog.fail('该活动不存在');
        return false;
    }
    if ($(_this).hasClass('has_vote')) {
        return false;
    }
    $(_this).addClass('has_vote');
    do_vote(_this, voteid, optionid, uid, 0);

}
function do_vote(_this, voteid, optionid, uid) {
    var url = "ajax/uploadHandler.aspx";
    $.post(url, { 'op': 'dovote', 'vote_id': voteid, 'option_id': optionid, 'uid': uid }, function (res) {
        var date = $.parseJSON(res);
        if (!date.IsSuccess) {
            $.Dialog.fail(date.Msg);
            $(_this).removeClass('has_vote');
        } else {
            $.Dialog.success(date.Msg);
            setTimeout(function () {
                location.reload();
            }, 1500)
        }
    });
}
