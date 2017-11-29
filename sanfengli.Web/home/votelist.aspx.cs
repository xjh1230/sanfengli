using Common;
using sanfengli.Common;
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
    public partial class votelist : PageBaseHome
    {
        public List<wp_shop_vote_option> list;
        public List<wp_shop_vote_log> list_log;
        public wp_user user;
        public wp_shop_vote vote;
        public bool is_validity;
        public int vote_id = 0;
        public string url;

        public votelist()
        {
            this.IsNeedUserInfo = true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            vote_id = RequestHelper.GetQueryInt("vote_id", 0);
            //vote_id = 4;
            int count = RequestHelper.GetQueryInt("count", 20);
            is_validity = false;
            vote = new Bll.WeChat.wp_shop_votebll().GetItem(vote_id);
            list = new Bll.WeChat.wp_shop_vote_optionbll().GetOptionListByVoteId(vote_id, count);
            url = Request.Url.ToString();
            if (vote != null)
            {
                var now = BaseClass.ConvertDataTimeToLong(DateTime.Now);
                is_validity = vote.start_time < now && vote.end_time > now;
            }
            if (!string.IsNullOrEmpty(openId))
            {
                user = new Bll.WeChat.wp_userbll().GetUserInfoByOpenId(openId);
                if (user != null)
                {
                    list_log = new Bll.WeChat.wp_shop_vote_logbll().GetVoteLogTodayByVoteId(user.Id, vote_id);
                }
            }

            if (list != null && list.Count > 0 && list_log != null && list_log.Count > 0)
            {
                list.ForEach(s =>
                {
                    s.IsVote = list_log.Count >= vote.multi_num;
                    //限制每个一天只能投一票
                    //s.IsVoteCurrent = list_log.Any(l => l.uid == user.Id && l.option_id == s.Id);
                    //不限制每个一天只能投一票
                    s.IsVoteCurrent = false;
                });
            }
            if (list != null && list.Count > 0)
            {
                list.ForEach(s =>
                {
                    var tmp = new Bll.WeChat.wp_picturebll().GetItem((int)s.image);
                    s.ImagePath = tmp == null ? "" : tmp.path;
                });
            }
        }
    }
}