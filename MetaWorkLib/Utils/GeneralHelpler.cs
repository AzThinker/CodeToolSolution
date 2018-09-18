using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
namespace MetaWorkLib.Utils
{
    public static class GeneralHelpler
    {

        public static bool IsDefaultValueField(this PropertyInfo propertyInfo)
        {

            return propertyInfo.GetCustomAttribute(typeof(DefaultValueAttribute)) != null;

        }
        //Confirm
        public static DialogResult ConfirmQuestionOperate(string msg)
        {
            return MessageBox.Show(msg, "提示",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);


        }

        public static void SomethingDone(string msg)
        {
            MessageBox.Show(msg, "提示",
              MessageBoxButtons.OK, MessageBoxIcon.Information);


        }


        public static void SomethingError(string msg)
        {
            MessageBox.Show(msg, "提示",
              MessageBoxButtons.OK, MessageBoxIcon.Error);


        }


        public static void SomethingWarning(string msg)
        {
            MessageBox.Show(msg, "提示",
              MessageBoxButtons.OK, MessageBoxIcon.Warning);


        }

        public static void SomethingDoSuccess()
        {
            MessageBox.Show("操作成功！", "提示",
              MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        public static string AddBrackets(this string oldString)
        {
            return "(" + oldString + ")";//single quotes
        }


        public static string AddSingleQuotes(this string oldString)
        {
            return "'" + oldString + "'";//single quotes
        }

        public static TLEntity To<TLEntity, T>(this IEnumerable<T> ts) where TLEntity : List<T>, new()
        {
            TLEntity tLEntity = new TLEntity();
            foreach (var item in ts)
            {
                tLEntity.Add(item);
            }
            return tLEntity;
        }

    }
}
