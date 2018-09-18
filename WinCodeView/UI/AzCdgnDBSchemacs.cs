
using MetaWorkLib.Config;
using MetaWorkLib.Domain;
using MetaWorkLib.MetaInit;
using MetaWorkLib.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WinCodeView.UI
{
    public partial class AzCdgnDBSchema : UserControl
    {
        private TreeNode tablesNode = new TreeNode("数据表") { Name = "atk_tablesNode" };
        private TreeNode customTablesNode = new TreeNode("自定义数据表") { Name = "atk_customTablesNode" };
        private TreeNode viewsNode = new TreeNode("数据视图") { Name = "atk_viewsNode" };
        //  private TreeNode customViewsNode = new TreeNode("自定义数据视图") { Name = "atk_customViewsNode" };
        private TreeNode childViewsNode = new TreeNode("子数据") { Name = "atk_childViewsNode" };
        private TreeNode storedProcedureNode = new TreeNode("存储过程") { Name = "atk_storedProcedureNode" };
        private List<TreeNode> listNodes = new List<TreeNode>();
        private AzMetaTableEntity metaTableEntity = new AzMetaTableEntity();
        private List<AzMetaTableEntity> azDBSchemas = new List<AzMetaTableEntity>();
        //  private string currentObjType = "TABLE";
        public AzCdgnDBSchema()
        {
            InitializeComponent();
            //

            treeView1.Nodes.Add(tablesNode);
            treeView1.Nodes.Add(customTablesNode);
            treeView1.Nodes.Add(viewsNode);
            //treeView1.Nodes.Add(customViewsNode);
            treeView1.Nodes.Add(childViewsNode);
            treeView1.Nodes.Add(storedProcedureNode);

            listNodes.Add(tablesNode);
            listNodes.Add(customTablesNode);
            listNodes.Add(viewsNode);
            //listNodes.Add(customViewsNode);
            listNodes.Add(childViewsNode);
            listNodes.Add(storedProcedureNode);
            tablesNode.ExpandAll();
            atkTreeView = treeView1;

            if (DesignMode)
            {
                return;
            }
            if (string.Compare(System.Diagnostics.Process.GetCurrentProcess().ProcessName, "devenv") == 0)
            {
                return;
            }
            WorkObjModeInit();
            this.labAppNameDisplay.Text = AzNormalSet.GetAzNormalSet().AzBase.AzNick;
        }

        public ContextMenuStrip GetContextMenu()
        {
            return contextMenuStrip1;
        }
        private string GetCurrentObjType(TreeNode treeNode)
        {
            if (treeNode.Parent == tablesNode)
            {
                return "TABLE";
            }
            if (treeNode.Parent == viewsNode)
            {
                return "VIEW";
            }
            if (treeNode.Parent == storedProcedureNode)
            {
                return "PROCEDURE";
            }

            return "";
        }

        public AzMetaTableEntity GetAzMetaTableEntity()
        {
            return metaTableEntity;
        }

        private TreeView atkTreeView;


        public TreeView AtkTreeView { get => atkTreeView; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public string CurrentSelectNodeName { get => currentSelectNodeName; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int CurrentSelectNodeLevel { get => currentSelectNodeLevel; }

        private string currentSelectNodeName;

        private int currentSelectNodeLevel;

        private void ClearNodes()
        {

            foreach (var item in listNodes)
            {
                item.Checked = false;
                item.Nodes.Clear();

            }
        }

        private void CheckMetaTableExist(AzMetaTableEntity azMetaTableEntity)
        {
            AzNormalSet azNormal = AzNormalSet.GetAzNormalSet();
            if (AzMetaTableHandle.Handle().Select().Where(m => m.SchemaName == azMetaTableEntity.SchemaName).Go().Count() > 0)
            {
                return;
            }
            azMetaTableEntity.IsCustom = false;
            AzMetaTableHandle.Handle().Insert(azMetaTableEntity);

        }

        private void WorkObjModeInit()
        {
            ClearNodes();
            azDBSchemas = AzMetaTableHandle.Handle().GetDBSchema();
            foreach (var item in azDBSchemas)
            {
                CheckMetaTableExist(item);
                switch (item.ObjDataType)
                {
                    case 1:
                        tablesNode.Nodes.Add(item.ObjModeName, $"{item.ObjModeName}{item.ClassDisPlay.AddBrackets()}");
                        break;

                    case 2:
                        viewsNode.Nodes.Add(item.ObjModeName, $"{item.ObjModeName}{item.ClassDisPlay.AddBrackets()}");
                        break;
                    case 3:
                        storedProcedureNode.Nodes.Add(item.ObjModeName, $"{item.ObjModeName}{item.ClassDisPlay.AddBrackets()}");
                        break;

                }
            }

            var entities = AzMetaTableHandle.Handle().Select()
                 .Where(e => e.ObjDataType == 1)
                 .Where(e => e.IsCustom == true)
                 .Go();


            foreach (AzMetaTableEntity item in entities)
            {
                switch (item.ObjModeType)
                {
                    case 1:
                        customTablesNode.Nodes.Add(item.ObjModeName, item.ObjModeName + item.ClassDisPlay.AddBrackets()).Tag = 1;
                        break;
                    case 4:
                        childViewsNode.Nodes.Add(item.ObjModeName, item.ObjModeName + item.ClassDisPlay.AddBrackets()).Tag = 4;
                        break;
                }
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (var item in listNodes)
            {
                item.Checked = true;
                setChildNodeCheckedState(item);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void setParentNodeCheckedState(TreeNode currentNode)
        {

            TreeNode parentNode = currentNode.Parent;

            if (parentNode != null)
            {
                foreach (TreeNode node in parentNode.Nodes)
                {
                    if (!node.Checked)
                    {

                        parentNode.Checked = false;


                        return;
                    }
                }
                parentNode.Checked = true;
            }
        }
        private void setChildNodeCheckedState(TreeNode currentNode)
        {

            if (currentNode.Nodes.Count > 0)
            {
                foreach (TreeNode node in currentNode.Nodes)
                {
                    node.Checked = currentNode.Checked;

                }
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {

        }


        [Category("Atk_TreeView")]
        public event TreeNodeMouseClickEventHandler TreeNodeMouseClick;
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            currentSelectNodeName = e.Node.Name;
            currentSelectNodeLevel = e.Node.Level;
            metaTableEntity = new AzMetaTableEntity();
            if (e.Node.Level == 0)
            {

                setChildNodeCheckedState(e.Node);
            }
            else if (e.Node.Level == 1)
            {

                var mt = AzMetaTableHandle.Handle().Select().Where(m => m.SchemaName == currentSelectNodeName).Go().FirstOrDefault();
                if (mt == null)
                {
                    metaTableEntity = azDBSchemas.Where(m => m.ObjModeName == currentSelectNodeName).FirstOrDefault();
                    metaTableEntity.ComeFrom = 1;

                }
                else
                {
                    metaTableEntity = mt;
                    metaTableEntity.ComeFrom = 2;
                }
                metaTableEntity.DbObjType = GetCurrentObjType(e.Node);
                setParentNodeCheckedState(e.Node);
            }

            TreeNodeMouseClick?.Invoke(sender, e);
        }

        private void btnClearAllSelect_Click(object sender, EventArgs e)
        {

            foreach (var item in listNodes)
            {
                item.Checked = false;
                setChildNodeCheckedState(item);
            }
        }

        private void BtnSaveSelect_Click(object sender, EventArgs e)
        {
            string xmlpath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            XmlSettings xmlSettings = new XmlSettings(xmlpath + @"WorkClassNodes.xml");
            xmlSettings.cleanupXML();
            foreach (TreeNode nitem in treeView1.Nodes)
            {
                xmlSettings.addBoolean(nitem.Name, nitem.Checked);
                if (nitem.Nodes.Count > 0)
                {
                    foreach (TreeNode cnitem in nitem.Nodes)
                    {
                        xmlSettings.addBoolean(cnitem.Name, cnitem.Checked);
                    }
                }
            }
        }

        private void btnImportSelect_Click(object sender, EventArgs e)
        {
            string xmlpath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            XmlSettings xmlSettings = new XmlSettings(xmlpath + @"WorkClassNodes.xml");
            xmlSettings.cleanupXML();

            foreach (TreeNode nitem in treeView1.Nodes)
            {
                nitem.Checked = xmlSettings.readBoolean(nitem.Name);
                if (nitem.Nodes.Count > 0)
                {
                    foreach (TreeNode cnitem in nitem.Nodes)
                    {
                        cnitem.Checked = xmlSettings.readBoolean(cnitem.Name);
                    }
                }
            }
        }

        [Category("Atk_TreeView")]
        public event EventHandler Atk_MetaDataExportXLS;
        private void aCMMetaDataExportXLSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Atk_MetaDataExportXLS?.Invoke(sender, e);
        }


        private void aCMMetaDataExportAllXLSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string exptxt = "";
            foreach (TreeNode node in treeView1.Nodes)
            {
                if (node.Nodes.Count > 0)
                {
                    foreach (TreeNode childnode in node.Nodes)
                    {
                        if (childnode.Checked)
                        {
                            exptxt += GetNodeExport(childnode);
                        }


                    }
                }
            }
            if (string.IsNullOrWhiteSpace(exptxt))
            {
                GeneralHelpler.SomethingError("当前没有任何数据需要导出！！");

                return;

            }
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (FileStream file = new System.IO.FileStream(saveFileDialog1.FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    using (System.IO.TextWriter text = new System.IO.StreamWriter(file, System.Text.Encoding.Default))
                    {
                        text.Write(exptxt);
                    }
                }



            }

        }

        [Category("Atk_TreeView")]
        public event EventHandler Atk_ReCreateDbData;
        private void aCMReCreateDbDataToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Atk_ReCreateDbData?.Invoke(sender, e);
        }



        private void aCMUpdateDisplayNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                return;
            }

            string sobjtype = GetCurrentObjType(treeView1.SelectedNode);
            if (string.IsNullOrWhiteSpace(sobjtype))
            {
                return;
            }

            string snew = AzDisplayName.ShowAzDisplayName(metaTableEntity.ClassDisPlay);
            if (string.IsNullOrWhiteSpace(snew))
            {
                return;
            }

            if (AzMetaTableHandle.Handle().ReNameTableDisplay(snew, metaTableEntity.SchemaName, sobjtype) == -1)
            {
                WorkObjModeInit();
            }

        }

        [Category("Atk_TreeView")]
        public event EventHandler Atk_ClearClassInf;
        private void aCMClearClassInfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Atk_ClearClassInf?.Invoke(sender, e);
        }


        private void aCMRefToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorkObjModeInit();
        }

        [Category("Atk_TreeView")]
        public event CancelEventHandler Atk_MenuStrip_Opening;
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            CMReCreateDbData.Enabled = currentSelectNodeLevel == 1;
            CMMetaDataExportXLS.Enabled = currentSelectNodeLevel == 1;
            CMUpdateDisplayName.Enabled = currentSelectNodeLevel == 1;
            //
            Atk_MenuStrip_Opening?.Invoke(sender, e);
        }


        private string GetNodeExport(TreeNode treeNode)
        {
            AzMetaTableEntity azMetaTable = new AzMetaTableEntity();
            var mt = AzMetaTableHandle.Handle().Select().Where(m => m.SchemaName == treeNode.Name).Go().FirstOrDefault();
            if (mt == null)
            {
                azMetaTable = azDBSchemas.Where(m => m.ObjModeName == treeNode.Name).FirstOrDefault();
                azMetaTable.ComeFrom = 1;
            }
            else
            {
                azMetaTable = mt;
                azMetaTable.ComeFrom = 2;
            }

            var azMetaCloumEntities = AzMetaCloumHandle.Handle().Select().Where(t => t.TableName == azMetaTable.SchemaName).OrderBy(t => t.ShowOrder).Go();
            string metatstr = "类名：" + azMetaTable.ClassName + " 表名：" + azMetaTable.SchemaName + " 显示名：" + azMetaTable.ClassDisPlay + "\r\n";
            var str = MetaExportTxT.GetDataRows<AzMetaCloumEntity>(azMetaCloumEntities);
            return metatstr + str + "\r\n\r\n";
        }

        [Category("Atk_TreeView")]
        public event Action OnClearAllAfter;

        private void CMClearAllClassInf_Click(object sender, EventArgs e)
        {
            if (GeneralHelpler.ConfirmQuestionOperate("初始所有类信息？") == DialogResult.Cancel)
            {
                return;
            }
            var listcol = AzMetaTableHandle.Handle().Select().Go();

            foreach (var col in listcol)
            {
                AzClassCreatProperty azClassCreatProperty = new AzClassCreatProperty();


                azClassCreatProperty.ClassName = col.ClassName.Replace(' ', '_');
                azClassCreatProperty.DisplayName = col.ClassDisPlay;
                azClassCreatProperty.CurrentSelect = col.SchemaName;
                azClassCreatProperty.NameSpace = AzNormalSet.GetAzNormalSet().AzBase.AzProjectSpace;
                azClassCreatProperty.ProjectName = AzNormalSet.GetAzNormalSet().AzBase.AzProjectName;
                azClassCreatProperty.Nick = AzNormalSet.GetAzNormalSet().AzBase.AzNick;
                switch (col.ObjDataType)
                {
                    case 1:
                        azClassCreatProperty.ObjPresentation.ObjDataType = ObjDataTypeEnum.atk_tables;
                        break;
                    case 2:
                        azClassCreatProperty.ObjPresentation.ObjDataType = ObjDataTypeEnum.atk_views;
                        break;
                    case 3:
                        azClassCreatProperty.ObjPresentation.ObjDataType = ObjDataTypeEnum.atk_FuncstoredProcedure;
                        break;
                }
                azClassCreatProperty.ObjPresentation.UpdateTableName = col.SchemaName;

                JavaScriptSerializer js = new JavaScriptSerializer();
                if (string.IsNullOrWhiteSpace(azClassCreatProperty.ObjPresentation.UpdateTableName))
                {
                    azClassCreatProperty.ObjPresentation.UpdateTableName = col.SchemaName;
                }
                col.CodeSetVales = js.Serialize(azClassCreatProperty);
                col.ClassName = col.ClassName.Replace(' ', '_');
                AzMetaTableHandle.Handle().Updata(col);

            }
            //  AzMetaTableHandle.Handle().UpdataCodeSetVales();
            WorkObjModeInit();
            OnClearAllAfter?.Invoke();
        }

        private void tspInitAllDBtoCodeType_Click(object sender, EventArgs e)
        {
            //
            if (GeneralHelpler.ConfirmQuestionOperate("初始所有字段代码数据类型？") == DialogResult.Cancel)
            {
                return;
            }
            var listcol = AzMetaCustomCloumHandle.Handle().Select().Go();
            foreach (var item in listcol)
            {
                var entitype = MetaDataTypeHandle.GetMetaDataType(item.FldType);
                item.FldCodeType = entitype.CodeType;
                AzMetaCustomCloumHandle.Handle().Update(item);
            }
            WorkObjModeInit();
        }

        [Category("Atk_TreeView")]
        public event EventHandler Atk_CurrentCreateCode;
        private void tspCurrentCreate_Click(object sender, EventArgs e)
        {
            Atk_CurrentCreateCode?.Invoke(sender, e);
        }

        [Category("Atk_TreeView")]
        public event EventHandler Atk_BatchCreateCode;
        private void tspBatchCreate_Click(object sender, EventArgs e)
        {
            Atk_BatchCreateCode?.Invoke(sender, e);
        }

        public List<string> GetSelectNodesName()
        {
            List<string> list = new List<string>();
            foreach (TreeNode node in treeView1.Nodes)
            {
                if (node.Nodes.Count > 0)
                {
                    foreach (TreeNode childnode in node.Nodes)
                    {
                        if (childnode.Checked)
                        {
                            list.Add(childnode.Name);
                        }


                    }
                }
            }
            return list;
        }
    }
}
