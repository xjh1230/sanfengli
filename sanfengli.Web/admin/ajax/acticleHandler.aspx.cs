using Common;
using sanfengli.Model.WeiXin;
using sanfengli.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sanfengli.Web.admin.ajax
{
    public partial class acticleHandler : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ResponseModel responseModel = new ResponseModel();
            string op = RequestHelper.GetQueryString("op");
            switch (op)
            {
                case "gettypes":
                    responseModel = GetActicleTypes();
                    break;
                case "getlist":
                    responseModel = GetActicleTypes();
                    break;
                default:
                    break;
            }
            Response.Write(JsonHelper.Serialize(responseModel));

        }


        private ResponseModel GetActicleList()
        {
            ResponseModel responseModel = new ResponseModel();
            int pageIndex = RequestHelper.GetFormInt("page", 1);
            int pageSize = RequestHelper.GetFormInt("size", 10);
            string conditions = RequestHelper.GetFormString("conditions");
            string keyWord = RequestHelper.GetFormString("conditions");
            int typeId = RequestHelper.GetFormInt("type", 0);
            int count = 0;
            wp_article_new query = new wp_article_new();
            var list = new Bll.WeChat.wp_article_newbll().GetList(query, out count, pageIndex, pageSize);
            if (list != null && list.Count > 0)
            {
                responseModel.IsSuccess = true;
                responseModel.Data = JsonHelper.Serialize(list);
                responseModel.Count = count;
            }
            return responseModel;
        }
        private ResponseModel GetActicleTypes()
        {
            ResponseModel responseModel = new ResponseModel();
            var list = new Bll.WeChat.wp_article_type_newbll().GetList();
            if (list != null && list.Count > 0)
            {
                responseModel.IsSuccess = true;
                responseModel.Data = JsonHelper.Serialize(list);
            }
            return responseModel;
        }
    }
}