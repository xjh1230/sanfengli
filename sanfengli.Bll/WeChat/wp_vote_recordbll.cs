using sanfengli.Model.WeiXin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChat
{
    public class wp_vote_recordbll : BaseBll<wp_vote_record>
    {
        public bool AddModel(wp_vote_record model)
        {
            bool result = false;
            try
            {
                result = InsertItem(model) > 0;
                if (result)
                {
                    //result = new Bll.WeChat.wp_vote_detailbll().UpdateDetailCount((int)model.vote_detail_id);
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
