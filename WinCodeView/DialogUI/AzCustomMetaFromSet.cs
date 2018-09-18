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
    public partial class AzCustomMetaFromSet : Form
    {
        private TreeNode tnTables = new TreeNode("业务模型");
        private TreeNode tnViews = new TreeNode("业务子模型");
        private string currentSelectTable = string.Empty;

        public AzCustomMetaFromSet()
        {
            InitializeComponent();

        }

        public static string ShowCustomMetaFromSet(string nodeTable)
        {
            AzCustomMetaFromSet azCustomMetaFromSet = new AzCustomMetaFromSet();

            azCustomMetaFromSet.AzGetCustomList(nodeTable);
            if (azCustomMetaFromSet.ShowDialog() == DialogResult.OK)
            {
                return azCustomMetaFromSet.currentSelectTable;
            }
            return string.Empty;
        }

        private void AzGetCustomList(string nodeTable)
        {

            treeViewBusiness.Nodes.Clear();
            tnTables.Nodes.Clear();
            tnViews.Nodes.Clear();
            treeViewBusiness.Nodes.Add(tnTables);
            treeViewBusiness.Nodes.Add(tnViews);
            //
            //
            var results = AzMetaTableHandle.Handle().Select()
                  .Where(e => e.ObjDataType == 1).And(e => e.ObjModeName != nodeTable)
                  .Go();


            foreach (AzMetaTableEntity item in results)
            {
                switch (item.ObjModeType)
                {
                    case 1:
                        tnTables.Nodes.Add(item.ObjModeName, item.ObjModeName + item.ClassDisPlay.AddBrackets());
                        break;
                    case 4:
                        tnViews.Nodes.Add(item.ObjModeName, item.ObjModeName + item.ClassDisPlay.AddBrackets());
                        break;
                }
            }
            treeViewBusiness.ExpandAll();

        }

        private void treeViewBusiness_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            AzMetaTableEntity metaTableEntity = new AzMetaTableEntity();
            if (e.Node.Level == 1)
            {
                textBox1.Text = e.Node.Name;
                currentSelectTable = e.Node.Name;
                btnOk.Enabled = true;
            }
            else
            {

                textBox1.Text = string.Empty;
                currentSelectTable = string.Empty;
                btnOk.Enabled = false;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
