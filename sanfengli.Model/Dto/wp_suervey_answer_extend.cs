using sanfengli.Model.WeiXin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Model.Dto
{
    public class wp_suervey_answer_extend:wp_survey_answer
    {
        public string keyword { get; set; }
        public string survey_title { get; set; }
        public string question_title { get; set; }
        public string nickname { get; set; }

    }
}
