using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll
{
    public class BaseBll<T> where T : class
    {
        public static readonly IDbConnectionFactory DbFactory = new OrmLiteConnectionFactory(DbConn.WeiXin, MySqlDialectProvider.Instance);
        public virtual T GetItem(int id)
        {
            using (var db = DbFactory.Open())
            {
                return db.SingleById<T>(id);
            }
        }

        public virtual bool SaveItem(T t)
        {
            using (var db = DbFactory.Open())
            {
                return db.Save(t);
            }
        }
        public virtual long InsertItem(T t)
        {
            using (var db = DbFactory.Open())
            {
                return db.Insert(t, true);
            }
        }
        public virtual bool UpdateItem(T t)
        {
            using (var db = DbFactory.Open())
            {
                return db.Update(t) > 0;
            }
        }

        public virtual bool DeleteItem(T t)
        {
            using (var db = DbFactory.Open())
            {
                return db.Delete(t) > 0;
            }
        }

        public static int ExecuteSql(string sql, object dbParams)
        {
            try
            {
                using (var db = DbFactory.OpenDbConnection())
                {
                    
                    return db.ExecuteSql(sql, dbParams);
                }
            }
            catch (Exception ex)
            {
                Bitauto.Mall.Aop.LogHandler.Error(ex);
                return -1;
            }
        }
        
    }
}
