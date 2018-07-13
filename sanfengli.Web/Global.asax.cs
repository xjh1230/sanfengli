using sanfengli.Common;
using Senparc.Weixin;
using Senparc.Weixin.Cache;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace sanfengli.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterWeixinThreads();    //激活微信缓存及队列线程
            RegisterSenparcWeixin();
        }


        /// <summary>
        /// 激活微信缓存
        /// </summary>
        private void RegisterWeixinThreads()
        {
            ThreadUtility.Register();
        }
        /// <summary>
        /// 注册所用微信公众号的账号信息
        /// </summary>
        private void RegisterSenparcWeixin()
        {
            //注册公众号
            AccessTokenContainer.Register(WebConfigurationManager.AppSettings["WeixinAppId"], WebConfigurationManager.AppSettings["WeixinAppSecret"]);
            Config.DefaultCacheNamespace = BaseClass.DefaultCacheNamespace;
        }
    }
}