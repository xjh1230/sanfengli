using Common;
using sanfengli.Common;
using sanfengli.Common.Enum;
using sanfengli.Model.Dto;
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
    public partial class infolist : System.Web.UI.Page
    {
        public int type;
        public string typename = "";
        public int pageIndex;
        public int pageSize;
        public bool IsData;
        public List<list_info> list;
        public List<wp_article_type_new> list_type;
        //public infolist()
        //{
        //    this.IsNeedUserInfo = false;
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            type = RequestHelper.GetQueryInt("type", 1);
            pageIndex = RequestHelper.GetQueryInt("pageIndex", 1);
            pageSize = RequestHelper.GetQueryInt("pageSize", 5000);
            pageSize = 5000;
            if (type > 4 || type < 1)
            {
                type = 1;
            }
            typename = Common.EnumHelper.GetEnumDesc((InfoTypeEnum)type);
            int count = 0;
            string url = "";
            switch ((InfoTypeEnum)type)
            {
                case InfoTypeEnum.文章列表:
                    int type_id = RequestHelper.GetQueryInt("article_type", 5);
                    count = 0;
                    wp_article_new query = new wp_article_new();
                    query.type_id = type_id;
                    var article_list = new Bll.WeChat.wp_article_newbll().GetList(query, out count, pageIndex, pageSize);
                    list = list_info.Instance.GetFromArticle(article_list);
                    url = $"{BaseClass.CurrentDomin}home/infodetail.aspx?id=";
                    list_type = new Bll.WeChat.wp_article_type_newbll().GetList();
                    if (list_type != null && list_type.Count > 0)
                    {
                        list_type.ForEach(s =>
                        {
                            s.url = $"{BaseClass.CurrentDomin}home/infolist.aspx?article_type={s.Id}&type={type}";
                        });
                    }
                    break;
                case InfoTypeEnum.投票列表:
                    var vote_list = new Bll.WeChat.wp_shop_votebll().GetList(new Model.WeiXin.wp_survey(), out count, pageIndex, pageSize);
                    list = list_info.Instance.GetFromShopVote(vote_list);
                    url = $"{BaseClass.CurrentDomin}home/votelist.aspx?vote_id=";
                    break;
                case InfoTypeEnum.活动列表:
                    var reserve_list = new Bll.WeChat.wp_reservebll().GetList(new Model.WeiXin.wp_reserve(), out count, pageIndex, pageSize);
                    list = list_info.Instance.GetFromReserve(reserve_list);
                    url = $"{BaseClass.CurrentDomin}index.php?s=/w16/Reserve/Wap/index/reserve_id/";
                    break;
                case InfoTypeEnum.问卷列表:
                    var survey_list = new Bll.WeChat.wp_surveybll().GetList(new Model.WeiXin.wp_survey(), out count, false, pageIndex, pageSize);
                    list = list_info.Instance.GetFromSurvey(survey_list);
                    url = $"{BaseClass.CurrentDomin}index.php?s=/w16/Survey/Wap/index/id/";
                    break;
                default:
                    break;
            }

            IsData = list != null && list.Count > 0;
            if (IsData)
            {
                list.ForEach(s =>
                {
                    s.url = url + s.id;
                });
            }
        }
    }
}