using Bitauto.Mall.Aop;
using sanfengli.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sanfengli.Web.Base
{
    public class PageBase : System.Web.UI.Page
    {
        public string CssDomin = Common.BaseClass.CssDomin;


        #region 构造函数
        HttpContext hContext = HttpContext.Current;
        public PageBase()
        {

        }
        public PageBase(HttpContext context)
        {

            hContext = context;
        }
        #endregion
        public string GetSignName()
        {

            return "";
        }
        protected override void OnInit(EventArgs e)
        {
            try
            {
                if (1 == 1) {

                }
                else
                {
                    string cookie = BaseClass.Login_Cookie;
                    var tmp = HttpContext.Current.Request.Cookies[cookie];
                    string loginCookie = tmp == null ? "" : tmp.Value;
                    LogHandler.Info($"key:{tmp}");
                    if (string.IsNullOrEmpty(loginCookie) || loginCookie == "123")
                    {
                        LogHandler.Info($"value:{loginCookie}");

                        Response.Redirect("/index.php?s=/w0/Home/User/login.html");
                    }
                    LogHandler.Info("已登录");
                }
                
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
            }
            base.OnInit(e);
        }
    }
}