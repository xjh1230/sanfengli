<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuManager.aspx.cs" Inherits="sanfengli.Web.Wx.MenuManager" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <meta name="referrer" content="never">
    <title>自定义菜单</title>
    <link rel="stylesheet" type="text/css" href="../css/index.css">
    <link rel="stylesheet" href="../css/emojis.css"/>
<script type="text/javascript" src="../Scripts/jquery-1.10.2.min.js"></script>
</head>
<body>
    <div class="menu-wrap">        
       <div class="menu-edit-box clearfix">
           <div class="menu-subEdit">
               <div class="menu-screen">
                  <p class="menu-screen-name">三丰里社区</p>
                  <div class="menu-screen-list" id="js-mainDrag">
                      <ul class="clearfix" id="lv1menu">
                          <li class="first js-add-menu">
                              <a href="javascript:;" class="mainMenu">
                                  <i class="iPlus">&emsp;&ensp;</i>
                                  <span>添加菜单</span>
                              </a>
                          </li>
                      </ul>
                  </div>
               </div>
           </div>
           <div class="menu-mainEdit">
               <div class="menu-con js-edit-box">
                   <din class="menu-con-box js-edit">
                       <input type="hidden" id="edit_currentId" value=""/>
                       <div class="menu-con-top">
                           <h4 id="edit_title">菜单名称</h4>
                           <div class="menu-del">
                               <a href="javascript:;" id="edit_delbtn">删除菜单</a>
                           </div>
                       </div>
                        <div class="menu-operate">
                            <div class="menu-oName clearfix">
                                <label>菜单名称</label>
                                <div class="menu-oWrap">
                                    <span class="menu-editName-box">
                                       <input type="text" class="editName" id="edit_name" placeholder="菜单名称">
                                    </span>
                                    <p class="overNum">字数超过上限</p>
                                    <p class="totleNum">字数不超过8个汉字或16个字母</p>
                                </div>
                            </div>
                            <div class="menu-oStyle clearfix">
                                <label>菜单内容</label>
                                <div class="menu-oWrap">
                                    <label id="l_msg" data-nav="new" class="js-tabNav"><input type="radio"  class="edit_radio">&ensp;发送消息&emsp;&ensp;</label> 
                                    <label id="l_url" data-nav="url" class="js-tabNav"><input type="radio"  class="edit_radio">&ensp;跳转网页&emsp;&ensp;</label>      
                                </div>
                            </div>
                            <div class="menu-oPart">
                                <!--发送消息-->
                                <div class="menu-sendPart js-tabCon" data-con="new" style="display:block;">
                                    <div class="menu-sendNav">
                                        <ul>
                                            <li data-nav="new1" id="tag_news" class="js-tabNav2" rtype="news"><a href="javascript:;" class="on"><i class="img-txt"></i><span>图文消息</span></a></li>
                                            <li data-nav="new3" id="tag_text" class="js-tabNav2" rtype="text"><a href="javascript:;"><i class="img-pen"></i><span>文字</span></a></li>
                                            <li data-nav="new2" id="tag_img" class="js-tabNav2" rtype="image"><a href="javascript:;"><i class="img"></i><span>图片</span></a></li>
                                        </ul>
                                    </div>
                                    <div class="menu-sendCon js-tabCon" data-con="new1" style="display:block;">
                                         <div class="menu-sendCon-item" id="div_medianews">
                                             <a href="javascript:;">
                                                 <i></i>
                                                <span>从素材库中选择</span>
                                             </a>
                                         </div>
                                         <!--已选中展示状态-->
                                         <div class="menu-selected-box" id="div_havenews">
                                             <div class="menu-selected-img" id="show_news">
                                                
                                             </div>
                                             <a href="javascript:;" id="del_newsItem" class="menu-selected-idel">删除</a>
                                         </div>
                                    </div>
                                    <div class="menu-sendCon js-tabCon" data-con="new3">
                                         <div class="menu-emoji-wrap">
                                            <div class="menu-emoji-area js_editorArea" contenteditable="true" id="saytext"></div>
                                            <div class="menu-emoji-toolbar clearfix">
                                                <a href="javascript:void(0);" class="icon icon-face emotion js_switch"></a>
                                                <p class="menu-emoji-tip js_editorTip">还可以输入<b>600</b>字</p>
                                               </div>
                                         </div>
                                    </div>
                                    <div class="menu-sendCon js-tabCon" data-con="new2">
                                         <div class="menu-sendCon-item"  id="div_mediaimg">
                                             <a href="javascript:;">
                                                 <i></i>
                                                <span>从素材库中选择</span>
                                                
                                             </a>
                                         </div>
                                         <!--已上传展示状态-->
                                         <div class="menu-selected-box" id="show_mediaimg">
                                             <div class="menu-selected-img menu-inpFlie-img">
                                                 <a href="javascript:;">
                                                     <img id="img_show" src="https://mmbiz.qlogo.cn/mmbiz_png/J5T4c4ydnBFMUBwczAtUXusB8DyN3dj1VsUQD6dNiakSGwzU4LfB1DnDdsRDzcMvmlcMfbYZj9reyFTCcnf1nXw/0?wx_fmt=png/640" alt="">
                                                 </a>
                                             </div>
                                             <a href="javascript:;" class="menu-selected-idel" id="del_imgItem">删除</a>
                                         </div>
                                    </div>
                                    <p class="sendTips">请设置当前菜单内容</p>
                                </div>
                                <!--跳转网页-->
                                <div class="menu-urlPart js-tabCon" data-con="url">
                                    <p class="menu-urlTips">订阅者点击该子菜单会跳到以下链接</p>
                                    <div class="menu-urlAdd clearfix">
                                        <label>页面地址</label>
                                        <div class="menu-urlAdd-box">
                                            <p class="menu-urlAdd-inp"><input id="edit_url" type="text"  ></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                   </din>
                    <!--菜单排序-->
                    <div class="menu-rank" style="text-align: center;"><span style="line-height: 30">请通过拖拽左边的菜单进行排序</span></div>
                    <span class="box-arrow">
                        <i class="arrow-s arrow-outter"></i>
                        <i class="arrow-s arrow-inner"></i>
                    </span>
               </div>
           </div>
       </div>
       <div class="menu-tool-box js-tool">
           <a href="javascript:;" class="sub-btn rank-btn menusort" id="sort">菜单排序</a>
           <a href="javascript:;" class="sub-btn rank-btn menusort" id="sortover" style="display: none">完成</a>
           <a href="javascript:;" class="main-btn" id="btn_submit">保存并发布</a>
           <%--<a href="javascript:;" class="sub-btn">预览</a>--%>
       </div>
    </div>
    <!--删除菜单提示-->
    <div class="wrap-pop del-pop" id="box_delalert">   
         <div class="title-pop">
             <h3>温馨提示</h3>
             <a href="javascript:;" class="close_alert">关闭</a>
         </div>
         <div class="box-pop">
             <div class="msg-pop">
                 <h4>删除确认</h4>
                 <p>删除后“<span id="alerttxt"></span>”菜单下设置的内容将被删除</p>
                 <i></i>
             </div>
         </div>
         <div class="btn-pop">
             <a href="javascript:;" id="btn_delsure" class="main-btn">确定</a>
             <a href="javascript:;" class="sub-btn close_alert">取消</a>
         </div>
    </div>
    <!--选择图文弹层  -->
    <div class="wrap-pop material-pop" id="panel_news">  
         <div class="title-pop">
             <h3>选择素材</h3>
             <a href="javascript:;" class="close_alert">关闭</a>
         </div>
         <div class="box-pop">
             <div class="topBar-pop">
                 <%--<label>
                     <input type="text" placeholder="标题">
                     <a href="javascript:;"><i>搜索</i></a>
                 </label>--%>
                 <a href="javascript:;" class="inStep-btn main-btn btn_sync">同步素材库</a>
             </div>
             <div class="list-pop">
                 <div class="list-pop-box" id="box_news">
                 </div>
             </div>
         </div>
         <div class="btn-pop">
             <a href="javascript:;" class="main-btn" id="btn_selectNews">确定</a>
             <a href="javascript:;" class="sub-btn close_alert">取消</a>
         </div>
    </div>
     <!--选择图片-->
    <div class="wrap-pop select-img-pop" id="panel_img">
        <div class="title-pop">
            <h3>选择图片</h3>
            <a href="javascript:;" class="close_alert">关闭</a>
        </div>
        <div class="box-pop" >
            <div class="tipBar-pop">
                <%--<span>大小不超过5M，已关闭图片水印<i>&emsp;&ensp;</i></span>--%>
               <%-- <a href="javascript:;" class="main-btn">本地上传</a>--%>
                 <a href="javascript:;" class="main-btn btn_sync">同步素材库</a>
            </div>
            <div class="list-pop">
                <div class="list-pop-box" id="box_img">
                    <div class="img-wrap">
                        <div class="img-box"><img src="http://image.bitautoimg.com/brandmarket/goods/image/20170609/6363262217006032465406236.jpg" alt=""></div>
                        <div class="img-mask"><i></i></div>
                        <p>图片名称</p>
                    </div>
                </div>
            </div>
        </div>
        <div class="btn-pop">
            <!--未选择状态添加类名：unSelect  -->
            <a href="javascript:;" id="btn_selectImg" class="main-btn ">确定</a>
            <a href="javascript:;" class="sub-btn close_alert">取消</a>
        </div>
    </div>
    <div class="mask"></div>
</body>
<script src="../Scripts/Sortable.min.js"></script>
<script src="../Scripts/menu/template.js"></script>
<script type="text/javascript" src="../Scripts/menu/index.js"></script>
<script src="../Scripts/emoji-data.js"></script>
<script src="../Scripts/jquery.emoji.min.js" type="text/javascript"></script>
<script src="../Scripts/layer/layer.js"></script>
<script type="text/javascript">
    $(document).ready(function(){
        $("#saytext").emoji({
            button: ".emotion",
            showTab: false,
            animation: 'fade',
            icons: [{
                name: "QQ表情",
                path: "../img/face/",
                maxNum: 99,
                alias: emojidata,
                file: ".png"
            }]
        });
    });
</script>
</html>