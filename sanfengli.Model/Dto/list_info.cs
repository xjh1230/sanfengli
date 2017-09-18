using sanfengli.Common;
using sanfengli.Model.WeiXin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Model.Dto
{
    /// <summary>
    /// 列表信息  前台用
    /// </summary>
    public class list_info
    {
        public static readonly list_info Instance = new list_info();
        #region 属性
        public uint id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public string url { get; set; }
        public string img { get; set; }
        public string create_time { get; set; }
        public string type_name { get; set; }
        public int type_id { get; set; }

        public int count { get; set; }
        #endregion


        /// <summary>
        /// 投票信息转换为list
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<list_info> GetFromShopVote(List<wp_shop_vote> list)
        {
            List<list_info> result = new List<list_info>();
            if (list != null && list.Count > 0)
            {
                list.ForEach(s =>
                {
                    list_info model = new list_info();
                    model.id = s.Id;
                    model.title = s.title;
                    model.create_time = DateTimeHelper.GetDateString(BaseClass.ConvertToDateTime((long)s.start_time));
                    model.count = 0;
                    model.desc = s.remark;
                    result.Add(model);
                });
            }
            return result;
        }

        /// <summary>
        /// 问卷信息转换为list
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<list_info> GetFromSurvey(List<full_wp_survey> list)
        {
            List<list_info> result = new List<list_info>();
            if (list != null && list.Count > 0)
            {
                list.ForEach(s =>
                {
                    list_info model = new list_info();
                    model.id = s.wp_survey.Id;
                    model.title = s.wp_survey.title;
                    model.create_time = DateTimeHelper.GetDateString(BaseClass.ConvertToDateTime((long)s.wp_survey.start_time));
                    model.count = 0;
                    model.img = s.wp_survey.Image;
                    model.desc = s.wp_survey.intro;
                    result.Add(model);
                });
            }
            return result;
        }


        /// <summary>
        /// 活动信息转换为list
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<list_info> GetFromReserve(List<wp_reserve> list)
        {
            List<list_info> result = new List<list_info>();
            if (list != null && list.Count > 0)
            {
                list.ForEach(s =>
                {
                    list_info model = new list_info();
                    model.id = s.Id;
                    model.title = s.title;
                    model.create_time = DateTimeHelper.GetDateString(BaseClass.ConvertToDateTime((long)s.cTime));
                    model.count = 0;
                    model.img = s.Image;
                    model.desc = s.intro;
                    result.Add(model);
                });
            }
            return result;
        }

        /// <summary>
        /// 文章信息转换为list
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<list_info> GetFromArticle(List<wp_article_new> list)
        {
            List<list_info> result = new List<list_info>();
            if (list != null && list.Count > 0)
            {
                list.ForEach(s =>
                {
                    list_info model = new list_info();
                    model.id = (uint)s.Id;
                    model.title = s.name;
                    model.create_time = DateTimeHelper.GetDateString((DateTime)s.cTime);
                    model.count = 0;
                    model.img = s.image;
                    model.desc = s.title;
                    result.Add(model);
                });
            }
            return result;
        }

    }
}
