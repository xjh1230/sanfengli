using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChatMp
{
    public class FeedBackBll : BaseBll<feedback>
    {
        public List<feedback> GetList()
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.Select<feedback>().OrderByDescending(s => s.CreateOn).ToList();
            }
        }

    }
}
