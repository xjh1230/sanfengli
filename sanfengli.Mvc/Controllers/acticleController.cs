using BitAuto.Utils;
using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using sanfengli.Common;
using sanfengli.Model.Dto;
using sanfengli.Model.WeiXin;
using sanfengli.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sanfengli.Mvc.Controllers
{
    public class acticleController : Controller
    {
        // GET: acticle



        public JsonResult Getypes(string op)
        {
            BaseOutput responseModel = new BaseOutput();
            var list = new Bll.WeChat.wp_article_type_newbll().GetList();
            if (list != null && list.Count > 0)
            {
                responseModel.IsSuccess = true;
                responseModel.Data = list;// JsonConvert.SerializeObject(list);
            }
            return Json(responseModel);
        }


        public JsonResult GetList(RequestModel model)
        {
            BaseOutput responseModel = new BaseOutput();
            int count = 0;
            wp_article_new query = new wp_article_new();
            query.type_id = ConvertHelper.GetInteger(model.conditions["typeid"]);
            query.name = model.conditions["name"];
            var list = new Bll.WeChat.wp_article_newbll().GetList(query, out count, model.page, model.size);
            if (list != null && list.Count > 0)
            {
                list.ForEach(s =>
                {
                    s.type_name = new Bll.WeChat.wp_article_type_newbll().GetItem((int)s.type_id).name;
                    s.url = $"{BaseClass.BaseDomin}home/infodetail.aspx?id={s.Id}";
                });
            }
            if (list != null && list.Count > 0)
            {
                responseModel.IsSuccess = true;
                responseModel.Data = list;
                responseModel.TotalCount = count;
            }
            return Json(responseModel);
        }


        public JsonResult edit(wp_article_new model)
        {
            BaseOutput responseModel = new BaseOutput();
            model.cTime = DateTime.Now;
            responseModel.IsSuccess = new Bll.WeChat.wp_article_newbll().SaveModel(model);
            responseModel.Msg = responseModel.IsSuccess ? "成功" : "失败";
            return Json(responseModel);
        }


        public JsonResult delete(wp_article_new model)
        {
            BaseOutput responseModel = new BaseOutput();
            //model.cTime = DateTime.Now;
            responseModel.IsSuccess = new Bll.WeChat.wp_article_newbll().DeleteModel(model.Id);
            responseModel.Msg = responseModel.IsSuccess ? "成功" : "失败";
            return Json(responseModel);
        }
        public JsonResult addType(wp_article_type_new model)
        {
            BaseOutput responseModel = new BaseOutput();

            responseModel.IsSuccess = new Bll.WeChat.wp_article_type_newbll().SaveModel(model);
            responseModel.Msg = responseModel.IsSuccess ? "成功" : "失败";
            return Json(responseModel);
        }


    }
}