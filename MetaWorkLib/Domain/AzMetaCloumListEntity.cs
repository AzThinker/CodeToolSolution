using SqlRepoEx.Abstractions;
using SqlRepoEx.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorkLib.Domain
{
    public class AzMetaCloumListEntity : List<AzMetaCloumEntity>
    {
        public AzMetaCloumListEntity()
        {

        }
        public static AzMetaCloumListEntity GetCustomCloumEntities(ISelectStatement<AzMetaCloumEntity> sqlStatement)
        {
            return ((SelectStatement<AzMetaCloumEntity>)sqlStatement).ListEntityGo<AzMetaCloumListEntity>();
        }
    }
}
