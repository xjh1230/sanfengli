using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Model.WeiXin
{
    public partial class wp_survey_answer
    {
        [Ignore]
        public string Url { get; set; }
        [Ignore]
        public string NickName { get; set; }
        [Ignore]
        public string mobile { get; set; }
        [Ignore]
        public DateTime CreateOn { get; set; }
    }
}
