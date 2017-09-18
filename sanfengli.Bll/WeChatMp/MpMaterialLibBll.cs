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
                        SaveModel(s);
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

        public bool SaveModel(mpmateriallib model)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                var tmp = db.Select<mpmateriallib>(s => s.MediaId == model.MediaId).FirstOrDefault();
                if (tmp == null)
                {
                    return db.Insert(model) > 0;
                }
                else
                {
                    tmp.MName = model.MName;
                    tmp.MType = model.MType;
                    tmp.MUrl = model.MUrl;
                    tmp.NewsContent = model.NewsContent;
                    tmp.UpdateTime = DateTime.Now;
                    return db.Update(tmp) > 0;
                }
            }
        }
    }
}
