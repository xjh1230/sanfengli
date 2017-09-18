using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sanfengli.Web.Base
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }

        public int Count { get; set; }
    }
}