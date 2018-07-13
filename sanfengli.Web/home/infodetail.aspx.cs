using Common;
using sanfengli.Model.WeiXin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sanfengli.Web.home
{
    public partial class infodetail : System.Web.UI.Page
    {
        public wp_article_new model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestHelper.GetQueryInt("id", 1);
            model = new Bll.WeChat.wp_article_newbll().GetItem(id);
        }
    }
}