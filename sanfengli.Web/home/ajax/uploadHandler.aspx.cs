using Common;
using sanfengli.Model.WeiXin;
using sanfengli.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Bitauto.Mall.Aop;
using Newtonsoft.Json;
using sanfengli.Bll.WeChatMp;

namespace sanfengli.Web.home.ajax
{
    public partial class uploadHandler : PageBaseHome
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                string src = HttpUtility.UrlDecode(RequestHelper.GetFormString("src"));
                string content = HttpUtility.UrlDecode(RequestHelper.GetFormString("content"));


                string openId = this.GetOpenId();

                int userId = 0;

                if (string.IsNullOrEmpty(src) && string.IsNullOrEmpty(content))
                {
                    response.IsSuccess = false;
                }
                else
                {
                    feedback model = new feedback();
                    model.Content = content;
                    model.CreateOn = DateTime.Now;
                    model.Image = src;
                    model.UserId = userId;

                    response.IsSuccess = new FeedBackBll().SaveItem(model);
                }

                response.Msg = response.IsSuccess ? "成功" : "失败";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Msg = "失败";
                LogHandler.Error(ex);
            }

            Response.Write(JsonConvert.SerializeObject(response));
        }
    }
}