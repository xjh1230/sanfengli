using sanfengli.Common;
using sanfengli.Model;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll
{
    public class Weixin : BaseClass
    {
        /// <summary>
        /// 获取显性授权url
        /// 回调后微信传入两个参数 code，state 带用户信息
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <param name="parm">接收参数为state</param>
        /// <param name="scope">应用授权作用域，snsapi_base （不弹出授权页面，直接跳转，只能获取用户openid），snsapi_userinfo （弹出授权页面，可通过openid拿到昵称、性别、所在地。并且，即使在未关注的情况下，只要用户授权，也能获取其信息）</param>
        /// <returns></returns>
        public static string OauthUrl(string redirectUrl, string parm, OAuthScope scope)
        {
            string url = OAuthApi.GetAuthorizeUrl(AppId, redirectUrl, parm, scope);
            return url;
        }

        public ShareParmModel GetSign(string url)
        {
            var timestamp = JSSDKHelper.GetTimestamp();
            //获取随机码
            var nonceStr = JSSDKHelper.GetNoncestr();
            string ticket = JsApiTicketContainer.TryGetJsApiTicket(AppId, Secret);
            //获取签名
            var signature = JSSDKHelper.GetSignature(ticket, nonceStr, timestamp, url);
            ShareParmModel shareModel = new ShareParmModel();
            shareModel.appid = AppId;
            shareModel.timestamp = timestamp;
            shareModel.nonceStr = nonceStr;
            shareModel.signature = signature;
            return shareModel;
        }
    }
}
