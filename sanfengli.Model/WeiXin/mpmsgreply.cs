using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Model.WeiXin
{
    public partial class mpmsgreply
    {
        [Ignore]
        public string Contents { get; set; }
        [Ignore]
        public string Keys { get; set; }
    }
}
