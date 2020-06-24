using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TD.Common
{
   public class ObjectHelper
    {
        #region 对象去空格
        /// <summary>
        /// 过滤字符类型的空格
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T ObjTrimString<T>(T t)
        {
            try
            {
                if (t == null)
                    return default(T);
                Type type = t.GetType();
                PropertyInfo[] props = type.GetProperties();

                Parallel.ForEach(props, p =>
                {
                    if (p.PropertyType.Name.Equals("String"))
                    {
                        if (p.GetValue(t, null) != null)
                        {
                            var tmp = (string)p.GetValue(t, null);
                            p.SetValue(t, tmp.Trim(), null);
                        }
                    }
                });

                return t;
            }
            catch
            {
                return default(T);
            }
        }
        /// <summary>
        /// 过滤字符类型的空格
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tt"></param>
        /// <returns></returns>
        public static List<T> ObjTrimString<T>(List<T> tt)
        {
            try
            {
                if (tt == null)
                    return default(List<T>);
                foreach (T t in tt)
                {
                    Type type = t.GetType();
                    PropertyInfo[] props = type.GetProperties();

                    Parallel.ForEach(props, p =>
                    {
                        if (p.PropertyType.Name.Equals("String"))
                        {
                            if (p.GetValue(t, null) != null)
                            {
                                var tmp = (string)p.GetValue(t, null);
                                p.SetValue(t, tmp.Trim(), null);
                            }

                        }
                    });
                }
                return tt;
            }
            catch
            {
                return default(List<T>);
            }
        }
        /// <summary>
        /// 对象去除去除空格
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static object ObjTrimString(object obj)
        {
            try
            {
                Type type = obj.GetType();
                PropertyInfo[] props = type.GetProperties();

                Parallel.ForEach(props, p =>
                {
                    if (p.PropertyType.Name.Equals("String"))
                    {
                        var tmp = (string)p.GetValue(obj, null);
                        p.SetValue(obj, tmp.Trim(), null);
                    }
                });

                return obj;
            }
            catch
            {
                return obj;
            }
        }

        /// <summary>
        /// 字典objcet值去空格
        /// </summary>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static Dictionary<string, object> KeyValuePairsTrim(Dictionary<string, object> pairs)
        {
            Dictionary<string, object> valuePairs = new Dictionary<string, object>();
            if (pairs != null && pairs.Count > 0)
            {
                foreach (var pair in pairs)
                {
                    valuePairs.Add(pair.Key, ObjTrimString(pair.Value));
                }
            }
            else
                return pairs;
            return valuePairs;
        }
        #endregion
    }
}
