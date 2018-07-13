using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Model.Dto
{
    public class BaseOutput
    {
        public bool IsSuccess { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }

        public int TotalCount { get; set; }

        public BaseOutput()
        {
            IsSuccess = true;
            Msg = "";
            Data = null;
        }
    }
}
