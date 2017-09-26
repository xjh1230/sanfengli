using sanfengli.Model.WeiXin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using ServiceStack.OrmLite;
using Senparc.Weixin.MP.AdvancedAPIs.User;

namespace sanfengli.Bll.WeChat
{
    public class wp_userbll : BaseBll<wp_user>
    {
        public long SaveUserInfo(OAuthUserInfo weixinUser, UserInfoJson fensi = null)
        {
            wp_user userModel = new wp_user();

            int useId = GetUserIdByOpenId(weixinUser.openid);
            if (useId == 0)
            {
                if (fensi == null)
                {
                    userModel.city = weixinUser.city;
                    userModel.country = weixinUser.country;
                    userModel.headimgurl = weixinUser.headimgurl;
                    userModel.nickname = weixinUser.nickname;
                    userModel.openid = weixinUser.openid;
                    userModel.province = weixinUser.province;
                    userModel.sex = (sbyte)weixinUser.sex;
                    userModel.unionid = weixinUser.unionid;
                    userModel.status = 0;
                }
                else
                {
                    userModel.city = fensi.city;
                    userModel.country = fensi.country;
                    userModel.headimgurl = fensi.headimgurl;
                    userModel.nickname = fensi.nickname;
                    userModel.openid = fensi.openid;
                    userModel.province = fensi.province;
                    userModel.sex = (sbyte)fensi.sex;
                    userModel.unionid = fensi.unionid;
                    userModel.status = (sbyte)fensi.subscribe;
                }


                return InsertItem(userModel);
            }
            else
            {
                return (long)useId;
            }

        }

        public int GetUserIdByOpenId(string openId)
        {
            int userId = 0;
            if (!string.IsNullOrEmpty(openId))
            {
                string sql = $"select uid from wp_user where openid='{openId}' LIMIT 0,1";
                userId = ScalarSql<int>(sql, null);
            }

            return userId;
        }
        public wp_user GetUserInfoByOpenId(string openId)
        {
            wp_user model = null;
            if (!string.IsNullOrEmpty(openId))
            {
                string sql = $"select * from wp_user where openid='{openId}' LIMIT 0,1";
                using (var db = DbFactory.OpenDbConnection())
                {
                    model = db.Select<wp_user>(sql).FirstOrDefault();
                }
            }
            return model;
        }


        /// <summary>
        /// 修改关注状态
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="state"></param>
        public void SetSubscribe(string openId, int state)
        {
            string sql = $"UPDATE Weixin_User SET status={state} WHERE openid={openId}";

            ExecuteSql(sql);
        }
    }
}
