using sanfengli.Common;
using sanfengli.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sanfengli.Mvc.Controllers
{
    public class fileController : Controller
    {

        public JsonResult uploadImg(string path)
        {
            BaseOutput responseModel = new BaseOutput();
            string requsetKey = "fileImage";
            var result = ImageHelper.UpLoadImg(path, requsetKey, false);
            responseModel.IsSuccess = result[0] == "true";
            responseModel.Msg =result[1];
            return Json(responseModel);
        }
    }
}