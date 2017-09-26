(function ($) {
    $.extend({
        messageBox: function (txt) {
            var $box = $('#___tips___');

            if ($box.length === 0) {
                $('body').append('<div id="___tips___" style="display:none; position: fixed; width: 150px; height:50px; margin-top:-25px; margin-left:-75px; line-height:50px; color: #fff; top: 50%; left: 50%; border-radius: 10px; font-size: 14px; background: #000; opacity: 0.7; text-align: center;"></div>');

                $box = $('#___tips___');
            }

            $box.html(txt).show();

            setTimeout(function () {
                $box.fadeOut();
            }, 2000);

            return $;
        }
    });
})(jQuery);