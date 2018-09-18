using MetaWorkLib.Config;
using MetaWorkLib.Utils;
using System;
using System.Windows.Forms;

namespace WinCodeView
{
    public partial class AzBaseSetEdit : Form
    {
        private AzBaseSet azBaseSet;
        public AzBaseSetEdit()
        {
            InitializeComponent();
            folderBrowserDialog1.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
        }

        private void BaseSetValueLoad()
        {
            azBaseSet = AzNormalSet.GetAzNormalSet().AzBase;// AzBaseSet.GetBaseSet();
            textAzClassPrefix.Text = azBaseSet.AzClassPrefix;
            textAzDbSqlConnectionName.Text = azBaseSet.AzDbSqlConnectionName;
            textAzNick.Text = azBaseSet.AzNick;
            textAzProjectName.Text = azBaseSet.AzProjectName;
            textAzProjectSpace.Text = azBaseSet.AzProjectSpace;
            textAzTablePrefix.Text = azBaseSet.AzTablePrefix;
            textAzTemplateFolder.Text = azBaseSet.AzTemplateFolder;
            textAzToolTable.Text = azBaseSet.AzToolTable;
            textSaveCodeFileFloder.Text= azBaseSet.AzSaveCodeFileFloder ;
        }

        private void AzBaseSetEdit_Load(object sender, EventArgs e)
        {

            BaseSetValueLoad();
        }

        public static void ShowAzBaseSetEdit()
        {
            AzBaseSetEdit azBaseSetEdit = new AzBaseSetEdit();
            azBaseSetEdit.ShowDialog();

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            BaseSetValueLoad();
        }

        private void ACM_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ACM_Save_Click(object sender, EventArgs e)
        {
            if (VerificationText())
            {
                return;
            }

            AzBaseSet azBaseSetedit = AzNormalSet.GetAzNormalSet().AzBase;
            azBaseSetedit.AzClassPrefix = textAzClassPrefix.Text;
            azBaseSetedit.AzDbSqlConnectionName = textAzDbSqlConnectionName.Text;
            azBaseSetedit.AzNick = textAzNick.Text;
            azBaseSetedit.AzProjectName = textAzProjectName.Text;
            azBaseSetedit.AzProjectSpace = textAzProjectSpace.Text;
            azBaseSetedit.AzTablePrefix = textAzTablePrefix.Text;
            azBaseSetedit.AzTemplateFolder = textAzTemplateFolder.Text;
            azBaseSetedit.AzToolTable = textAzToolTable.Text;
            azBaseSetedit.AzSaveCodeFileFloder = textSaveCodeFileFloder.Text;
            AzBaseSet.AzSetBase(azBaseSetedit);
            GeneralHelpler.SomethingDoSuccess();
        }

        private void button_templatefolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                textAzTemplateFolder.Text = folderBrowserDialog1.SelectedPath;

            }
        }

        private void textAzNick_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        private bool VerificationText()
        {
            errorProvider1.Clear();
            if (!RegexValidator.IsMatch(textAzToolTable.Text, @"[a-zA-Z0-9_]*$"))
            {
                errorProvider1.SetError(textAzToolTable, "当前必需为字符开头，或含有非法字符！");
                return true;
            }
            else
            {
                errorProvider1.SetError(textAzToolTable, string.Empty);
            }

            if (!RegexValidator.IsMatch(textAzProjectName.Text, @"^[a-zA-Z][a-zA-Z0-9_]*$"))
            {
                errorProvider1.SetError(textAzProjectName, "当前必需为字符开头，或含有非法字符！");
                return true;
            }
            else
            {
                errorProvider1.SetError(textAzProjectName, string.Empty);
            }

            if (!RegexValidator.IsMatch(textAzClassPrefix.Text, @"^[a-zA-Z][a-zA-Z0-9_]*$"))
            {
                errorProvider1.SetError(textAzClassPrefix, "当前必需为字符开头，或含有非法字符！");
                return true;
            }
            else
            {
                errorProvider1.SetError(textAzClassPrefix, string.Empty);
            }
            if (!RegexValidator.IsMatch(textAzDbSqlConnectionName.Text, @"^[a-zA-Z][a-zA-Z0-9_]*$"))
            {
                errorProvider1.SetError(textAzDbSqlConnectionName, "当前必需为字符开头，或含有非法字符！");
                return true;
            }
            else
            {
                errorProvider1.SetError(textAzDbSqlConnectionName, string.Empty);
            }
            if (!RegexValidator.IsMatch(textAzTemplateFolder.Text, @"^[a-zA-Z][a-zA-Z0-9_]*$"))
            {
                errorProvider1.SetError(textAzTemplateFolder, "当前必需为字符开头，或含有非法字符！");
                return true;
            }
            else
            {
                errorProvider1.SetError(textAzTemplateFolder, string.Empty);
            }
            if (!string.IsNullOrWhiteSpace(textAzTablePrefix.Text))
            {
                if (!RegexValidator.IsMatch(textAzTablePrefix.Text, @"^[a-zA-Z][a-zA-Z0-9_]*$"))
                {
                    errorProvider1.SetError(textAzTablePrefix, "如果当前不为空，必需为字符开头，或含有非法字符！");
                    return true;
                }
                else
                {
                    errorProvider1.SetError(textAzTablePrefix, string.Empty);
                }
            }
            else
            {
                errorProvider1.SetError(textAzTablePrefix, string.Empty);
            }
            return false;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                textSaveCodeFileFloder.Text = folderBrowserDialog1.SelectedPath;

            }
        }
    }
}
