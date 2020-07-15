using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Appendesk
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static void ToDataTable<T>(this DataTable dt, List<T> items)
        {
            if (dt == null)
            {
                dt = new DataTable(typeof(T).Name);
            }

            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                var t = DataTableHelper.GetCoreType(prop.PropertyType);
                dt.Columns.Add(prop.Name, t);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];

                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dt.Rows.Add(values);
            }
        }
    }
}