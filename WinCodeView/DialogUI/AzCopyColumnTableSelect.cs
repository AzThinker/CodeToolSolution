using MetaWorkLib.Domain;
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

namespace WinCodeView
{
    public partial class AzCopyColumnTableSelect : Form
    {
        private TreeNode tablesNode = new TreeNode("数据表") { Name = "atk_tablesNode" };
        private TreeNode viewsNode = new TreeNode("数据视图") { Name = "atk_viewsNode" };
        private List<TreeNode> listNodes = new List<TreeNode>();
        private List<AzMetaTableEntity> azDBSchemas = new List<AzMetaTableEntity>();
        private string result = string.Empty;
        public AzCopyColumnTableSelect()
        {
            InitializeComponent();
            treeView1.Nodes.Add(tablesNode);
            treeView1.Nodes.Add(viewsNode);

            listNodes.Add(tablesNode);
            listNodes.Add(viewsNode);
            WorkObjModeInit();
            tablesNode.ExpandAll();
        }

        public static string ShowAzCopyColumnTableSelect()
        {
            AzCopyColumnTableSelect azCopyColumnTableSelect = new AzCopyColumnTableSelect();

            if (azCopyColumnTableSelect.ShowDialog() == DialogResult.OK)
            {
                return azCopyColumnTableSelect.result;

            }
            return string.Empty;
        }

        private void ClearNodes()
        {

            foreach (var item in listNodes)
            {
                item.Checked = false;
                item.Nodes.Clear();

            }
        }

        private void WorkObjModeInit()
        {
            ClearNodes();
            azDBSchemas = AzMetaTableHandle.Handle().GetDBSchema();
            foreach (var item in azDBSchemas)
            {

                switch (item.ObjDataType)
                {
                    case 1:
                        tablesNode.Nodes.Add(item.SchemaName, $"{item.SchemaName}{item.ClassDisPlay.AddBrackets()}");
                        break;

                    case 2:
                        viewsNode.Nodes.Add(item.SchemaName, $"{item.SchemaName}{item.ClassDisPlay.AddBrackets()}");
                        break;


                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                return;
            }

            if (treeView1.SelectedNode.Level == 1)
            {
                result = treeView1.SelectedNode.Name;
                DialogResult = DialogResult.OK;
            }
        }
    }
}
