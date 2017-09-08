using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChatMp
{
    public class MpEventBll : BaseBll<mpeventreply>
    {
        public static bool SaveEventInfo(mpeventreply dto)
        {

            bool result = false;
//            string sql = @"MERGE INTO MpEventReply e
//USING
//    ( SELECT @EventType AS EventType,@EventKey AS EventKey,@ReplyType AS ReplyType,@ReplyContent AS ReplyContent ) t
//ON  t.EventKey = e.EventKey
//WHEN MATCHED THEN
//    UPDATE SET e.ReplyType = t.ReplyType ,
//               e.ReplyContent = t.ReplyContent,
//							 e.UpdateOn=GETDATE()
//WHEN NOT MATCHED THEN
//    INSERT(EventType,EventKey,ReplyType,ReplyContent) VALUES(t.EventType,t.EventKey,t.ReplyType,t.ReplyContent);";

            //SqlParameter[] parameters = new SqlParameter[]
            //{
            //    new SqlParameter("@EventType",dto.EventType),
            //    new SqlParameter("@EventKey",dto.EventKey),
            //    new SqlParameter("@ReplyType",dto.ReplyType),
            //    new SqlParameter("@ReplyContent",dto.ReplyContent),
            //};

            using (var db = DbFactory.OpenDbConnection())
            {
                var model = db.Select<mpeventreply>(s => s.EventKey == dto.EventKey).FirstOrDefault();
                //mpeventreply model = null;
                if (model == null)
                {
                    dto.CreateOn = DateTime.Now;
                    dto.UpdateOn = DateTime.Now;
                    return db.Insert<mpeventreply>(dto) > 0;
                }
                else
                {
                    string sql = "UPDATE MpEventReply SET EventType=@EventType,EventKey=@EventKey,ReplyType=@ReplyType,ReplyContent=@ReplyContent,UpdateOn=NOW() WHERE EventKey=@EventKey ";
                    var parameters = new { EventType = dto.EventType, EventKey = dto.EventKey, ReplyType = dto.ReplyType, ReplyContent = dto.ReplyContent };
                    return ExecuteSql(sql, parameters) > 0;
                }
            }
            return result;
        }

        public static void SaveAllButtonEvent(YchButtonGroup result)
        {
            foreach (var item in result.button)
            {
                if (item.sub_button != null && item.sub_button.Count > 0)
                {
                    foreach (var sItem in item.sub_button)
                    {
                        if (sItem.type == "click")
                        {
                            mpeventreply eventItem = new mpeventreply()
                            {
                                EventKey = sItem.key,
                                EventType = sItem.type,
                                ReplyType = sItem.replyType,
                                ReplyContent = sItem.replyContent
                            };
                            SaveOneEvent(eventItem);
                        }
                    }
                }
                else
                {
                    if (item.type == "click")
                    {
                        mpeventreply eventItem = new mpeventreply()
                        {
                            EventKey = item.key,
                            EventType = item.type,
                            ReplyType = item.replyType,
                            ReplyContent = item.replyContent
                        };
                        SaveOneEvent(eventItem);
                    }
                }

            }
        }

        public static mpeventreply GetByKey(string eventKey)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.Single<mpeventreply>(s => s.EventKey == eventKey);
            }
        }

        public static bool DeletEventByKey(string eventKey)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.Delete<mpeventreply>(s => s.EventKey == eventKey) > 0;
            }
        }

        public static bool SaveOneEvent(mpeventreply dto)
        {
            //            string sql = @"MERGE INTO MpEventReply e
            //USING
            //    ( SELECT @EventType AS EventType,@EventKey AS EventKey,@ReplyType AS ReplyType,@ReplyContent AS ReplyContent ) t
            //ON  t.EventKey = e.EventKey
            //WHEN MATCHED THEN
            //    UPDATE SET e.ReplyType = t.ReplyType ,
            //               e.ReplyContent = t.ReplyContent,
            //							 e.UpdateOn=now()
            //WHEN NOT MATCHED THEN
            //    INSERT(EventType,EventKey,ReplyType,ReplyContent) VALUES(t.EventType,t.EventKey,t.ReplyType,t.ReplyContent);";
            //SqlParameter[] parameters = new SqlParameter[]
            //{
            //    new SqlParameter("@EventType",dto.EventType),
            //    new SqlParameter("@EventKey",dto.EventKey),
            //    new SqlParameter("@ReplyType",dto.ReplyType),
            //    new SqlParameter("@ReplyContent",dto.ReplyContent),
            //};

            using (var db = DbFactory.OpenDbConnection())
            {
                var model = db.Select<mpeventreply>(s => s.EventKey == dto.EventKey).FirstOrDefault();
                //mpeventreply model = null;
                if (model == null)
                {
                    dto.CreateOn = DateTime.Now;
                    dto.UpdateOn = DateTime.Now;
                    return db.Insert<mpeventreply>(dto) > 0;
                }
                else
                {
                    string sql = "UPDATE MpEventReply SET EventType=@EventType,EventKey=@EventKey,ReplyType=@ReplyType,ReplyContent=@ReplyContent,UpdateOn=NOW() WHERE EventKey=@EventKey ";
                    var parameters = new { EventType = dto.EventType, EventKey = dto.EventKey, ReplyType = dto.ReplyType, ReplyContent = dto.ReplyContent };
                    return ExecuteSql(sql, parameters) > 0;
                }
            }


        }
    }
}
