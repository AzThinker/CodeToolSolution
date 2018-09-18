using MetaWorkLib.Config;
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
    public partial class AzCreateCodeSelect : Form
    {
        public AzCreateCodeSelect()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string xmlpath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            XmlSettings xmlSettings = new XmlSettings(xmlpath + @"CreateCodeSelect.xml");
            xmlSettings.cleanupXML();
            foreach (TreeNode nitem in treeView1.Nodes)
            {
               
                if (nitem.Nodes.Count > 0)
                {
                    foreach (TreeNode cnitem in nitem.Nodes)
                    {
                        xmlSettings.addBoolean(cnitem.Name, cnitem.Checked);
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string xmlpath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            XmlSettings xmlSettings = new XmlSettings(xmlpath + @"CreateCodeSelect.xml");
            xmlSettings.cleanupXML();

            foreach (TreeNode nitem in treeView1.Nodes)
            {
               
                if (nitem.Nodes.Count > 0)
                {
                    foreach (TreeNode cnitem in nitem.Nodes)
                    {
                        cnitem.Checked = xmlSettings.readBoolean(cnitem.Name);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (TreeNode nitem in treeView1.Nodes)
            {

                if (nitem.Nodes.Count > 0)
                {
                    foreach (TreeNode cnitem in nitem.Nodes)
                    {
                        cnitem.Checked = !cnitem.Checked;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (TreeNode nitem in treeView1.Nodes)
            {

                if (nitem.Nodes.Count > 0)
                {
                    foreach (TreeNode cnitem in nitem.Nodes)
                    {
                        cnitem.Checked = true;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (TreeNode nitem in treeView1.Nodes)
            {

                if (nitem.Nodes.Count > 0)
                {
                    foreach (TreeNode cnitem in nitem.Nodes)
                    {
                        cnitem.Checked = false;
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private List<string> GetSelects()
        {
            List<string> lists = new List<string>();
            foreach (TreeNode nitem in treeView1.Nodes)
            {

                if (nitem.Nodes.Count > 0)
                {
                    foreach (TreeNode cnitem in nitem.Nodes)
                    {
                        if (cnitem.Checked)
                        {
                            lists.Add(cnitem.Name);
                        }
                    }
                }
            }
            return lists;
        }

        public static List<string> ShowAzCreateCodeSelect()
        {
            AzCreateCodeSelect azCreateCodeSelect = new AzCreateCodeSelect();
            if (azCreateCodeSelect.ShowDialog() == DialogResult.OK)
            {
               return azCreateCodeSelect.GetSelects();
            }
            return new List<string>();
        }

        private void AzCreateCodeSelect_Load(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }
    }
}
