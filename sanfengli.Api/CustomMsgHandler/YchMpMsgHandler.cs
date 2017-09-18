using Bitauto.Mall.Aop;
using Newtonsoft.Json;
using sanfengli.Bll.WeChat;
using sanfengli.Bll.WeChatMp;
using sanfengli.Common;
using sanfengli.Model.WeiXin;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace sanfengli.Api.CustomMsgHandler
{
    public class YchMpMsgHandler : MessageHandler<YchMsgContext>
    {

        public YchMpMsgHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {
        }
        public YchMpMsgHandler(Senparc.Weixin.MP.Entities.RequestMessageBase requestMessage)
            : base(requestMessage)
        {

        }
        /// <summary>
        /// 处理文字请求
        /// </summary>
        /// <returns></returns>
        public override Senparc.Weixin.MP.Entities.IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            Task.Run(() =>
            {
                try
                {
                    string openId = requestMessage.FromUserName;
                    string replyMode;
                    LogHandler.Info(JsonConvert.SerializeObject(requestMessage));
                    var contentList = MsgReplyBll.GetReplyInfo(requestMessage.Content, out replyMode);
                    LogHandler.Info(JsonConvert.SerializeObject(contentList));
                    LogHandler.Info("replyMode" + replyMode);
                    if (contentList != null && contentList.Count > 0)
                    {
                        if (replyMode == AutoReplyMode.random_one.ToString())
                        {
                            contentList = new List<mpmsgreplycontent>() { contentList[0] };
                        }
                        foreach (var replyContent in contentList)
                        {
                            switch (replyContent.ReplyType)
                            {
                                //类型枚举参照 Senparc.Weixin.MP.AutoReplyType 枚举
                                case "text":
                                    CustomApi.SendText(BaseClass.AppId, openId, EConvert.ConvertEmojiHtml(Senparc.Weixin.HttpUtility.RequestUtility.UrlDecode(replyContent.ReplyContent)));
                                    break;
                                case "img":
                                    CustomApi.SendImage(BaseClass.AppId, openId, replyContent.ReplyContent);
                                    break;
                                case "news":
                                    CustomApi.SendMpNews(BaseClass.AppId, openId, replyContent.ReplyContent);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        var replyItem = MpEventBll.GetByKey("ych_automsg");
                        if (replyItem != null)
                        {
                            switch (replyItem.ReplyType)
                            {
                                case "text":
                                    CustomApi.SendText(BaseClass.AppId, openId, EConvert.ConvertEmojiHtml(Senparc.Weixin.HttpUtility.RequestUtility.UrlDecode(replyItem.ReplyContent)));
                                    break;
                                case "img":
                                    CustomApi.SendImage(BaseClass.AppId, openId, replyItem.ReplyContent);
                                    break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    LogHandler.Error($"错误位置：微信文字请求回复\n请求数据：{JsonConvert.SerializeObject(requestMessage)}\n错误信息：{e.StackTrace}");
                }

            });
            //将消息转发到客服
            var responseMessage = CreateResponseMessage<ResponseMessageTransfer_Customer_Service>();
            return responseMessage;
        }
        /// <summary>
        /// 处理图片请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override Senparc.Weixin.MP.Entities.IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        {
            Task.Run(() =>
            {
                try
                {
                    string openId = requestMessage.FromUserName;
                    var replyItem = MpEventBll.GetByKey("ych_automsg");
                    if (replyItem != null)
                    {
                        switch (replyItem.ReplyType)
                        {
                            case "text":
                                CustomApi.SendText(BaseClass.AppId, openId, EConvert.ConvertEmojiHtml(Senparc.Weixin.HttpUtility.RequestUtility.UrlDecode(replyItem.ReplyContent)));
                                break;
                            case "img":
                                CustomApi.SendImage(BaseClass.AppId, openId, replyItem.ReplyContent);
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    LogHandler.Error($"错误位置：微信图片消息回复\n请求数据：{JsonConvert.SerializeObject(requestMessage)}\n错误信息：{e.StackTrace}");
                }
            });
            //将消息转发到客服
            var responseMessage = CreateResponseMessage<ResponseMessageTransfer_Customer_Service>();
            return responseMessage;
        }
        /// <summary>
        /// 处理语音请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override Senparc.Weixin.MP.Entities.IResponseMessageBase OnVideoRequest(RequestMessageVideo requestMessage)
        {
            Task.Run(() =>
            {
                try
                {
                    string openId = requestMessage.FromUserName;
                    var replyItem = MpEventBll.GetByKey("ych_automsg");
                    if (replyItem != null)
                    {
                        switch (replyItem.ReplyType)
                        {
                            case "text":
                                CustomApi.SendText(BaseClass.AppId, openId, EConvert.ConvertEmojiHtml(Senparc.Weixin.HttpUtility.RequestUtility.UrlDecode(replyItem.ReplyContent)));
                                break;
                            case "img":
                                CustomApi.SendImage(BaseClass.AppId, openId, replyItem.ReplyContent);
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    LogHandler.Error($"错误位置：微信图片消息回复\n请求数据：{JsonConvert.SerializeObject(requestMessage)}\n错误信息：{e.StackTrace}");
                }
            });
            //将消息转发到客服
            var responseMessage = CreateResponseMessage<ResponseMessageTransfer_Customer_Service>();
            return responseMessage;
        }
        /// <summary>
        /// 处理视频请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override Senparc.Weixin.MP.Entities.IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        {
            Task.Run(() =>
            {
                try
                {
                    string openId = requestMessage.FromUserName;
                    var replyItem = MpEventBll.GetByKey("ych_automsg");
                    if (replyItem != null)
                    {
                        switch (replyItem.ReplyType)
                        {
                            case "text":
                                CustomApi.SendText(BaseClass.AppId, openId, EConvert.ConvertEmojiHtml(Senparc.Weixin.HttpUtility.RequestUtility.UrlDecode(replyItem.ReplyContent)));
                                break;
                            case "img":
                                CustomApi.SendImage(BaseClass.AppId, openId, replyItem.ReplyContent);
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    LogHandler.Error($"错误位置：微信图片消息回复\n请求数据：{JsonConvert.SerializeObject(requestMessage)}\n错误信息：{e.StackTrace}");
                }
            });
            //将消息转发到客服
            var responseMessage = CreateResponseMessage<ResponseMessageTransfer_Customer_Service>();
            return responseMessage;
        }
        /// <summary>
        /// 处理小视频请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnShortVideoRequest(RequestMessageShortVideo requestMessage)
        {
            Task.Run(() =>
            {
                try
                {
                    string openId = requestMessage.FromUserName;
                    var replyItem = MpEventBll.GetByKey("ych_automsg");
                    if (replyItem != null)
                    {
                        switch (replyItem.ReplyType)
                        {
                            case "text":
                                CustomApi.SendText(BaseClass.AppId, openId, EConvert.ConvertEmojiHtml(Senparc.Weixin.HttpUtility.RequestUtility.UrlDecode(replyItem.ReplyContent)));
                                break;
                            case "img":
                                CustomApi.SendImage(BaseClass.AppId, openId, replyItem.ReplyContent);
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    LogHandler.Error($"错误位置：微信图片消息回复\n请求数据：{JsonConvert.SerializeObject(requestMessage)}\n错误信息：{e.StackTrace}");
                }
            });
            //将消息转发到客服
            var responseMessage = CreateResponseMessage<ResponseMessageTransfer_Customer_Service>();
            return responseMessage;
        }

        /// <summary>
        /// 订阅（关注）事件
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            Task.Run(() =>
            {
                string openId = requestMessage.FromUserName;
                var replyItem = MpEventBll.GetByKey("ych_subscribe");
                if (replyItem != null)
                {
                    switch (replyItem.ReplyType)
                    {
                        case "text":
                            CustomApi.SendText(BaseClass.AppId, openId, EConvert.ConvertEmojiHtml(Senparc.Weixin.HttpUtility.RequestUtility.UrlDecode(replyItem.ReplyContent)));
                            break;
                        case "img":
                            CustomApi.SendImage(BaseClass.AppId, openId, replyItem.ReplyContent);
                            break;
                    }
                }
            });
            //将消息转发到客服
            var responseMessage = new SuccessResponseMessage();
            return responseMessage;
        }

        /// <summary>
        /// Click事件接收
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            Task.Run(() =>
            {
                try
                {
                    string key = requestMessage.EventKey;
                    string openId = requestMessage.FromUserName;
                    var replyItem = MpEventBll.GetByKey(key);
                    if (replyItem != null)
                    {
                        switch (replyItem.ReplyType)
                        {
                            case "text":
                                CustomApi.SendText(BaseClass.AppId, openId, replyItem.ReplyContent);
                                break;
                            case "img":
                                CustomApi.SendImage(BaseClass.AppId, openId, replyItem.ReplyContent);
                                break;
                            case "news":
                                CustomApi.SendMpNews(BaseClass.AppId, openId, replyItem.ReplyContent);
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    LogHandler.Error($"错误位置：微信CLIKC事件回复\n请求数据：{JsonConvert.SerializeObject(requestMessage)}\n错误信息：{e.StackTrace}");
                }

            });
            var responseMessage = new SuccessResponseMessage();
            return responseMessage;
        }

        public override void OnExecuting()
        {
            Task.Run(() =>
            {
                try
                {
                    string openId = RequestMessage.FromUserName;
                    var userInfo = UserApi.Info(BaseClass.AppId, openId);
                    if (userInfo.subscribe == 1)
                    {
                        var id = new wp_userbll().SaveUserInfo(null, userInfo);
                        LogHandler.Info($"user_id={id}");
                        new wp_userbll().SetSubscribe(openId, 1);
                        LogHandler.Info($"openId+1={openId}");
                    }
                    else
                    {
                        new wp_userbll().SetSubscribe(openId, 0);
                        LogHandler.Info($"openId+0={openId}");
                    }
                }
                catch (Exception e)
                {
                    LogHandler.Warn(e);
                }

            });

            base.OnExecuting();
        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            return new SuccessResponseMessage();
        }
    }
}