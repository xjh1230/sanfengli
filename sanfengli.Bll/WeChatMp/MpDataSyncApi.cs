using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Bitauto.Mall.Aop;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.HttpUtility;
using Newtonsoft.Json;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.Media;
using sanfengli.Common;
using sanfengli.Model.WeiXin;

namespace sanfengli.Bll.WeChatMp
{
    /// <summary>
    /// 公众号相关数据同步
    /// </summary>
    public class MpDataSyncApi
    {
        #region 微信接口
        private MpMenuJsonResult GetMenuData()
        {
            try
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/get_current_selfmenu_info?access_token={0}", AccessTokenContainer.TryGetAccessToken(BaseClass.AppId, BaseClass.Secret));
                var jsonString = RequestUtility.HttpGet(url, Encoding.UTF8);
                return JsonConvert.DeserializeObject<MpMenuJsonResult>(jsonString);
            }
            catch (Exception e)
            {
                return null;
            }

        }
        private MediaList_OthersResult GetMediaList(string accessToken, UploadMediaFileType type, int offset,
            int count, int timeOut = 10000)
        {
            string urlFormat = string.Format("https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={0}", accessToken.AsUrlData());
            var data = new
            {
                type = type.ToString(),
                offset = offset,
                count = count
            };
            using (MemoryStream memoryStream = new MemoryStream())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
                memoryStream.Write(bytes, 0, bytes.Length);
                memoryStream.Seek(0L, SeekOrigin.Begin);
                string returnText = RequestUtility.HttpPost(urlFormat, null, memoryStream, null, null, Encoding.UTF8, null, timeOut);
                return JsonConvert.DeserializeObject<MediaList_OthersResult>(returnText);
            }

        }
        private MediaList_NewsResult GetNewsMediaList(string accessToken, int offset,
            int count, int timeOut = 10000)
        {
            string urlFormat = string.Format("https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={0}", accessToken.AsUrlData());
            var data = new
            {
                type = "news",
                offset = offset,
                count = count
            };
            using (MemoryStream memoryStream = new MemoryStream())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
                memoryStream.Write(bytes, 0, bytes.Length);
                memoryStream.Seek(0L, SeekOrigin.Begin);
                string returnText = RequestUtility.HttpPost(urlFormat, null, memoryStream, null, null, Encoding.UTF8, null, timeOut);
                return JsonConvert.DeserializeObject<MediaList_NewsResult>(returnText);
            }

        }
        #endregion

        public bool SaveMenuData()
        {
            var data = GetMenuData();
            if (data == null)
            {
                return false;
            }
            var buttonList = data.selfmenu_info.button;
            MenuResult result = new MenuResult { menu = new YchButtonGroup() };
            foreach (var item in buttonList)
            {
                var btn = new Button() { name = item.name };
                if (item.sub_button?.list != null && item.sub_button?.list.Count > 0)
                {
                    btn.sub_button = new List<Button>();
                    foreach (var sItem in item.sub_button.list)
                    {
                        var sbtn = ChangeButton(sItem);
                        btn.sub_button.Add(sbtn);
                    }
                    btn.sub_button.Reverse();
                }
                else
                {
                    btn = ChangeButton(item);
                }
                result.menu.button.Add(btn);
            }
            string jonLog = JsonConvert.SerializeObject(result.menu, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });
            MpMenuLogBll.WriteLog(jonLog, "system");
            return true;
        }
        /// <summary>
        /// 将原公众号菜单按钮调整为自定义click类型
        /// </summary>
        /// <param name="item"></param>
        private Button ChangeButton(MpButtonJsonResult item)
        {
            //需要转换的按钮类型
            string btnType = item.type;
            List<string> typeList = new List<string>() { "img", "news", "text", "voice", "video" };
            if (typeList.Contains(btnType.ToLower()))
            {
                item.type = "click";
                item.key = Guid.NewGuid().ToString("N");
            }
            Button btn = new Button()
            {
                name = item.name,
                type = item.type,
                key = item.key,
                replyType = btnType
            };
            switch (btnType)
            {
                case "text":
                    btn.replyContent = EConvert.DConvertEmojiHtml(item.value).UrlEncode().Replace("+", "%20");
                    break;
                case "view":
                    btn.replyContent = item.url;
                    break;
                default:
                    btn.replyContent = item.value;
                    break;
            }
            if (btn.type == "click")
            {
                MpEventBll.SaveEventInfo(new mpeventreply()
                {
                    EventKey = item.key,
                    EventType = "click",
                    ReplyType = btnType,
                    ReplyContent = item.value
                });
            }
            return btn;
        }
        public bool SaveMsgReplyData()
        {
            var mpMsgData = AutoReplyApi.GetCurrentAutoreplyInfo(BaseClass.AppId);
            if (mpMsgData.errcode == 0)
            {
                //初始化关注回复
                if (mpMsgData.add_friend_autoreply_info != null)
                {
                    var sereply = mpMsgData.add_friend_autoreply_info;
                    if (sereply.type.ToString() == "text")
                    {
                        sereply.content = EConvert.DConvertEmojiHtml(sereply.content).UrlEncode().Replace("+", "%20");
                    }
                    MpEventBll.SaveEventInfo(new mpeventreply()
                    {
                        EventType = WxEventTypeEnum.subscribe.ToString(),
                        EventKey = "ych_subscribe",
                        ReplyType = sereply.type.ToString(),
                        ReplyContent = sereply.content,
                    });

                }
                //初始化自动消息回复
                if (mpMsgData.message_default_autoreply_info != null)
                {
                    var autoreply = mpMsgData.message_default_autoreply_info;
                    if (autoreply.type.ToString() == "text")
                    {
                        autoreply.content = EConvert.DConvertEmojiHtml(autoreply.content).UrlEncode().Replace("+", "%20");
                    }
                    MpEventBll.SaveEventInfo(new mpeventreply()
                    {
                        EventType = WxEventTypeEnum.automsg.ToString(),
                        EventKey = "ych_automsg",
                        ReplyType = autoreply.type.ToString(),
                        ReplyContent = autoreply.content
                    });
                }
                //初始化关键字回复
                if (mpMsgData.keyword_autoreply_info?.list != null && mpMsgData.keyword_autoreply_info.list.Count > 0)
                {
                    mpMsgData.keyword_autoreply_info.list.Reverse();
                    foreach (var ruleItem in mpMsgData.keyword_autoreply_info.list)
                    {
                        var dto = new MsgReplyResult
                        {
                            Msg =
                            {
                                RuleName = ruleItem.rule_name,
                                ReplyMode = ruleItem.reply_mode.ToString()
                            }
                        };
                        if (ruleItem.keyword_list_info != null && ruleItem.keyword_list_info.Count > 0)
                        {
                            foreach (var keyItem in ruleItem.keyword_list_info)
                            {
                                mpmsgreplykey key = new mpmsgreplykey
                                {
                                    KeyVal = keyItem.content,
                                    MatchMode = keyItem.match_mode.ToString()
                                };
                                dto.Keys.Add(key);
                            }
                        }
                        if (ruleItem.reply_list_info != null && ruleItem.reply_list_info.Count > 0)
                        {
                            foreach (var contentItem in ruleItem.reply_list_info)
                            {
                                if (contentItem.type.ToString() == "text")
                                {
                                    contentItem.content = EConvert.DConvertEmojiHtml(contentItem.content).UrlEncode().Replace("+", "%20");
                                }
                                mpmsgreplycontent content = new mpmsgreplycontent
                                {
                                    ReplyType = contentItem.type.ToString(),
                                    ReplyContent = contentItem.content
                                };
                                dto.Contents.Add(content);
                            }
                        }
                        MsgReplyBll.SaveMsgRule(dto);
                    }
                }
            }
            return true;
        }
        public bool SaveMmaterialData()
        {
            try
            {
                var count = MediaApi.GetMediaCount(BaseClass.AppId);
                var listDto = new List<mpmateriallib>();
                if (count.image_count > 0)
                {
                    var imglist = GetMediaList(AccessTokenContainer.TryGetAccessToken(BaseClass.AppId, BaseClass.Secret), UploadMediaFileType.image, 0, count.image_count);
                    foreach (var imgItem in imglist.item)
                    {
                        mpmateriallib dto = new mpmateriallib()
                        {
                            MType = UploadMediaFileType.image.ToString(),
                            MName = imgItem.name,
                            MediaId = imgItem.media_id,
                            MUrl = imgItem.url,
                            UpdateTime = EConvert.ConvertToDateTime(imgItem.update_time)
                        };
                        listDto.Add(dto);
                    }
                }
                if (count.news_count > 0)
                {
                    var newslist = GetNewsMediaList(AccessTokenContainer.TryGetAccessToken(BaseClass.AppId, BaseClass.Secret), 0, count.news_count);
                    foreach (var newsItem in newslist.item)
                    {
                        mpmateriallib dto = new mpmateriallib()
                        {
                            MType = UploadMediaFileType.news.ToString(),
                            MName = newsItem.content.news_item[0].title,
                            MediaId = newsItem.media_id,
                            NewsContent = JsonConvert.SerializeObject(newsItem.content),
                            UpdateTime = EConvert.ConvertToDateTime(newsItem.update_time)
                        };
                        listDto.Add(dto);
                    }
                }
                //MpMaterialLibDal.Instantiation.SyncMpData(EConvert.ListToDataTable(listDto));
                return new MpMaterialLibBll().SyncMpData(listDto);
                //return true;
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);


                LogHandler.Error("请检查微信同步素材接口使用次数");
                return false;
            }


        }
    }
    #region 获取自定义菜单配置接口Json转换对象
    public class MpMenuJsonResult
    {
        public int is_menu_open { get; set; }
        public MpMenuInfo selfmenu_info { get; set; }
    }

    public class MpMenuInfo
    {
        public List<MpButtonJsonResult> button { get; set; }
    }

    public class MpButtonJsonResult
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 菜单的类型，公众平台官网上能够设置的菜单类型有view（跳转网页）、text（返回文本，下同）
        /// 、img、photo、video、voice。使用API设置的则有8种，详见《自定义菜单创建接口》
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 对于不同的菜单类型，value的值意义不同。官网上设置的自定义菜单：Text:保存文字到value； 
        /// Img、voice：保存mediaID到value； Video：保存视频下载链接到value； 
        /// News：保存图文消息到news_info，同时保存mediaID到value； 
        /// View：保存链接到url。
        /// 使用API设置的自定义菜单： click、scancode_push、scancode_waitmsg、pic_sysphoto、
        /// pic_photo_or_album、	pic_weixin、location_select：保存值到key；view：保存链接到url
        /// </summary>
        public string value { get; set; }
        public string url { get; set; }
        public string key { get; set; }
        public MpNewsJsonResult news_info { get; set; }
        public MpSubButton sub_button { get; set; }
    }

    public class MpSubButton
    {
        public List<MpButtonJsonResult> list { get; set; }
    }

    public class MpNewsJsonResult
    {
        public List<MpNewsInfo> list { get; set; }
    }

    public class MpNewsInfo
    {
        /// <summary>
        /// 图文消息的标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string digest { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 是否显示封面，0为不显示，1为显示
        /// </summary>
        public int show_cover { get; set; }
        /// <summary>
        /// 封面图片的URL
        /// </summary>
        public string cover_url { get; set; }
        /// <summary>
        /// 正文的URL
        /// </summary>
        public string content_url { get; set; }
        /// <summary>
        /// 原文的URL，若置空则无查看原文入口
        /// </summary>
        public string source_url { get; set; }
    }
    #endregion


}
