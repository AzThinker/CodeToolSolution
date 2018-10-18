using DataTableToObjectEx;
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

namespace WinCodeView.UI
{
    public partial class AzCdgnDetail : UserControl
    {
        private IEnumerable<AzMetaCloumEntity> currentDatas;
        private bool _LookParam = false;
        private bool _IsStoreQuery = false;
        public AzCdgnDetail()
        {
            InitializeComponent();
        }

        private AzMetaTableEntity currentObject;

        public AzMetaTableEntity GetCurrentObject()
        {
            return currentObject;
        }

        public void SetCurrentObject(AzMetaTableEntity value, int level)
        {
            currentObject = value;
            tabControl1.SelectedTab = tabPage1;
            richTextBox1.Clear();
            if (level == 1)
            {
                LoadMataColum();

            }
            else
            {
                azMetaCloumListEntityBindingSource.DataSource = new AzMetaCloumListEntity();
                toolStrip2.Enabled = false;
            }

        }
        private bool DataChangeAskSave()
        {
            if (azMetaCloumListEntityBindingSource.DataSource == null)
            {
                return false;
            }
            if ((azMetaCloumListEntityBindingSource.DataSource as DataTable) == null)
            {
                return false;
            }
            var list = (azMetaCloumListEntityBindingSource.DataSource as DataTable).GetChanges();
            return list != null;
        }


        private void Column_Changing(object sender, DataColumnChangeEventArgs e)
        {
            tspBtnSave.Enabled = true;
        }


        public List<AzMetaCloumEntity> GetCurrentData()
        {
            return currentDatas.ToList<AzMetaCloumEntity>();
        }
        private void LoadMataColum(bool look = false)
        {
            toolStrip2.Enabled = true;
            string querytable = currentObject.SchemaName;
            var cpro = AzCdgnClassProperty.GetItemClassCreatProperty(currentObject);
            _IsStoreQuery = cpro.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_QuerystoredProcedure;
            _LookParam = false;
            if (cpro.ObjPresentation.ObjDataType == ObjDataTypeEnum.atk_QuerystoredProcedure && !look)
            {
                if (!string.IsNullOrWhiteSpace(cpro.ObjPresentation.StoreProcedureQuery))
                {
                    querytable = cpro.ObjPresentation.StoreProcedureQuery;
                    _LookParam = true;
                }

            }
            var c = AzMetaCloumHandle.Handle().Select().Where(t => t.TableName == querytable).OrderBy(t => t.ShowOrder).Go();
            DataTable data = Translator.Translate<AzMetaProfile, AzMetaCloumEntity>(c, "AtkMetaCloum");

            data.AcceptChanges();
            currentDatas = c;
            data.ColumnChanging += new DataColumnChangeEventHandler(Column_Changing);


            azMetaCloumListEntityBindingSource.DataSource = data;
            azMetaCloumListEntityBindingSource.EndEdit();
            tspBtnSave.Enabled = false;
            toolStripButton4.Enabled = false;
            toolStripButton5.Enabled = false;
            tspButtonSort.Enabled = false;
            tspBtnEdit.Enabled = false;
            tspBtnDelete.Enabled = false;
            toolStripButton8.Enabled = true;
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[0].Value = row.Index + 1;
            }
        }

        private void tspButtonFirst_Click(object sender, EventArgs e)
        {
            azMetaCloumListEntityBindingSource.MoveFirst();
        }

        private void tspButtonBefort_Click(object sender, EventArgs e)
        {
            azMetaCloumListEntityBindingSource.MovePrevious();
        }

        private void tspButtonNext_Click(object sender, EventArgs e)
        {
            azMetaCloumListEntityBindingSource.MoveNext();
        }

        private void tspButtonLast_Click(object sender, EventArgs e)
        {
            azMetaCloumListEntityBindingSource.MoveLast();
        }

        private void tspButtonSort_Click(object sender, EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定生成顺序？") == DialogResult.Cancel)
            {
                return;
            }
            var listcol = AzMetaCloumHandle.Handle().Select().Where(t => t.TableName == currentObject.SchemaName).OrderBy(t => t.ShowOrder).Go();
            var odi = 1;
            foreach (var item in listcol)
            {

                item.ShowOrder = odi;
                AzMetaCloumHandle.Handle().Update(item);

                odi += 1;
            }

            LoadMataColum();
        }

        private void azMetaCloumListEntityBindingSource_PositionChanged(object sender, EventArgs e)
        {

            tspBtnSave.Enabled = DataChangeAskSave();
            tspButtonSort.Enabled = !tspBtnSave.Enabled && tspBtnEdit.Enabled;

            toolStripButton4.Enabled = true;
            toolStripButton5.Enabled = true;
            tspButtonSort.Enabled = dataGridView1.Rows.Count > 0;
            tspBtnEdit.Enabled = dataGridView1.Rows.Count > 0;
            tspBtnDelete.Enabled = dataGridView1.Rows.Count > 0;
        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].ErrorText = null;
            tspBtnSave.Enabled = DataChangeAskSave();
            tspButtonSort.Enabled = !tspBtnSave.Enabled && tspBtnEdit.Enabled;
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

            if ((dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "fldlen") ||
               (dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "ShowOrder"))
            {
                if (!int.TryParse(e.FormattedValue.ToString(), out int newInteger) || newInteger < 0)
                {
                    e.Cancel = true;

                    dataGridView1.Rows[e.RowIndex].ErrorText = "当前字段只能是数字，格式错误，请重新输入。";

                    return;
                }
            }
        }

        private void tspBtnAdd_Click(object sender, EventArgs e)
        {
            AzColumsEdit.ShowAzColumsEdit(azMetaCloumListEntityBindingSource, currentObject.ObjModeName, true);

            LoadMataColum();
        }

        private void tspBtnEdit_Click(object sender, EventArgs e)
        {
            AzColumsEdit.ShowAzColumsEdit(azMetaCloumListEntityBindingSource, currentObject.ObjModeName);
            LoadMataColum();
        }

        private void tspBtnDelete_Click(object sender, EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定删除当前所选行吗？") == DialogResult.Cancel)
            {
                return;
            }
            var drow = azMetaCloumListEntityBindingSource.Current;
            if (drow != null)
            {
                var row = ((DataRowView)drow).Row;
                var entity = row.ToObjectSafe<AzMetaCloumEntity>();
                if (AzMetaCloumHandle.Handle().Delete(entity) == 1)
                {
                    azMetaCloumListEntityBindingSource.RemoveCurrent();
                    row.AcceptChanges();
                    tspBtnSave.Enabled = DataChangeAskSave();
                }
            }
        }

        private void tspBtnSave_Click(object sender, EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定保存当前所有修改吗？") == DialogResult.Cancel)
            {
                return;
            }

            azMetaCloumListEntityBindingSource.EndEdit();
            dataGridView1.EndEdit();
            var vlist = (azMetaCloumListEntityBindingSource.DataSource as DataTable).GetChanges();
            if (vlist == null)
            {
                return;
            }

            foreach (DataRow item in vlist.Rows)
            {

                var entity = item.ToObjectSafe<AzMetaCloumEntity>();
                AzMetaCloumHandle.Handle().Update(entity);

            }
            vlist.AcceptChanges();
            (azMetaCloumListEntityBindingSource.DataSource as DataTable).AcceptChanges();
            tspBtnSave.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var lp = AzViewCustomListEdit.ShowAzViewCustomListEdit(textBox2.Text);
            var drow = azMetaCloumListEntityBindingSource.Current;
            if (drow != null)
            {
                var row = ((DataRowView)drow).Row;
                row["ListParam"] = lp;
                row["CustomList"] = !(string.IsNullOrWhiteSpace(row["ListParam"].ToString()));
            }
            tspBtnSave.Enabled = true;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (AzColumnDefaultValue.ShowAzColumnDefaultValue(azMetaCloumListEntityBindingSource))
            {
                var drow = azMetaCloumListEntityBindingSource.Current;
                if (drow != null)
                {
                    var row = ((DataRowView)drow).Row;
                    row["IsDefvalue"] = !(string.IsNullOrWhiteSpace(row["Defvalue"].ToString()));
                }
                tspBtnSave.Enabled = true;
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (AzCustomAttribute.ShowAzCustomAttribute(azMetaCloumListEntityBindingSource))
            {
                tspBtnSave.Enabled = true;
            }
        }

        private void ACM_CopyFiledValue_Click(object sender, EventArgs e)
        {
            string copyfrom = AzCopyColumnTableSelect.ShowAzCopyColumnTableSelect();
            if (string.IsNullOrWhiteSpace(copyfrom))
            {
                return;
            }
            AzMetaCloumHandle.Handle().UpdataDisplayFormOtherSchemaName(currentObject.SchemaName, copyfrom);
            LoadMataColum();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            ACM_CopyFiledValue.Enabled = currentObject == null ? false : currentObject.Id > 0;
            var drow = azMetaCloumListEntityBindingSource.Current;

            ACM_ReNameColumn.Enabled = false;
            tspLookParam.Enabled = _IsStoreQuery && _LookParam;
            tspLookSchema.Enabled = _IsStoreQuery && !_LookParam;
            if (drow != null)
            {
                ACM_ReNameColumn.Enabled = ACM_CopyFiledValue.Enabled;
            }

        }

        private void ACM_ReNameColumn_Click(object sender, EventArgs e)
        {
            var drow = azMetaCloumListEntityBindingSource.Current;
            if (drow != null)
            {
                var row = ((DataRowView)drow).Row;
                var entity = row.ToObjectSafe<AzMetaCloumEntity>();
                if (AzMetaTableHandle.Handle().ReNameColumnDisplay(entity.FldDisplay,
                    currentObject.SchemaName, currentObject.DbObjType, entity.FldName) == -1)
                {
                    LoadMataColum();
                }
            }

        }

        private void ACM_ReNameColumns_Click(object sender, EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定全部更新显示字段至数据库？") == DialogResult.Cancel)
            {
                return;
            }
            var listcol = AzMetaCloumHandle.Handle().Select().Where(t => t.TableName == currentObject.SchemaName).OrderBy(t => t.ShowOrder).Go();

            foreach (var item in listcol)
            {
                AzMetaTableHandle.Handle().ReNameColumnDisplay(item.FldDisplay, currentObject.SchemaName, currentObject.DbObjType, item.FldName);
            }

            LoadMataColum();
        }

        private void ACM_ReIniValue_Click(object sender, EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定初始全部字段？") == DialogResult.Cancel)
            {
                return;
            }
            AzMetaCloumHandle.Handle().InitColumnValueSchema(currentObject.SchemaName);
            LoadMataColum();
        }

        private void tspMenuItemTurn_Click(object sender, EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定根据DB数据类型初始代码数据类型吗？") == DialogResult.Cancel)
            {
                return;
            }
            var listcol = AzMetaCustomCloumHandle.Handle().Select().Where(t => t.TableName == currentObject.SchemaName).Go();
            foreach (var item in listcol)
            {
                var entitype = MetaDataTypeHandle.GetMetaDataType(item.FldType);
                item.FldCodeType = entitype.CodeType;
                AzMetaCustomCloumHandle.Handle().Update(item);
            }
            LoadMataColum();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            AzPropertyCopyOp.ShowAzPropertyCopyOp(currentDatas);
        }

        public void SetCodeCreate(string codeStr)
        {
            richTextBox1.Text = codeStr;
            tabControl1.SelectedTab = tabPage2;
        }
        public void SetCodeCreateMsg(string msg)
        {

            textBox1.Text = msg;
        }


        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {

        }

        private void contextMenuStrip2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (richTextBox1.CanRedo)//redo
                {
                    richRedo.Enabled = true;
                }
                else
                {
                    richRedo.Enabled = false;
                }

                if (richTextBox1.CanUndo)//undo
                {
                    richUndo.Enabled = true;
                }
                else
                {
                    richUndo.Enabled = false;
                }

                if (richTextBox1.SelectionLength > 0)
                {
                    richCopy.Enabled = true;
                    richCut.Enabled = true;
                }
                else
                {
                    richCopy.Enabled = false;
                    richCut.Enabled = false;
                }
                if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
                {
                    richPaste.Enabled = true;
                }
                else
                {
                    richPaste.Enabled = false;
                }

                contextMenuStrip2.Show(richTextBox1, new Point(e.X, e.Y));
            }
        }

        private void richCopy_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void richClear_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void richPaste_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.SelectionStart + richTextBox1.SelectionLength;
            richTextBox1.Paste();
        }

        private void richCut_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void richUndo_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanUndo)
            {
                richTextBox1.Undo();
            }
        }

        private void richRedo_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanRedo)
            {
                richTextBox1.Redo();
            }
        }

        private void richSelectAll_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            tspBtnSave.Enabled = true;
        }

        private void tspLookSchema_Click(object sender, EventArgs e)
        {
            LoadMataColum(false);
        }

        private void tspLookParam_Click(object sender, EventArgs e)
        {
            LoadMataColum(true);
        }
    }
}
