<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="answerdetail.aspx.cs" Inherits="sanfengli.Web.admin.answerdetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>答题详情</title>
    <link href="../Content/style.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.8.3.min.js"></script>
</head>
<body>
    <div class="wrapper">
        <div class="config-custom">
            <h1>答题详情</h1>

        </div>
        <div id="loadingPage">
            <div class="form_box_w">
                <table class="list">
                    <tbody>
                        <tr>
                            <th width="200">问题
                            </th>
                            <th width="150">答案
                            </th>
                        </tr>
                        <% if (list != null && list.Count > 0)
                            {
                                foreach (var model in list)
                                { %>
                            <td><%= model.Querstion %>
                            </td>
                            <td><%= model.AnswerValue %>
                            </td>
                        </tr>

                        <% }
                            } %>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</body>
</html>
