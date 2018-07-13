using Bitauto.Mall.Aop;
using Common;
using sanfengli.Bll;
using sanfengli.Bll.WeChat;
using sanfengli.Common;
using sanfengli.Web.Base;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sanfengli.Web
{
    public partial class test : System.Web.UI.Page //PageBase
    {

        HttpContext hContext = HttpContext.Current;

        public string openId = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                string url = Request.Url.ToString();

                var urlTmp = WebTools.BuildUrl(url,"code","");
                Response.Write(urlTmp);
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("Id");
                dt1.Columns.Add("UserId");
                dt1.Columns.Add("Content");
                dt1.Columns.Add("Image");
                dt1.Columns.Add("CreateOn");
                dt1.Columns.Add("name");
                dt1.Columns.Add("phone");
                dt1.Columns.Add("remark");
                MySqlHelper helper = new MySqlHelper(DbConn.WeiXin);
                string sql = $@"select  * from feedback";
                dt = helper.ExecuteDataTable(sql);
                var row = dt.Select("CreateOn>'2017-08-14 09:37:36'");
                row.CopyToDataTable(dt1,LoadOption.OverwriteChanges);
                DataRow row1 = row[0];
                dt1.Rows.Add(row1.ItemArray);
                dt1.Rows.Add(row1.ItemArray);

                var tmp = dt1;
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}