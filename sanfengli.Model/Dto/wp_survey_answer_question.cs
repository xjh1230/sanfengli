using sanfengli.Model.WeiXin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Model.Dto
{
    public class wp_survey_answer_question : wp_survey_answer
    {
        /// <summary>
        /// 问题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 问题选项
        /// </summary>
        public string extra { get; set; }
        /// <summary>
        /// 问题分类
        /// </summary>
        public string type { get; set; }
    }
}
