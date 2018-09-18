using MetaWorkLib.Config;
using MetaWorkLib.Domain;
using MetaWorkLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaWorkLib.CodeTools
{
    public static class CodeHandle
    {

        public static string GetCurrentTemplatePath()
        {
            var azset = AzNormalSet.GetAzNormalSet().AzBase;
            string filspath = azset.AzTemplateFolder + @"\";
            return filspath;
        }

        public static string GetTemplateConfig()
        {
            string path = GetCurrentTemplatePath() + "TemplateSet.json";
            return FileHelper.ReadTemplateFile(path);
        }

        public static string ReplacContext(string codeStr, AzMetaTableEntity azMetaTable)
        {
            var aznormalset = AzNormalSet.GetAzNormalSet();
            var azbase = aznormalset.AzBase;
            codeStr = codeStr.ReaplaceTemplateForWord("Ai_Project_NameSpace", azbase.AzProjectSpace)
                             .ReaplaceTemplateForWord("Ai_Project_UI_FullNameSpace", azbase.AzProjectSpace + @".WebUI")
                             .ReaplaceTemplateForWord("Ai_Bll_ClassName", azMetaTable.ClassName)
                             .ReaplaceTemplateForWord("Ai_Bll_Edit_ClassName", azMetaTable.ClassName)
                             .ReaplaceTemplateForWord("Ai_Object_ChineseName", string.IsNullOrWhiteSpace(azMetaTable.ClassDisPlay) ? azMetaTable.ClassName : azMetaTable.ClassDisPlay)
                             .ReaplaceTemplateForWord("Ai_Bll_List_ClassName", azMetaTable.ClassName+"List")
                             .ReaplaceTemplateForWord("Ai_ProjectName", azbase.AzProjectName)
                             .ReaplaceTemplateForWord("Ai_SqlDB_ConnectionString", azMetaTable.ClassName + "List")
                             .ReaplaceTemplateForWord("Ai_Bll_List_ClassName", azMetaTable.ClassName + "List")
                             .ReaplaceTemplateForWord("Ai_Bll_List_ClassName", azMetaTable.ClassName + "List")
                             .ReaplaceTemplateForWord("Ai_Bll_List_ClassName", azMetaTable.ClassName + "List")
                             .ReaplaceTemplateForWord("Ai_Bll_List_ClassName", azMetaTable.ClassName + "List");




            return codeStr;
        }

    }
}
