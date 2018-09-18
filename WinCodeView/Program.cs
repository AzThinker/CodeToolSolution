using MetaWorkLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableTranslatorEx;

namespace WinCodeView
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Translator.AddProfile<AzMetaProfile>();
            Translator.Initialize();
            Application.Run(new MainForm());
        }
    }
}
