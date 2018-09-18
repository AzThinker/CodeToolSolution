using MetaWorkLib.Config;
using MetaWorkLib.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinCodeView
{
    public partial class AzCustomAttribute : Form
    {
        public AzCustomAttribute()
        {
            InitializeComponent();
        }

        private void AddToText()
        {
            if (listBox2.SelectedItem == null) return;
            List<string> list = textBox1.Lines.ToList<string>();

            string additem = listBox2.SelectedItem.ToString();
            int i = list.IndexOf(additem);
            if (list.IndexOf(additem)>-1) return;
            list.Add($"{additem}");

            textBox1.Lines = list.ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddToText();
        }


        public static bool ShowAzCustomAttribute(BindingSource bindingSource)
        {
            AzCustomAttribute azCustomAttribute = new AzCustomAttribute();
            azCustomAttribute.labFldName.DataBindings.Add("Text", bindingSource, "FldName", true);
            azCustomAttribute.labFldDisplay.DataBindings.Add("Text", bindingSource, "FldDisplay", true);
            azCustomAttribute.textBox1.DataBindings.Add("Text", bindingSource, "AddAttr", true);
             
            azCustomAttribute.bindingNavigator1.BindingSource = bindingSource;
           return  azCustomAttribute.ShowDialog()==DialogResult.OK;

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void AzCustomAttribute_Load(object sender, EventArgs e)
        {
            var list = FileHelper.ReadListFile(BaseConstants.AttributeDefaultfile);
            listBox2.Items.Clear();
            if (list != null)
            {
                listBox2.Items.AddRange(list.ToArray());
            }
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            AddToText();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
    }
}
