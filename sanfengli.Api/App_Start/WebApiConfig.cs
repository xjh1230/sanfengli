using sanfengli.Api.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace sanfengli.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            //跨域配置
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",//api/
                defaults: new { id = RouteParameter.Optional }
            );
            config.Filters.Add(new CustomGlobalExceptionFilterAttribute());
            //设置json序列化时间类型
            JsonTimeSerializerSettings();

            var format = GlobalConfiguration.Configuration.Formatters;
            //清除默认xml
            format.XmlFormatter.SupportedMediaTypes.Clear();
        }
        private static void JsonTimeSerializerSettings()
        {
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
            json.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss.fff";
            json.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
        }
    }
}
