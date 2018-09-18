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
    public abstract class AtkHandle<TEntity> where TEntity : class, new()
    {
        protected AzNormalSet azNormalSet;
        protected IRepository<TEntity> repository;
        protected AtkHandle()
        {
            azNormalSet = AzNormalSet.GetAzNormalSet();
            repository = RepoFactory.Create<TEntity>();
        }


    }
}
