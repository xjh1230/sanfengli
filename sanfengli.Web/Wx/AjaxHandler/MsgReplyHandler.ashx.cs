
using Newtonsoft.Json;
using sanfengli.Bll.WeChatMp;
using sanfengli.Common;
using sanfengli.Model.WeiXin;
using sanfengli.Web.Handler;

namespace sanfengli.Web.Wx.AjaxHandler
{
    /// <summary>
    /// MsgReplyHandler 的摘要说明
    /// </summary>
    public class MsgReplyHandler : ActionHandler
    {
        #region 关键字回复管理
        /// <summary>
        /// 获取关键字回复规则
        /// </summary>
        protected void GetAllRule()
        {
            var result = MsgReplyBll.GetAllMsgRule();
            if (result != null && result.Count > 0)
            {
                Json(new { state = true, data = result });
                return;
            }
            Json(new { state = false });
        }
        /// <summary>
        /// 保存规则
        /// </summary>
        protected void SaveRule()
        {
            string json = Request["ruleInfo"];
            var dto = JsonConvert.DeserializeObject<MsgReplyResult>(json);
            bool result = MsgReplyBll.SaveMsgRule(dto);
            Json(new { state = result });
        }
        /// <summary>
        /// 删除规则
        /// </summary>
        protected void DeleteMsgRule()
        {
            int msgId = EConvert.ConvertTo<int>(Request["msgId"]);
            bool result = MsgReplyBll.DeleteMsgRule(msgId);
            Json(new { state = result });
        }
        #endregion


        #region 关注回复、自动回复管理
        protected void GetReplyEvent()
        {
            string key = Request["eventKey"];
            var model = MpEventBll.GetByKey(key);
            if (model != null)
            {
                Json(new { state = true, data = model });
                return;
            }
            Json(new { state = false });
        }
        protected void SaveReplyEvent()
        {
            string key = Request["eventKey"];
            string eventType = Request["eventType"];
            string replyType = Request["replyType"];
            string replyContent = Request["replyContent"];
            bool result = MpEventBll.SaveEventInfo(new mpeventreply()
            {
                EventKey = key,
                EventType = eventType,
                ReplyType = replyType,
                ReplyContent = replyContent
            });
            Json(new { state = result });
        }
        protected void DeleteReplyEvent()
        {
            var key = Request["eventKey"];
            bool result = MpEventBll.DeletEventByKey(key);
            Json(new { state = result });
        }
        #endregion

    }
}