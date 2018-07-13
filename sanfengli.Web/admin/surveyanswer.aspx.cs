using Common;
using sanfengli.Common;
using sanfengli.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sanfengli.Web.admin
{
    public partial class surveyanswer : System.Web.UI.Page
    {
        public PageInfo pageInfo;
        public List<Model.WeiXin.wp_survey_answer> list;
        protected void Page_Load(object sender, EventArgs e)
        {
            int survey_id = RequestHelper.GetQueryInt("survey_id", 0);
            int pageIndex;
            int.TryParse(Request.QueryString["pageIndex"], out pageIndex);
            int pageSize;
            int.TryParse(Request.QueryString["pageSize"], out pageSize);
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = pageSize == 0 ? 10 : pageSize;
            int totalRecord;
            list = new Bll.WeChat.wp_survey_answerbll().GetListBySurveyId(survey_id, out totalRecord, pageIndex, pageSize);

            if (list != null && list.Count > 0)
            {
                list.ForEach(s =>
                {
                    var user = new Bll.WeChat.wp_userbll().GetUserInfoByOpenId(s.openid);
                    s.NickName = user == null ? "" : user.nickname;
                    s.mobile = user == null ? "" : user.mobile;
                    s.CreateOn = BaseClass.ConvertToDateTime((long)s.cTime);
                    s.Url = $"answerdetail.aspx?uid={s.uid}&surevyid={s.survey_id}";
                });
            }

            #region 分页信息
            pageInfo = new PageInfo();
            pageInfo.TotalCount = totalRecord;
            pageInfo.PageSize = pageSize;
            pageInfo.CurrentPage = pageIndex;
            pageInfo.InitPage();
            #endregion
        }
    }
}