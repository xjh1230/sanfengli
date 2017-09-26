<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="articlelist.aspx.cs" Inherits="sanfengli.Web.home.articlelist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="apple-mobile-web-app-capable" content="yes"/>
    <meta http-equiv="Cache-Control" content="no-transform"/>
    <meta http-equiv="Cache-Control" content="no-siteapp"/>
    <meta name="viewport" content="width=device-width, maximum-scale=1.0, user-scalable=no"/>
    <meta name="viewport" content="width=device-width, minimum-scale=0.1, maximum-scale=1.0, user-scalable=yes"/>
    <link href="../Scripts/elementui/index.css" rel="stylesheet" />
    <%--<link rel="stylesheet" href="https://unpkg.com/element-ui/lib/theme-default/index.css">--%>
    <title>文章列表</title>
    <style type="text/css" abt="234"></style>
    <style>
        .none-data{
            display:none;
            margin-top: 200px;
            font-size: 26px;
            text-align: center;
            color: #ff4949;
        }
        .fixed_bg{
            position: fixed;
            z-index: -1;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            background: url(/img/bg2.jpg) no-repeat;
            background-size: 100% 100%;
        }
        .el-card__body{
            background-color:#F0AAD5;
        }
        .el-icon-arrow-right{
            float:right;
        }
        .el-button{
             float:right;
        }
    </style>
</head>
<body>
    <div id="app">
        <div class="fixed_bg"></div>
        <div  v-for="o in list" :key="o" style="display:none" id="list">
            <el-card class="box-card"  >
               <div v-on:click="jump(o)">
                <span >{{o.name}}<i class="el-icon-arrow-right"  ></i></span>
                </div>
            </el-card>
        </div>
            
        <div  class="none-data">
            <p>暂无数据</p>
        </div>
    </div>

</body>
<script src="../Scripts/jquery-1.10.2.min.js"></script>
<script src="../Scripts/elementui/vue.js"></script>
<script src="../Scripts/elementui/index.js"></script>
<script>

    new Vue({
        el: '#app',
        data() {
            return {
                dialogVisible:false,
                list: [],
            }
        },
        mounted() {
            this.init()
        },
        methods: {
            init() {
                var _this= this;
                $.post('ajax/infoHandler.aspx', { op: 'getInfoType' }, function (res) {
                    var data = $.parseJSON(res);
                    if (data.IsSuccess) {
                        $('#list').show();
                        $('.none-data').hide();
                        _this.list = data.Data;
                    } else {
                        $('.none-data').show();
                        $('#list').hide();
                    }

                    //_this.dialogVisible = true;

                })
            },
            jump: function (o) {
                window.location.href = o.url;
            } 
        }
    })
</script>
</html>
