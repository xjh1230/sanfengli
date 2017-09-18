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
    public partial class votedetaile : PageBaseHome
    {


        public votedetaile()
        {
            this.IsNeedUserInfo = false;
        }

        public wp_shop_vote vote = new wp_shop_vote();
        public wp_shop_vote_option option = new wp_shop_vote_option();
        public int option_id = 0;
        public int vote_id = 0;
        public int uid = 0;
        public bool is_vote = false;
        public string url;
        protected void Page_Load(object sender, EventArgs e)
        {
            vote_id = RequestHelper.GetQueryInt("vote_id", 0);
            option_id = RequestHelper.GetQueryInt("option_id", 0);
            vote = new Bll.WeChat.wp_shop_votebll().GetItem(vote_id);
            option = new Bll.WeChat.wp_shop_vote_optionbll().GetItem(option_id);
            url = Request.Url.ToString();
            if (vote == null || option == null)
            {
                //Response.Redirect("votelist.aspx?vote_id=" + vote_id);
            }
            else
            {
                var tmp = new Bll.WeChat.wp_picturebll().GetItem((int)option.image);
                option.ImagePath = tmp == null ? "" : tmp.path;
                uid = new Bll.WeChat.wp_userbll().GetUserIdByOpenId(openId);
                option.IsVoteCurrent = new Bll.WeChat.wp_shop_vote_logbll().GetVoteCountTodayByOptionId(uid, option_id) > 0;
                var list = new Bll.WeChat.wp_shop_vote_logbll().GetVoteLogTodayByVoteId(uid, vote_id);
                option.IsVote = false;
                if (list != null && list.Count > 0)
                {
                    option.IsVote = list.Count >= vote.multi_num;
                }
            }
        }
    }
}