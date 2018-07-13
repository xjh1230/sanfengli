using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll
{
    public class DbConn
    {
        public static string WeiXin => ConfigurationManager.ConnectionStrings["WeiXin"].ConnectionString;
    }
}
