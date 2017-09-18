using sanfengli.Model.Dto;
using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Legacy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChat
{
    public class wp_survey_answerbll : BaseBll<wp_survey_answer>
    {
        public List<full_wp_survey_answer> GetListBySurveyId(int survey_id, out int count, int pageIndex = 1, int pageSize = 20)
        {
            List<full_wp_survey_answer> list = new List<full_wp_survey_answer>();
            List<wp_survey_answer> tmp = new List<wp_survey_answer>();
            try
            {
                count = 0;
                string sqlCount = $"select count(*)  from (select uid from wp_survey_answer where survey_id={survey_id}  GROUP BY uid ) a";
                count = ScalarSql<int>(sqlCount, null);
                using (var db = DbFactory.OpenDbConnection())
                {
                    string sqlList = $"select min(nickname),min(headimgurl),min(oper_time),min(survey_id),uid from wp_survey_answer where survey_id={survey_id}   GROUP BY uid LIMIT {(pageIndex - 1) * pageSize},{pageSize} ;";

                    tmp = db.Select<wp_survey_answer>(sqlList);
                    if (tmp != null && tmp.Count > 0)
                    {
                        tmp.ForEach(s =>
                        {
                            full_wp_survey_answer model = new full_wp_survey_answer();
                            model.wp_survey_answer = s;
                            model.list = GetListBySurveyIdAndUsrtId((int)s.survey_id, (int)s.uid);
                            list.Add(model);
                        });

                    }
                }

            }
            catch (Exception ex)
            {
                count = 0;
                tmp = new List<wp_survey_answer>();
            }
            return list;
        }


        public List<wp_survey_answer_question> GetListBySurveyIdAndUsrtId(int survey_id, int userId)
        {
            List<wp_survey_answer_question> list = new List<wp_survey_answer_question>();
            using (var db = DbFactory.OpenDbConnection())
            {
                //var ev = db.From<wp_survey_answer>()
                //         .Where(s => s.survey_id == survey_id && s.uid == userId)
                //         .OrderBy(s => s.question_id)
                //         .Join<wp_survey_question>((a, b) => a.survey_id == b.survey_id)
                //         .Select(s=>s);
                string sql = $"select a.answer,q.extra,q.title,q.type from wp_survey_answer a join wp_survey_question q on a.survey_id = q.survey_id where a.survey_id ={survey_id} and a.uid ={userId} ";
                list = db.Select<wp_survey_answer_question>(sql);

                return list;
            }
        }
        public bool SaveList(List<wp_survey_answer> list)
        {
            bool result = false;
            if (list != null && list.Count > 0)
            {
                list.ForEach(s =>
                {
                    InsertItem(s);
                });
            }
            return result;
        }

        /// <summary>
        /// 获取用户的答案
        /// </summary>
        /// <param name="survey_id"></param>
        /// <returns></returns>
        public DataTable GetAnswerExport(int survey_id)
        {
            DataTable dt = new DataTable();

            try
            {
                MySqlHelper helper = new MySqlHelper(DbConn.WeiXin);
                string sql = $@"select 
		a.uid,a.openid,a.cTime,group_concat(a.answer  SEPARATOR '^') as answers,
		group_concat(a.question_id SEPARATOR '^') as question_ids,
        group_concat(q.extra SEPARATOR '^') as extras,
		group_concat(q.title  SEPARATOR '^')  as titles,
		group_concat(q.type SEPARATOR '^') as types,
		s.keyword as '问卷关键字', s.title as '问卷标题',u.nickname as '用户昵称'
from wp_survey_answer a
join wp_survey s on a.survey_id = s.id
join wp_survey_question q on a.question_id = q.id
join wp_user u on a.uid = u.uid
where a.survey_id={survey_id}
GROUP BY a.uid
ORDER BY q.sort";
                dt = helper.ExecuteDataTable(sql);
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        /// <summary>
        /// 获取问卷的问题ID
        /// </summary>
        /// <param name="survey_id"></param>
        /// <returns></returns>
        public List<int> GetQuestionIds(int survey_id)
        {
            List<int> list = new List<int>();
            string sql = $"select id from wp_survey_question where survey_id={survey_id} ORDER BY sort";
            using (var db = DbFactory.OpenDbConnection())
            {
                list = db.Select<int>(sql);

                return list;
            }
        }

        public List<string> GetAnswer(string str)
        {
            List<string> list = new List<string>();
            string reg = "\"([^\"]+)\"";
            var matches = Regex.Matches(str, reg);
            foreach (Match match in matches)
            {
                list.Add(match.Groups[1].Value);
            }
            return list;

        }
    }
}
