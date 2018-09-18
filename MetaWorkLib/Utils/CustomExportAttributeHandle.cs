using MetaWorkLib.CustomAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorkLib.Utils
{
    internal static class CustomExportAttributeHandle
    {
        private static bool ExportMetaField(this PropertyInfo propertyInfo)
        {

            return propertyInfo.GetCustomAttribute(typeof(ExportMetaAttribute)) != null;

        }


        private static string FieldName(this PropertyInfo propertyInfo)
        {

            var dis = propertyInfo.GetCustomAttribute(typeof(DisplayAttribute));
            if (dis != null)
            {
                return ((DisplayAttribute)dis).Name;
            }
            return propertyInfo.Name;
        }


        public static string FieldDisplayNames<TEntity>()
        {
            var propertys = typeof(TEntity).GetProperties()
                                             .Where(p => p.ExportMetaField());

            string result = "";
            foreach (var item in propertys)
            {
                 
                if (string.IsNullOrWhiteSpace(result))
                {
                    result = item.FieldName();
                }
                else
                {
                    result = result+"\t" + item.FieldName();
                }
            }

            return result;
        }


        public static string FieldDatas<TEntity>(this TEntity entity)
        {
            var propertys = typeof(TEntity).GetProperties()
                                             .Where(p => p.ExportMetaField());

            string result = "";
            foreach (var item in propertys)
            {
                string str = "";
                if (item.GetValue(entity)!=null)
                {
                    str = item.GetValue(entity).ToString();
                }
                if (string.IsNullOrWhiteSpace(result))
                {
                    result = str;
                }
                else
                {
                    result = result+ "\t" + str;
                }
            }

            return result+ "\r\n";
        }
    }
}
