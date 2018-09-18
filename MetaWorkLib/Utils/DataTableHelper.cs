using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorkLib.Utils
{
    public static class DataTableHelper
    {
        #region DataTale转为实体列表
        /// <summary>
        /// DataTale转为实体列表
        /// </summary>
        /// <typeparam name="TEntity">实体类类型</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List<T></returns>
        public static List<TEntity> DataTableToModelList<TEntity>(DataTable table)
        {
            List<TEntity> list = new List<TEntity>();
            TEntity t = default(TEntity);
            PropertyInfo[] propertypes = null;
            string tempName = string.Empty;
            foreach (DataRow row in table.Rows)
            {
                t = Activator.CreateInstance<TEntity>();
                propertypes = t.GetType().GetProperties();
                foreach (PropertyInfo pro in propertypes)
                {
                    tempName = pro.Name;
                    if (table.Columns.Contains(tempName))
                    {
                        object value = row[tempName];
                        if (value.GetType() == typeof(System.DBNull))
                        {
                            value = null;
                        }
                        pro.SetValue(t, value, null);
                    }
                }
                list.Add(t);
            }
            return list;
        }
        #endregion
        #region   DataRow转为实体类
        /// <summary>
        /// DataRow转为实体类
        /// </summary>
        /// <typeparam name="TEntity">实体类类型</typeparam>
        /// <param name="row">DataRow</param>
        /// <returns>T</returns>
        public static TEntity DataRowToModel<TEntity>(DataRow row)
        {

            TEntity t = default(TEntity);
            PropertyInfo[] propertypes = null;
            string tempName = string.Empty;
            t = Activator.CreateInstance<TEntity>();
            propertypes = t.GetType().GetProperties();
            foreach (PropertyInfo pro in propertypes)
            {
                tempName = pro.Name;
                if (row.Table.Columns.Contains(tempName))
                {
                    object value = row[tempName];
                    if (value.GetType() == typeof(System.DBNull))
                    {
                        value = null;
                    }
                    pro.SetValue(t, value, null);
                }
            }
            return t;
        }
        #endregion
    }
}
