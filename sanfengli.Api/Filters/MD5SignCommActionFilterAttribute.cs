

using sanfengli.Common;
using sanfengli.Model.Dto;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace sanfengli.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MD5SignCommAttribute : ActionFilterAttribute
    {
        const string SignParamName = "sign";
        private static string SignKey = "";
        public MD5SignCommAttribute()
        {
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            SortedDictionary<string, string> sortDic = new SortedDictionary<string, string>();
            ReflectedHttpActionDescriptor actionDescriptor = actionContext.ActionDescriptor as ReflectedHttpActionDescriptor;
            ParameterInfo[] parameters = actionDescriptor.MethodInfo.GetParameters();

            var currRequest = HttpContext.Current.Request;
            NameValueCollection collection = actionContext.Request.Method == HttpMethod.Post ? currRequest.Form : currRequest.QueryString;
            foreach (ParameterInfo pi in parameters)
            {
                if (pi.ParameterType.IsClass)
                {
                    var _modelType = pi.ParameterType;
                    var properties = _modelType.GetProperties();
                    foreach (var p in properties)
                    {
                        var piname = p.Name.ToLower();
                        sortDic.Add(piname, collection.AllKeys.Contains(piname) ? collection[piname].ToString() : "");
                    }
                }
                else
                {
                    var piname = pi.Name.ToLower();
                    sortDic.Add(piname, collection.AllKeys.Contains(pi.Name) ? collection[pi.Name].ToString() : "");
                }
            }

            string userSign = "", localSign = "";
            var sb = new StringBuilder();
            foreach (var p in sortDic)
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