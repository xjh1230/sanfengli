using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Model
{
    /// <summary>
    /// 微信分享所需参数
    /// </summary>
    public class ShareParmModel
    {
        public string appid { get; set; }

        public string nonceStr { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }

        /// <summary>
        /// 分享所需签名
        /// </summary>
        public string signature { get; set; }
    }
}
