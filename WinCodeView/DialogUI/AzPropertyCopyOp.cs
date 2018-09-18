using MetaWorkLib.Domain;
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
    public partial class AzPropertyCopyOp : Form
    {
        private IEnumerable<AzMetaCloumEntity> currentDatas;
        public AzPropertyCopyOp()
        {
            InitializeComponent();
        }

        public static void ShowAzPropertyCopyOp(IEnumerable<AzMetaCloumEntity> columnDatas)
        {
            AzPropertyCopyOp azPropertyCopyOp = new AzPropertyCopyOp
            {
                currentDatas = columnDatas
            };
            azPropertyCopyOp.ShowDialog();

        }

        private void PropertyCopy()
        {
            textBox3.Text = "";
            List<string> vs = new List<string>();
            string endMark = ",";
            string lstr = textBox1.Text.Trim() + ".";
            string rstr = textBox2.Text.Trim() + ".";
            if (radioButton2.Checked)
            {
                endMark = ";";
            }

            foreach (var item in currentDatas)
            {
                if (item.IsSelect ?? false)
                {
                    vs.Add(lstr + item.FldName + " = " + rstr + item.FldName+endMark);
                }
            }
            textBox3.Lines = vs.ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PropertyCopy();
        }
    }
}
