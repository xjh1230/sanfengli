using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sanfengli.Model.WeiXin;
using System.Collections.Generic;

namespace sanfengli.Web.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {


            wp_article mode = new wp_article();
            //mode.Id = 2;
            mode.content = "测试1";
            mode.name = "测试1";
            mode.title = "测试1";
            mode.image = "测试1";
            mode.end_time = DateTime.Now;
            mode.is_delete = 0;

            //var tmp = new Bll.WeChat.wp_articlebll().InsertItem(mode);
            string replyMode = "";
            //var tmp = new Bll.WeChat.wp_shop_vote_logbll().GetVoteCountTodayByOptionId(1, 1);

            string str = "a:2:{i:0;s:1:\"0\";i:1;s:1:\"1\";}";
            str= "s:12:\"我文案无\";";
            //var tmp = new Bll.WeChat.wp_surveybll().GetAnswer(str);

            var tmp = new Bll.WeChat.wp_survey_answerbll().GetAnswerExport(11);
            //var cvs = Bll.MySqlHelper.DataTableToCsv(tmp);
            var s = tmp;
        }
    }
}
