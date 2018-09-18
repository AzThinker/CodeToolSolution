using MetaWorkLib.Config;
using SqlRepoEx.Abstractions;
using SqlRepoEx.SqlServer.Static;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorkLib.Domain
{
    public class AzMetaTableHandle : AtkHandle<AzMetaTableEntity>
    {

        private AzMetaTableHandle() : base()
        {
        }

        public static AzMetaTableHandle Handle()
        {
            return new AzMetaTableHandle();
        }


        public ISelectStatement<AzMetaTableEntity> Select()
        {
            return repository.Query()
                  .From(tableName: azNormalSet.AzMetaTableName);
        }

        public AzMetaTableEntity Insert(AzMetaTableEntity entity)
        {
            return repository.Insert().UsingTableName(azNormalSet.AzMetaTableName).For(entity).Go();
        }

        public int Updata(AzMetaTableEntity entity)
        {
            return repository.Update().UsingTableName(azNormalSet.AzMetaTableName).For(entity).Go();
        }

        public int UpdataCodeSetVales()
        {
            return repository.Update()
                    .UsingTableName(azNormalSet.AzMetaTableName)
                    .Set(m => m.CodeSetVales, string.Empty)
                    .Where(m => m.AppName == MetadataOperate.GetDefAppNameUpdate(azNormalSet.AzBase.AzTablePrefix))
                    .Go();
        }
        public int Delete(string delkey)
        {
            return repository.Delete().UsingTableName(azNormalSet.AzMetaTableName).Where(c => c.ClassName == delkey).Go();
        }

        private string ReMovePer(string oldvalue)
        {
            string preFix = "";
            if (!string.IsNullOrWhiteSpace(azNormalSet.AzBase.AzTablePrefix))
            {
                preFix = azNormalSet.AzBase.AzTablePrefix + "_";
                if (oldvalue.StartsWith(preFix))
                {
                    return oldvalue.Remove(0, preFix.Length);
                }
                return oldvalue;
            }
            else
            {
                return oldvalue;
            }

        }
        public List<AzMetaTableEntity> GetDBSchema()
        {
            List<AzMetaTableEntity> result = new List<AzMetaTableEntity>();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" SELECT  TOP (100) PERCENT A.name AS SchemaName, CASE WHEN A.xtype = 'U' THEN 1 WHEN A.xtype = 'V' THEN 2 ELSE 3 END AS ObjDataType, B.value as ClassDisPlay");
            stringBuilder.Append(" FROM sys.sysobjects AS A LEFT OUTER JOIN");
            stringBuilder.Append(" (SELECT     TOP (100) PERCENT value, major_id");
            stringBuilder.Append(" FROM sys.extended_properties");
            stringBuilder.Append(" WHERE (name = N'MS_Description') AND (minor_id = 0)) AS B ON A.id = B.major_id");
            if (string.IsNullOrWhiteSpace(azNormalSet.AzBase.AzTablePrefix))
            {
                stringBuilder.Append(" WHERE (NOT (A.name LIKE N'sys%')) AND (NOT (A.name LIKE N'sp_%')) And(A.xtype = 'U' OR");
            }
            else
            {
                stringBuilder.Append($" WHERE (NOT (A.name LIKE N'sys%')) AND (NOT (A.name LIKE N'sp_%')) And     (A.name LIKE   '{azNormalSet.AzBase.AzTablePrefix}%') AND (A.xtype = 'U' OR");
            }

            stringBuilder.Append($" A.xtype = 'V' OR A.xtype = 'P')  AND (NOT (A.name LIKE N'{BaseConstants.CodeToolName}%'))");
            stringBuilder.Append(" ORDER BY SchemaName");
            string cmdstr = stringBuilder.ToString();
            string dbConnectionString = azNormalSet.AzConnectionString;
            using (SqlConnection cn = new SqlConnection(dbConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(cmdstr, cn))
                {
                    using (SqlDataReader atkDataReader = cmd.ExecuteReader())
                    {
                        int id = 1;
                        while (atkDataReader.Read())
                        {
                            string scname = atkDataReader["SchemaName"] is DBNull ? null : (string)atkDataReader["SchemaName"];
                            string objname = ReMovePer(scname);
                            AzMetaTableEntity schemaEntity = new AzMetaTableEntity();


                            schemaEntity.Id = id;
                            schemaEntity.ClassName = azNormalSet.AzBase.AzClassPrefix + objname;
                            schemaEntity.ObjModeName = objname;
                            schemaEntity.SchemaName = scname;
                            schemaEntity.AppName = MetadataOperate.GetDefAppNameUpdate(azNormalSet.AzBase.AzTablePrefix);
                            schemaEntity.ObjDataType = (int)atkDataReader["ObjDataType"]; ;
                            schemaEntity.ClassDisPlay = atkDataReader["ClassDisPlay"] is DBNull ? null : (string)atkDataReader["ClassDisPlay"];
                            schemaEntity.ObjModeType = 1;

                            result.Add(schemaEntity);
                            id += 1;

                        }
                    }
                }
            }

            return result;
        }


        public List<string> GetDBTableList()
        {
            List<string> result = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" SELECT TOP (100) PERCENT name AS SchemaName");
            stringBuilder.Append(" FROM sys.sysobjects AS A");
            if (string.IsNullOrWhiteSpace(azNormalSet.AzBase.AzTablePrefix))
            {
                stringBuilder.Append(" WHERE (NOT (A.name LIKE N'sys%')) AND (NOT (A.name LIKE N'sp_%'))  And   (A.xtype = 'U' OR");
            }
            else
            {
                stringBuilder.Append($" WHERE (NOT (A.name LIKE N'sys%')) AND (NOT (A.name LIKE N'sp_%'))  And   (A.name LIKE   '{azNormalSet.AzBase.AzTablePrefix}%') AND (A.xtype = 'U' OR");
            }
            stringBuilder.Append($" A.xtype = 'V' OR A.xtype = 'P')  AND (NOT (A.name LIKE N'{BaseConstants.CodeToolName}%'))");
            stringBuilder.Append(" ORDER BY SchemaName");
            string cmdstr = stringBuilder.ToString();
            string dbConnectionString = azNormalSet.AzConnectionString;
            using (SqlConnection cn = new SqlConnection(dbConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(cmdstr, cn))
                {
                    using (SqlDataReader atkDataReader = cmd.ExecuteReader())
                    {
                        while (atkDataReader.Read())
                        {
                            string scname = atkDataReader["SchemaName"] is DBNull ? null : (string)atkDataReader["SchemaName"];
                            result.Add(scname);
                        }
                    }
                }
            }

            return result;
        }

        public List<string> GetAllDBTableList()
        {
            List<string> result = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" SELECT TOP (100) PERCENT name AS SchemaName");
            stringBuilder.Append(" FROM sys.sysobjects AS A");
            stringBuilder.Append(" WHERE   (A.xtype = 'U' OR");
            stringBuilder.Append($" A.xtype = 'V' OR A.xtype = 'P')");
            stringBuilder.Append(" ORDER BY SchemaName");
            string cmdstr = stringBuilder.ToString();
            string dbConnectionString = azNormalSet.AzConnectionString;
            using (SqlConnection cn = new SqlConnection(dbConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(cmdstr, cn))
                {
                    using (SqlDataReader atkDataReader = cmd.ExecuteReader())
                    {
                        while (atkDataReader.Read())
                        {
                            string scname = atkDataReader["SchemaName"] is DBNull ? null : (string)atkDataReader["SchemaName"];
                            result.Add(scname);
                        }
                    }
                }
            }

            return result;
        }

        public List<string> GetDBTableFieldList(string tablename)
        {
            List<string> result = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" SELECT TOP (100) PERCENT A.name AS FldName");
            stringBuilder.Append(" FROM sys.syscolumns AS A LEFT OUTER JOIN");
            stringBuilder.Append(" sys.sysobjects AS B ON A.id = B.id");
            stringBuilder.Append($" WHERE (NOT (B.name LIKE N'{BaseConstants.CodeToolName}%')) AND (B.name = '{tablename}')");
            stringBuilder.Append(" ORDER BY FldName");
            string cmdstr = stringBuilder.ToString();
            string dbConnectionString = azNormalSet.AzConnectionString;
            using (SqlConnection cn = new SqlConnection(dbConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(cmdstr, cn))
                {
                    using (SqlDataReader atkDataReader = cmd.ExecuteReader())
                    {

                        while (atkDataReader.Read())
                        {
                            string scname = atkDataReader["FldName"] is DBNull ? null : (string)atkDataReader["FldName"];
                            result.Add(scname);


                        }
                    }
                }
            }

            return result;
        }
        public int ReNameTableDisplay(string newName, string tableName, string objType)
        {
            return repository
                .ExecuteNonQueryProcedure()
                .WithName(BaseConstants.Az_StoreProcedureUpdateRemark)
                .WithParameter("@Remark", newName)
                .WithParameter("@ObjType", objType)
                .WithParameter("@ObjName", tableName)
                .Go();


        }
        public int ReNameColumnDisplay(string newName, string tableName, string objType, string columnName)
        {
            return repository
                .ExecuteNonQueryProcedure()
                .WithName(BaseConstants.Az_StoreProcedureUpdateRemark)
                .WithParameter("@Remark", newName)
                .WithParameter("@ObjType", objType)
                .WithParameter("@ObjName", tableName)
                .WithParameter("@ColumnName", columnName)
                .Go();


        }


    }
}
