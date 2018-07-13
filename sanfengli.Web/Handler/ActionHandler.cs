using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace sanfengli.Web.Handler
{
    public class ActionHandler : IHttpHandler
    {
        public bool IsReusable => false;
        protected HttpContext Context { get; private set; }
        protected HttpRequest Request { get; private set; }
        protected HttpResponse Response { get; private set; }
        public void ProcessRequest(HttpContext context)
        {
            Context = context;
            Request = context.Request;
            Response = context.Response;
            string actionName = GetActionName(context.Request.PathInfo);
            if (actionName == null)
            {
                NotFound();
                return;
            }
            MethodInfo method = GetMethod(actionName);
            if (method == null)
            {
                NotFound();
                return;
            }
            try
            {
                method.Invoke(this, null);
            }
            catch (Exception e)
            {
                if (method.DeclaringType != null)
                    //LogHandler.Error("错误定位:" + method.DeclaringType.FullName + method + "\n" + "错误信息：" + e);
                Json(new { error = true, msg = "服务器错误" });
            }
        }
        protected virtual string GetActionName(string pathInfo)
        {
            string action = pathInfo.Trim('/');
            if (string.IsNullOrEmpty(action))
            {
                action = null;
            }
            return action;
        }
        protected virtual MethodInfo GetMethod(string action)
        {
            Type type = GetType();
            MethodInfo method = type.GetMethod(action, BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Instance);
            return method;
        }
        private void NotFound(string message = "未提供或者无匹配Action")
        {
            Response.StatusCode = 404;
            Response.Write(message);
            Response.End();
        }
        protected void Json(object value, string contentType = "application/json")
        {
            string result = JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                Error = delegate (object sender, ErrorEventArgs args1)
                {
                    args1.ErrorContext.Handled = true;
                },
                Converters = { new IsoDateTimeConverter() }
            });
            Response.ContentType = contentType;
            Response.Write(result);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}