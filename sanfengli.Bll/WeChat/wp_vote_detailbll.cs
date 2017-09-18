using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChat
{
    //public class wp_vote_detailbll : BaseBll<wp_vote_detail>
    //{
    //    public List<wp_vote_detail> GetList(int vote_id, out int count, int pageIndex = 1, int pageSize = 20)
    //    {
    //        List<wp_vote_detail> list = new List<wp_vote_detail>();
    //        StringBuilder sqlwhere = new StringBuilder();
    //        if (vote_id > 0)
    //        {
    //            sqlwhere.Append($" AND vote_id = '{vote_id}'");
    //        }
    //        try
    //        {
    //            using (var db = DbFactory.OpenDbConnection())
    //            {
    //                var ev = db.From<wp_vote_detail>()
    //              .OrderByDescending(x => x.count)
    //              .Limit((pageIndex - 1) * pageSize, pageSize);
    //                ev.WhereExpression = "where 0=0" + sqlwhere;
    //                string sqlcount = ev.ToCountStatement();
    //                count = db.Single<int>(sqlcount);
    //                list = db.Select<wp_vote_detail>(ev);
    //                return list;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            count = 0;
    //            return list;
    //        }
    //    }

    //    public bool UpdateDetailCount(int id)
    //    {
    //        string sql = $"update wp_vote_detail set count=count+1 where id={id};";
    //        return ExecuteSql(sql, null) > 0;
    //    }
    //}
}
