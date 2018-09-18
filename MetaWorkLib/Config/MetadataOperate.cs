using MetaWorkLib.Domain;
using MetaWorkLib.MetaInit;
using MetaWorkLib.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorkLib.Config
{
    public static class MetadataOperate
    {

        private static string dbConnectionString = string.Empty;
        static MetadataOperate()
        {
            dbConnectionString = AzNormalSet.GetAzNormalSet().AzConnectionString;
        }

        /// <summary>
        /// 删除指定元数据表中删除数据
        /// </summary>
        /// <param name="deletetable">删除表名</param>
        public static int DeleteByTableName(string deletetable)
        {
            if (DataHelper.DbTableNameExist(deletetable))
            {
                using (SqlConnection cn = new SqlConnection(dbConnectionString))
                {

                    cn.Open();
                    var azset = AzNormalSet.GetAzNormalSet().AzBase;
                    string cmdstr = string.Format(BaseConstants.DeleteTblesStr, deletetable, GetDefAppNameUpdate(azset.AzTablePrefix));
                    using (SqlCommand cmd = new SqlCommand(cmdstr, cn))
                    {
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            return -10;
        }

        public static int ExecuteCmd(string cmdstr)
        {
            using (SqlConnection cn = new SqlConnection(dbConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(cmdstr, cn))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 清除所有当前项目的元数据（根据配置）
        /// </summary>
        public static void ClearAllMetaData()
        {
            var azset = AzNormalSet.GetAzNormalSet();

            DeleteByTableName(azset.AzMetaTableName);
            DeleteByTableName(azset.AzMetaCloumName);
        }

        /// <summary>
        /// 替换初始化元数据表字串
        /// </summary>
        /// <param name="loadFile">模板文件</param>
        /// <returns></returns>
        public static string DbCreateLoad(string loadFile)
        {
            string templatestr = string.Empty;
            var azset = AzNormalSet.GetAzNormalSet();
            templatestr = FileHelper.ReadTemplateFile(loadFile);

            templatestr = templatestr.ReaplaceTemplate(BaseConstants.Az_MetaTable, azset.AzMetaTableName)
                  .ReaplaceTemplate(BaseConstants.Az_MetaCloum, azset.AzMetaCloumName)
                  .ReaplaceTemplate(BaseConstants.Az_MetaQueryView, azset.AzMetaQueryView);

            return templatestr;

        }

        public static bool ConfigWhetherInit()
        {
            var azset = AzNormalSet.GetAzNormalSet();

            if (string.IsNullOrWhiteSpace(azset.AzConnectionString))
            {
                return false;
            }
            try
            {
                using (SqlConnection cn = new SqlConnection(azset.AzConnectionString))
                {

                    cn.Open();
                }
            }
            catch (Exception)
            {
                throw new Exception($"{azset.AzConnectionString},此连接数据库不存在，或不能打开，请正确设置！！");
               // GeneralHelpler.SomethingWarning("数据库不能打开，请正确设置！！");
               // return false;
            }

            if (string.IsNullOrWhiteSpace(azset.AzMetaTableName))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(azset.AzMetaQueryView))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(azset.AzMetaCloumName))
            {
                return false;
            }

            return true;
        }

        public static bool MetaWhetherInit()
        {
            var azset = AzNormalSet.GetAzNormalSet();
            if (!DataHelper.DbTableNameExist(azset.AzMetaTableName))
            {
                return false;
            }

            if (!DataHelper.DbTableNameExist(azset.AzMetaCloumName))
            {
                return false;
            }

            if (!DataHelper.DbTableNameExist(azset.AzMetaQueryView))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 创建元数据表名存储表
        /// </summary>
        public static int CreateMetaTable()
        {
            var azset = AzNormalSet.GetAzNormalSet();

            if (!DataHelper.DbTableNameExist(azset.AzMetaTableName))
            {
                string cmdstr = DbCreateLoad(BaseConstants.Az_MetaTablefile);

                return ExecuteCmd(cmdstr);

            }

            return -10;

        }

        /// <summary>
        /// 创建元数据列（类属性）存储表
        /// </summary>
        public static int CreateMetaCloum()
        {
            var azset = AzNormalSet.GetAzNormalSet();

            if (!DataHelper.DbTableNameExist(azset.AzMetaCloumName))
            {
                string cmdstr = DbCreateLoad(BaseConstants.Az_MetaCloumfile);

                return ExecuteCmd(cmdstr);

            }
            return -10;
        }

        /// <summary>
        /// 创建元数据查询视图
        /// </summary>
        public static int CreateMetaQueryView()
        {
            var azset = AzNormalSet.GetAzNormalSet();

            if (!DataHelper.DbTableNameExist(azset.AzMetaQueryView))
            {
                string cmdstr = DbCreateLoad(BaseConstants.Az_MetaQueryViewfile);

                return ExecuteCmd(cmdstr);

            }
            return -10;
        }

        public static int CreateUpdateRemarkSp()
        {
            var list = AzMetaTableHandle.Handle().GetAllDBTableList();
            if (list.IndexOf(BaseConstants.Az_StoreProcedureUpdateRemark) > -1)
            {
                return 0;
            }

            string cmdstr = DbCreateLoad(BaseConstants.Az_StoreProcedureUpdateRemarkfile);
            return ExecuteCmd(cmdstr);
        }
        public static int CreateExecSp()
        {
            var list = AzMetaTableHandle.Handle().GetAllDBTableList();
            if (list.IndexOf(BaseConstants.Az_StoreProcedureExe) > -1)
            {
                return 0;
            }

            string cmdstr = DbCreateLoad(BaseConstants.Az_StoreProcedureExefile);
            return ExecuteCmd(cmdstr);
        }
        /// <summary>
        /// 导入元数据（列信息，类的属性）
        /// </summary>
        public static int ImportMetaData()
        {
            var azset = AzNormalSet.GetAzNormalSet();

            if (DataHelper.DbTableNameExist(azset.AzMetaCloumName))
            {
                string cmdstr = DbCreateLoad(BaseConstants.Az_ImportMetaDatafile);
                cmdstr = cmdstr + string.Format(BaseConstants.WhereAppStr, azset.AzBase.AzTablePrefix);
                cmdstr = cmdstr.ReaplaceTemplate(BaseConstants.AppNameDefautSign, GetDefAppNameUpdate(azset.AzBase.AzTablePrefix));
                return ExecuteCmd(cmdstr);

            }
            return -10;
        }

        public static int ImportCustomMetaData(string impFrom, string impTo, bool istmp = false)
        {
            var azset = AzNormalSet.GetAzNormalSet();


            string cmdstr = string.Empty;
            if (istmp)
            {
                cmdstr = DbCreateLoad(BaseConstants.Az_MetaImportTmpfile);
            }
            else
            {
                cmdstr = DbCreateLoad(BaseConstants.Az_MetaImportfile);
            }
            cmdstr = cmdstr.ReaplaceTemplate(BaseConstants.Az_Parameters1, impTo)
                            .ReaplaceTemplate(BaseConstants.Az_Parameters2, impFrom)
                            .ReaplaceTemplate(BaseConstants.AppNameDefautSign, GetDefAppNameUpdate(azset.AzBase.AzTablePrefix)); ;
            return ExecuteCmd(cmdstr);


        }

        /// <summary>
        /// 初始化元数据值
        /// </summary>
        public static int InitMetaData()
        {
            var azset = AzNormalSet.GetAzNormalSet();

            if (DataHelper.DbTableNameExist(azset.AzMetaCloumName))
            {
                string cmdstr = DbCreateLoad(BaseConstants.Az_InitMetaDatafile);
                cmdstr = cmdstr.ReaplaceTemplate(BaseConstants.Az_Parameters1, GetDefAppNameUpdate(azset.AzBase.AzTablePrefix));
                return ExecuteCmd(cmdstr);

            }
            return -10;
        }

        public static string GetDefAppNameUpdate(string appName)
        {
            if (string.IsNullOrWhiteSpace(appName))
            {
                return BaseConstants.AppNameDefaut;
            }
            return appName;
        }

        public static string GetDefAppName(string appName)
        {
            if (appName == BaseConstants.AppNameDefaut)
            {
                return "";
            }
            return appName;
        }
    }
}
