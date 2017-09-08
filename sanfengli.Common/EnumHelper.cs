using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Common
{
    public class EnumHelper
    {
        /// <summary>
        /// 获取枚举的描述[Description(描述)]
        /// </summary>
        /// <param name="e">枚举值</param>
        /// <returns></returns>
        public static string GetEnumDesc(System.Enum e)
        {
            FieldInfo enumInfo = e.GetType().GetField(e.ToString());
            if (enumInfo != null)
            {
                DescriptionAttribute[] enumAttributes = (DescriptionAttribute[])enumInfo.
              GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (enumAttributes.Length > 0)
                {
                    return enumAttributes[0].Description;
                }
            }
            return e.ToString();
        }

        /// <summary>
        /// 获取枚举的描述[Description(描述)]
        /// </summary>
        /// <param name="enumValue">枚举text</param>
        /// <param name="enumType">枚举type</param>
        /// <returns></returns>
        public static string GetDescriptionByEnumByProperty(string enumValue, Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException();
            }
            Type typeFromHandle = typeof(DescriptionAttribute);
            FieldInfo field = enumType.GetField(enumValue);
            object[] customAttributes = field.GetCustomAttributes(typeFromHandle, true);
            string result;
            if (customAttributes.Length > 0)
            {
                DescriptionAttribute descriptionAttribute = (DescriptionAttribute)customAttributes[0];
                result = descriptionAttribute.Description;
            }
            else
            {
                result = field.Name;
            }
            return result;
        }
        /// <summary>
        /// 枚举转换为数据源（值为key，描述为value）
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="isIncludeBlank">是否包含空选项</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetCollectionOfEnumWithDesc(Type enumType, bool isIncludeBlank)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (isIncludeBlank)
            {
                dic.Add("", "--请选择--");
            }
            int[] keys = (int[])System.Enum.GetValues(enumType);
            string[] names = System.Enum.GetNames(enumType);
            for (int i = 0; i < names.Length; i++)
            {
                string text = names[i];
                int key = keys[i];
                string description = GetDescriptionByEnumByProperty(text, enumType);
                dic.Add(key.ToString(), description);
            }
            return dic;
        }

        /// <summary>
        /// 将枚举转换为数据源（值为key，Name为value）
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="isIncludeBlank">是否包含控制</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetCollectionOfEnum(Type enumType, bool isIncludeBlank)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (isIncludeBlank)
            {
                dic.Add("0", "--请选择--");
            }
            int[] keys = (int[])System.Enum.GetValues(enumType);
            string[] names = System.Enum.GetNames(enumType);
            for (int i = 0; i < names.Length; i++)
            {
                string text = names[i];
                int key = keys[i];
                dic.Add(key.ToString(), text);
            }
            return dic;
        }
        /// <summary>
        /// 将枚举转换为数据源（值为key，Name为value）
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="isIncludeBlank"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetCollectionOfEnumAll(Type enumType, bool isIncludeBlank)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (isIncludeBlank)
            {
                dic.Add("-1", "全部");
            }
            int[] keys = (int[])System.Enum.GetValues(enumType);
            string[] names = System.Enum.GetNames(enumType);
            for (int i = 0; i < names.Length; i++)
            {
                string text = names[i];
                int key = keys[i];
                dic.Add(key.ToString(), text);
            }
            return dic;
        }

        /// <summary>
        /// 数字是否在枚举内
        /// </summary>
        /// <param name="value"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static bool IsInEnum(int value, Type enumType)
        {
            return System.Enum.GetValues(enumType).Cast<int>().Any(i => value == i);
        }
    }
}
