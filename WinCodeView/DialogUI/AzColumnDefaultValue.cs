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
    public partial class AzColumnDefaultValue : Form
    {
        public AzColumnDefaultValue()
        {
            InitializeComponent();
        }

        public static bool ShowAzColumnDefaultValue(BindingSource bindingSource)
        {
            AzColumnDefaultValue azColumnDefaultValue = new AzColumnDefaultValue();
            azColumnDefaultValue.labFldName.DataBindings.Add("Text", bindingSource, "FldName", true);
            azColumnDefaultValue.labFldDisplay.DataBindings.Add("Text", bindingSource, "FldDisplay", true);
            azColumnDefaultValue.labFldCodeType.DataBindings.Add("Text", bindingSource, "FldCodeType", true);
            azColumnDefaultValue.textDefvalue.DataBindings.Add("Text", bindingSource, "Defvalue", true);
            azColumnDefaultValue.bindingNavigator1.BindingSource = bindingSource;
           return  azColumnDefaultValue.ShowDialog()==DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void AzColumnDefaultValue_Load(object sender, EventArgs e)
        {
            var list = FileHelper.ReadListFile(BaseConstants.ColumnDefaultfile);
            listBox1.Items.Clear();
            if (list != null)
            {
                listBox1.Items.AddRange(list.ToArray());
            }

        }

        private void SetDefaultValue()
        {
            if (listBox1.SelectedItem == null)
            {
                return;
            }

            textDefvalue.Text = listBox1.SelectedItem.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetDefaultValue();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            SetDefaultValue();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textDefvalue.Text = "";
        }
    }
}
