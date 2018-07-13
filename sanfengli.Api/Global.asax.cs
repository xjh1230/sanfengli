using sanfengli.Common;
using Senparc.Weixin;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace sanfengli.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterWeixinThreads();    //激活微信缓存及队列线程
            RegisterSenparcWeixin();
            WeixinContextGlobal.UseWeixinContext = false;
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
            AccessTokenContainer.Register(BaseClass.AppId, BaseClass.Secret);
            JsApiTicketContainer.Register(BaseClass.AppId, BaseClass.Secret);
            Config.DefaultCacheNamespace = BaseClass.DefaultCacheNamespace;
        }
    }
}
