using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using Newtonsoft.Json;
using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Containers;
using sanfengli.Web.Handler;
using sanfengli.Common;
using sanfengli.Bll.WeChatMp;
using sanfengli.Model.WeiXin;
using Senparc.Weixin.Entities;

namespace sanfengli.Web.Wx.AjaxHandler
{
    /// <summary>
    /// MpHandler 的摘要说明
    /// </summary>
    public class MpHandler : ActionHandler
    {
        //创建菜单
        protected void PublishMenu()
        {
            if (Request["menu"] == null)
            {
                Json(new { state = false });
                return;
            }
            string publishJson = EConvert.ConvertEmojiHtml(HttpContext.Current.Server.UrlDecode(Request["menu"]));
            publishJson = publishJson.Replace("&amp;","&");
            var menu = JsonConvert.DeserializeObject<YchButtonGroup>(publishJson);
            MpEventBll.SaveAllButtonEvent(menu);
            var tmp = AccessTokenContainer.TryGetAccessToken(BaseClass.AppId, BaseClass.Secret);
            //tmp = "weiphp";
            var result = MenuWeChatApi.CreateMenu(tmp, publishJson);
            //var result = new WxJsonResult();
            if (result.errcode == 0)
            {
                //记录菜单保存记录
                MpMenuLogBll.WriteLog(Request["menu"], "cs");
                Json(new { state = true });
                return;
            }
            Json(new { state = false });
        }
        //获取菜单
        protected void GetMenu()
        {
            try
            {
                var lastLog = MpMenuLogBll.ReadLastLog();
                if (lastLog != null)
                {
                    Json(new { state = true, buttons = lastLog.menu });
                    return;
                }
                Json(new { state = false });
            }
            catch (Exception)
            {
                Json(new { state = false });
            }
        }
        //获取图文、图片永久素材列表
        protected void GetMediaList()
        {
            bool useSync = EConvert.ConvertTo<bool>(Request["mode"]);
            string type = Request["type"];
            try
            {
                if (useSync)
                {
                    MpDataSyncApi api = new MpDataSyncApi();
                    api.SaveMmaterialData();
                }
                List<mpmateriallib> newsList = null;
                List<mpmateriallib> imgList = null;
                int imgNum = EConvert.ConvertTo<int>(ConfigurationManager.AppSettings["WeChatImgDefaultCount"]);
                int newsNum = EConvert.ConvertTo<int>(ConfigurationManager.AppSettings["WeChatNewsDefaultCount"]);
                if (type == "img")
                {
                    imgList = MpMaterialLibBll.GetMaterialList(UploadMediaFileType.image.ToString(), imgNum);
                }
                else if (type == "news")
                {
                    newsList = MpMaterialLibBll.GetMaterialList(UploadMediaFileType.news.ToString(), newsNum);
                }
                else
                {
                    imgList = MpMaterialLibBll.GetMaterialList(UploadMediaFileType.image.ToString(), imgNum);
                    newsList = MpMaterialLibBll.GetMaterialList(UploadMediaFileType.news.ToString(), newsNum);
                }
                Json(new { state = true, newsList = newsList, imgList = imgList });
            }
            catch (Exception e)
            {
                Json(new { state = false });
            }

        }
    }
}