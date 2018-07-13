using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChat
{
    public class wp_survey_questionbll : BaseBll<wp_survey_question>
    {
        public bool SaveQuestion(List<wp_survey_question> list)
        {
            bool result = false;
            if (list != null && list.Count > 0)
            {
                list.ForEach(s =>
                {
                    if (s.Id > 0)
                    {
                        UpdateItem(s);
                    }
                    else
                    {
                        InsertItem(s);
                    }
                });
            }
            return result;
        }

        public List<wp_survey_question> GetListBySurveyId(int suevey_id)
        {
            List<wp_survey_question> list = new List<wp_survey_question>();
            StringBuilder sqlwhere = new StringBuilder();
            if (suevey_id > 0)
            {
                sqlwhere.Append($" AND suevey_id = '{suevey_id}'");
            }

            try
            {
                using (var db = DbFactory.OpenDbConnection())
                {
                    var ev = db.From<wp_survey_question>()
                  .OrderBy(x => x.sort);
                    ev.WhereExpression = "where 0=0" + sqlwhere;
                    string sqlcount = ev.ToCountStatement();
                    list = db.Select<wp_survey_question>(ev);
                    return list;
                }
            }
            catch (Exception ex)
            {
                return list;
            }

        }


    }
}
