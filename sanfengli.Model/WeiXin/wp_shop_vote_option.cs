using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Model.WeiXin
{
    public partial class wp_shop_vote_option
    {
        /// <summary>
        /// 当前用户是否已投票
        /// </summary>
        [Ignore]
        public bool IsVote { get; set; }
        /// <summary>
        ///当前选票是否已投
        /// </summary>
        [Ignore]
        public bool IsVoteCurrent { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        [Ignore]
        public string ImagePath { get; set; }
    }
}
