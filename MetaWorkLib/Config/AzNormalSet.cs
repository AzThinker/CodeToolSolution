using SqlRepoEx;
using SqlRepoEx.SqlServer.ConnectionProviders;
using SqlRepoEx.SqlServer.Static;

namespace MetaWorkLib.Config
{
    public class AzNormalSet
    {
        private static AzNormalSet normalSet;

        private static readonly object locker = new object();

        public AzNormalSet()
        {
            //AzBaseSet baseSet = AzBaseSet.GetBaseSet();
            //baseSet.AzConnectionString = dialog.ConnectionString;
            var connectionString = AzBase.AzConnectionString;
            ConnectionStringConnectionProvider connectionProvider = new ConnectionStringConnectionProvider(connectionString);
            if (connectionProvider != null)
            {
                RepoFactory.UseConnectionProvider(connectionProvider);
                RepoFactory.UseLogger(new NoOpSqlLogger());
            }


        }

        public static AzNormalSet GetAzNormalSet()
        {
           

            if (normalSet == null)
            {
                lock (locker)
                {
                    if (normalSet == null)
                    {
                        normalSet = new AzNormalSet();



                    }
                }
            }
            return normalSet;
        }

        private string azConnectionString = string.Empty;
        public string AzConnectionString
        {
            get
            {
                if (azConnectionString == string.Empty)
                {
                    azConnectionString = normalSet.AzBase.AzConnectionString;// AzDataSourceSet.AzGetConnectionString();
                }
                return azConnectionString;
            }
            internal set => azConnectionString = value;
        }

        private AzBaseSet baseSet;

        /// <summary>
        /// 基础设置
        /// </summary>
        public AzBaseSet AzBase
        {
            get
            {
                if (baseSet == null)
                {
                    baseSet = AzBaseSet.GetBaseSet();

                }
                return baseSet;
            }
            internal set
            {
                baseSet = value;
                AzMetaTableName = string.Format(BaseConstants.MetaTableNameCon, AzBase.AzToolTable);
                AzMetaCloumName = string.Format(BaseConstants.MetaCloumNameCon, AzBase.AzToolTable);
                AzMetaQueryView = string.Format(BaseConstants.MetaQueryViewCon, AzBase.AzToolTable);
            }
        }

        private string azMetaTableName = string.Empty;

        /// <summary>
        /// 元数据表信息表名
        /// </summary>
        public string AzMetaTableName
        {
            get
            {
                if (azMetaTableName == string.Empty)
                {
                    azMetaTableName = string.Format(BaseConstants.MetaTableNameCon, AzBase.AzToolTable);
                }
                return azMetaTableName;
            }
            internal set => azMetaTableName = value;
        }

        private string azMetaCloumName = string.Empty;

        /// <summary>
        /// 元数据列信息表名
        /// </summary>
        public string AzMetaCloumName
        {
            get
            {
                if (azMetaCloumName == string.Empty)
                {
                    azMetaCloumName = string.Format(BaseConstants.MetaCloumNameCon, AzBase.AzToolTable);
                }
                return azMetaCloumName;
            }
            internal set => azMetaCloumName = value;
        }

        private string azMetaQueryView = string.Empty;

        /// <summary>
        /// 元数据查询视图
        /// </summary>
        public string AzMetaQueryView
        {
            get
            {
                if (azMetaQueryView == string.Empty)
                {
                    azMetaQueryView = string.Format(BaseConstants.MetaQueryViewCon, AzBase.AzToolTable);
                }
                return azMetaQueryView;
            }
            internal set => azMetaQueryView = value;
        }
    }
}
