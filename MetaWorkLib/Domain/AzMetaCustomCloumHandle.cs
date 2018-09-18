using MetaWorkLib.Config;
using SqlRepoEx.Abstractions;
using SqlRepoEx.SqlServer.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorkLib.Domain
{
    public class AzMetaCustomCloumHandle : AtkHandle<AzMetaCustomCloumEntity>
    {

        private AzMetaCustomCloumHandle() : base()
        {
        }

        public static AzMetaCustomCloumHandle Handle()
        {
            return new AzMetaCustomCloumHandle();
        }

        public IEnumerable<AzMetaCustomCloumEntity> NewAdd(string addtablename)
        {
            List<AzMetaCustomCloumEntity> azMetas = new List<AzMetaCustomCloumEntity>();
            azMetas.Add(new AzMetaCustomCloumEntity
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

        public ISelectStatement<AzMetaCustomCloumEntity> Select()
        {
            return repository.Query()
                  .UsingTableName(azNormalSet.AzMetaCloumName);
        }

        public AzMetaCustomCloumEntity SelectOne(AzMetaCustomCloumEntity entity)
        {
            return repository.Query()
                  .UsingTableName(azNormalSet.AzMetaCloumName).Where(m => m.Id == entity.Id).Go().FirstOrDefault();
        }

        public AzMetaCustomCloumEntity Insert(AzMetaCustomCloumEntity entity)
        {
            return repository.Insert().UsingTableName(azNormalSet.AzMetaCloumName).For(entity).Go();
        }

        public int UpdateBatch(AzMetaCustomCloumEntity entity)
        {
            if (entity == null)
            {
                return 0;
            }

            return repository.Update().UsingTableName(azNormalSet.AzMetaCloumName)
                .Set(m => m.FldLen, entity.FldLen)
                .Set(m => m.FldDisplay, entity.FldDisplay)
                .Set(m => m.IsNullable, entity.IsNullable)
                .Set(m => m.IsKeyField, entity.IsKeyField)
                .Set(m => m.ShowOrder, entity.ShowOrder)
                .Set(m => m.IsIdentity, entity.IsIdentity).Where(m => m.Id == entity.Id).Go();

        }

        public int Update(AzMetaCustomCloumEntity entity)
        {
            if (entity == null)
            {
                return 0;
            }

            return repository.Update().UsingTableName(azNormalSet.AzMetaCloumName).For(entity).Go();

        }

        public int Delete(AzMetaCustomCloumEntity entity)
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
    }
}
