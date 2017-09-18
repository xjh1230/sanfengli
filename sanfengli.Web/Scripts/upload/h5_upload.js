;
var demo_h5_upload_ops = {
    init: function () {
        this.eventBind();
    },
    eventBind: function () {
        var that = this;
        $("#upload").change(function () {
            var reader = new FileReader();
            reader.onload = function (e) {
                that.compress(this.result);
            };
            $('#uploadImg').submit();
            var formData = new FormData();
            var name = $("input").val();
            var path = $(this).data('path');
            formData.append("fileImage", $("#upload")[0].files[0]);
            formData.append("op", "uploadImg");
            formData.append("path", path);
            
            $.ajax({
                url: "/home/ajax/uploadHandler.aspx?op=uploadImg&path=" + path,
                type: "POST",
                data: formData,
                // 告诉jQuery不要去处理发送的数据
                processData: false,
                // 告诉jQuery不要去设置Content-Type请求头
                contentType: false,
                success: function (res) {
                    data = $.parseJSON(res);
                    if (data.IsSuccess) {
                        $(".img_wrap").attr("src", data.Msg);
                        $('#image_src').val(data.Msg);
                        $(".img_wrap").show();
                        //$('#upload-btn').css("display", "none");
                    } else {
                        alert(data.Msg, "error");
                    }
                }
            });
            //reader.readAsDataURL(this.files[0]);
        });
    },
    compress: function (res) {
        var that = this;
        var img = new Image(),
            maxH = 600;

        img.onload = function () {
            var cvs = document.createElement('canvas'),
                ctx = cvs.getContext('2d');

            if (img.height > maxH) {
                img.width *= maxH / img.height;
                img.height = maxH;
            }
            cvs.width = img.width;
            cvs.height = img.height;

            ctx.clearRect(0, 0, cvs.width, cvs.height);
            ctx.drawImage(img, 0, 0, img.width, img.height);
            var dataUrl = cvs.toDataURL('image/jpeg', 1);
            //$(".img_wrap1").attr("src", dataUrl);
            $(".img_wrap").attr("src", dataUrl);
            $(".img_wrap").show();
            //$(".img_wrap1").show();
            $('#image_src').val(dataUrl);
            // 上传略
            //that.upload( dataUrl );
        };

        img.src = res;
    },
    upload: function (image_data) {
        /*上次方法屏蔽了*/
        return;
        $.ajax({
            url: common_ops.buildWapUrl("/demo/h5_upload"),
            type: 'POST',
            data: { image_data: image_data },
            dataType: 'json',
            success: function (res) {

            }
        });
    }
};


$(document).ready(function () {
    demo_h5_upload_ops.init();
});