using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChat
{
    public class wp_reservebll : BaseBll<wp_reserve>
    {
        public List<wp_reserve> GetList(wp_reserve query, out int count,  int pageIndex = 1, int pageSize = 20)
        {
            count = 0;
            List<wp_reserve> list = new List<wp_reserve>();
            StringBuilder sqlwhere = new StringBuilder();
            if (query.Id > 0)
            {
                sqlwhere.Append($" AND Id = '{query.Id}'");
            }
            if (!string.IsNullOrEmpty(query.title))
            {
                sqlwhere.Append($" AND keyword like '%{query.title}%' ");
            }

            try
            {
                using (var db = DbFactory.OpenDbConnection())
                {
                    var ev = db.From<wp_reserve>()
                  .OrderByDescending(x => x.mTime)
                  .Limit((pageIndex - 1) * pageSize, pageSize);
                    ev.WhereExpression = "where 0=0" + sqlwhere;
                    string sqlcount = ev.ToCountStatement();
                    count = db.Single<int>(sqlcount);
                    list = db.Select<wp_reserve>(ev);
                }
            }
            catch (Exception ex)
            {
                count = 0;
                return list;
            }

            if (list != null && list.Count > 0)
            {
                list.ForEach(s =>
                {
                    s.Image = new Bll.WeChat.wp_picturebll().GetItem((int)s.cover).path;

                });
            }
            return list;
        }
    }
}
