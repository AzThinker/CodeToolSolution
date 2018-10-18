using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorkLib.Config
{
    public static class BaseConstants
    {


        public const string AppNameDefaut = "9Atk";
        public const string AppNameDefautSign = @"<\$Az_AppName>";
        //
        public const string CodeToolName = "Azthinker";
        public const string MetaTableNameCon = "Azthinker{0}_MetaTable";
        public const string MetaCloumNameCon = "Azthinker{0}_MetaCloum";
        public const string MetaQueryViewCon = "Azthinker{0}_MetaQuery";
        public const string ParamPer = "az";
        //


        public const string Az_MetaQueryView = @"<\$Az_MetaQueryView>";
        public const string Az_MetaTable = @"<\$Az_MetaTable>";
        public const string Az_MetaCloum = @"<\$Az_MetaCloum>";

        public const string Az_Parameters1 = @"<\$Parameters1>";
        public const string Az_Parameters2 = @"<\$Parameters2>";
        //
        public const string Az_MetaQueryViewfile = @"DbWork\Az_MetaQueryView.txt";
        public const string Az_MetaTablefile = @"DbWork\Az_MetaTable.txt";
        public const string Az_MetaCloumfile = @"DbWork\Az_MetaCloum.txt";
        public const string Az_ImportMetaDatafile = @"DbWork\Az_MetaInsert.txt";
        public const string Az_InitMetaDatafile = @"DbWork\Az_MetaInit.txt";
        public const string Az_StoreProcedureExefile = @"DbWork\AzThinker_Exec.txt";
        public const string Az_StoreProcedureUpdateRemarkfile = @"DbWork\AzThinker_UpdateRemark.txt";
        public const string Az_StoreProcedureExe = @"AzThinker_Exec";
        public const string Az_StoreProcedureUpdateRemark = @"AzThinker_UpdateRemark";


        public const string Az_MetaImportfile = @"DbWork\Az_MetaImport.txt";
        public const string Az_MetaImportTmpfile = @"DbWork\Az_MetaImportTmp.txt";
        public const string Az_CopyMetaCloumfile = @"DbWork\Az_MetaDataCopy.txt";
        public const string Az_MetaDataMList_IniOnefile = @"DbWork\Az_MetaDataMList_IniOne.txt";
        public const string Az_InitColumnValueSchemafile = @"DbWork\Az_InitColumnValueSchema.txt";
        public const string Az_InitOneColumnValueSchemafile = @"DbWork\Az_InitOneColumnValueSchema.txt";
        //
        public const string Az_DBMetaDataTypefile = @"MetaFiles\DBMetaDataType.txt";
        public const string Az_ClassDataTypefile = @"MetaFiles\ClassDataType.txt";
        public const string Az_DBMetaDataLenfile = @"MetaFiles\DBMetaDataLen.txt";
        //
        public const string DeleteTblesStr = "DELETE FROM dbo.{0}  WHERE  AppName ='{1}' ";
        public const string WhereAppStr = " WHERE  AppName ='{0}' ";

        //
        public const string ColumnDefaultfile = @"Defaults\DefValueList.txt";

        public const string AttributeDefaultfile = @"Defaults\DefAttrList.txt";
    }
}
