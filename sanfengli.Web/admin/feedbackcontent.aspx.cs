using Bitauto.Mall.Aop;
using Common;
using sanfengli.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sanfengli.Web.admin
{
    public partial class feedbackcontent : System.Web.UI.Page
    {
        public PageInfo pageInfo;
        public List<Model.WeiXin.feedback> list;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DefaultDataBind();
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
            }
        }
        private void DefaultDataBind()
        {
            int pageIndex;
            int.TryParse(Request.QueryString["pageIndex"], out pageIndex);
            int pageSize;
            int.TryParse(Request.QueryString["pageSize"], out pageSize);
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = pageSize == 0 ? 10 : pageSize;
            int totalRecord;

            Model.WeiXin.feedback model = new Model.WeiXin.feedback();
            model.Content = RequestHelper.GetQueryString("Content");
            model.name = RequestHelper.GetQueryString("name");
            list = new Bll.WeChat.FeedBackBll().GetList(model, pageIndex, pageSize, out totalRecord);
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