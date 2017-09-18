using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChat
{
    public class wp_article_newbll : BaseBll<wp_article_new>
    {
        public List<wp_article_new> GetList(wp_article_new query, out int count, int pageIndex = 1, int pageSize = 20)
        {
            List<wp_article_new> list = new List<wp_article_new>();
            StringBuilder sqlwhere = new StringBuilder();
            if (query.type_id > 0)
            {
                sqlwhere.Append($" AND type_id = '{query.type_id}'");
            }
            if (!string.IsNullOrEmpty(query.name))
            {
                sqlwhere.Append($" AND name like '%{query.name}%' ");
            }

            try
            {
                using (var db = DbFactory.OpenDbConnection())
                {

                    var ev = db.From<wp_article_new>()
                  .OrderByDescending(x => x.cTime)
                  .Limit((pageIndex - 1) * pageSize, pageSize);
                    ev.WhereExpression = "where 0=0 " + sqlwhere;
                    string sqlcount = ev.ToCountStatement();
                    count = db.Single<int>(sqlcount);
                    list = db.Select<wp_article_new>(ev);
                    return list;
                }
            }
            catch (Exception ex)
            {
                count = 0;
                return list;
            }
        }
        public bool SaveModel(wp_article_new model)
        {
            try
            {
                using (var db = DbFactory.OpenDbConnection())
                {
                    model.cTime = DateTime.Now;
                    if (model.Id <= 0)
                    {
                        return db.Insert(model) > 0;
                    }
                    else
                    {
                        return db.Update(model) > 0;
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
