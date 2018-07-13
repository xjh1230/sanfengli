using Bitauto.Mall.Aop;
using sanfengli.Api.Filters;
using sanfengli.Model.Dto;
using sanfengli.Model.WeiXin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json;

namespace sanfengli.Api.Controllers
{
    [RoutePrefix("api/Survey")]
    public class SurveyController : ApiController
    {
        /// <summary>
        ///获取问卷调查信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("GetSurvey")]
        [HttpGet]
        // [MD5Sign(typeof(OrderChangAnHeXiaoInput))]
        //[MD5SignComm]
        public SurveyOutput GetSurvey([FromUri]wp_survey input, int pageIndex, int pageSize, int isGetQuestion = 0)
        {
            var ret = new SurveyOutput();
            var list = new List<full_wp_survey>();
            try
            {

                int count = 0;
                list = new Bll.WeChat.wp_surveybll().GetList(input, out count, isGetQuestion == 1, pageIndex, pageSize);
                ret.TotalCount = count;
                //ret.Data = list;
            }
            catch (Exception ex)
            {
                ret.IsSuccess = false;
                ret.Msg = "服务异常";
                LogHandler.Error(ex);
            }
            ret.Data = JsonConvert.SerializeObject(list);
            return ret;
        }
        /// <summary>
        /// 保存问卷信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveSurvey")]
        public BaseOutput SaveSurvey([FromBody]full_wp_survey input, string name)
        {
            var ret = new BaseOutput();
            ret.IsSuccess = new Bll.WeChat.wp_surveybll().SaveMode(input);
            return ret;
        }
        /// <summary>
        /// 获取问卷的答题信息
        /// </summary>
        /// <param name="surver_id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSurveyAnswer")]
        public BaseOutput GetSurveyAnswer(int surver_id, int pageIndex, int pageSize)
        {
            var ret = new BaseOutput();
            int count = 0;
            var list = new Bll.WeChat.wp_survey_answerbll().GetListBySurveyId(surver_id, out count, pageIndex, pageSize);
            ret.TotalCount = count;
            ret.Data = list;
            return ret;
        }
    }
}