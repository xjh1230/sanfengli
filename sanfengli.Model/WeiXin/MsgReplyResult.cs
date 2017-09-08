using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Model.WeiXin
{
    public class MsgReplyResult
    {
        public MsgReplyResult()
        {
            Msg = new mpmsgreply();
            Keys = new List<mpmsgreplykey>();
            Contents = new List<mpmsgreplycontent>();
        }

        public mpmsgreply Msg { get; set; }
        public List<mpmsgreplykey> Keys { get; set; }
        public List<mpmsgreplycontent> Contents { get; set; }
    }

    public class MsgReplyJsonResult<T>
    {
        public List<T> Item { get; set; }
    }
}
