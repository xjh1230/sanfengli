using Common;
using sanfengli.Common;
using sanfengli.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sanfengli.Web.admin.forms
{
    public partial class list : PageBase
    {

        public int typeId { get; set; }
        public List<string> listTitle { get; set; }

        public string typeName { get; set; }

        public string reviewUrl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            typeId = RequestHelper.GetQueryInt("typeId", 1);
            if (typeId > 5 || typeId < 1)
            {
                typeId = 1;
            }
            InitData();
        }


        private void InitData()
        {
            this.typeName = EnumHelper.GetEnumDesc((PageTypeEnum)this.typeId);
            reviewUrl = "";
            listTitle = new List<string>();
            switch ((PageTypeEnum)this.typeId)
            {
                case PageTypeEnum.问卷调查:
                    listTitle.Add("问卷ID");
                    listTitle.Add("关键字");
                    listTitle.Add("标题");
                    listTitle.Add("发布时间");
                    listTitle.Add("操作");

                    reviewUrl += "survey.aspx?id=";
                    break;
                case PageTypeEnum.在线投票:
                    listTitle.Add("投票ID");
                    listTitle.Add("活动名称");
                    listTitle.Add("发布时间");
                    listTitle.Add("开始时间");
                    listTitle.Add("结束时间");
                    listTitle.Add("活动说明");
                    listTitle.Add("操作");
                    reviewUrl += "vote.aspx?id=";
                    break;
                case PageTypeEnum.活动管理:
                    listTitle.Add("活动ID");
                    listTitle.Add("活动名称");
                    listTitle.Add("发布时间");
                    listTitle.Add("开始时间");
                    listTitle.Add("结束时间");
                    listTitle.Add("活动说明");
                    listTitle.Add("操作");

                    reviewUrl += "activity.aspx?id=";
                    break;
                case PageTypeEnum.随手拍:
                    listTitle.Add("OpenId");
                    listTitle.Add("随手拍内容");
                    listTitle.Add("随手拍图片");
                    listTitle.Add("操作");

                    reviewUrl += "feedback.aspx?id=";
                    break;
                case PageTypeEnum.文章列表:
                    break;
                default:
                    break;
            }
        }
    }
}