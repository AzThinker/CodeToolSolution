using MetaWorkLib.Config;
using MetaWorkLib.Utils;
using Microsoft.Data.ConnectionUI;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinCodeView
{
    public partial class AzDataBSetEdit : Form
    {
        private F_Progress f_Progress;
        public AzDataBSetEdit()
        {
            InitializeComponent();
            AzBaseSet baseSet = AzNormalSet.GetAzNormalSet().AzBase; //AzBaseSet.GetBaseSet();
            txtConnectionString.Text = baseSet.AzConnectionString;//.AzGetConnectionString();
            f_Progress = new F_Progress(backgroundWorker1);

        }

        public static void ShowAzDataBSetEdit()
        {
            AzDataBSetEdit azDataBSetEdit = new AzDataBSetEdit();
            azDataBSetEdit.ShowDialog();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataConnectionDialog dialog = new DataConnectionDialog();
            //添加数据源列表，可以向窗口中添加自己程序所需要的数据源类型
            dialog.DataSources.Add(DataSource.SqlDataSource);
            //  dialog.DataSources.Add(DataSource.OdbcDataSource);
            // DataSource.AddStandardDataSources(dialog);

            dialog.SelectedDataSource = DataSource.SqlDataSource;
            dialog.SelectedDataProvider = DataProvider.SqlDataProvider;
            if (!(string.IsNullOrEmpty(txtConnectionString.Text)))
            {
                dialog.ConnectionString = txtConnectionString.Text;
            }

            //只能够通过DataConnectionDialog类的静态方法Show出对话框
            //不同使用dialog.Show()或dialog.ShowDialog()来呈现对话框
            if (DataConnectionDialog.Show(dialog, this) == DialogResult.OK)
            {
                AzBaseSet baseSet = AzNormalSet.GetAzNormalSet().AzBase;//  AzBaseSet.GetBaseSet();
                baseSet.AzConnectionString= dialog.ConnectionString;
                AzBaseSet.AzSetBaseCnns(baseSet);
                //txtConnectionString.Text = dialog.ConnectionString;
                //AzDataSourceSet.AzSetConnectionString(dialog.ConnectionString);
            }
        }

        private void ACM_Exist_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void ACM_DBCreateOp_Click(object sender, EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定要重新生成数据库数据?重新生成后原设置将丢失！")
                      == DialogResult.OK)
            {
                try
                {
                    backgroundWorker1.DoWork += new DoWorkEventHandler(ACM_DBCreate);
                    f_Progress.MsgText("重新生成数据库数据");
                    backgroundWorker1.RunWorkerAsync();
                    f_Progress.ShowDialog(this);
                }
                finally
                {
                    backgroundWorker1.DoWork -= new DoWorkEventHandler(ACM_DBCreate);
                }
            }
        }

        private void ACM_DBCreate(object sender, DoWorkEventArgs e)
        {

            // 创建
            MetadataOperate.CreateMetaTable();
            MetadataOperate.CreateMetaCloum();
            MetadataOperate.CreateMetaQueryView();
            MetadataOperate.CreateUpdateRemarkSp();
            MetadataOperate.CreateExecSp();


            // 清除数据
            MetadataOperate.ClearAllMetaData();

            //
            MetadataOperate.ImportMetaData();
            MetadataOperate.InitMetaData();
        }
        private void ACM_DBClearAll(object sender, DoWorkEventArgs e)
        {
            MetadataOperate.ClearAllMetaData();
        }
        private void ACM_DBInit(object sender, DoWorkEventArgs e)
        {
            MetadataOperate.InitMetaData();
        }

        private void ACM_DBClearData_Click(object sender, EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定要清除本程序生成的数据库数据！")
                     == DialogResult.OK)
            {
                try
                {
                    backgroundWorker1.DoWork += new DoWorkEventHandler(ACM_DBClearAll);
                    f_Progress.MsgText("清除生成数据库数据");
                    backgroundWorker1.RunWorkerAsync();
                    f_Progress.ShowDialog(this);
                }
                finally
                {
                    backgroundWorker1.DoWork -= new DoWorkEventHandler(ACM_DBClearAll);
                }
            }
        }

        private void ACM_DBIniData_Click(object sender, EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定要初始所有元数据吗！")
                     == DialogResult.OK)
            {
                try
                {
                    backgroundWorker1.DoWork += new DoWorkEventHandler(ACM_DBInit);
                    f_Progress.MsgText("初始所有元数据");
                    backgroundWorker1.RunWorkerAsync();
                    f_Progress.ShowDialog(this);
                }
                finally
                {
                    backgroundWorker1.DoWork -= new DoWorkEventHandler(ACM_DBInit);
                }
            }
        }

        private void AzDataBSetEdit_Paint(object sender, PaintEventArgs e)
        {
            button3.Enabled = !string.IsNullOrWhiteSpace(txtConnectionString.Text);
            button4.Enabled = !string.IsNullOrWhiteSpace(txtConnectionString.Text);
            button5.Enabled = !string.IsNullOrWhiteSpace(txtConnectionString.Text);
        }
    }
}
