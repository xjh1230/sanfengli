
using sanfengli.Common;
using sanfengli.Model.Dto;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace sanfengli.Api.Filters
{
    /// <summary>
    /// 验证MD5签名
    /// </summary>
    public class MD5SignAttribute : ActionFilterAttribute
    {
        private static string SignKey = "";
        private Type _modelType;
        /// <summary>
        /// 复杂输入参数验证MD5签名，只适用单个参数输入
        /// </summary>
        /// <param name="modelType"></param>
        public MD5SignAttribute(Type modelType)
        {
            _modelType = modelType;
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            SortedDictionary<string, string> param = new SortedDictionary<string, string>();
            var properties = _modelType.GetProperties();
            var currRequest = HttpContext.Current.Request;
            NameValueCollection collection = actionContext.Request.Method == HttpMethod.Post ? currRequest.Form : currRequest.QueryString;
            foreach (var p in properties)
            {
                var name = p.Name.ToLower();
                param.Add(name, collection.AllKeys.Contains(name) ? collection[name].ToString() : "");
            }
            string userSign = "", localSign = "";
            var sb = new StringBuilder();
            foreach (var p in param)
            {
                if (p.Key != "sign")
                {
                    if (!string.IsNullOrEmpty(p.Key) && !string.IsNullOrEmpty(p.Value))
                    {
                        sb.Append(p.Value);
                    }
                }
                else
                    userSign = p.Value;
            }
            localSign = Md5Helper.Md5(sb.Append(SignKey).ToString()).ToLower();
            if (!string.IsNullOrWhiteSpace(userSign) && localSign.Equals(userSign))
            {
                base.OnActionExecuting(actionContext);
            }
            else
            {
                var ret = new BaseOutput { IsSuccess = false, Msg = "签名错误" };
                actionContext.Response = actionContext.Request.CreateResponse(ret);
            }
        }
    }
}