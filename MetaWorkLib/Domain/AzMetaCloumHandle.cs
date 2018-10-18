using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaWorkLib.Config;
using MetaWorkLib.Utils;
using SqlRepoEx.Abstractions;

namespace MetaWorkLib.Domain
{
    public class AzMetaCloumHandle : AtkHandle<AzMetaCloumEntity>
    {
        private AzMetaCloumHandle() : base()
        {
        }

        public static AzMetaCloumHandle Handle()
        {
            return new AzMetaCloumHandle();
        }
        public IEnumerable<AzMetaCloumEntity> NewAdd(string addtablename)
        {
            List<AzMetaCloumEntity> azMetas = new List<AzMetaCloumEntity>();
            azMetas.Add(new AzMetaCloumEntity
            {
                Id = 0,
                FldCodeType = "",
                FldLen = 0,
                FldLenCode = 0,
                FldName = "",
                FldDisplay = "",
                FldType = "",
                IsIdentity = false,
                IsKeyField = false,
                IsNullable = false,
                IsRequired = false,
                IsSelect = false,
                ShowOrder = 0,
                TableName = addtablename,
            });
            return azMetas;
        }
        public ISelectStatement<AzMetaCloumEntity> Select()
        {
            return repository.Query()
                  .UsingTableName(azNormalSet.AzMetaCloumName);
        }

        public AzMetaCloumEntity SelectOne(AzMetaCloumEntity entity)
        {
            return repository.Query()
                  .UsingTableName(azNormalSet.AzMetaCloumName).Where(m => m.Id == entity.Id).Go().FirstOrDefault();
        }

        public void GenerateOne(AzMetaTableEntity azMetaTable)
        {

            MetadataOperate.ImportCustomMetaData(azMetaTable.SchemaName, azMetaTable.SchemaName);


        }

        public AzMetaCloumEntity Insert(AzMetaCloumEntity entity)
        {
            var result = repository.Insert().UsingTableName(azNormalSet.AzMetaCloumName).For(entity).Go();

        

            InitOneColumnValueSchema(entity.TableName, entity.FldName);

            return result;

        }


        public int Update(AzMetaCloumEntity entity)
        {
            if (entity == null)
            {
                return 0;
            }

            return repository.Update().UsingTableName(azNormalSet.AzMetaCloumName).For(entity).Go();

        }

        public int Delete(AzMetaCloumEntity entity)
        {
            if (entity == null)
            {
                return 0;
            }

            return repository.Delete().UsingTableName(azNormalSet.AzMetaCloumName).For(entity).Go();
        }


        public int DeleteAll(string deltablename)
        {
            return repository.Delete().UsingTableName(azNormalSet.AzMetaCloumName).Where(m => m.TableName == deltablename).Go();
        }

        public int UpdataDisplayFormOtherSchemaName(string currentSchemaName, string copyfromSchemaName)
        {
            string sql = MetadataOperate.DbCreateLoad(BaseConstants.Az_CopyMetaCloumfile)
                         .ReaplaceTemplate(BaseConstants.Az_Parameters1, currentSchemaName.AddSingleQuotes())
                            .ReaplaceTemplate(BaseConstants.Az_Parameters2, copyfromSchemaName.AddSingleQuotes());

            return repository.ExecuteNonQuerySql().WithSql(sql).Go();

        }

        public int InitColumnSchema(string initSchemaName)
        {
            string sql = MetadataOperate.DbCreateLoad(BaseConstants.Az_MetaDataMList_IniOnefile)
                         .ReaplaceTemplate(BaseConstants.Az_Parameters1, initSchemaName.AddSingleQuotes())
                         .ReaplaceTemplate(BaseConstants.AppNameDefautSign, MetadataOperate.GetDefAppNameUpdate(azNormalSet.AzBase.AzTablePrefix)); ;

            return repository.ExecuteNonQuerySql().WithSql(sql).Go();

        }

        public int InitColumnValueSchema(string initSchemaName)
        {
            string sql = MetadataOperate.DbCreateLoad(BaseConstants.Az_InitColumnValueSchemafile)
                         .ReaplaceTemplate(BaseConstants.Az_Parameters1, initSchemaName.AddSingleQuotes())
                         .ReaplaceTemplate(BaseConstants.AppNameDefautSign, MetadataOperate.GetDefAppNameUpdate(azNormalSet.AzBase.AzTablePrefix)); ;

            return repository.ExecuteNonQuerySql().WithSql(sql).Go();

        }

        public int InitOneColumnValueSchema(string initSchemaName, string fldName)
        {
            string sql = MetadataOperate.DbCreateLoad(BaseConstants.Az_InitOneColumnValueSchemafile)
                         .ReaplaceTemplate(BaseConstants.Az_Parameters1, initSchemaName.AddSingleQuotes())
                          .ReaplaceTemplate(BaseConstants.Az_Parameters2, fldName.AddSingleQuotes())
                         .ReaplaceTemplate(BaseConstants.AppNameDefautSign, MetadataOperate.GetDefAppNameUpdate(azNormalSet.AzBase.AzTablePrefix)); ;

            return repository.ExecuteNonQuerySql().WithSql(sql).Go();

        }
    }
}
