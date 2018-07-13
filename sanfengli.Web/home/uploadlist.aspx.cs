using Bitauto.Mall.Aop;
using sanfengli.Model.WeiXin;
using sanfengli.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sanfengli.Web.home
{
    public partial class uploadlist : PageBaseHome
    {

        public uploadlist()
        {
            this.IsNeedUserInfo = true;
        }

        public List<feedback> list = new List<Model.WeiXin.feedback>();
        public bool HasData = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            int totalCount = 0;
            LogHandler.Info(this.openId);

            if (this.currentUser != null)
            {
                LogHandler.Info(this.currentUser.Id.ToString());
                feedback query = new feedback();
                query.UserId = currentUser.Id;
                list = new Bll.WeChat.FeedBackBll().GetList(query, 1, 200, out totalCount);
                HasData = totalCount > 0;
            }
            else
            {
                LogHandler.Info("获取用户信息失败");
            }
        }
    }
}