<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="surveyanswer.aspx.cs" Inherits="sanfengli.Web.admin.surveyanswer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>问卷数据管理</title>
    <link href="../Content/style.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.8.3.min.js"></script>
</head>
<body>
     <div class="wrapper">
        <div class="config-custom">
            <h2>问卷数据管理</h2>
          
        </div>
        <div id="loadingPage">
            <div class="form_box_w">
    <div class="form_top_w">
        <span class="putout">找到<%=pageInfo.TotalCount%>条数据</span>
        <p class="pageInfo">
            显示
            <select name="selectUp" class="selectUp">
                <option value="10" <%=pageInfo.PageSize==10?"selected='selected'":"" %>>10</option>
                <option value="20" <%=pageInfo.PageSize==20?"selected='selected'":"" %>>20</option>
                <option value="30" <%=pageInfo.PageSize==30?"selected='selected'":"" %>>30</option>
                <option value="40" <%=pageInfo.PageSize==40?"selected='selected'":"" %>>40</option>
            </select>
            条
                 共 <%=pageInfo.TotalCount%> 项 <a href="<%=pageInfo.BeginUrl %>">首页</a>&nbsp;<a href="<%=pageInfo.PreUrl %>" class="btn_page">上一页</a>&nbsp;<span class="down">│<b><%=pageInfo.CurrentPage %></b>│</span>&nbsp; <a href="<%=pageInfo.NextUrl %>">下一页</a>&nbsp;<a href="<%=pageInfo.EndUrl %>">尾页</a>
        </p>
    </div>
    <table class="list">
        <tbody>
            <tr>
                <th width="200">OpenId
                </th>
                <th width="150">用户昵称
                </th>
                <th width="150">手机号
                </th>
                <th width="100">参与时间
                </th>
                <th>操作
                </th>
            </tr>
            <% if (pageInfo != null && pageInfo.PageSize > 0)
                {
                    foreach (var model in list)
                    { %>
            <tr data-id="<%=model.Id %>"  >
             
                <td><%= model.openid %>
                </td>
                <td><%= model.NickName %>
                </td>
                <td><%= model.mobile %>
                </td>
                 <td><%= model.CreateOn %>
                </td>
                <td>
                    <a href="<%=model.Url %>" class="copy">回答内容</a>
                </td>
            </tr>

            <% }
                } %>
        </tbody>
    </table>
    <div class="form_top_w">
        <p class="pageInfo">
            显示
            <select name="selectUp" class="selectUp">
                <option value="10" <%=pageInfo.PageSize==10?"selected='selected'":"" %>>10</option>
                <option value="20" <%=pageInfo.PageSize==20?"selected='selected'":"" %>>20</option>
                <option value="30" <%=pageInfo.PageSize==30?"selected='selected'":"" %>>30</option>
                <option value="40" <%=pageInfo.PageSize==40?"selected='selected'":"" %>>40</option>
            </select>
            条
                  共 <%=pageInfo.TotalCount%> 项 <a href="<%=pageInfo.BeginUrl %>">首页</a>&nbsp;<a href="<%=pageInfo.PreUrl %>" class="btn_page">上一页</a>&nbsp;<span class="down">│<b><%=pageInfo.CurrentPage %></b>│</span>&nbsp; <a href="<%=pageInfo.NextUrl %>">下一页</a>&nbsp;<a href="<%=pageInfo.EndUrl %>">尾页</a>
        </p>
    </div>
</div>
        </div>
    </div>
</body>
</html>
