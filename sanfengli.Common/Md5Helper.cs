using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Common
{
    public class Md5Helper
    {
        /// <summary>md5加密字符串
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string Md5(string val, string encoding = "UTF-8")
        {
            byte[] data = Encoding.GetEncoding(encoding).GetBytes(val);
            MD5 md5 = MD5CryptoServiceProvider.Create();
            byte[] result = md5.ComputeHash(data);
            //将加密后的数组以16进制转化为普遍字符串
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sBuilder.Append(result[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        //public void qct(string query)
        //{
        //    MD5 md5 = MD5.Create();
        //    byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(query.ToString()));
        //    StringBuilder result = new StringBuilder();

        //    for (int i = 0; i < bytes.Length; i++)
        //    {
        //        string hex = bytes[i].ToString("X");
        //        if (hex.Length == 1)
        //        {
        //            result.Append("0");
        //        }
        //        result.Append(hex);
        //    }
        //}
    }
}
