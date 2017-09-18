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
                                .Where(s => s.MType == type);
                    return db.Select(query);
                }
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
                return null;
            }
        }

        public bool SyncMpData(List<mpmateriallib> listDto)
        {
            bool result = false;
            try
            {
                if (listDto != null && listDto.Count > 0)
                {

                    listDto.ForEach(s =>
                    {
                        InsertItem(s);
                    });
                    result = true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return result;
        }
    }
}
