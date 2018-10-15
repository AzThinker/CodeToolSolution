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

namespace WinCodeView.UI
{
    public partial class AzCdgnMasterDisplay : UserControl
    {
        public AzCdgnMasterDisplay()
        {
            InitializeComponent();
        }

        [Category("Atk_MasterDisplay")]
        public event EventHandler Atk_LookCurrentDbData;
        private void btnLookDBdata_Click(object sender, EventArgs e)
        {
            Atk_LookCurrentDbData?.Invoke(sender, e);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool Atk_MasterInit
        {
            get => chkMasterInit.Checked;
            set => chkMasterInit.Checked = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool Atk_WillCreatFiles
        {
            get => chkCreatFiles.Checked;
            set => chkCreatFiles.Checked = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public string Atk_CurrentClass
        {
            get => labCurrentClass.Text;
            set => labCurrentClass.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public string Atk_CurrentSchemaName
        {
            get => labCurrentSchemaName.Text;
            set
            {
                labCurrentSchemaName.Text = value;
                btnLookDBdata.Enabled = !string.IsNullOrWhiteSpace(value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public string Atk_DisplayName
        {
            get => labDisplayName.Text;
            set => labDisplayName.Text = value;
        }

        public void SetMetaTableEntity(AzMetaTableEntity azMetaTable)
        {
            Atk_CurrentClass = azMetaTable.ClassName;
            Atk_CurrentSchemaName = azMetaTable.SchemaName;
            Atk_DisplayName = azMetaTable.ClassDisPlay;
            btnLookDBdata.Enabled = !(string.IsNullOrWhiteSpace(azMetaTable.SchemaName)) && (azMetaTable.IsCustom == false)
                 && (azMetaTable.ObjDataType < 3);
            chkMasterInit.Checked = string.IsNullOrWhiteSpace(azMetaTable.CodeSetVales) ? true : azMetaTable.CodeSetVales.Length < 100;


        }

        public bool GetHasSummary()
        {
            return this.hasSummary.Checked;
        }

        public void SetAzSaveCodeFileFloder(string path)
        {
            textBox1.Text = path;
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog()==DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
