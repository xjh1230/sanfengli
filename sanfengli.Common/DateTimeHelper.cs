using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Common
{
    public class DateTimeHelper
    {
        public static string GetDateString(DateTime dt)
        {
            string result = "";
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            #region 先不用
            //TimeSpan span = DateTime.Now - dt;
            //if (span.TotalDays > 60)
            //{
            //    return dt.ToString("yyyy:MM:dd");
            //}
            //else
            //{
            //    if (span.TotalDays > 30)
            //    {
            //        return
            //        "1个月前";
            //    }
            //    else
            //    {
            //        if (span.TotalDays > 14)
            //        {
            //            return
            //            "2周前";
            //        }
            //        else
            //        {
            //            if (span.TotalDays > 7)
            //            {
            //                return
            //                "1周前";
            //            }
            //            else
            //            {
            //                if (span.TotalDays > 1)
            //                {
            //                    return
            //                    string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
            //                }
            //                else
            //                {
            //                    return dt.ToString("HH:mm:ss");
            //                }
            //            }
            //        }
            //    }
            //} 
            #endregion
            return result;
        }
    }
}
