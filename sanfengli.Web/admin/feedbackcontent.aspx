<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="feedbackcontent.aspx.cs" Inherits="sanfengli.Web.admin.feedbackcontent" %>

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
                <th width="60">所属分类
                </th>
                <th width="200">反馈图片
                </th>
                <th width="200">反馈内容
                </th>
                <th width="100">姓名
                </th>
                <th width="100">电话
                </th>
                <th width="100">反馈时间
                </th>
                <th>操作
                </th>
            </tr>
            <% if (pageInfo != null && pageInfo.PageSize > 0)
                {
                    foreach (var model in list)
                    { %>
            <tr data-id="<%=model.Id %>" data-remark="<%=model.remark %>" data-userid="<%=model %>">
                <td><%= model.typename %>
                </td>
                <td>
                    <%if (string.IsNullOrEmpty(model.Image))
                        {%>
                        <img src="<%= model.Image %>">
                        <%}
                        else {%>
                    <a target="_blank" href="<%=model.Image %>">
                        <img src="<%= model.Image %>"></a>
                        <%} %>
                    
                </td>
                <td><%= model.Content %>
                </td>
                <td><%= model.name %>
                </td>
                <td><%= model.phone %>
                </td>
                 <td><%= model.CreateOn %>
                </td>
                <td>
                    <a href="javascript:;" class="copy">回复</a>
                     <a href="javascript:;" onclick="deleteModel(<%=model.Id %>)">删除</a>
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
