using Bitauto.Mall.Aop;
using sanfengli.Api.CustomMsgHandler;
using sanfengli.Api.Models;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace sanfengli.Api.Areas.HelpPage.Controllers
{
    [AllowAnonymous]
    public class WeChatController : Controller
    {
        public static readonly string Token = WebConfigurationManager.AppSettings["WeixinToken"];
        public static readonly string EncodingAesKey = WebConfigurationManager.AppSettings["WeixinEncodingAESKey"];
        public static readonly string AppId = WebConfigurationManager.AppSettings["WeixinAppId"];
        /// <summary>
        /// 微信后台验证地址（使用Get）
        /// </summary>
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(PostModel postModel, string echostr)
        {
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return Content(echostr); //返回随机字符串则表示验证通过
            }
            else
            {
                return Content("");
            }
        }
        /// <summary>
        /// 处理流程
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(PostModel postModel)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return new WeixinResult("参数错误！");
            }
            postModel.Token = Token;
            postModel.EncodingAESKey = EncodingAesKey;//根据自己后台的设置保持一致
            postModel.AppId = AppId;//根据自己后台的设置保持一致
            try
            {
                var messageHandler = new YchMpMsgHandler(Request.InputStream, postModel, 5)
                {
                    //是否开启消息去重（未开启消息上下文暂时无效）
                    OmitRepeatedMessage = true
                };
                messageHandler.Execute();//执行微信处理过程
                return new FixWeixinBugWeixinResult(messageHandler);
            }
            catch (Exception e)
            {
                LogHandler.Error("易车惠微信公众号消息处理接口异常：" + e.Message);
                return Content("");
            }

        }

        [HttpGet]
        [ActionName("test1")]
        public ActionResult Test()
        {
            string replyMode = "";
            var key = Bll.WeChatMp.MsgReplyBll.GetReplyInfo("你好", out replyMode);
            return Content(key + "test" + replyMode);
        }
    }
}