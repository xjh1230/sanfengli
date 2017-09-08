<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgReply.aspx.cs" Inherits="sanfengli.Web.Wx.MsgReply" %>

<!DOCTYPE html>
<html>
<head lang="en">
    <meta charset="UTF-8">
    <meta name="referrer" content="never">
    <title>关键字自动回复</title>
    <link rel="stylesheet" href="../css/basic.css"/>
    <link rel="stylesheet" href="../css/emojis.css"/>
</head>
<body>
    <div class="wx-page">
        <div class="main_hd">
            <h2>关键字自动回复</h2>
        </div>
        <div class="main_bd">
            <div class="content_wrap">
                <div class="tab_navs clearfix">
                    <ul class="clearfix">
                        <li><a href="PassiveReply.aspx">被添加自动回复</a></li>
                        <li><a href="AutoReply.aspx">消息自动回复</a></li>
                        <li class="on"><a href="MsgReply.aspx">关键词自动回复</a></li>
                    </ul>
                    
                </div>
                <div class="btn_wrp">
                    <a href="javascript:void(0);" class="btn btn_add btn_primary" id="Js_rule_add">
                        <i class="icon icon-add-white"></i>添加规则</a>      
                </div>
                <ul id="Js_ruleList" class="keywords_rule_list">
                    
                </ul>
                <p class="empty_tips" style="display: none">暂无创建规则</p>
            </div>
        </div>
        <div style="display: none">
            <li class="keywords_rule_item open" id="Js_ruleItem_0">
                <div class="keywords_rule_hd no_extra dropdown_area Js_detail_switch dropdown_opened" data-id="0" data-reply="loaded">
                    <div class="info">
                        <em class="keywords_rule_num">新规则</em>
                        <strong class="keywords_rule_name"></strong>
                    </div>
                    <div class="opr">
                        &nbsp;
                                <a href="javascript:void(0);" class="icon_dropdown_switch"><i class="triangle-bottom"></i><i class="triangle-top"></i></a>
                    </div>
                </div>
                <div class="keywords_rule_bd keywords_rule_overview">
                    <div class="keywords_info keywords">
                        <strong class="keywords_info_title">关键词</strong>
                        <div class="keywords_info_detail">
                            <ul class="overview_keywords_list">
                            </ul>
                        </div>
                    </div>
                    <div class="keywords_info reply">
                        <strong class="keywords_info_title">回复</strong>
                        <div class="keywords_info_detail">
                            <p class="reply_info">
                                <em class="num total">0</em>条（<em data-type="1" class="num textnum">0</em>条文字，<em data-type="2" class="num imgnum">0</em>条图片，<em data-type="5" class="num newsnum">0</em>条图文）
                            </p>
                        </div>
                    </div>
                    <!--<div id="Js_replyAllOverview_0" class="dn">发送全部回复</div>-->
                </div>
                <div class="keywords_rule_bd keywords_rule_detail">
                    <div class="rule_name_area">
                        <div class="frm_control_group">
                            <label for="" class="frm_label">规则名</label>
                            <div class="frm_controls">
                                <span class="frm_input_box">
                                    <input type="text" class="frm_input" value=""></span>
                                <p class="frm_tips">规则名最多60个字</p>
                            </div>
                        </div>
                    </div>
                    <div class="keywords_tap keywords no_data">
                        <div class="keywords_tap_hd">
                            <div class="info">
                                <h4>关键字</h4>
                            </div>
                            <div class="opr">
                                <a href="javascript:;" data-id="0" class="Js_keyword_add">添加关键字</a>
                            </div>
                        </div>
                        <div class="keywords_tap_bd">
                            <ul class="keywords_list keybox" style="display: block;">
                            </ul>
                        </div>
                    </div>
                    <div class="keywords_tap reply no_data">
                        <div class="keywords_tap_hd">
                            <div class="info">
                                <h4>回复</h4>
                            </div>
                            <div class="opr">
                                        <label for="Js_replyAll_0" class="frm_checkbox_label">
                                            <i class="icon_checkbox"></i>
                                            <input id="Js_replyAll_0" type="checkbox" class="frm_checkbox Js_reply_all">
                                            回复全部</label>
                                    </div>
                        </div>
                        <div class="keywords_tap_bd">

                            <ul class="media_type_list">
                                <li class="tab_text" data-tooltip="文字"><a href="javascript:;" data-type="1" data-id="0" class="Js_reply_add">&nbsp;<i class="icon icon-txt"></i></a></li>
                                <li class="tab_img" data-tooltip="图片"><a href="javascript:;" data-type="2" data-id="0" class="Js_reply_add">&nbsp;<i class="icon icon-pic"></i></a></li>

                                <li class="tab_appmsg" data-tooltip="图文"><a href="javascript:;" data-type="5" data-id="0" class="Js_reply_add">&nbsp;<i class="icon icon-pic-txt"></i></a></li>
                            </ul>
                            <ul class="keywords_list contentbox"  style="display: block;">
                                
                            </ul>
                        </div>
                    </div>
                    <p class="mini_tips warn dn js_warn profile_link_msg_global keywords">请勿添加其他公众号的主页链接</p>
                </div>
                <div class="keywords_rule_ft">
                    <p class="media_stat info">
                        文字(<em data-type="1" class="num Js_reply_cnt">0</em>)、图片(<em data-type="2" class="num Js_reply_cnt">0</em>)、图文(<em data-type="5" class="num Js_reply_cnt">0</em>)
                    </p>
                    <div class="opr">
                        <a href="javascript:;" data-id="0" class="btn btn_primary Js_rule_save">保存</a>
                        <a href="javascript:;" data-id="0" class="btn btn_primary Js_rule_del">删除</a>
                    </div>
                </div>
            </li>

            <li data-index="0" class="keyItem" kIndex="" id="keyTempale" >
                <div class="desc">
                    <strong class="title Js_keyword_val"></strong>
                </div>
                <div class="opr">
                    <a href="javascript:;" class="keywords_mode_switch Js_keyword_mode" data-mode="contain">未全匹配</a>
                    <a href="javascript:;" class="icon14_common edit_gray Js_keyword_edit">编辑</a>
                    <a href="javascript:;" data-id="410251848" class="icon14_common del_gray Js_keyword_del">删除</a>
                </div>
            </li>
            <li data-index="0" class="replyItem" cIndex="" id="contentTempale" >
                <div class="desc">
                    <strong class="title replyTextContent"></strong>
                </div>
                <div class="opr">
                    <a href="javascript:;" class="icon14_common edit_gray edit_replyContent">编辑</a>
                    <a href="javascript:;" data-id="410251848" class="del_replyContent">删除</a>
                </div>
            </li>
        </div>
    </div>
    <div class="popover pos_right">
        <div class="popover_inner">
            <div class="popover_content jsPopOverContent">删除后，关注该公众号的用户将不再接收该回复，确定删除？</div>
            <div class="popover_bar"><a href="javascript:;" class="btn btn_primary jsPopoverBt">确定</a>&nbsp;<a href="javascript:;" class="btn btn_default jsPopoverBt jsCancel">取消</a></div>
        </div>
        <i class="popover_arrow popover_arrow_out"></i>
        <i class="popover_arrow popover_arrow_in"></i> 
    </div>
    <!--添加关键词弹窗-->
    <div class="pop-panel key-panel">
	    <div class="popTit">添加关键字</div>
        <div class="pop-con">
            <div class="emotion_editor">
                <div class="edit_area js_editorArea" style="-webkit-user-modify: read-write-plaintext-only;" contenteditable="true" id="saytext"></div>
                <div class="editor_toolbar clearfix">
                    <p class="editor_tip js_editorTip f-right">还可以输入<b>30</b>字</p>
                </div>
            </div>
        </div>
	    <div class="opt-order">
            <span class="btn" id="sub_key">确定</span>
            <span class="btn btn_default js-close" id="esc_editkey">取消</span>
        </div>
        <div class="js-close close-btn icon icon-close">关闭</div>
    </div>
    <!--添加关键词弹窗end-->

    <!--选择图片-->
    <div class="pop-panel pic-panel" id="div_img">
	    <div class="popTit">选择图片</div>
        <div class="pop-con clearfix">
            <div class="btn sync-btn">同步素材库</div>
            <div class="pic-msg select-item-wp clearfix" id="box_img">
                
            </div>
        </div>
	    <div class="opt-order">
            <span class="btn" id="btn_selectimg">确定</span>
            <span class="btn btn_default js-close">取消</span>
        </div>
        <div class="js-close close-btn icon icon-close">关闭</div>
    </div>
    <!--选择图片end-->

    <!--添加回复文字-->
    <div class="pop-panel txt-panel">
	    <div class="popTit">添加回复文字</div>
        <div class="pop-con">
           
            <div class="emotion_editor">
                <div class="edit_area js_editorArea" contenteditable="true" id="edittext"></div>
                <div class="editor_toolbar clearfix">
                    <a href="javascript:void(0);" class="icon icon-face emotion js_switch f-left" id="txt-emoji"></a>
                    <p class="editor_tip js_editorTip f-right">还可以输入<b>600</b>字</p>
                </div>
            </div>
        </div>
	    <div class="opt-order">
            <span class="btn" id="btn_replyText">确定</span>
            <span class="btn btn_default js-close" id="esc_editText">取消</span>
        </div>
        <div class="js-close close-btn icon icon-close">关闭</div>
    </div>
    <!--添加回复文字end-->

<!--添加图文-->
    <div class="pop-panel txt-pic-panel">
	    <div class="popTit">选择素材</div>
        <div class="pop-con">
             <div class="btn sync-btn">同步素材库</div>
            <%--<p class="myserch left">
                <input type="search" placeholder="标题\作者\摘要">
                <a href="javascript:void(0)" class="btn_ser icon icon-ser"></a>
            </p>--%>
            <div class="list-pop select-item-wp">
                <div class="list-pop-box" id="box_news">
                </div>
            </div>
        </div>
	    <div class="opt-order">
            <span class="btn" id="btn_selectnews">确定</span>
            <span class="btn btn_default js-close">取消</span>
        </div>
        <div class="js-close close-btn icon icon-close">关闭</div>
    </div>
    <!--添加图文end-->

</body>
<script src="../Scripts/jquery-1.10.2.min.js"></script>
 <script src="../Scripts/layer/layer.js"></script>
<script src="../Scripts/jquery.emoji.min.js" type="text/javascript"></script>
<script src="../Scripts/menu/template.js"></script>
<script src="../Scripts/msgReply/common.js"></script>
<script src="../Scripts/msgReply/msgreply.js"></script>

</html>
