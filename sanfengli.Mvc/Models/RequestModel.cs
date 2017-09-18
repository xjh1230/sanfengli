using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sanfengli.Mvc.Models
{
    public class RequestModel
    {
        public int page { get; set; }
        public int size { get; set; }
        public Dictionary<string, string> conditions { get; set; }
        //public int page { get; set; }
    }
}