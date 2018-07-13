using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChat
{
    public class wp_articlebll : BaseBll<wp_article>
    {
        public List<wp_article> GetList(wp_article query, out int count, int pageIndex = 1, int pageSize = 20)
        {
            List<wp_article> list = new List<wp_article>();
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
                    
                    var ev = db.From<wp_article>()
                  .OrderByDescending(x => x.Id)
                  .Limit((pageIndex - 1) * pageSize, pageSize);
                    ev.WhereExpression = "where is_delete=0 " + sqlwhere;
                    string sqlcount = ev.ToCountStatement();
                    count = db.Single<int>(sqlcount);
                    list = db.Select<wp_article>(ev);
                    return list;
                }
            }
            catch (Exception ex)
            {
                count = 0;
                return list;
            }
        }

        public bool SaveModel(wp_article model)
        {
            try
            {
                using (var db = DbFactory.OpenDbConnection())
                {
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
