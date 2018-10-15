using DataTableToObjectEx;
using MetaWorkLib.Config;
using MetaWorkLib.Domain;
using MetaWorkLib.MetaInit;
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
using TableTranslatorEx;

namespace WinCodeView
{
    public partial class AzCustomColumsEdit : Form
    {
        private bool IsNew = false;
        public AzCustomColumsEdit()
        {
            InitializeComponent();

            LoadList();
        }

        private void LoadList()
        {

            var list1 = MetaDataTypeHandle.GetMetaDataTypes();
            foreach (var lt in list1)
            {
                cboBoxFldCodeType.Items.Add(lt.CodeType);
                cboBoxFldType.Items.Add(lt.DBType);
            }

        }

        public static void ShowAzCustomColumsEdit(BindingSource dataSource, string tablename = "", bool isNew = false)
        {
            AzCustomColumsEdit customColumsEdit = new AzCustomColumsEdit();


            if (isNew)
            {
                var row = (dataSource.DataSource as DataTable).NewRow();
                var c = AzMetaCustomCloumHandle.Handle().NewAdd(tablename);
                DataTable data = Translator.Translate<AzMetaProfile, AzMetaCustomCloumEntity>(c, "AtkMetaCustomCloum");
                row.ItemArray = data.AsEnumerable().FirstOrDefault().ItemArray;
                (dataSource.DataSource as DataTable).Rows.Add(row);
                dataSource.MoveLast();
                customColumsEdit.IsNew = true;
            }

            customColumsEdit.bindingSource1 = dataSource;

            customColumsEdit.textFldName.DataBindings.Add("Text", dataSource, "FldName", true);
            customColumsEdit.cboBoxFldCodeType.DataBindings.Add("Text", dataSource, "FldCodeType", true);
            customColumsEdit.cboBoxFldType.DataBindings.Add("Text", dataSource, "FldType", true);
            customColumsEdit.textFldDisplay.DataBindings.Add("Text", dataSource, "FldDisplay", true);
            customColumsEdit.textFldLen.DataBindings.Add("Text", dataSource, "FldLen", true);
            customColumsEdit.ckBoxIsIdentity.DataBindings.Add("Checked", dataSource, "IsIdentity", true);
            customColumsEdit.ckBoxIsKeyField.DataBindings.Add("Checked", dataSource, "IsKeyField", true);
            customColumsEdit.ckBoxIsNullable.DataBindings.Add("Checked", dataSource, "IsNullable", true);

            customColumsEdit.ShowDialog();
        }

        private int SaveData()
        {
            if (VerificationText())
            {
                return 0;
            }
            if (GeneralHelpler.ConfirmQuestionOperate("确定保存修改？") == DialogResult.Cancel)
            {
                return 0;
            }
            var drow = bindingSource1.Current;
            if (drow != null)
            {
                var row = ((DataRowView)drow).Row;
                row.EndEdit();
                var entity = row.ToObjectSafe<AzMetaCustomCloumEntity>();
                int result = 0;
                if (IsNew)
                {
                    result = AzMetaCustomCloumHandle.Handle().Insert(entity).Id;
                    IsNew = false;

                }
                else
                {

                    result = AzMetaCustomCloumHandle.Handle().Update(entity);
                }
                if (result > 0)
                {
                    row.AcceptChanges();
                    return 1;
                }
            }
            return 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            SaveData();
        }

        private void bindingSource1_BindingComplete(object sender, BindingCompleteEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定取消当前修改吗？") == DialogResult.Cancel)
            {
                return;
            }
            var drow = bindingSource1.Current;
            if (drow != null)
            {
                var row = ((DataRowView)drow).Row;

                var entity = row.ToObjectSafe<AzMetaCustomCloumEntity>();
                if (entity.Id == 0)
                {
                    bindingSource1.RemoveCurrent();
                    return;
                }
                var c = AzMetaCustomCloumHandle.Handle().Select().Where(t => t.Id == entity.Id).Go();
                DataTable data = Translator.Translate<AzMetaProfile, AzMetaCustomCloumEntity>(c, "AtkMetaCustomCloum");
                row.ItemArray = data.AsEnumerable().FirstOrDefault().ItemArray;
                row.AcceptChanges();
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {


            var drow = bindingSource1.Current;
            if (drow != null)
            {
                var row = ((DataRowView)drow).Row;
                if (row.RowState != DataRowState.Unchanged)
                {
                    if (GeneralHelpler.ConfirmQuestionOperate("确定取消修改吗？") == DialogResult.Cancel)
                    {
                        return;
                    }
                    if (row.RowState == DataRowState.Detached)
                    {
                        bindingSource1.RemoveCurrent();
                    }

                }

            }
            Close();
        }

        private void btnSaveExit_Click(object sender, EventArgs e)
        {
            if (SaveData() == 1)
            {
                Close();
            }
        }

        private void textfldlen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private bool VerificationText()
        {
            errorProvider1.Clear();
            if (!RegexValidator.IsMatch(textFldName.Text, @"^[a-zA-Z][a-zA-Z0-9_]*$"))
            {
                errorProvider1.SetError(textFldName, "当前必需为字符开头，或含有非法字符！");
                return true;
            }
            else
            {
                errorProvider1.SetError(textFldName, string.Empty);
            }

            if (string.IsNullOrWhiteSpace(cboBoxFldCodeType.Text))
            {
                errorProvider1.SetError(cboBoxFldCodeType, "当前项不能为空！");
                return true;
            }
            else
            {
                errorProvider1.SetError(cboBoxFldCodeType, string.Empty);
            }

            if (string.IsNullOrWhiteSpace(cboBoxFldType.Text))
            {
                errorProvider1.SetError(cboBoxFldType, "当前项不能为空！");
                return true;
            }
            else
            {
                errorProvider1.SetError(cboBoxFldType, string.Empty);
            }

            if (string.IsNullOrWhiteSpace(textFldDisplay.Text))
            {
                errorProvider1.SetError(textFldDisplay, "当前项不能为空！");
                return true;
            }
            else
            {
                errorProvider1.SetError(textFldDisplay, string.Empty);
            }

            return false;


        }
    }
}
