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
    public partial class AzDisplayName : Form
    {
        public AzDisplayName()
        {
            InitializeComponent();
        }

        public static string ShowAzDisplayName(string OldDisplay)
        {
            AzDisplayName azDisplayName = new AzDisplayName();
            azDisplayName.textBox1.Text = OldDisplay;
            if (azDisplayName.ShowDialog() == DialogResult.OK)
            {
                return azDisplayName.textBox1.Text;
            }

            return string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
