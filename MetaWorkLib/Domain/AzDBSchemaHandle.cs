using MetaWorkLib.Config;
using SqlRepoEx.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorkLib.Domain
{
    public class AzDBSchemaHandle : AtkHandle<AzDBSchemaEntity>
    {

        private AzDBSchemaHandle() : base()
        {
        }

        public static AzDBSchemaHandle Handle()
        {
            return new AzDBSchemaHandle();
        }

        public List<AzDBSchemaEntity> GetDBSchema()
        {
            List<AzDBSchemaEntity> result = new List<AzDBSchemaEntity>();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" SELECT  TOP (100) PERCENT A.name AS ObjDataName, CASE WHEN A.xtype = 'U' THEN 1 WHEN A.xtype = 'V' THEN 2 ELSE 3 END AS ObjDataType, B.value as ObjDataDisplay");
            stringBuilder.Append(" FROM sys.sysobjects AS A LEFT OUTER JOIN");
            stringBuilder.Append(" (SELECT     TOP (100) PERCENT value, major_id");
            stringBuilder.Append(" FROM sys.extended_properties");
            stringBuilder.Append(" WHERE (name = N'MS_Description') AND (minor_id = 0)) AS B ON A.id = B.major_id");
            if (string.IsNullOrWhiteSpace(azNormalSet.AzBase.AzTablePrefix))
            {
                stringBuilder.Append(" WHERE   (A.name NOT LIKE N'sys%') And   (A.xtype = 'U' OR");
            }
            else
            {
                stringBuilder.Append($" WHERE  (A.name NOT LIKE N'sys%') And   (A.name LIKE   '{azNormalSet.AzBase.AzTablePrefix}%') AND (A.xtype = 'U' OR");
            }

            stringBuilder.Append($" A.xtype = 'V' OR A.xtype = 'P')  AND (NOT (A.name LIKE N'{BaseConstants.CodeToolName}%'))");
            stringBuilder.Append(" ORDER BY ObjDataName");
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
                            AzDBSchemaEntity schemaEntity = new AzDBSchemaEntity();
                            schemaEntity.Id = id;
                            schemaEntity.ObjDataName = atkDataReader["ObjDataName"] is DBNull ? null : (string)atkDataReader["ObjDataName"];
                            schemaEntity.ObjDataType = (int)atkDataReader["ObjDataType"]; ;
                            schemaEntity.ObjDataDisplay = atkDataReader["ObjDataDisplay"] is DBNull ? null : (string)atkDataReader["ObjDataDisplay"];
                            result.Add(schemaEntity);
                            id += 1;

                        }
                    }
                }
            }

            return result;
        }
    }
}
