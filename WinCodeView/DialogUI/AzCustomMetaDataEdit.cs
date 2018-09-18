using MetaWorkLib.Config;
using MetaWorkLib.Domain;
using MetaWorkLib.Utils;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
namespace WinCodeView
{
    public partial class AzCustomMetaDataEdit : AtkForm
    {
        private AzMetaTableEntity metaTableEntity;
        private bool IsEdit = false;
        private int azId = 0;
        private string azObjModeName = "";
        public AzCustomMetaDataEdit() : base()
        {
            InitializeComponent();


        }

        public static DialogResult ShowAzCustomMetaDataEdit(IWin32Window owner, string objModeName = "")
        {
            AzCustomMetaDataEdit azCustomMetaDataEdit = new AzCustomMetaDataEdit();
            if (string.IsNullOrWhiteSpace(objModeName))
            {
                azCustomMetaDataEdit.Text = "增加" + azCustomMetaDataEdit.Text;
            }
            else
            {
                azCustomMetaDataEdit.IsEdit = true;
                azCustomMetaDataEdit.Text = "编辑" + azCustomMetaDataEdit.Text;
                azCustomMetaDataEdit.LoadData(objModeName);
            }
            azCustomMetaDataEdit.azObjModeName = objModeName;
            azCustomMetaDataEdit.textObjModeName.Focus();
            return azCustomMetaDataEdit.ShowDialog(owner);
        }

        private void textModelName_TextChanged(object sender, System.EventArgs e)
        {
            textClassName.Text = azNormalSet.AzBase.AzClassPrefix + textObjModeName.Text;
            if (string.IsNullOrWhiteSpace(azNormalSet.AzBase.AzTablePrefix))
            {
                textTableName.Text = textObjModeName.Text;
            }
            else
            {
                textTableName.Text = azNormalSet.AzBase.AzTablePrefix + "_" + textObjModeName.Text;
            }

        }

        private void btCancle_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private bool VerificationText()
        {
            errorProvider1.Clear();
 
            if (!RegexValidator.IsMatch(textObjModeName.Text, @"^[a-zA-Z][a-zA-Z0-9_]*$"))
            {
                errorProvider1.SetError(textObjModeName, "当前必需为字符开头，或含有非法字符！");
                return true;
            }
            else
            {
                errorProvider1.SetError(textObjModeName, string.Empty);
            }
            if (string.IsNullOrWhiteSpace(textClassDisPlay.Text))
            {
                errorProvider1.SetError(textClassDisPlay, "当前项不能为空！");
                return true;
            }
            else
            {
                errorProvider1.SetError(textClassDisPlay, string.Empty);
            }
            return false;
        }

        private void btSave_Click(object sender, System.EventArgs e)
        {
            if (VerificationText())
            {
                return;
            }

            int objModeType = 1;
            if (rdbMetaChild.Checked)
            {
                objModeType = 4;
            }

            metaTableEntity = new AzMetaTableEntity
            {
                ObjModeName = textObjModeName.Text,
                ClassName = textClassName.Text,
                SchemaName = textTableName.Text,
                SchemaFrom = textSchemaFrom.Text,
                ClassDisPlay = textClassDisPlay.Text,
                CodeSetVales = "",
                Remark = textRemark.Text,
                IsCustom = true,
                ObjDataType = 1,
                ObjModeType = objModeType,
                AppName = MetadataOperate.GetDefAppNameUpdate(azNormalSet.AzBase.AzTablePrefix),
            };
            if (IsEdit)
            {
                metaTableEntity.Id = azId;
                AzMetaTableHandle.Handle().Updata(metaTableEntity);
            }
            else
            {
                AzMetaTableHandle.Handle().Insert(metaTableEntity);
            }
            DialogResult = DialogResult.OK;
        }

        private void LoadData(string objModeName)
        {
            metaTableEntity = AzMetaTableHandle.Handle().Select()
                .Where(c => c.ObjModeName == objModeName)
                .Go().AsQueryable().FirstOrDefault();
            if (metaTableEntity == null)
            {
                return;
            }
            textObjModeName.Text = metaTableEntity.ObjModeName;
            textTableName.Text = metaTableEntity.SchemaName;
            textClassDisPlay.Text = metaTableEntity.ClassDisPlay;
            textSchemaFrom.Text = metaTableEntity.SchemaFrom;
            textRemark.Text = metaTableEntity.Remark;
            azId = metaTableEntity.Id;
            switch (metaTableEntity.ObjModeType)
            {
                case 1:
                    rdbMetaClass.Checked = true;
                    break;
                case 4:
                    rdbMetaChild.Checked = true;
                    break;

            }

        }

        private void textModelName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void btMetaSource_Click(object sender, System.EventArgs e)
        {
            textSchemaFrom.Text = AzCustomMetaFromSet.ShowCustomMetaFromSet(azObjModeName);
        }


    }
}
