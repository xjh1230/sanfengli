using sanfengli.Model.Dto;
using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChat
{
    public class wp_surveybll : BaseBll<wp_survey>
    {
        public List<full_wp_survey> GetList(wp_survey query, out int count, bool isGetQuestion = true, int pageIndex = 1, int pageSize = 20)
        {
            count = 0;
            List<full_wp_survey> list = new List<full_wp_survey>();
            List<wp_survey> tmp = new List<wp_survey>();
            StringBuilder sqlwhere = new StringBuilder();
            if (query.Id > 0)
            {
                sqlwhere.Append($" AND Id = '{query.Id}'");
            }
            if (!string.IsNullOrEmpty(query.keyword))
            {
                sqlwhere.Append($" AND keyword like '%{query.keyword}%' ");
            }

            try
            {
                using (var db = DbFactory.OpenDbConnection())
                {
                    var ev = db.From<wp_survey>()
                  .OrderByDescending(x => x.cTime)
                  .Limit((pageIndex - 1) * pageSize, pageSize);
                    ev.WhereExpression = "where 0=0" + sqlwhere;
                    string sqlcount = ev.ToCountStatement();
                    count = db.Single<int>(sqlcount);
                    tmp = db.Select<wp_survey>(ev);
                }
            }
            catch (Exception ex)
            {
                count = 0;
                return list;
            }

            if (tmp != null && tmp.Count > 0)
            {

                tmp.ForEach(s =>
                {
                    s.Image = new Bll.WeChat.wp_picturebll().GetItem((int)s.cover).path;
                    full_wp_survey model = new full_wp_survey();
                    model.wp_survey = s;
                    if (isGetQuestion)
                    {
                        //model.list_question = new Bll.WeChat.wp_survey_questionbll().GetListBySurveyId((int)s.Id);
                    }
                    list.Add(model);
                });
            }
            return list;
        }


        public full_wp_survey GetListExport(int survey_id)
        {
            full_wp_survey model = new full_wp_survey();
            wp_survey tmp = new wp_survey();
            StringBuilder sqlwhere = new StringBuilder();
            if (survey_id > 0)
            {
                sqlwhere.Append($" AND Id = '{survey_id}'");
            }
            try
            {
                using (var db = DbFactory.OpenDbConnection())
                {
                    var ev = db.From<wp_survey>()
                  .OrderByDescending(x => x.cTime);
                    ev.WhereExpression = "where 0=0" + sqlwhere;
                    string sqlcount = ev.ToCountStatement();
                    tmp = db.Select<wp_survey>(ev).FirstOrDefault();
                }
                if (tmp != null)
                {

                    model.wp_survey = tmp;
                    model.list_question = new Bll.WeChat.wp_survey_questionbll().GetListBySurveyId((int)tmp.Id);
                }

            }
            catch (Exception ex)
            {
                return null;
            }
            return model;
        }

        public bool SaveMode(full_wp_survey model)
        {
            bool result = false;
            try
            {
                if (model.wp_survey.Id > 0)
                {
                    var isOk = UpdateItem(model.wp_survey);
                    if (isOk)
                    {
                        if (model.list_question != null && model.list_question.Count > 0)
                        {
                            //result = new Bll.WeChat.wp_survey_questionbll().SaveQuestion(model.list_question);
                        }

                    }
                }
                else
                {
                    var id = InsertItem(model.wp_survey);
                    if (id > 0)
                    {
                        if (model.list_question != null && model.list_question.Count > 0)
                        {
                            //result = new Bll.WeChat.wp_survey_questionbll().SaveQuestion(model.list_question);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }



     
    }
}
