using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChat
{
    public class wp_votebll : BaseBll<wp_vote>
    {
        //public List<wp_vote> GetList(wp_vote query, out int count, int pageIndex = 1, int pageSize = 20)
        //{
        //    List<wp_vote> list = new List<wp_vote>();
        //    StringBuilder sqlwhere = new StringBuilder();
        //    if (query.Id > 0)
        //    {
        //        sqlwhere.Append($" AND Id = '{query.Id}'");
        //    }
        //    if (!string.IsNullOrEmpty(query.name))
        //    {
        //        sqlwhere.Append($" AND name like '%{query.name}%' ");
        //    }

        //    try
        //    {
        //        using (var db = DbFactory.OpenDbConnection())
        //        {
        //            var ev = db.From<wp_vote>()
        //          .OrderByDescending(x => x.oper_time)
        //          .Limit((pageIndex - 1) * pageSize, pageSize);
        //            ev.WhereExpression = "where is_delete=0" + sqlwhere;
        //            string sqlcount = ev.ToCountStatement();
        //            count = db.Single<int>(sqlcount);
        //            list = db.Select<wp_vote>(ev);
        //            return list;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        count = 0;
        //        return list;
        //    }
        //}


    }
}
