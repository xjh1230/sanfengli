using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChat
{
    public class wp_huodong_recordbll : BaseBll<wp_huodong_record>
    {
        /// <summary>
        /// 获取活动的参与信息
        /// </summary>
        /// <param name="huodong_id"></param>
        /// <param name="user_id"></param>
        /// <param name="count"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<wp_huodong_record> GetList(int huodong_id, int user_id, out int count, int pageIndex = 1, int pageSize = 20)
        {
            List<wp_huodong_record> list = new List<wp_huodong_record>();
            StringBuilder sqlwhere = new StringBuilder();
            if (huodong_id > 0)
            {
                sqlwhere.Append($" AND huodong_id = '{huodong_id}'");
            }
            if (user_id > 0)
            {
                sqlwhere.Append($" AND user_id = {user_id} ");
            }

            try
            {
                using (var db = DbFactory.OpenDbConnection())
                {
                    var ev = db.From<wp_huodong_record>()
                  .OrderByDescending(x => x.oper_time)
                  .Limit((pageIndex - 1) * pageSize, pageSize);
                    ev.WhereExpression = "where 1=1" + sqlwhere;
                    string sqlcount = ev.ToCountStatement();
                    count = db.Single<int>(sqlcount);
                    list = db.Select<wp_huodong_record>(ev);
                    return list;
                }
            }
            catch (Exception ex)
            {
                count = 0;
                return list;
            }
        }
        /// <summary>
        /// 获取活动的参与人数
        /// </summary>
        /// <param name="huodong_id"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public int GetRecoreCount(int huodong_id, int user_id)
        {
            int count = 0;
            StringBuilder sqlwhere = new StringBuilder();
            if (huodong_id > 0)
            {
                sqlwhere.Append($" AND query = '{huodong_id}'");
            }
            if (user_id > 0)
            {
                sqlwhere.Append($" AND user_id = {user_id} ");
            }
            try
            {
                using (var db = DbFactory.OpenDbConnection())
                {
                    var ev = db.From<wp_huodong_record>()
                  .OrderByDescending(x => x.oper_time);
                    ev.WhereExpression = "where 1=1" + sqlwhere;
                    string sqlcount = ev.ToCountStatement();
                    count = db.Single<int>(sqlcount);
                }
            }
            catch (Exception ex)
            {
                count = 0;
            }
            return count;
        }

        public bool SaveModel(wp_huodong_record model)
        {
            bool result = false;
            try
            {
                using (var db = DbFactory.OpenDbConnection())
                {
                    if (model.Id > 0)
                    {
                        return db.Update(db) > 0;
                    }
                    else
                    {
                        return db.Insert(db) > 0;
                    }
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
