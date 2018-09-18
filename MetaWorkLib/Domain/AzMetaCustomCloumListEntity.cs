using MetaWorkLib.Utils;
using SqlRepoEx.Abstractions;
using SqlRepoEx.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableTranslatorEx.Model;

namespace MetaWorkLib.Domain
{
    public class AzMetaCustomCloumListEntity : List<AzMetaCustomCloumEntity>
    {
        public AzMetaCustomCloumListEntity()
        {

        }
        public static AzMetaCustomCloumListEntity GetCustomCloumEntities(ISelectStatement<AzMetaCustomCloumEntity> sqlStatement)
        {
            return ((SelectStatement<AzMetaCustomCloumEntity>)sqlStatement).ListEntityGo<AzMetaCustomCloumListEntity>();
        }
    }

}
