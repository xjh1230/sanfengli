using Bitauto.Mall.Aop;
using Newtonsoft.Json;
using sanfengli.Common;
using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace sanfengli.Bll.WeChatMp
{
    /// <summary>
    /// 公众号自动回复管理
    /// </summary>
    public class MsgReplyBll : BaseBll<MsgReplyResult>
    {
        public static List<MsgReplyResult> GetAllMsgRule()
        {
            string sql = @"SELECT m.*, ('<root>'+( SELECT c.* FROM MpMsgReplyContent c WHERE c.MsgId = m.MsgId FOR XML PATH ('Item') )+'</root>') AS Contents,('<root>'+(SELECT k.* FROM MpMsgReplyKeys k WHERE k.MsgId = m.MsgId FOR XML PATH ('Item') )+'</root>') AS Keys FROM MpMsgReply m ";

            try
            {
                using (var db = DbFactory.CreateDbConnection())
                {
                    List<mpmsgreply> list = db.Select<mpmsgreply>(sql);
                    List<MsgReplyResult> listResult = new List<MsgReplyResult>();
                    if (list != null && list.Count > 0)
                    {

                        list.ForEach(s =>
                        {
                            MsgReplyResult result = new MsgReplyResult
                            {
                                Msg =
                            {
                            Id = s.Id,
                            RuleName = s.RuleName,
                            ReplyMode = s.ReplyMode,
                            CreateOn = s.CreateOn
                            },
                                Contents = ConvertXmlToJson<MsgReplyJsonResult<mpmsgreplycontent>>(s.Contents)?.Item,
                                Keys = ConvertXmlToJson<MsgReplyJsonResult<mpmsgreplykey>>(s.Keys)?.Item

                            };
                            listResult.Add(result);
                        });
                    }
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
                return null;
            }

        }
        public static bool SaveMsgRule(MsgReplyResult dto)
        {
            if (dto == null)
            {
                return false;
            }
            try
            {
                int msgId = SaveMsgReply(dto.Msg);
                if (dto.Keys != null && dto.Keys.Count > 0)
                {
                    foreach (var item in dto.Keys)
                    {
                        item.MsgId = msgId;
                        if (item.IsDel == 1)
                        {
                            DeleteKey(item.Id);
                        }
                        else
                        {
                            SaveKey(item);
                        }

                    }
                }
                if (dto.Contents != null && dto.Contents.Count > 0)
                {
                    foreach (var item in dto.Contents)
                    {
                        item.MsgId = msgId;
                        if (item.IsDel == 1)
                        {
                            DeleteContent(item.Id);
                        }
                        else
                        {
                            SaveReplyContent(item);
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                LogHandler.Error(e);
                return false;
            }
        }
        public static bool DeleteMsgRule(int msgId)
        {
            string sql = @"DELETE MpMsgReply WHERE MsgId=@MsgId
DELETE MpMsgReplyContent WHERE MsgId=@MsgId
DELETE MpMsgReplyKeys WHERE MsgId=@MsgId";
            // SqlParameter[] parameters = new SqlParameter[]
            //{
            //     new SqlParameter("@MsgId",msgId),
            //};
            var parameters = new { MsgId = msgId };
            return ExecuteSql(sql, parameters) > 0;
        }

        /// <summary>
        /// 注意此方法将清空关键字回复规则
        /// </summary>
        /// <returns></returns>
        public static void ClearMpEventAndMsg()
        {
            string sql = @"DELETE MpMsgReply 
                        DELETE MpMsgReplyContent
                        DELETE MpMsgReplyKeys 
                        DELETE MpEventReply ";
            ExecuteSql(sql, null);
        }

        public static List<mpmsgreplycontent> GetReplyInfo(string key, out string replyMode)
        {
            try
            {
                using (var db = DbFactory.CreateDbConnection())
                {
                    string sqlmId = @"SELECT  MsgId FROM MpMsgReplyKeys
WHERE (@Key LIKE '%'+KeyVal+'%' AND MatchMode='contain') OR (@Key=KeyVal AND MatchMode='equal')
ORDER BY CreateOn DESC limit 1";

                    var parameters = new { Key = key };
                    var MsgId = db.Scalar<int>(sqlmId, parameters);

                    var parametersModel = new { mId = MsgId };
                    replyMode = db.Scalar<string>("SELECT ReplyMode FROM MpMsgReply WHERE MsgId=@mId", parametersModel);

                    string sqlModel = @"SELECT ReplyId,MsgId,ReplyType,ReplyContent,CreateOn FROM MpMsgReplyContent WHERE MsgId =@mId
ORDER BY CreateOn DESC";

                    List<mpmsgreplycontent> list = db.SqlList<mpmsgreplycontent>(sqlModel, parametersModel);
                    return list;
                }
            }
            catch (Exception ex)
            {
                Bitauto.Mall.Aop.LogHandler.Error(ex);
                replyMode = "";
                return null;
            }

        }


        public static int SaveMsgReply(mpmsgreply dto)
        {
            using (var db = DbFactory.CreateDbConnection())
            {
                string sql = @"INSERT INTO MpMsgReply  (RuleName,ReplyMode) VALUES(@RuleName,@ReplyMode) SELECT @@IDENTITY";
                if (dto.Id > 0)
                {
                    sql = @"UPDATE  MpMsgReply SET RuleName = @RuleName ,ReplyMode = @ReplyMode,
 UpdateOn=GETDATE() WHERE MsgId=@MsgId";
                }
                //SqlParameter[] parameters = new SqlParameter[]
                //{
                //new SqlParameter("@MsgId",dto.MsgId),
                //new SqlParameter("@RuleName",dto.RuleName),
                //new SqlParameter("@ReplyMode",dto.ReplyMode)
                //};

                int newId = db.Scalar<int>(sql, dto);
                return dto.Id > 0 ? dto.Id : newId;
            }
        }

        public static bool DeleteKey(int keyId)
        {
            if (keyId == 0)
                return false;
            string sql = @"DELETE MpMsgReplyKeys WHERE KeyId=@KeyId";
            return ExecuteSql(sql, new { KeyId = keyId }) > 0;
        }

        public static bool SaveKey(mpmsgreplykey keyItem)
        {
            string sql = @"MERGE INTO MpMsgReplyKeys e
USING ( SELECT @KeyId AS KeyId, @MsgId AS MsgId,@KeyVal AS KeyVal,@MatchMode AS MatchMode) t
ON t.KeyId = e.KeyId
WHEN MATCHED THEN
    UPDATE SET e.KeyVal = t.KeyVal ,
               e.MatchMode = t.MatchMode,
               e.UpdateOn=GETDATE()
WHEN NOT MATCHED THEN
  INSERT (MsgId,KeyVal,MatchMode) VALUES(t.MsgId,t.KeyVal,t.MatchMode);";
            //SqlParameter[] parameters = new SqlParameter[]
            //        {
            //            new SqlParameter("@KeyId",keyItem.KeyId),
            //            new SqlParameter("@MsgId",keyItem.MsgId),
            //            new SqlParameter("@KeyVal",keyItem.KeyVal),
            //            new SqlParameter("@MatchMode",keyItem.MatchMode)
            //        };


            try
            {
                using (var db = DbFactory.OpenDbConnection())
                {
                    var model = db.Select<mpmsgreplykey>(s => s.Id == keyItem.Id).FirstOrDefault();
                    if (model == null)
                    {
                        keyItem.CreateOn = DateTime.Now;
                        keyItem.UpdateOn = DateTime.Now;
                        return db.Insert<mpmsgreplykey>(keyItem) > 0;
                    }
                    else
                    {
                        keyItem.UpdateOn = DateTime.Now;
                        return db.Update<mpmsgreplykey>(keyItem) > 0;
                        //var parameters = new { KeyId = keyItem.KeyId, MsgId = keyItem.MsgId, KeyVal = keyItem.KeyVal, MatchMode = keyItem.MatchMode };
                        //return ExecuteSql(sql, parameters) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
                return false;
            }

        }

        public static bool DeleteContent(int replyId)
        {
            if (replyId == 0)
                return false;
            string sql = @"DELETE MpMsgReplyContent WHERE ReplyId=@ReplyId";
            return ExecuteSql(sql, new { ReplyId = replyId }) > 0;
        }

        public static bool SaveReplyContent(mpmsgreplycontent dto)
        {
            string sql = @"MERGE INTO MpMsgReplyContent e
USING ( SELECT @ReplyId AS ReplyId, @MsgId AS MsgId,@ReplyType AS ReplyType,@ReplyContent AS ReplyContent) t
ON t.ReplyId = e.ReplyId
WHEN MATCHED THEN
    UPDATE SET e.ReplyType = t.ReplyType ,
               e.ReplyContent = t.ReplyContent,
               e.UpdateOn=GETDATE()
WHEN NOT MATCHED THEN
  INSERT (MsgId,ReplyType,ReplyContent) VALUES(t.MsgId,t.ReplyType,t.ReplyContent);";


            try
            {
                using (var db = DbFactory.OpenDbConnection())
                {
                    var model = db.Select<mpmsgreplycontent>(s => s.Id == dto.Id).FirstOrDefault();
                    if (model == null)
                    {
                        dto.CreateOn = DateTime.Now;
                        dto.UpdateOn = DateTime.Now;
                        return db.Insert<mpmsgreplycontent>(dto) > 0;
                    }
                    else
                    {
                        dto.UpdateOn = DateTime.Now;
                        return db.Update<mpmsgreplycontent>(dto) > 0;
                        //var parameters = new { ReplyId = dto.ReplyId, MsgId = dto.MsgId, ReplyType = dto.ReplyType, ReplyContent = dto.ReplyContent };
                        //return ExecuteSql(sql, parameters) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
                return false;
            }


        }

        private static T ConvertXmlToJson<T>(string xml) where T : class
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            xml = xml.Replace("<root>", "<root xmlns:json='http://james.newtonking.com/projects/json'>").Replace("<Item>", "<Item json:Array='true'>");
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                string json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented, true);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
