using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sanfengli.Web.admin
{
    public partial class feedbacktype : System.Web.UI.Page
    {
        public List<Model.WeiXin.feedback_type> list = new List<Model.WeiXin.feedback_type>();
        protected void Page_Load(object sender, EventArgs e)
        {
            list = new Bll.WeChat.feedback_typebll().GetList();
        }
    }
}