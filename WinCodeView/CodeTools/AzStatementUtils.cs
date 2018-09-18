using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinCodeView.CodeTools
{
    internal static class AzStatementUtils
    {
        public static string AddPerTab(this string OldStr, int Tabtimes = 1)
        {
            string tab = new string('\t', Tabtimes);
            return tab + OldStr;
        }

        public static StringBuilder AddLineStatement(this StringBuilder stringBuilder, string value, int Tabtimes = 1)
        {
            string tab = new string('\t', Tabtimes);
            return stringBuilder.AppendLine(tab + value);
        }

    }


}
