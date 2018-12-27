using DataTableToObjectEx;
using MetaWorkLib.Config;
using MetaWorkLib.Domain;
using MetaWorkLib.MetaInit;
using MetaWorkLib.Utils;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TableTranslatorEx;

namespace WinCodeView
{
    public partial class AzCustomMateManage : AtkForm
    {
        private TreeNode tnTables = new TreeNode("业务模型");
        private TreeNode tnViews = new TreeNode("业务子模型");
        private string currentSelectObjModeName = string.Empty;
        private string currentschemaFrom = string.Empty;
        private IEnumerable<AzMetaTableEntity> azMetaTableEntities = null;
        private AzMetaTableEntity metaTableEntity = new AzMetaTableEntity();
        public AzCustomMateManage() : base()
        {
            InitializeComponent();

            AzGetCustomList();
        }

        private void AzGetCustomList()
        {

            treeViewBusiness.Nodes.Clear();
            tnTables.Nodes.Clear();
            tnViews.Nodes.Clear();
            treeViewBusiness.Nodes.Add(tnTables);
            treeViewBusiness.Nodes.Add(tnViews);
            //
            //
            azMetaTableEntities = AzMetaTableHandle.Handle().Select()
                  .Where(e => e.ObjDataType == 1)
                  .Where(e => e.IsCustom == true)
                  .Go();


            foreach (AzMetaTableEntity item in azMetaTableEntities)
            {
                switch (item.ObjModeType)
                {
                    case 1:
                        tnTables.Nodes.Add(item.ObjModeName, item.ObjModeName + item.ClassDisPlay.AddBrackets()).Tag = 1;
                        break;
                    case 4:
                        tnViews.Nodes.Add(item.ObjModeName, item.ObjModeName + item.ClassDisPlay.AddBrackets()).Tag = 4;
                        break;
                }
            }
            treeViewBusiness.ExpandAll();
            ButtonAction();
        }

        public static void ShowAzCustomMateManage(Form owner)
        {
            if (!MetadataOperate.ConfigWhetherInit())
            {
                GeneralHelpler.SomethingWarning("当前配置未正确设置，请重新设置！！");
                return;
            }

            if (!MetadataOperate.MetaWhetherInit())
            {
                GeneralHelpler.SomethingWarning("当前配置元数据没有初始化，请先生成！！");
                return;
            }
            foreach (var f in owner.MdiChildren)
            {

                if (f.Name == typeof(AzCustomMateManage).Name)
                {
                    f.BringToFront();
                    return;
                }
            }
            AzCustomMateManage azCustomMateManage = new AzCustomMateManage
            {
                MdiParent = owner
            };
            azCustomMateManage.Show();
        }

        private void tspButtonClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void tspButtonAdd_Click(object sender, System.EventArgs e)
        {
            if (AzCustomMetaDataEdit.ShowAzCustomMetaDataEdit(this) == DialogResult.OK)
            {
                AzGetCustomList();
            }
        }

        private void LoadMataColum(string schemaFrom)
        {
            toolStrip2.Enabled = true;
            var c = AzMetaCustomCloumHandle.Handle().Select().Where(t => t.TableName == currentSelectObjModeName).OrderBy(t => t.ShowOrder).Go();
            DataTable data = Translator.Translate<AzMetaProfile, AzMetaCustomCloumEntity>(c, "AtkMetaCustomCloum");
            data.AcceptChanges();
            azMetaCustomCloumListEntityBindingSource.DataSource = data;
            azMetaCustomCloumListEntityBindingSource.EndEdit();
            if (string.IsNullOrWhiteSpace(schemaFrom))
            {
                tspButtonImport.Enabled = false;
            }
            else
            {
                tspButtonImport.Enabled = c.Where(m => m.SchemaFrom == schemaFrom).Count() == 0;
            }

        }

        private void treeViewBusiness_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            metaTableEntity = new AzMetaTableEntity();
            if (e.Node.Level == 1)
            {
                currentSelectObjModeName = e.Node.Name;
                metaTableEntity = azMetaTableEntities.AsQueryable().Where(m => m.ObjModeName == currentSelectObjModeName).FirstOrDefault();
                tspButtonImport.Enabled = !string.IsNullOrWhiteSpace(metaTableEntity.SchemaFrom);
                currentschemaFrom = metaTableEntity.SchemaFrom;
                LoadMataColum(metaTableEntity.SchemaFrom);
                azMetaTableEntityBindingSource.DataSource = metaTableEntity;
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
            else
            {
                azMetaCustomCloumListEntityBindingSource.DataSource = new AzMetaCustomCloumListEntity();
                azMetaTableEntityBindingSource.DataSource = metaTableEntity;
                toolStrip2.Enabled = false;
                currentSelectObjModeName = string.Empty;
                currentschemaFrom = string.Empty;


            }
            ButtonAction(e.Node);
        }

        private void tspButtonEdit_Click(object sender, System.EventArgs e)
        {
            if (treeViewBusiness.SelectedNode.Level == 1)
            {
                if (AzCustomMetaDataEdit.ShowAzCustomMetaDataEdit(this, treeViewBusiness.SelectedNode.Name) == DialogResult.OK)
                {
                    AzGetCustomList();
                }
            }

        }

        private void tspButtonDel_Click(object sender, System.EventArgs e)
        {
            if (treeViewBusiness.SelectedNode.Level == 1)
            {
                var lused = AzMetaTableHandle.Handle().Select().Where(t => t.SchemaFrom == currentSelectObjModeName).Go();

                if (lused != null)
                {
                    if (lused.Count() > 0)
                    {
                        GeneralHelpler.SomethingWarning("当前元数已经被引用，不能删除？");

                        return;

                    }

                }

                string msg = $"确定要删除  {treeViewBusiness.SelectedNode.Text}";
                if (GeneralHelpler.ConfirmQuestionOperate(msg) == DialogResult.OK)
                {
                    AzMetaTableHandle.Handle().Delete(treeViewBusiness.SelectedNode.Name);
                    AzGetCustomList();
                }
            }
        }
        private void ButtonAction(TreeNode node = null)
        {
            bool enabled = false;
            if (node != null)
            {
                enabled = node.Level == 1;
            }
            tspButtonEdit.Enabled = enabled;
            tspButtonDel.Enabled = enabled;
            tspBtnSave.Enabled = DataChangeAskSave();
            tspButtonSort.Enabled = !tspBtnSave.Enabled && enabled;
        }

        private void treeViewBusiness_Click(object sender, System.EventArgs e)
        {

        }

        private void tspButtonFirst_Click(object sender, System.EventArgs e)
        {
            azMetaCustomCloumListEntityBindingSource.MoveFirst();

        }

        private void tspButtonBefort_Click(object sender, System.EventArgs e)
        {
            azMetaCustomCloumListEntityBindingSource.MovePrevious();
        }

        private void tspButtonNext_Click(object sender, System.EventArgs e)
        {
            azMetaCustomCloumListEntityBindingSource.MoveNext();

        }

        private void tspButtonLast_Click(object sender, System.EventArgs e)
        {
            azMetaCustomCloumListEntityBindingSource.MoveLast();
        }



        private void azMetaCustomCloumListEntityBindingSource_CurrentChanged(object sender, System.EventArgs e)
        {

        }

        private void azMetaCustomCloumListEntityBindingSource_CurrentItemChanged(object sender, System.EventArgs e)
        {

        }

        private void tspBtnSave_Click(object sender, System.EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定保存当前所有修改吗？") == DialogResult.Cancel)
            {
                return;
            }

            azMetaCustomCloumListEntityBindingSource.EndEdit();
            dataGridView1.EndEdit();
            var vlist = (azMetaCustomCloumListEntityBindingSource.DataSource as DataTable).GetChanges();
            if (vlist == null)
            {
                return;
            }

            foreach (DataRow item in vlist.Rows)
            {

                var entity = item.ToObjectSafe<AzMetaCustomCloumEntity>();
                AzMetaCustomCloumHandle.Handle().UpdateBatch(entity);

            }
            vlist.AcceptChanges();
            (azMetaCustomCloumListEntityBindingSource.DataSource as DataTable).AcceptChanges();
            ButtonAction();
        }

        private void tspButtonImport_Click(object sender, System.EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定导入元数据吗？") == DialogResult.Cancel)
            {
                return;
            }
            var azMetaTable = azMetaTableEntities.AsQueryable().Where(m => m.ObjModeName == currentSelectObjModeName).FirstOrDefault();
            if (azMetaTable != null)
            {

                var listcol = AzMetaCustomCloumHandle.Handle().Select().Where(m => m.TableName == azMetaTable.SchemaFrom).Go();
                MetadataOperate.ImportCustomMetaData(azMetaTable.SchemaFrom, azMetaTable.SchemaName, listcol.Count() == 0);
                LoadMataColum(azMetaTable.SchemaFrom);
                //  tspButtonImport.Enabled = false;
            }

        }

        private void tspBtnDelete_Click(object sender, System.EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定删除当前所选行吗？") == DialogResult.Cancel)
            {
                return;
            }
            var drow = azMetaCustomCloumListEntityBindingSource.Current;
            if (drow != null)
            {
                var row = ((DataRowView)drow).Row;
                var entity = row.ToObjectSafe<AzMetaCustomCloumEntity>();
                if (AzMetaCustomCloumHandle.Handle().Delete(entity) == 1)
                {
                    azMetaCustomCloumListEntityBindingSource.RemoveCurrent();
                }
            }
        }

        private void tspBtnEdit_Click(object sender, System.EventArgs e)
        {
            AzCustomColumsEdit.ShowAzCustomColumsEdit(azMetaCustomCloumListEntityBindingSource);
        }

        private void tspBtnAdd_Click(object sender, System.EventArgs e)
        {
            AzCustomColumsEdit.ShowAzCustomColumsEdit(azMetaCustomCloumListEntityBindingSource, currentSelectObjModeName, true);
        }

        private void tspCancelEdit_Click(object sender, System.EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定取消当前修改吗？") == DialogResult.Cancel)
            {
                return;
            }
            var drow = azMetaCustomCloumListEntityBindingSource.Current;
            if (drow != null)
            {
                var row = ((DataRowView)drow).Row;
                var entity = row.ToObject<AzMetaCustomCloumEntity>();
                var c = AzMetaCustomCloumHandle.Handle().Select().Where(t => t.Id == entity.Id).Go();
                DataTable data = Translator.Translate<AzMetaProfile, AzMetaCustomCloumEntity>(c, "AtkMetaCustomCloum");
                row.ItemArray = data.AsEnumerable().FirstOrDefault().ItemArray;
                row.AcceptChanges();
                tspBtnSave.Enabled = DataChangeAskSave();
                tspButtonSort.Enabled = !tspBtnSave.Enabled && tspBtnEdit.Enabled;
            }
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

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[0].Value = row.Index + 1;
            }
        }

        private void azMetaCustomCloumListEntityBindingSource_PositionChanged(object sender, System.EventArgs e)
        {

            tspBtnSave.Enabled = DataChangeAskSave();
            tspButtonSort.Enabled = !tspBtnSave.Enabled && tspBtnEdit.Enabled;

        }

        private bool DataChangeAskSave()
        {
            if (azMetaCustomCloumListEntityBindingSource.DataSource == null)
            {
                return false;
            }
            if ((azMetaCustomCloumListEntityBindingSource.DataSource as DataTable) == null)
            {
                return false;
            }
            var list = (azMetaCustomCloumListEntityBindingSource.DataSource as DataTable).GetChanges();
            return list != null;
        }

        private void tspMenuItemTurn_Click(object sender, System.EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定根据DB数据类型初始代码数据类型吗？") == DialogResult.Cancel)
            {
                return;
            }
            // var listdbtype = TypeMetaHandle.GetTypes();

            var listcol = AzMetaCustomCloumHandle.Handle().Select().Where(t => t.TableName == currentSelectObjModeName).Go();

            foreach (var item in listcol)
            {
                var entitype = MetaDataTypeHandle.GetMetaDataType(item.FldType);
                if (entitype != null)
                {
                    item.FldCodeType = entitype.CodeType;
                    AzMetaCustomCloumHandle.Handle().Update(item);
                }

            }

            LoadMataColum(currentschemaFrom);
        }

        private void tspClearAll_Click(object sender, System.EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定清除所有行吗？") == DialogResult.Cancel)
            {
                return;
            }
            AzMetaCustomCloumHandle.Handle().DeleteAll(currentSelectObjModeName);
            LoadMataColum(currentschemaFrom);
        }

        private void tspButtonSort_Click(object sender, System.EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定生成顺序？") == DialogResult.Cancel)
            {
                return;
            }
            var listcol = AzMetaCustomCloumHandle.Handle().Select().Where(t => t.TableName == currentSelectObjModeName).OrderBy(t => t.ShowOrder).Go();
            var odi = 1;
            foreach (var item in listcol)
            {

                item.ShowOrder = odi;
                AzMetaCustomCloumHandle.Handle().Update(item);

                odi += 1;
            }

            LoadMataColum(currentschemaFrom);
        }

        private void tspMenuItemDisplay_Click(object sender, System.EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("确定生成显示名（已经有显示名的不合更改）？") == DialogResult.Cancel)
            {
                return;
            }
            var listcol = AzMetaCustomCloumHandle.Handle().Select().Where(t => t.TableName == currentSelectObjModeName).OrderBy(t => t.ShowOrder).Go();

            foreach (var item in listcol)
            {
                if (string.IsNullOrWhiteSpace(item.FldDisplay))
                {
                    item.FldDisplay = item.FldName;
                    AzMetaCustomCloumHandle.Handle().Update(item);
                }
            }

            LoadMataColum(currentschemaFrom);
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool enable = !tspBtnSave.Enabled && tspBtnEdit.Enabled;
            tspClearAll.Enabled = enable;
            if (tspBtnEdit.Enabled)
            {
                var cuitem = (azMetaCustomCloumListEntityBindingSource.Current as DataRowView);

                tspCancelEdit.Enabled = cuitem.Row.RowState == DataRowState.Modified;
            }

            tspMenuItemDisplay.Enabled = enable;
            tspMenuItemTurn.Enabled = enable;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ACM_CreateDBTabel_Click(object sender, System.EventArgs e)
        {

        }

        private void dataGridView1_Validated(object sender, System.EventArgs e)
        {

        }

        private int CreateDBTabeOp(string objModeName)
        {
            AzMetaTableEntity azMetaTable = AzMetaTableHandle.Handle().Select()
                           .Where(t => t.ObjModeName == objModeName)
                           .Go().FirstOrDefault();
            IEnumerable<AzMetaCustomCloumEntity> cloumListEntities = AzMetaCustomCloumHandle.Handle().Select()
                .Where(t => t.TableName == azMetaTable.SchemaName)
                .Go();
            string strcreate = DataOperate.CreateDBObject(azMetaTable, cloumListEntities);
            int result = MetadataOperate.ExecuteCmd(strcreate);
            if (result > 0)
            {
                azMetaTable.ObjModeType = 0;
                azMetaTable.ObjDataType = 0;
                AzMetaTableHandle.Handle().Updata(azMetaTable);



            }
            return result;
        }

        private void tspMenuCreateDBTabelToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (CreateDBTabeOp(currentSelectObjModeName) > 0)
            {
                azMetaCustomCloumListEntityBindingSource.DataSource = new AzMetaCustomCloumListEntity();
                AzGetCustomList();
            }
        }

        private void ACM_CreateDBTabelAll_Click(object sender, System.EventArgs e)
        {
            azMetaCustomCloumListEntityBindingSource.DataSource = new AzMetaCustomCloumListEntity();

            foreach (var n in tnTables.Nodes)
            {
                if (n is TreeNode treeNode)
                {
                    if (treeNode.Checked)
                    {
                        CreateDBTabeOp(treeNode.Name);

                    }
                }
            }
            AzGetCustomList();
        }

        private void contextMenuStrip2_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tspMenuCreateDBTabel.Enabled = false;
            tspMenuCreateAllDBTabel.Enabled = false;
            if (metaTableEntity == null)
            {
                return;
            }

            tspMenuCreateDBTabel.Enabled = metaTableEntity.ObjModeType == 1;
            if (tnTables.Nodes.Count == 0)
            {
                return;
            }

            foreach (var n in tnTables.Nodes)
            {
                if (n is TreeNode treeNode)
                {
                    if (treeNode.Checked)
                    {
                        tspMenuCreateAllDBTabel.Enabled = true;
                        return;
                    }
                }
            }


        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
