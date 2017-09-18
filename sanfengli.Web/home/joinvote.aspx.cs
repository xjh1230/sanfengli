using Common;
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
    public partial class joinvote : PageBaseHome
    {

        //System.Web.UI.Page
        //public string openId = "o_7F30X3iijkdt0zsNQrxuGpOL8U";
        public wp_shop_vote model;
        //PageBaseHome
        public joinvote()
        {
            this.IsNeedUserInfo = true;
        }
        public int voteId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            voteId = RequestHelper.GetQueryInt("voteId", 0);

            if (voteId > 0)
            {
                model = new Bll.WeChat.wp_shop_votebll().GetItem(voteId);
                int uid = new Bll.WeChat.wp_userbll().GetUserIdByOpenId(openId);
                var option = new Bll.WeChat.wp_shop_vote_optionbll().GetOptionByUid(voteId, uid);
                if (option != null)
                {
                    Response.Redirect($"/index.php?s=/w16/Vote/Wap/option_detail.html&option_id={option.Id}&vote_id={option.vote_id}");
                }
            }
        }
    }
}