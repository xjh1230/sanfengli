using Bitauto.Mall.Aop;
using Common;
using sanfengli.Bll;
using sanfengli.Bll.WeChat;
using sanfengli.Common;
using sanfengli.Web.Base;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sanfengli.Web
{
    public partial class test : PageBase
    {
       
        HttpContext hContext = HttpContext.Current;
      
        public string openId = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}