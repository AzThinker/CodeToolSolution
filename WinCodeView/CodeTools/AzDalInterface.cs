using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinCodeView.CodeTools
{
    public static class AzDalInterface
    {
        static List<string> webtUiIgnoreProperty;
        static AzDalInterface()
        {
            webtUiIgnoreProperty = new List<string>() { "Id" };
        }

        public static string GetDalInterface(string op)
        {
            switch (op)
            {
                case "DB_Insert": return "void DB_Insert(<$Ai_Bll_ClassName>Entity azItem);".AddPerTab();
                case "DB_Update": return "void DB_Update(<$Ai_Bll_ClassName>Entity azItem);".AddPerTab();
                case "DB_Delete": return "void DB_Delete(<$Ai_Bll_ClassName>Entity azItem);".AddPerTab();
                case "DB_Fetch": return "void DB_Fetch(<$Ai_Bll_ClassName>Entity azItem);".AddPerTab();
                case "DB_FetchList": return "void DB_FetchList(<$Ai_Bll_List_ClassName>Entity azItems);".AddPerTab();
                case "DB_Execute": return "void DB_Execute(<$Ai_Bll_ClassName>Entity azItem);".AddPerTab();

            }
            return string.Empty;
        }


        public static string GetBLLInterface(string op)
        {
            switch (op)
            {
                case "DB_Insert": return "IBusinessInsert";
                case "DB_Update": return "IBusinessUpdate";
                case "DB_Delete": return "IBusinessDelete";
                case "DB_Fetch": return "IBusinessFetch";
                case "DB_FetchList": return "IBusinessFetch";
                case "DB_Execute": return "IBusinessExecute";
            }
            return string.Empty;
        }

        public static bool WebtUiIgnoreProperty(string fldName)
        {
            return webtUiIgnoreProperty.Where(m => m == fldName).Count() > 0;
        }
    }
}

