using Common;
using sanfengli.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sanfengli.Web.home.ajax
{
    public partial class infoHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ResponseModel response = new ResponseModel();
            string op = RequestHelper.GetFormString("op");
            switch (op)
            {
                case "getInfoType":
                    var list = new Bll.WeChat.wp_article_type_newbll().GetList();
                    if (list != null && list.Count > 0)
                    {
                        list.ForEach(s =>
                        {
                            s.url = $"infolist.aspx?type=1&article_type={s.Id}";
                        });
                    }
                    response.IsSuccess = true;
                    response.Data = list;
                    break;
                default:
                    break;
            }
            Response.Write(JsonHelper.Serialize(response));
        }
    }
}