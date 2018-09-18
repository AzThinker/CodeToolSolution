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
    public partial class AzColumsEdit : Form
    {
        private bool IsNew = false;
        public AzColumsEdit()
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

        public static void ShowAzColumsEdit(BindingSource dataSource, string tablename = "", bool isNew = false)
        {
            AzColumsEdit columsEdit = new AzColumsEdit();


            if (isNew)
            {
                var row = (dataSource.DataSource as DataTable).NewRow();
                var c = AzMetaCloumHandle.Handle().NewAdd(tablename);
                DataTable data = Translator.Translate<AzMetaProfile, AzMetaCloumEntity>(c, "AtkMetaCloum");
                row.ItemArray = data.AsEnumerable().FirstOrDefault().ItemArray;
                (dataSource.DataSource as DataTable).Rows.Add(row);
                dataSource.MoveLast();
                columsEdit.IsNew = true;
            }

            columsEdit.bindingSource1 = dataSource;

            columsEdit.textFldName.DataBindings.Add("Text", dataSource, "FldName", true);
            columsEdit.cboBoxFldCodeType.DataBindings.Add("Text", dataSource, "FldCodeType", true);
            columsEdit.cboBoxFldType.DataBindings.Add("Text", dataSource, "FldType", true);
            columsEdit.textFldDisplay.DataBindings.Add("Text", dataSource, "FldDisplay", true);
            columsEdit.textFldLen.DataBindings.Add("Text", dataSource, "FldLen", true);
            columsEdit.ckBoxIsIdentity.DataBindings.Add("Checked", dataSource, "IsIdentity", true);
            columsEdit.ckBoxIsKeyField.DataBindings.Add("Checked", dataSource, "IsKeyField", true);
            columsEdit.ckBoxIsNullable.DataBindings.Add("Checked", dataSource, "IsNullable", true);

            columsEdit.ShowDialog();
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


                var entity = row.ToObjectSafe<AzMetaCloumEntity>();
                int tyid = 1;
                if (radioButton2.Checked)
                {
                    tyid = 2;
                }
                entity.TpyeId = tyid;

                int result = 0;
                if (IsNew)
                {
                    result = AzMetaCloumHandle.Handle().Insert(entity).Id;

                }
                else
                {

                    result = AzMetaCloumHandle.Handle().Update(entity);
                }
                if (result > 0)
                {
                    row.AcceptChanges();
                    return 1;
                }
            }
            return 0;
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

                var entity = row.ToObjectSafe<AzMetaCloumEntity>();
                if (entity.Id == 0)
                {
                    bindingSource1.RemoveCurrent();
                    return;
                }
                var c = AzMetaCloumHandle.Handle().Select().Where(t => t.Id == entity.Id).Go();
                DataTable data = Translator.Translate<AzMetaProfile, AzMetaCloumEntity>(c, "AtkMetaCloum");
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
                    if ((row.RowState == DataRowState.Detached) || (row.RowState == DataRowState.Added))
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
            //!RegexValidator.IsMatch(textAzProjectName.Text, @"^[a-zA-Z][a-zA-Z0-9_]*$")
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
