using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sanfengli.Common
{
    public class EConvert
    {
        public static T ConvertTo<T>(object obj)
        {
            try
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            catch (Exception )
            {
                return default(T);
            }
            
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(long timeStamp)
        {
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = timeStamp * 10000000;
            TimeSpan nowTimeSpan = new TimeSpan(lTime);
            DateTime resultDateTime = dateTimeStart.Add(nowTimeSpan);
            return resultDateTime;
        }

        /// <summary>
        /// 将泛类型集合List类转换成DataTable
        /// </summary>
        /// <param name="entitys">泛类型集合</param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> entitys)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }
            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                dt.Columns.Add(entityProperties[i].Name);
            }
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }
        /// <summary>
        /// 转换可编辑div中表情符号
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ConvertEmojiHtml(string html)
        {
            html = html.Replace("<div>", "").Replace("</div>", "\n").Replace("<br>", "").Replace("&nbsp;", "  ");
            string pattern = @"<img[^>]*>";
            string patterndata = @"\[\S*\]";
            foreach (Match match in Regex.Matches(html, pattern))
            {
                string strimg = match.Value;
                string code = Regex.Match(strimg, patterndata).Value;
                html = html.Replace(strimg, code);
            }
            return html;
        }

        public static string DConvertEmojiHtml(string msg)
        {
            msg=msg.Replace("\\", "");
            string[] strs = msg.Split('\n');
            string result = "";
            if (strs.Length > 0)
            {
                foreach (var item in strs)
                {
                    result += $"<div>{item}</div>";
                }
                return result;
            }
            else
            {
                return msg;
            }
            
        }
    }
}
