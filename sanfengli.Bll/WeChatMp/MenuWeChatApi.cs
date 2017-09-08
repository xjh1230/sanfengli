using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP;

namespace sanfengli.Bll.WeChatMp
{
    public class MenuWeChatApi
    {
        
        public static MenuResult GetMenu(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", accessToken.AsUrlData());
                var jsonString = RequestUtility.HttpGet(url, Encoding.UTF8);
                MenuResult finalResult;
                finalResult = JsonConvert.DeserializeObject<MenuResult>(jsonString);
                if (finalResult.menu == null || finalResult.menu.button.Count == 0)
                {
                    return null;
                }

                return finalResult;

            }, accessTokenOrAppId);
        }

        public static WxJsonResult CreateMenu(string accessToken, string json)
        {
           var urlFormat = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}";
            urlFormat = string.Format(urlFormat, accessToken);
            using (MemoryStream ms = new MemoryStream())
            {
                var bytes = Encoding.UTF8.GetBytes(json);
                ms.Write(bytes, 0, bytes.Length);
                ms.Seek(0, SeekOrigin.Begin);
                return Post.PostGetJson<WxJsonResult>(urlFormat, null, ms, timeOut: 10000, checkValidationResult: false);
            }
            
        }
       
    }
    public class MenuResult: WxJsonResult
    {
        public YchButtonGroup menu { get; set; }
    }
    public class YchButtonGroup
    {
        /// <summary>
        /// 按钮数组，按钮个数应为1-3个
        /// </summary>
        public List<Button> button { get; set; }

        public YchButtonGroup()
        {
            button = new List<Button>();
        }
    }
    public class Button
    {
        public string name { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string key { get; set; }
        public List<Button> sub_button { get; set; }

        #region 非接口字段
        public string replyType { get; set; }
        public string replyContent { get; set; }

        #endregion
    }
}
