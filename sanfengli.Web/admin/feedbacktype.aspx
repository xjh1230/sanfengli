<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="feedbacktype.aspx.cs" Inherits="sanfengli.Web.admin.feedbacktype" %>



<div class="form_box_w">
    <table class="list">
        <tbody>
            <tr>
                <th width="60">Id
                </th>
                <th width="200">随手拍分类
                </th>
                <th>操作
                </th>
            </tr>
            <% if (list != null && list.Count > 0)
                {
                    foreach (var model in list)
                    { %>
            <tr data-id="<%=model.Id %>">

                <td><%= model.Id %>
                </td>
                <td><%= model.Name %>
                </td>
                <td>
                    <a href="javascript:;"  onclick="editType(<%=model.Id %>,'<%=model.Name %>')">修改</a>
                    <a href="javascript:;" onclick="deleteType(<%=model.Id %>)">删除</a>
                </td>
            </tr>

            <% }
                } %>
        </tbody>
    </table>

</div>
