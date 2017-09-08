using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace sanfengli.Web.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var s = new Bll.WeChatMp.FeedBackBll().GetList();
            var a = s;
        }
    }
}
