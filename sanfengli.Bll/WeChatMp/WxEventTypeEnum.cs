using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChatMp
{
    public enum WxEventTypeEnum
    {
        /// <summary>
        /// 订阅事件
        /// </summary>
        subscribe,
        /// <summary>
        /// 自动回复
        /// </summary>
        automsg,
        /// <summary>
        /// 自定义菜单按钮点击事件
        /// </summary>
        click,
    }
}
