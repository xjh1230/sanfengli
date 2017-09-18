using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChat
{
    public class wp_article_type_newbll : BaseBll<wp_article_type_new>
    {
        public List<wp_article_type_new> GetList()
        {
            try
            {
                using (var db=DbFactory.OpenDbConnection())
                {
                    return db.Select<wp_article_type_new>().ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<wp_article_type_new>();
            }
        }

        public bool SaveModel(wp_article_type_new model)
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
