using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorkLib.Config
{
    /// <summary>
    /// 程序应用所操作的数据库设置
    /// </summary>
    public class AzDataSourceSet : ConfigurationSection
    {
        //[ConfigurationProperty("azconnectionstring", IsRequired = true)]
        //public string AzConnectionString
        //{
        //    get { return this["azconnectionstring"].ToString(); }
        //    set { this["azconnectionstring"] = value; }
        //}

        ///// <summary>
        ///// 获取数据连接
        ///// </summary>
        ///// <returns></returns>
        //public static string AzGetConnectionString()
        //{
        //    AzDataSourceSet azDataSourceSet = ConfigurationManager.GetSection("AzDataSource") as AzDataSourceSet;
        //    if (azDataSourceSet == null)
        //    {
        //        //增加此处代码，主要解决“C#设计界面时，未将对象引用设置到对象实例问题解决方案”
        //        // 如无此行代码，winForm时，就会报错。
        //        return "";
        //    }
        //    else
        //    {
        //        return azDataSourceSet.AzConnectionString;
        //    }

        //}

        /// <summary>
        /// 设置数据库连接
        /// </summary>
        /// <param name="azsqllink"></param>
        public static void AzSetConnectionString(string azsqllink)
        {
            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //AzDataSourceSet azDataSourceSet = config.GetSection("AzDataSource") as AzDataSourceSet;
            //azDataSourceSet.AzConnectionString = azsqllink;

            //config.Save();
            AzNormalSet normalSet = AzNormalSet.GetAzNormalSet();
            normalSet.AzBase.AzConnectionString = azsqllink;
            AzBaseSet.AzSetBaseCnns(normalSet.AzBase);

        }

    }
}
