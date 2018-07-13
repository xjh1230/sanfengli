using sanfengli.Model.WeiXin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Model.Dto
{
    public class full_wp_survey_answer
    {
        public wp_survey_answer wp_survey_answer { get; set; }

        public List<wp_survey_answer_question> list { get; set; }
    }
}
