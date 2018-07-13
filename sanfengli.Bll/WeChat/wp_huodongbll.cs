using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChat
{
    public class wp_huodongbll : BaseBll<wp_huodong>
    {
        public List<wp_huodong> GetList(wp_huodong query, out int count, int pageIndex = 1, int pageSize = 20)
        {
            List<wp_huodong> list = new List<wp_huodong>();
            StringBuilder sqlwhere = new StringBuilder();
            if (query.Id > 0)
            {
                sqlwhere.Append($" AND Id = '{query.Id}'");
            }
            if (!string.IsNullOrEmpty(query.name))
            {
                sqlwhere.Append($" AND name like '%{query.name}%' ");
            }

            try
            {
                using (var db = DbFactory.OpenDbConnection())
                {
                    var ev = db.From<wp_huodong>()
                  .OrderByDescending(x => x.oper_time)
                  .Limit((pageIndex - 1) * pageSize, pageSize);
                    ev.WhereExpression = "where is_delete=0" + sqlwhere;
                    string sqlcount = ev.ToCountStatement();
                    count = db.Single<int>(sqlcount);
                    list = db.Select<wp_huodong>(ev);
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
        /// 获取用户的参与记录
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<wp_huodong> GetListByUserId(int user_id)
        {
            List<wp_huodong> list = new List<wp_huodong>();
            try
            {
                using (var db = DbFactory.OpenDbConnection())
                {
                    var ev = db.From<wp_huodong>()
                  .OrderByDescending(x => x.oper_time)
                  .Where(s => s.is_delete == 0)
                  .Join<wp_huodong_record>()
                  .Where<wp_huodong_record>(s => s.user_id == user_id);
                    list = db.Select<wp_huodong>(ev);
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
