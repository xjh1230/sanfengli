﻿using sanfengli.Model.WeiXin;
using sanfengli.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sanfengli.Web.home
{
    public partial class upload : PageBaseHome
    {

        public upload()
        {
            this.IsNeedUserInfo = true;
        }
        //System.Web.UI.Page
        //public string openId = "o_7F30X3iijkdt0zsNQrxuGpOL8U";
        //PageBaseHome
        public List<feedback_type> listType = new List<feedback_type>();
        protected void Page_Load(object sender, EventArgs e)
        {
            listType = new Bll.WeChat.feedback_typebll().GetList();
        }
    }
}