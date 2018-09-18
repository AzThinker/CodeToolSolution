using System;
using System.Windows.Forms;

namespace WinCodeView
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void AzExitApp_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = MessageBox.Show("确定退出？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                     == DialogResult.Cancel;
        }

        private void ACM_AzDBSet_Click(object sender, EventArgs e)
        {
            AzDataBSetEdit.ShowAzDataBSetEdit();
        }

        private void ACM_BaseSet_Click(object sender, EventArgs e)
        {
            AzBaseSetEdit.ShowAzBaseSetEdit();
        }

        private void ACM_CustomMateManage_Click(object sender, EventArgs e)
        {
            AzCustomMateManage.ShowAzCustomMateManage(this);
        }

        private void ACM_BusinessDesign_Click(object sender, EventArgs e)
        {
            AzClassDesign.ShowAzClassDesign(this);
        }
    }
}
