using Bitauto.Mall.Aop;
using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChatMp
{
    public class MpMaterialLibBll : BaseBll<mpmateriallib>
    {
        public static List<mpmateriallib> GetMaterialList(string type, int count)
        {
            try
            {
                using (var db = DbFactory.OpenDbConnection())
                {
                    var query = db.From<mpmateriallib>()
                                .OrderByDescending(e => e.UpdateTime)
                                .Take(count)
                                .Select(s => s.MType == type);
                    return db.Select(query);
                }
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
                return null;
            }
        }
    }
}
