using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChat
{
    public class wp_shop_votebll : BaseBll<wp_shop_vote>
    {
        public List<wp_shop_vote> GetList(wp_shop_vote query, out int count, int pageIndex = 1, int pageSize = 20)
        {
            count = 0;
            List<wp_shop_vote> list = new List<wp_shop_vote>();
            StringBuilder sqlwhere = new StringBuilder();
            if (query.Id > 0)
            {
                sqlwhere.Append($" AND Id = '{query.Id}'");
            }
            try
            {
                using (var db = DbFactory.OpenDbConnection())
                {
                    var now = Common.BaseClass.ConvertDataTimeToLong(DateTime.Now);
                    var ev = db.From<wp_shop_vote>()
                  .OrderByDescending(x => x.start_time)
                  .Limit((pageIndex - 1) * pageSize, pageSize);
                    ev.WhereExpression = $"where 0=0 and end_time>{now}  {sqlwhere}";
                    string sqlcount = ev.ToCountStatement();
                    count = db.Single<int>(sqlcount);
                    list = db.Select<wp_shop_vote>(ev);
                }
            }
            catch (Exception ex)
            {
                count = 0;
                return list;
            }
            return list;
        }

        public wp_shop_vote GetModel(int id)
        {
            wp_shop_vote model = new wp_shop_vote();
            using (var db = DbFactory.OpenDbConnection())
            {
                {
                    model = db.Select<wp_shop_vote>(s => s.Id == id).FirstOrDefault();
                }
            }
            return model;
        }
    }
}
