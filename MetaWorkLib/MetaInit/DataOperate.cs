using MetaWorkLib.Config;
using MetaWorkLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorkLib.MetaInit
{
    public static class DataOperate
    {



        public static string CreateDBObject(AzMetaTableEntity azMetaTable, IEnumerable<AzMetaCustomCloumEntity> azCloumListEntity)
        {
            if (azMetaTable == null)
            {
                throw new Exception(" 元数主表类为null，不能生成！");
            }
            if (azCloumListEntity == null)
            {
                throw new Exception(" 元数属性null，不能生成！");
            }
            string createSql = $" CREATE TABLE [dbo].[{azMetaTable.SchemaName}](";
            string identitySql = " [{0}] [{1}]  IDENTITY(1,1) NOT NULL,";
            string normalSql = " [{0}] [{1}] {2},";
            string haveLenSql = " [{0}] [{1}] ({2}) {3},";
            string constraintSql = " CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED({1})) ON [PRIMARY]";

            string nullstr = "";
            string keyid = "";
            string tempstr = "";
            int idCount = 0;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(createSql);

            foreach (var item in azCloumListEntity)
            {
                nullstr = item.IsNullable ?? true ? " Not NULL" : " NULL";

                if (item.IsKeyField ?? true)

                {
                    keyid = (string.IsNullOrWhiteSpace(keyid)) ? keyid = $" {item.FldName} ASC" : $"{keyid},{item.FldName} ASC";
                }

                if (item.FldType.ToLower().Contains("char"))
                {
                    tempstr = string.Format(haveLenSql, item.FldName, item.FldType, item.FldLen, nullstr);
                }
                else
                {
                    if (item.IsIdentity ?? true)
                    {
                        tempstr = string.Format(identitySql, item.FldName, item.FldType);
                        idCount += 1;
                        if (idCount > 1)
                        {
                            throw new Exception(string.Format(" '{0}'有多个自增字段，不能生成！", azMetaTable.ClassName));
                        }
                    }
                    else
                    {
                        tempstr = string.Format(normalSql, item.FldName, item.FldType, nullstr);
                    }

                }
                stringBuilder.Append(tempstr);
            }

            if (string.IsNullOrWhiteSpace(keyid))
            {
                throw new Exception(string.Format(" '{0}'没有设置关键字段，不能生成！", azMetaTable.ClassName));
            }
            tempstr = string.Format(constraintSql, azMetaTable.SchemaName, keyid);
            stringBuilder.Append(tempstr);
            stringBuilder.Append($"; UPDATE [dbo].[{AzNormalSet.GetAzNormalSet().AzMetaCloumName}] SET TpyeId=1 where TableName='{azMetaTable.SchemaName}'");

            return stringBuilder.ToString();
        }

    }
}
