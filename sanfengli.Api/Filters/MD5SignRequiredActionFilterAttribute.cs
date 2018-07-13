using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Reflection;
using System.Collections.Specialized;
using System.Text;
using System.Configuration;
using sanfengli.Model.Dto;
using sanfengli.Common;

namespace sanfengli.Api.Filters
{
    /// <summary>
    /// MD5校验并且验证参数是否必填
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MD5SignRequiredAttribute : ActionFilterAttribute
    {

        private string _appId;
        private string _appSignKey;
        /// <summary>
        /// 必填字段标识
        /// </summary>
        private static readonly Type RequiredAttributeType = typeof(RequiredAttribute);
        public MD5SignRequiredAttribute(string appId, string appSignKey)
        {
            _appId = ConfigurationManager.AppSettings[appId]; ;
            _appSignKey = ConfigurationManager.AppSettings[appSignKey];
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //var appid = HttpContext.Current.Request["appid"];
            if (string.IsNullOrEmpty(_appId) || string.IsNullOrEmpty(_appSignKey))
            {
                var ret = new BaseOutput { IsSuccess = false, Msg = "AppId不能为空" };
                actionContext.Response = actionContext.Request.CreateResponse(ret);
                base.OnActionExecuting(actionContext);
                return;
            }
            // var appInfo = AppIdAndKeyBll.Instance.GetAppInfoByAppid(EConvert.ToInt(appid));
            //string SignKey = appInfo.AppKey;

            SortedDictionary<string, string> sortDic = new SortedDictionary<string, string>();
            ReflectedHttpActionDescriptor actionDescriptor = actionContext.ActionDescriptor as ReflectedHttpActionDescriptor;
            ParameterInfo[] parameters = actionDescriptor.MethodInfo.GetParameters();

            var currRequest = HttpContext.Current.Request;
            NameValueCollection collection = actionContext.Request.Method == HttpMethod.Post ? currRequest.Form : currRequest.QueryString;
            var allKeys = collection.AllKeys.Select(m => m.ToLower());
            foreach (ParameterInfo pi in parameters)
            {
                if (pi.ParameterType.IsClass)
                {
                    var _modelType = pi.ParameterType;
                    var properties = _modelType.GetProperties();
                    foreach (var p in properties)
                    {
                        var piname = p.Name.ToLower();
                        var pValue = allKeys.Contains(piname) ? collection[piname].ToString() : "";
                        //校验必须输入参数
                        var required = p.CustomAttributes.Any(c => c.AttributeType == RequiredAttributeType);
                        if (required && string.IsNullOrEmpty(pValue))
                        {
                            var ret = new BaseOutput { IsSuccess = false, Msg = piname + "不能为空" };
                            actionContext.Response = actionContext.Request.CreateResponse(ret);
                            base.OnActionExecuting(actionContext);
                            return;
                        }
                        sortDic.Add(piname, pValue);
                    }
                }
                else
                {
                    var piname = pi.Name.ToLower();
                    sortDic.Add(piname, allKeys.Contains(pi.Name) ? collection[pi.Name].ToString() : "");
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
            localSign = Md5Helper.Md5(sb.Append(_appSignKey).ToString()).ToLower();
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