using MetaWorkLib.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MetaWorkLib.Utils
{
    /// <summary>
    /// 文件操作
    /// </summary>
    public static class FileHelper
    {
        private static string currentDir = string.Empty;
        static FileHelper()
        {
            currentDir = AppDomain.CurrentDomain.BaseDirectory;

        }

        public static string GetTemplateFilePath()
        {
            return currentDir;
        }

        public static void SaveCodeToFile(string dir, string floder, string fildName, string codes, string ext = ".cs")
        {
            string codepath = AzNormalSet.GetAzNormalSet().AzBase.AzSaveCodeFileFloder;
            if (string.IsNullOrEmpty(codepath))
            {
                dir = currentDir + @"\生成的代码\" + dir + @"\" + floder;
            }
            else
            {
                dir = codepath + @"\生成的代码\" + dir + @"\" + floder;
            }

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }


            using (FileStream file = new System.IO.FileStream(dir + @"\" + fildName + ext, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                using (System.IO.TextWriter text = new System.IO.StreamWriter(file, System.Text.Encoding.UTF8))
                {
                    text.Write(codes);
                }
            }
        }

        /// <summary>
        /// 读取模版文件，模版需要是UTF-8
        /// </summary>
        /// <param name="fileName">文件相对路径</param>
        /// <returns>文件内容</returns>
        public static string ReadTemplateFile(string fileName)
        {

            string filePath = currentDir + fileName;
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath, Encoding.UTF8);
            }
            return string.Empty;
        }

        /// <summary>
        /// 模版字串替换
        /// </summary>
        /// <param name="oldValue">要替换的文本</param>
        /// <param name="model">替换模版</param>
        /// <param name="newValue">替换值</param>
        /// <returns>替换后的字串</returns>
        public static string ReaplaceTemplate(this string oldValue, string model, string newValue)
        {
            return Regex.Replace(oldValue, model, newValue, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        }


        public static string ReaplaceTemplateForWord(this string oldValue, string model, string newValue)
        {
            model = model.AddAngleBracketA();
            return Regex.Replace(oldValue, model, newValue, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        }

        public static bool ContainsForWord(this string Value, string model)
        {
            model = model.AddAngleBracket();
            return Value.Contains(model);
        }

        public static string AddAngleBracket(this string oldValue)
        {
            return @"<$" + oldValue + @">";
        }
        public static string AddAngleBracketA(this string oldValue)
        {
            return @"<\$" + oldValue + @">";
        }

        public static IEnumerable<string> ReadListFile(string fileName)
        {

            string filePath = currentDir + fileName;
            if (File.Exists(filePath))
            {

                return File.ReadLines(filePath, Encoding.UTF8);
            }
            return null;
        }

    }
}
