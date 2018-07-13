using sanfengli.Model.WeiXin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Model.Dto
{
    public partial class full_wp_survey
    {
        public wp_survey wp_survey { get; set; }
        public List<wp_survey_question> list_question { get; set; }
    }
}
