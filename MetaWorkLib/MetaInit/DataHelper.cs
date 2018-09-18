using MetaWorkLib.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MetaWorkLib.MetaInit
{
    public class DataHelper
    {

        private static List<string> ShowDataTableName(DataTable table)
        {
            return (from d in table.AsEnumerable()
                    orderby d.Field<string>("TABLE_NAME")
                    select d.Field<string>("TABLE_NAME")).ToList<string>();
        }

        public static List<string> GetGetDBListForProperty()
        {
            List<string> list = new List<string> { ""};
            list.AddRange(GetDBList());
            return list;
        }
        public static List<string> GetDBList()
        {
            string dbConnectionString = AzNormalSet.GetAzNormalSet().AzConnectionString;
            using (SqlConnection cn = new SqlConnection(dbConnectionString))
            {
                cn.Open();
                List<string> list = new List<string>();
                DataTable allTablesSchemaTable = cn.GetSchema("Tables");
                list= ShowDataTableName(allTablesSchemaTable);
                DataTable allViewSchemaTable = cn.GetSchema("Views");
                list.AddRange(ShowDataTableName(allViewSchemaTable));
                return list;
            }
        }

        /// <summary>
        /// 数据表是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static bool DbTableNameExist(string tableName)
        {
            string dbConnectionString = AzNormalSet.GetAzNormalSet().AzConnectionString;
            using (SqlConnection cn = new SqlConnection(dbConnectionString))
            {
                cn.Open();
                DataTable allSchemaTable = cn.GetSchema("Tables");

                DataTable allSchemaView = cn.GetSchema("Views");

                bool tablesCount = (from d in allSchemaTable.AsEnumerable()
                                    where d.Field<string>("TABLE_NAME") == tableName
                                    select d.Field<string>("TABLE_NAME")).Count<string>() > 0;
                bool viewsCount = (from d in allSchemaView.AsEnumerable()
                                   where d.Field<string>("TABLE_NAME") == tableName
                                   select d.Field<string>("TABLE_NAME")).Count<string>() > 0;
                return tablesCount || viewsCount;
            }


        }

        public static DataTable LookDbData(string tableName, int top = 100)
        {
            string dbConnectionString = AzNormalSet.GetAzNormalSet().AzConnectionString;
            string querystr = $"select top {top} *  from [{tableName}] ";
            using (SqlConnection cn = new SqlConnection(dbConnectionString))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand(querystr, cn))
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    return dataTable;
                }
            }

        }

    }
}
