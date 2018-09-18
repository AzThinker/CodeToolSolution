using System.Configuration;

namespace MetaWorkLib.Config
{
    /// <summary>
    /// 基础设置
    /// </summary>
    public class AzBaseSet
    {
        private XmlSettings xmlSettings;

        public AzBaseSet()
        {
            string xmlpath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            xmlSettings = new XmlSettings(xmlpath + @"ConfigurationBaseSet.xml");
            Refresh();
        }

        public void Refresh()
        {
            AzClassPrefix = xmlSettings.readString("azclassprefix");
            AzDbSqlConnectionName = xmlSettings.readString("azdbsqlconnectionname");
            AzNick = xmlSettings.readString("aznick");
            AzProjectName = xmlSettings.readString("azprojectname");
            AzProjectSpace = xmlSettings.readString("azprojectspace");
            AzTablePrefix = xmlSettings.readString("aztableprefix");
            AzTemplateFolder = xmlSettings.readString("aztemplatefolder");
            AzToolTable = xmlSettings.readString("aztooltable");
            AzSaveCodeFileFloder = xmlSettings.readString("azsavecodefilefloder");
            AzConnectionString = xmlSettings.readString("azconnectionstring");
        }
        private string azToolTable;
        /// <summary>
        /// 工具表标识
        /// </summary>
        public string AzToolTable
        {
            get { return azToolTable; }
            set
            {
                azToolTable = value;
            }

        }
        private string azTemplateFolder;
        /// <summary>
        /// 代码模板
        /// </summary>
        public string AzTemplateFolder
        {
            get { return azTemplateFolder; }
            set
            {
                azTemplateFolder = value;
            }
        }
        private string azCodeFileFloder;
        /// <summary>
        /// 代码模板
        /// </summary>
        public string AzSaveCodeFileFloder
        {
            get { return azCodeFileFloder; }
            set
            {
                azCodeFileFloder = value;
            }

        }
        private string azNick;
        /// <summary>
        /// 解决方案昵称
        /// </summary>
        public string AzNick
        {
            get { return azNick; }
            set
            {
                azNick = value;
            }
        }
        private string azProjectSpace;
        /// <summary>
        /// 项目空间名
        /// </summary>
        public string AzProjectSpace
        {
            get { return azProjectSpace; }
            set
            {
                azProjectSpace = value;
            }
        }
        private string azProjectName;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string AzProjectName
        {
            get { return azProjectName; }
            set
            {
                azProjectName = value;
            }
        }
        private string azDbSqlConnectionName;
        /// <summary>
        /// 数据库连接名
        /// </summary>
        public string AzDbSqlConnectionName
        {
            get { return azDbSqlConnectionName; }
            set
            {
                azDbSqlConnectionName = value;
            }
        }
        private string azClassPrefix;
        /// <summary>
        /// 类前缀
        /// </summary>
        public string AzClassPrefix
        {
            get { return azClassPrefix; }
            set
            {
                azClassPrefix = value;
            }
        }

        private string azTablePrefix;
        /// <summary>
        /// 表前缀
        /// </summary>
        public string AzTablePrefix
        {
            get { return azTablePrefix; }
            set
            {
                azTablePrefix = value;
            }
        }

        private string azConnectionString;

        public string AzConnectionString
        {
            get { return azConnectionString; }
            set
            {
                azConnectionString = value;
            }
        }

        /// <summary>
        /// 获取基础设置
        /// </summary>
        /// <returns></returns>
        public static AzBaseSet GetBaseSet()
        {
            return new AzBaseSet();
        }

        /// <summary>
        /// 保存基础设置
        /// </summary>
        /// <param name="azBase"></param>
        public static void AzSetBase(AzBaseSet azBase)
        {
            azBase.xmlSettings.trySetString("azclassprefix", azBase.AzClassPrefix);
            azBase.xmlSettings.trySetString("azdbsqlconnectionname", azBase.AzDbSqlConnectionName);
            azBase.xmlSettings.trySetString("aznick", azBase.AzNick);
            azBase.xmlSettings.trySetString("azprojectname", azBase.AzProjectName);
            azBase.xmlSettings.trySetString("azprojectspace", azBase.AzProjectSpace);
            azBase.xmlSettings.trySetString("aztableprefix", azBase.AzTablePrefix);
            azBase.xmlSettings.trySetString("aztemplatefolder", azBase.AzTemplateFolder);
            azBase.xmlSettings.trySetString("aztooltable", azBase.AzToolTable);
            azBase.xmlSettings.trySetString("azsavecodefilefloder", azBase.AzSaveCodeFileFloder);
            azBase.Refresh();
        }
        public static void AzSetBaseCnns(AzBaseSet azBase)
        {
            azBase.xmlSettings.trySetString("azconnectionstring", azBase.AzConnectionString);
            azBase.Refresh();
        }
    }
}
