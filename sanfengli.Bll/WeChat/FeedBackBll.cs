using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChat
{
    public class FeedBackBll : BaseBll<feedback>
    {
        public List<feedback> GetList(feedback query, int pageIndex, int pageSize, out int count)
        {
            List<feedback> list = new List<feedback>();
            StringBuilder sqlwhere = new StringBuilder();
            if (query.Id > 0)
            {
                sqlwhere.Append($" AND Id = {query.Content}");
            }
            if (!string.IsNullOrEmpty(query.Content))
            {
                sqlwhere.Append($" AND Content like '%{query.Content}%'");
            }
            if (!string.IsNullOrEmpty(query.name))
            {
                sqlwhere.Append($" AND name like '%{query.name}%'");
            }
            if (query.typeid>0)
            {
                sqlwhere.Append($" AND typeid ={query.typeid}");
            }
            if (query.UserId > 0)
            {
                sqlwhere.Append($" AND UserId ={query.UserId}");
            }

            try
            {
                using (var db = DbFactory.OpenDbConnection())
                {

                    var ev = db.From<feedback>()
                  .OrderByDescending(x => x.Id)
                  .Limit((pageIndex - 1) * pageSize, pageSize);
                    ev.WhereExpression = "where 0=0 " + sqlwhere;
                    string sqlcount = ev.ToCountStatement();
                    count = db.Single<int>(sqlcount);
                    list = db.Select<feedback>(ev);
                    return list;
                }
            }
            catch (Exception ex)
            {
                count = 0;
                return list;
            }
        }
        
        public bool UpdateRemarkById(int id, string remark)
        {
            string sql = $"update feedback set remark='{remark}' where id={id}";

            return ExecuteSql(sql) > 0;
        }

        public bool DeleteById(int id)
        {
            try
            {
                using (var db = DbFactory.OpenDbConnection())
                {
                    return db.Delete<feedback>(s => s.Id == id) > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
