using Bitauto.Mall.Aop;
using Common;
using sanfengli.Bll;
using sanfengli.Bll.WeChat;
using sanfengli.Common;
using sanfengli.Model.WeiXin;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sanfengli.Web.Base
{
    public class PageBaseHome : System.Web.UI.Page
    {

        public string openId = "";
        public long uId = 0;
        
        public wp_user currentUser;
        public bool IsNeedUserInfo { get; set; }

        static Dictionary<string, OAuthAccessTokenResult> OAuthCodeCollection = new Dictionary<string, OAuthAccessTokenResult>();
        static object OAuthCodeCollectionLock = new object();

        #region 构造函数
        HttpContext hContext = HttpContext.Current;
        public PageBaseHome()
        {

        }
        public PageBaseHome(HttpContext context)
        {
            hContext = context;
        }
        #endregion
        protected override void OnInit(EventArgs e)
        {
            //启用微信打开控制
            var openIdCookie = HttpContext.Current.Request.Cookies[BaseClass.OpenId_Cookie];
            var openId = openIdCookie == null ? "" : openIdCookie.Value;

            var uIdCookie = HttpContext.Current.Request.Cookies[BaseClass.Uid_Cookie];

            uId = uIdCookie == null ? 0: BitAuto.Utils.ConvertHelper.GetInteger(uIdCookie.Value);
            LogHandler.Info("uid:"+uId);
            //if (string.IsNullOrEmpty(openId))
            if (uId==0)
            {
                if (BaseClass.IsNeedWeiXin)
                {
                    string returnurl = "/";
                    returnurl = Request.Url?.ToString();
                    string code = RequestHelper.GetQueryString("code");
                    string state = RequestHelper.GetQueryString("state");
                    if (string.IsNullOrWhiteSpace(code))
                    {
                        OAuthScope scope;
                        if (IsNeedUserInfo)
                        {
                            scope = OAuthScope.snsapi_userinfo;
                        }
                        else
                        {
                            scope = OAuthScope.snsapi_base;
                        }
                        string oauthUrl = Weixin.OauthUrl(Request.Url?.ToString(), returnurl, scope);
                        LogHandler.Info("code为空,oauthUrl:" + oauthUrl);
                        Response.Redirect(oauthUrl);
                    }
                    else
                    {
                        OAuthAccessTokenResult oAuthAccessTokenResult = null;

                        try
                        {
                            //通过，用code换取access_token

                            var isSecondRequest = false;
                            lock (OAuthCodeCollectionLock)
                            {
                                isSecondRequest = OAuthCodeCollection.ContainsKey(code);
                            }

                            if (!isSecondRequest)
                            {
                                //第一次请求
                                LogHandler.Info($"第一次微信OAuth到达，code：{code}" );
                                lock (OAuthCodeCollectionLock)
                                {
                                    OAuthCodeCollection[code] = null;
                                }
                            }
                            else
                            {
                                //第二次请求
                                LogHandler.Info($"第二次微信OAuth到达，code：{code}");
                                lock (OAuthCodeCollectionLock)
                                {
                                    oAuthAccessTokenResult = OAuthCodeCollection[code];
                                }
                            }
                            try
                            {
                                oAuthAccessTokenResult = oAuthAccessTokenResult ?? OAuthApi.GetAccessToken(BaseClass.AppId, BaseClass.Secret, code);
                            }
                            catch (Exception ex)
                            {
                                LogHandler.Info($"微信网页授权api信息：{ex.Message}。请求Url：{Request.Url},api参数：url={returnurl},code={code},state={state}");
                            }

                            if (oAuthAccessTokenResult != null)
                            {
                                lock (OAuthCodeCollectionLock)
                                {
                                    OAuthCodeCollection[code] = oAuthAccessTokenResult;
                                }
                            }
                            //var oAuthAccessTokenResult = OAuthApi.GetAccessToken(BaseClass.AppId, BaseClass.Secret, code);
                            if (oAuthAccessTokenResult.errcode != 0)
                            {
                                Response.Write("您拒绝了授权");
                                LogHandler.Info($"您拒绝了授权,code:{code }");
                            }
                            LogHandler.Info(oAuthAccessTokenResult.access_token + oAuthAccessTokenResult.openid);
                            var oAuthUserInfo = OAuthApi.GetUserInfo(oAuthAccessTokenResult.access_token, oAuthAccessTokenResult.openid);
                            this.openId = oAuthAccessTokenResult.openid;
                            HttpCookie cookie = new HttpCookie(BaseClass.OpenId_Cookie);
                            cookie.Value = this.openId;
                            cookie.Expires = DateTime.Now.AddDays(1);
                            HttpContext.Current.Response.Cookies.Add(cookie);
                            var userModel = new wp_userbll().SaveUserInfo(oAuthUserInfo);
                            LogHandler.Info("oAuthUserInfo:" + JsonHelper.Serialize(oAuthUserInfo)+ "|uid:"+ userModel);
                            this.uId = userModel ;
                            HttpCookie cookieUid = new HttpCookie(BaseClass.Uid_Cookie);
                            cookieUid.Value = this.uId.ToString();
                            cookieUid.Expires = DateTime.Now.AddDays(1);
                            HttpContext.Current.Response.Cookies.Add(cookieUid);
                            string token = new LoginTokenID(userModel).ToString();
                            WebTools.WriteCookie(WebTools.ych_weixintoken, token, 1);
                            //Response.Redirect("test.aspx"); //Redirect(url);
                        }
                        catch (Exception ex)
                        {
                            LogHandler.Info($"微信网页授权api信息：{ex.Message}。请求Url：{Request.Url},api参数：url={returnurl},code={code},state={state}");
                            //Response.Write("授权失败");
                        }
                    }
                }
                else
                {
                    openId = "o_7F30X3iijkdt0zsNQrxuGpOL8U";//测试环境账号
                }
            }
           
            if (this.uId > 0)
            {
                
                this.currentUser = new Bll.WeChat.wp_userbll().GetUserInfoByUId(this.uId);
                this.openId = this.currentUser == null ? "" : currentUser.openid;
                LogHandler.Info("currentUser:"+JsonHelper.Serialize(currentUser)+"uid:"+this.uId.ToString());
            }
            if (currentUser==null &&!string.IsNullOrEmpty(this.openId))
            {
                this.currentUser = new Bll.WeChat.wp_userbll().GetUserInfoByOpenId(this.openId);
                this.openId = this.currentUser == null ? "" : currentUser.openid;
                LogHandler.Info("currentUser:" + JsonHelper.Serialize(currentUser) + "openId:" + this.openId.ToString());
            }
            base.OnInit(e);
        }

        public string GetOpenId()
        {
            return this.openId;
        }
        public wp_user GetUser()
        {
           
            return this.currentUser;
        }

    }
}