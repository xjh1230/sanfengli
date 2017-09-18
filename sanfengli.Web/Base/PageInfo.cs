using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace sanfengli.Web.Base
{
    public class PageInfo
    {
        public int TotalCount;
        public int PageSize;
        public int TotalPage;
        public int CurrentPage;
        public string NextUrl;
        public string PreUrl;
        public string BeginUrl;
        public string EndUrl;
        public string CurrentUrl;

        public PageInfo()
        {
            this.CurrentPage = 1;
            this.PageSize = 10;
            this.TotalCount = this.TotalPage = 0;
            this.CurrentUrl = System.Web.HttpContext.Current.Request.RawUrl;
            this.BeginUrl = this.EndUrl = this.PreUrl = this.NextUrl = "javascript:void(0);";
        }
        public bool InitPage()
        {
            bool result = false;
            try
            {
                if (TotalCount > 0)
                {
                    this.TotalPage = Convert.ToInt32(Math.Ceiling(TotalCount * 1.0 / PageSize));
                    this.BeginUrl = BuildUrl(CurrentUrl, "pageIndex", "1");
                    this.EndUrl = BuildUrl(CurrentUrl, "pageIndex", TotalPage.ToString());
                    if (CurrentPage > 1)
                    {
                        this.PreUrl = BuildUrl(CurrentUrl, "pageIndex", (CurrentPage - 1).ToString());
                    }
                    if (CurrentPage < TotalPage)
                    {
                        this.NextUrl = BuildUrl(CurrentUrl, "pageIndex", (CurrentPage + 1).ToString());
                    }
                    //if (CurrentPage == 1)
                    //{
                    //    this.BeginUrl = "javascript:void(0);";
                    //}
                    //if (CurrentPage == TotalPage)
                    //{
                    //    this.EndUrl = "javascript:void(0);";
                    //}
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// url里有key的值，就替换为value,没有的话就追加
        /// </summary>
        /// <param name="url">要处理的URL</param>
        /// <param name="paramText">参数名</param>
        /// <param name="paramValue">参数值</param>
        /// <returns></returns>
        private static string BuildUrl(string url, string paramText, string paramValue)
        {
            string regStr = string.Format("([&?])({0}=[^&]*)", paramText);
            Regex reg = new Regex(regStr, RegexOptions.IgnoreCase);
            Match mat = reg.Match(url);
            var _url = url;
            if (mat.Success)
            {
                var mc = reg.Match(url);
                if (mc.Groups.Count > 2)
                {
                    _url = url.Replace(mc.Groups[2].Value, "");
                }
            }

            if (_url.IndexOf("?") == -1)
                _url += string.Format("?{0}={1}", paramText, paramValue);//?
            else
                _url += string.Format("&{0}={1}", paramText, paramValue);//&
            Regex reg1 = new Regex("[&]{2,}", RegexOptions.IgnoreCase);
            _url = reg1.Replace(_url, "&");
            _url = _url.Replace("?&", "?");
            return _url;
        }
    }
}