<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="infodetail.aspx.cs" Inherits="sanfengli.Web.home.infodetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta http-equiv="Cache-Control" content="no-transform">
    <meta http-equiv="Cache-Control" content="no-siteapp">
    <meta name="viewport" content="width=device-width, maximum-scale=1.0, user-scalable=no">
    <meta name="viewport" content="width=device-width, minimum-scale=0.1, maximum-scale=1.0, user-scalable=yes">
    <title><%=model==null?"文章信息":model.name %></title>
</head>
<body>
    <%if (model == null)
        {%>
    <h1>暂无内容</h1>
    <%}
        else
        {%>
    <h1><%=model.title %></h1>
    <hr />
    <div>
        <%=model.content %>
    </div>
    <%} %>
</body>
</html>
