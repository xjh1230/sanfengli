using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Common
{
    public class DataTableToList<T> where T : new()
    {
        public static List<T> GetPageByLinq(List<T> listT, int PageIndex, int PageSize)
        {
            if (listT == null)
            {
                return null;
            }
            if (listT.Count == 0)
            {
                return new List<T>();
            }

            return listT.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
        public static List<T> TableToList(DataTable dt)
        {
            List<T> lt = new List<T>();//构建泛型集合
            if (dt == null || dt.Rows.Count == 0)
            {
                return lt;
            }
            Type objType = typeof(T);//获取类的类型
            foreach (DataRow dr in dt.Rows)
            {
                object obj = System.Activator.CreateInstance(objType);
                foreach (System.Reflection.PropertyInfo pi in objType.GetProperties())
                {
                    if (pi.PropertyType.IsPublic && pi.CanWrite && dt.Columns.Contains(pi.Name))
                    {
                        Type pType = Type.GetType(pi.PropertyType.FullName);//字段类型   
                        if (dr[pi.Name] != null && !string.IsNullOrWhiteSpace(dr[pi.Name].ToString()) && dr[pi.Name] != DBNull.Value)
                        {
                            object value = SD_ChanageType(dr[pi.Name], pType);
                            objType.GetProperty(pi.Name).SetValue(obj, value, null);
                        }

                    }
                }
                lt.Add((T)obj);
            }
            return lt;
        }

        /// <param name="value"></param>
        /// <param name="convertType"></param>
        /// <returns></returns>
        public static object SD_ChanageType(object value, Type convertsionType)
        {
            if (convertsionType.IsGenericType && convertsionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null || value.ToString().Length == 0)
                {
                    return null;
                }
                NullableConverter nullableConverter = new NullableConverter(convertsionType);
                convertsionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, convertsionType);
        }
    }
}
