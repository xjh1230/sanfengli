using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Common
{
    public class CalculateHelper
    {
        #region 根据时间戳获取唯一编号
        /// <summary>
        /// 生产唯一的编号
        /// </summary>
        /// <returns></returns>
        public static string CreateNumCode()
        {
            Random random = new Random();
            int rad = random.Next(100, 999);

            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString("yyMMddHHmmssfff"));
            sb.Append(rad);

            return sb.ToString();
        }

        /// <summary>
        /// 生产唯一的编号
        /// </summary>
        /// <returns></returns>
        public static string CreateNumCode(string code)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString("yyMMddHHmmss"));

            if (!string.IsNullOrEmpty(code))
                sb.Append(code);
            return sb.ToString();
        }


        public static string CreateLetterCode()
        {
            //验证码的字符集，去掉了一些容易混淆的字符  
            char[] numChar = { '0', '1', '2', '3', '4', '5', '6', '8', '9' };
            char[] letterChar = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M',
 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            StringBuilder sb = new StringBuilder();

            Random r = new Random();
            for (int i = 0; i < 3; i++)
            {
                var num = r.Next(letterChar.Length);
                sb.Append(letterChar[num]);
            }

            for (int i = 0; i < 6; i++)
            {
                var num = r.Next(numChar.Length);
                sb.Append(numChar[num]);
            }

            return sb.ToString();

        }
        #endregion
    }
}
