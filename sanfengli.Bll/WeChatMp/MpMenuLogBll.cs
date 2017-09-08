using System;
using Newtonsoft.Json;
using Bitauto.Mall.Aop;
using System.Data.SqlClient;
using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;

namespace sanfengli.Bll.WeChatMp
{
    public class MpMenuLogBll : BaseBll<mpmenulog>
    {
        public static bool WriteLog(string content, string op)
        {
            string sql = @"INSERT INTO MpMenuLog (Content,Operator) VALUES (@Content,@Operator)";
            //SqlParameter[] parameters = new SqlParameter[]
            //{
            //    new SqlParameter("@Content",content),
            //    new SqlParameter("@Operator",op)
            //};

            var parameters = new { Content = content, Operator = op };
            return ExecuteSql(sql, parameters) > 0;
        }
        public static MenuResult ReadLastLog()
        {
            try
            {

                using (var db = DbFactory.OpenDbConnection())
                {
                    string sql = @"SELECT   Content FROM MpMenuLog ORDER BY CreateOn DESC LIMIT 1";
                    var jsonLog = db.Scalar<string>(sql);
                    if (!string.IsNullOrEmpty(jsonLog))
                    {
                        var result = new MenuResult { menu = JsonConvert.DeserializeObject<YchButtonGroup>(jsonLog) };
                        foreach (var button in result.menu.button)
                        {
                            if (button.sub_button != null && button.sub_button.Count > 0)
                            {
                                button.sub_button.Reverse();
                            }
                        }
                        return result;
                    }
                    return null;
                }

            }
            catch (Exception e)
            {
                LogHandler.Error(e);
                return null;
            }

        }
    }
}
