using sanfengli.Model.WeiXin;
using sanfengli.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sanfengli.Web.admin
{
    public partial class feedbackmgr : PageBase
    {

        public List<feedback_type> listType = new List<feedback_type>();
        protected void Page_Load(object sender, EventArgs e)
        {
            listType = new Bll.WeChat.feedback_typebll().GetList();
        }
    }
}