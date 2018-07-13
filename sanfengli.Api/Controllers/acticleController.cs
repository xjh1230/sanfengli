using Common;
using Newtonsoft.Json;
using sanfengli.Model.Dto;
using sanfengli.Model.WeiXin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace sanfengli.Api.Controllers
{
    [RoutePrefix("api/acticle")]
    public class acticleController : ApiController
    {
        [Route("Getypes")]
        [HttpPost]
        public BaseOutput Getypes(string op)
        {
            BaseOutput responseModel = new BaseOutput();
            var list = new Bll.WeChat.wp_article_type_newbll().GetList();
            if (list != null && list.Count > 0)
            {
                responseModel.IsSuccess = true;
                responseModel.Data = list;// JsonConvert.SerializeObject(list);
            }
            return responseModel;
        }

        [Route("GetList")]
        [HttpPost]
        public BaseOutput GetList()
        {
            BaseOutput responseModel = new BaseOutput();
            int pageIndex = RequestHelper.GetFormInt("page", 1);
            int pageSize = RequestHelper.GetFormInt("size", 10);
            string conditions = RequestHelper.GetFormString("conditions");
            string keyWord = RequestHelper.GetFormString("conditions");
            int typeId = RequestHelper.GetFormInt("type", 0);
            int count = 0;
            wp_article_new query = new wp_article_new();
            var list = new Bll.WeChat.wp_article_newbll().GetList(query, out count, pageIndex, pageSize);
            if (list != null && list.Count > 0)
            {
                responseModel.IsSuccess = true;
                responseModel.Data = JsonConvert.SerializeObject(list);
                responseModel.TotalCount = count;
            }
            return responseModel;
        }

    }
}