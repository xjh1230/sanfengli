using System;
using System.Configuration;

namespace sanfengli.Common
{
    public class BaseClass
    {

        public static readonly string AppId = ConfigurationManager.AppSettings["WeixinAppId"];
        public static readonly string Secret = ConfigurationManager.AppSettings["WeixinAppSecret"];
        public static readonly string DefaultCacheNamespace = ConfigurationManager.AppSettings["DefaultCacheNamespace"];

        public static readonly string CssDomin = ConfigurationManager.AppSettings["CssDomin"];
        public static readonly string CurrentDomin = ConfigurationManager.AppSettings["CurrentDomin"];
        public static readonly string cfg_dir_upImg = ConfigurationManager.AppSettings["cfg_dir_upImg"];

        public static readonly bool IsNeedWeiXin = ConfigurationManager.AppSettings["IsNeedWeiXin"] == "1";

        public static readonly string Login_Cookie = ConfigurationManager.AppSettings["Cookie_Prefix"] + "ScanLoginKey";
        public static long ConvertDataTimeToLong(DateTime dt)
        {
            return (dt.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            //System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            //long timeStamp = (long)(DateTime.Now - startTime).TotalMilliseconds;
            //return timeStamp;
        }

        public static DateTime ConvertToDateTime(long timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            //return dtStart.AddMilliseconds(timeStamp);
            return dtStart.AddSeconds(timeStamp);
        }
    }
}
