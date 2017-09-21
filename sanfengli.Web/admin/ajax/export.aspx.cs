using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BitAuto.Utils;
using sanfengli.Web.Base;

namespace sanfengli.Web.admin.ajax
{
    public partial class export :PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = RequestHelper.GetQueryString("type");
            int id = RequestHelper.GetQueryInt("id", 0);

            DataTable dt = new DataTable();
            MemoryStream ms;
            switch (type)
            {
                case "survey":
                    ms = GetSurvey(id);
                    break;
                default:
                    ms = null;
                    break;
            }

            int random6Jiance = new Random(DateTime.Now.Millisecond).Next(1, 1000000);
            string fileName = DateTime.Now.ToString("yyyyMMdd") + random6Jiance + ".xlsx";
            if (Request.Browser.Browser == "IE")
            {
                fileName = HttpUtility.UrlEncode(fileName);
            }
            Response.AddHeader("Content-Disposition", "attachment;fileName=" + fileName);
            if (ms != null)
            {
                Response.AddHeader("Content-Length", ms.ToArray().Length.ToString());
                Response.BinaryWrite(ms.ToArray());
            }
            else
            {
                Response.AddHeader("Content-Length", "0");
                Response.BinaryWrite(new byte[] { });
            }

        }

        private MemoryStream GetSurvey(int id)
        {

            DataTable dtAnswer = new Bll.WeChat.wp_survey_answerbll().GetAnswerExport(id);
            IList<int> listQuestionId = new Bll.WeChat.wp_survey_answerbll().GetQuestionIds(id);
            if (dtAnswer != null && listQuestionId != null && listQuestionId.Count > 0)
            {
                List<string> ColumnName = new List<string>();
                List<string> questionList = new List<string>();
                for (int i = 0; i < listQuestionId.Count; i++)
                {
                    ColumnName.Add($"第{i + 1}题");
                    ColumnName.Add($"第{i + 1}题答案");
                }

                foreach (var item in ColumnName)
                {
                    dtAnswer.Columns.Add(item, typeof(string));
                }
                dtAnswer.Columns.Add("答题时间", typeof(string));
                //for (int i = 0; i < dtAnswer.Columns.Count; i++)
                //{
                //    dt.Columns.Remove
                //}

                foreach (DataRow dr in dtAnswer.Rows)
                {
                    var questionIds = dr["question_ids"].ToString().Split('^').ToList();
                    var answers = dr["answers"].ToString().Split('^').ToList();
                    var titles = dr["titles"].ToString().Split('^').ToList();
                    var types = dr["types"].ToString().Split('^').ToList();
                    var extras = dr["extras"].ToString().Split('^').ToList();
                    dr["答题时间"] = Common.BaseClass.ConvertToDateTime(ConvertHelper.GetLong(dr["cTime"])).ToString("yyyy-MM-dd HH:mm:ss");
                    for (int i = 0; i < listQuestionId.Count; i++)
                    {
                        var index = questionIds.IndexOf(listQuestionId[i].ToString());
                        var answer = "";
                        var quesrtion = "";
                        try
                        {
                            #region 获取问题和答案
                            var answertmp = new Bll.WeChat.wp_survey_answerbll().GetAnswer(answers[index]);
                            if (answertmp != null && answertmp.Count > 0)
                            {
                                var type = types[index];
                                var answervaluetmp = extras[i].ToString().Split(' ').ToList();
                                var answervalue = new List<string>();
                                answervaluetmp.ForEach(s =>
                                {
                                    if (!string.IsNullOrEmpty(s.Trim()))
                                    {
                                        answervalue.Add(s);
                                    }
                                });
                                switch (type)
                                {

                                    case "radio":
                                        answer = answervalue[ConvertHelper.GetInteger(answertmp[0])];
                                        break;
                                    case "checkbox":
                                        List<string> list = new List<string>();
                                        answertmp.ForEach(s =>
                                        {
                                            var tmp = ConvertHelper.GetInteger(s);
                                            list.Add(answervalue[tmp]);
                                        });
                                        answer = string.Join(",", list.ToArray());
                                        break;
                                    default:
                                        answer = string.Join(",", answertmp.ToArray());
                                        break;

                                }
                            }
                            quesrtion = titles[index]; 
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            answer = "";
                            quesrtion = "";
                        }
                        //问题
                        dr[ColumnName[i * 2]] = quesrtion;
                        //答案
                        dr[ColumnName[i * 2 + 1]] = answer;
                    }
                }
                dtAnswer.Columns.Remove("answers");
                dtAnswer.Columns.Remove("question_ids");
                dtAnswer.Columns.Remove("titles");
                dtAnswer.Columns.Remove("types");
                dtAnswer.Columns.Remove("cTime");
                dtAnswer.Columns.Remove("extras");
                dtAnswer.Columns.Remove("uid");
                
                MemoryStream ms = Bll.ExportExcelNew.ExportDataTableToExcelWithTitle(dtAnswer, "问卷调查");

                return ms;
            }
            else
            {
                return null;
            }
        }
    }
}