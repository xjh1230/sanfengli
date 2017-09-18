<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="feedbackmgr.aspx.cs" Inherits="sanfengli.Web.admin.feedbackmgr" %>

<!DOCTYPE html>
<include file="Home@Public/head" />	
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>随手拍列表</title>
    <link href="../Content/style.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.8.3.min.js"></script>
    <%--<script src="../Scripts/page.js"></script>
    <script src="../Scripts/datapattern.js"></script>--%>
</head>
<body>
    <div class="wrapper">
        <div class="config-custom">
            <h2>随手拍列表</h2>
            <form id="form1" method="GET">
                <ul class="config-ul">
                    <li><span class="c-tit">内容：</span><input type="text" class="inputxt column-5" placeholder="不超过20个字符" name="Content" /></li>
                    <li>
                        <span class="c-tit">姓名：</span>
                        <input type="text" name="name" class="inputxt custom-online" />
                    </li>
                    <li style="text-align: right;">
                        <a class="btn btn-save" href="javascript:getFeedback();">查询</a>
                    </li>
                </ul>
            </form>
        </div>
        <div id="loadingPage">
        </div>
    </div>

    <div class="pop-box2">
        <span class="pop-close">x</span>
        <textarea id="remark"></textarea>
        <div class="main-tit add-custom h-center">
            <span class="btn" id="saveRemark" data-canclick="true">保存</span>
        </div>
    </div>
    <div class="mask">
    </div>
    <script>
        $(function () {

            $("#loadingPage").load('feedbackcontent.aspx');


            //分页点击事件
            jQuery(document).on("click", '.pageInfo a', function () {
                if (jQuery(this).attr("href") != "javascript:void(0);") {
                    jQuery("#loadingPage").html("<div style='text-align:center;width:100%'>正在加载，请稍后...</div>");
                    jQuery("#loadingPage").load(jQuery(this).attr("href"));
                    return false;
                }
            });
            //备注打开
            $(document).on('click', '.copy', function () {

                var currId = $(this).parent().parent().data('id');
                var Remark = $(this).parent().parent().data('remark');
                $('#remark').val(Remark);
                $('#saveRemark').data('id', currId);
                $('#saveRemark').data('remark', Remark);
                $('.pop-box2').fadeIn(200);
                $('.mask').fadeIn(200);
            })
            //备注关闭事件
            $(document).on('click', '.pop-close', function () {
                $(this).parent().fadeOut(200)
                $('.mask').fadeOut(200);
            })
            //保存备注
            $('#saveRemark').on('click', function () {
                var $this = $(this);
                var canclick = $this.data('canclick');
                if (!canclick) {
                    return false;
                }
                $this.data('canclick', false);
                var id = $('#saveRemark').data('id');
                var remark = $('#remark').val();
                if (id == 0) {
                    alert("请选择用户")
                    $this.data('canclick', true);
                }
                if (remark == $this.data('remark')) {
                    alert("请编辑后保存");
                    $this.data('canclick', true);
                } else {
                    $.ajax({
                        url: "ajax/feedbackHandler.aspx?op=edit",
                        data: {
                            id: id,
                            remark: remark
                        },
                        success: function (data) {
                            $this.data('canclick', true);
                            var data = $.parseJSON(data);
                            alert(data.Msg);
                            if (data.IsSuccess) {
                                var $tr = $('.list tr');
                                $tr.map(function (i, v) {
                                    if ($(v).data('id') == id) {
                                        $(v).data('remark', remark);
                                    }
                                });
                                $this.data('remark', remark);
                                $('.pop-box2').fadeOut(200);
                                $('.mask').fadeOut(200);

                            }
                        },
                        error: function () {
                            $this.data('canclick', true);
                        }
                    });
                }
            })
        });
        //查询
        function getFeedback() {
            $("#loadingPage").html("<div style='text-align:center;width:100%'>正在加载，请稍后...</div>");
            $("#loadingPage").load('feedbackcontent.aspx?1=1' + getSearchCondition());
        }
        //拼接搜索条件
        function getSearchCondition() {
            var data = jQuery('#form1').serialize();
            data += "&pageSize=10&pageIndex=1";
            data = "&" + data;
            return data;
        }
        //json时间格式化
        function dataFormat(data) {
            return ChangeDateFormat(data).pattern("yyyy-MM-dd");
        }
    </script>
</body>
</html>

