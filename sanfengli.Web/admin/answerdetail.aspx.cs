using BitAuto.Utils;
using Common;
using sanfengli.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sanfengli.Web.admin
{
    public partial class answerdetail : System.Web.UI.Page
    {
        public List<SyrevyAnswer> list = new List<SyrevyAnswer>();
        protected void Page_Load(object sender, EventArgs e)
        {
            int uid = RequestHelper.GetQueryInt("uid", 0);
            int surevyid = RequestHelper.GetQueryInt("surevyid", 0);
            var dt = new Bll.WeChat.wp_survey_answerbll().GetAnswerBySurveyId(uid, surevyid);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SyrevyAnswer model = new SyrevyAnswer();
                    model.Querstion = dr["title"].ToString();
                    var answers = dr["extra"].ToString().Split(' ').ToList();
                    var answervalue = new List<string>();
                    answers.ForEach(s =>
                    {
                        if (!string.IsNullOrEmpty(s.Trim()))
                        {
                            answervalue.Add(s);
                        }
                    });

                    //获取答案的索引
                    var answerIndex = new Bll.WeChat.wp_survey_answerbll().GetAnswer(dr["answer"].ToString());
                    var type = dr["type"].ToString();
                    if (answerIndex != null && answerIndex.Count > 0)
                    {
                        switch (type)
                        {
                            case "radio":
                                model.AnswerValue = answervalue[ConvertHelper.GetInteger(answerIndex[0])];
                                break;
                            case "checkbox":
                                foreach (var item in answerIndex)
                                {
                                    model.AnswerValue += answervalue[ConvertHelper.GetInteger(item)] + "  ";
                                }
                                break;
                            default:
                                model.AnswerValue = answerIndex[0];
                                break;
                        }
                    }
                    else
                    {
                        model.AnswerValue = "";
                    }
                    list.Add(model);
                }
            }
        }
    }

    public class SyrevyAnswer
    {
        public string Querstion { get; set; }
        public string AnswerValue { get; set; }
    }
}