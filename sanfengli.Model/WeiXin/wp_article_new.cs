using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Model.WeiXin
{
   public partial class wp_article_new
    {
        [Ignore]
        public string url { get; set; }
    }
}
