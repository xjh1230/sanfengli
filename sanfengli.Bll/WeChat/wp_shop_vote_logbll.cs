using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChat
{
    public class wp_shop_vote_logbll : BaseBll<wp_shop_vote_log>
    {
        /// <summary>
        /// 获取用户某活动今天投票的次数
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="vote_id"></param>
        /// <returns></returns>
        public List<wp_shop_vote_log> GetVoteLogTodayByVoteId(int uid, int vote_id)
        {
            List<wp_shop_vote_log> list = new List<wp_shop_vote_log>();

            try
            {
                string sql = $"select * from wp_shop_vote_log where uid={uid} and vote_id={vote_id} and TO_DAYS(now())=TO_DAYS(from_unixtime(ctime))";
                using (var db = DbFactory.OpenDbConnection())
                {
                    list = db.Select<wp_shop_vote_log>(sql);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return list;
        }

        /// <summary>
        /// 获取用户某活动投票的次数
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="vote_id"></param>
        /// <returns></returns>
        public List<wp_shop_vote_log> GetVoteLogByVoteId(int uid, int vote_id)
        {
            List<wp_shop_vote_log> list = new List<wp_shop_vote_log>();

            try
            {
                string sql = $"select * from wp_shop_vote_log where uid={uid} and vote_id={vote_id} ";
                using (var db = DbFactory.OpenDbConnection())
                {
                    list = db.Select<wp_shop_vote_log>(sql);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return list;
        }

        /// <summary>
        /// 获取某活动今天投票的次数
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="vote_id"></param>
        /// <returns></returns>
        public int GetVoteCountTodayByOptionId(int uid, int option_id)
        {
            int result = 0;
            string sql = $"select count(1) from wp_shop_vote_log where uid={uid} and option_id={option_id} and TO_DAYS(now())=TO_DAYS(from_unixtime(ctime))";
            result = ScalarSql<int>(sql);
            return result;
        }

        /// <summary>
        /// 获取某活动投票的次数
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="vote_id"></param>
        /// <returns></returns>
        public int GetVoteCountByOptionId(int uid, int option_id)
        {
            int result = 0;
            string sql = $"select count(1) from wp_shop_vote_log where uid={uid} and option_id={option_id} ";
            result = ScalarSql<int>(sql);
            return result;
        }

        public long InsertModel(wp_shop_vote_log model)
        {
            long result = InsertItem(model);
            if (result > 0)
            {
                new Bll.WeChat.wp_shop_vote_optionbll().AddVoteCount((int)model.option_id);
            }
            return result;
        }
    }
}
