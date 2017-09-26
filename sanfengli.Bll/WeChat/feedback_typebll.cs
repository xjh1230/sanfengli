using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChat
{
    public class feedback_typebll : BaseBll<feedback_type>
    {
        public List<feedback_type> GetList()
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.Select<feedback_type>();
            }
        }

        public bool SaveMode(feedback_type model)
        {
            if (model.Id <= 0)
            {
                return InsertItem(model) > 0;
            }
            else
            {
                return UpdateItem(model);
            }
        }
    }
}
