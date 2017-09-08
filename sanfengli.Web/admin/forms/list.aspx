<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="sanfengli.Web.admin.forms.list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../public/css/base.css" rel="stylesheet" />
    <link href="../public/css/dropdown.css" rel="stylesheet" />
    <link href="../public/css/datetimepicker.css" rel="stylesheet" />
    <link href="../public/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../public/css/module.css" rel="stylesheet" />
    <link href="../public/css/weiphp.css" rel="stylesheet" />
    <script src="../public/js/jquery-2.0.3.min.js"></script>
    <script src="../public/js/jquery.dragsort-0.5.2.min.js"></script>
    <script src="../public/js/masonry.pkgd.min.js"></script>
    <script src="../public/js/webuploader.min.js"></script>
    <script src="../public/js/dialog.js"></script>
    <script src="../public/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../public/js/bootstrap-datetimepicker.zh-CN.js"></script>
    <%--<script src="../public/js/admin_common.js"></script>
    <script src="../public/js/admin_image.js"></script>--%>


    <title>列表</title>
</head>
<body>
    <div class="main_body">

        <div class="span9 page_message">
            <section id="contents">
                <ul class="tab-nav nav">
                    <li class="current"><a href="#"><%=this.typeName %></a></li>
                </ul>
                <div class="table-bar">
                    <div class="fl">
                        <div class="tools">
                            <a class="btn" href="edit.aspx?typeId=<%=this.typeId %>">新 增</a>
                            <button class="btn ajax-post confirm" target-form="ids">删 除</button>

                        </div>
                    </div>
                    <!-- 高级搜索 -->
                    <div class="search-form fr cf">
                        <div class="sleft">
                            <input type="text" name="title" class="search-input" value="" placeholder="请输入标题">
                            <a class="sch-btn" href="javascript:;" id="search" url="ajax_lists_data.aspx?typeId=<%=typeId %>"><i class="btn-search"></i></a>
                        </div>
                    </div>
                    <!-- 多维过滤 -->
                </div>
                <!-- 数据列表 -->
                <div class="data-table">
                    <div class="table-striped">
                        <table cellspacing="1">
                            <!-- 表头 -->
                            <thead>
                                <tr>
                                    <th class="row-selected row-selected">
                                        <input autocomplete="off" type="checkbox" id="checkAll" class="check-all regular-checkbox"><label for="checkAll"></label></th>
                                    <%if (listTitle != null && listTitle.Count > 0)
                                        {
                                            foreach (var item in listTitle)
                                            {%>
                                    <th><%=item %></th>
                                    <%}
                                        } %>
                                </tr>
                            </thead>

                            <!-- 列表 -->
                            <tbody id="list_content">
                                <tr>
                                    <td>
                                        <input autocomplete="off" class="ids regular-checkbox" type="checkbox" value="12" name="ids[]" id="check_12"><label for="check_12"></label></td>
                                    <td>12</td>
                                    <td>as</td>
                                    <td>2017-09-08 11:54</td>
                                    <td><a target="_self" href="http://15x01307w0.iask.in/index.php?s=/w17/Survey/Survey/edit/id/12/model/survey.html">编辑</a>
                                        <a class="confirm" href="http://15x01307w0.iask.in/index.php?s=/w17/Survey/Survey/del/id/12/model/90.html">删除</a>
                                        <a target="_self" href="http://15x01307w0.iask.in/index.php?s=/w17/Survey/Survey/survey_answer/id/12/model/90.html">数据管理</a>
                                        <a target="_blank" href="http://15x01307w0.iask.in/index.php?s=/w17/Survey/Survey/preview/id/12/model/90.html">预览</a></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="page"></div>
            </section>
        </div>

    </div>
</body>

<script>

</script>
</html>
