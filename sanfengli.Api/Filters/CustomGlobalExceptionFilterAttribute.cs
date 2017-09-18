using Bitauto.Mall.Aop;
using sanfengli.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace sanfengli.Api.Filters
{
    public class CustomGlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //  actionExecutedContext.Exception
            LogHandler.Error(actionExecutedContext.Exception);
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(new BaseOutput { IsSuccess = false, Msg = "无法处理的异常" });
            base.OnException(actionExecutedContext);
        }
    }
}