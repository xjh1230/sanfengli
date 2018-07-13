using sanfengli.Model.WeiXin;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Bll.WeChat
{
    public class wp_shop_vote_optionbll : BaseBll<wp_shop_vote_option>
    {
        public wp_shop_vote_option GetOptionByUid(int vote_id, int uid)
        {
            wp_shop_vote_option model = new wp_shop_vote_option();
            try
            {
                string sql = $"select * from wp_shop_vote_option where  uid={uid} and vote_id={vote_id} LIMIT 0 ,1";
                using (var db = DbFactory.OpenDbConnection())
                {
                    model = db.Select<wp_shop_vote_option>(sql).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return model;
        }

        /// <summary>
        /// 获取排名前多少名次
        /// </summary>
        /// <param name="vote_id"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<wp_shop_vote_option> GetOptionListByVoteId(int vote_id, int count = 20)
        {
            List<wp_shop_vote_option> list = new List<wp_shop_vote_option>();
            try
            {
                string sql = $"select * from wp_shop_vote_option where option_status=1 and vote_id={vote_id} order by opt_count desc LIMIT 0 ,{count}";
                using (var db = DbFactory.OpenDbConnection())
                {
                    list = db.Select<wp_shop_vote_option>(sql);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return list;
        }


        public int GetOptionCountByVoteId(int voteId)
        {
            string sql = $"select count(1)  from wp_shop_vote_option where vote_id={voteId}";
            return ScalarSql<int>(sql);
        }

        public bool AddVoteCount(int option_id, int count = 1)
        {
            bool result = false;
            string sql = $"update wp_shop_vote_option set opt_count=opt_count+{count} WHERE id={option_id}";
            result = ExecuteSql(sql) > 0;
            return result;
        }
    }
}
