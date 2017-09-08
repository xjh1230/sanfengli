using System.Configuration;

namespace sanfengli.Common
{
    public class BaseClass
    {
       
        public static readonly string AppId = ConfigurationManager.AppSettings["WeixinAppId"];
        public static readonly string Secret = ConfigurationManager.AppSettings["WeixinAppSecret"];
        public static readonly string DefaultCacheNamespace = ConfigurationManager.AppSettings["DefaultCacheNamespace"];
    }
}
