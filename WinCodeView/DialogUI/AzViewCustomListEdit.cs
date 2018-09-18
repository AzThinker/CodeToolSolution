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

namespace WinCodeView
{
    public partial class AzViewCustomListEdit : Form
    {
        public AzViewCustomListEdit()
        {
            InitializeComponent();
        }

        public static string ShowAzViewCustomListEdit(string customlist)
        {
            AzViewCustomListEdit azViewCustomList = new AzViewCustomListEdit();
            azViewCustomList.SetCustomList(customlist);
            if (azViewCustomList.ShowDialog() == DialogResult.OK)
            {
                return azViewCustomList.GetCustomList();
            }
            return customlist;
        }

        private void SetCustomList(string customlist)
        {
            string[] clist = customlist.Split('|');
            if (clist.Count() < 3)
            {
                SetTableFrom();
                return;
            }
            SetTableFrom(clist[0]);
            SetTableFiedFrom(clist[0], clist[1], clist[2]);
        }

        private string GetCustomList()
        {
            if (string.IsNullOrWhiteSpace(cbTableFrom.Text))
            {
                return string.Empty;
            }

            return cbTableFrom.Text + "|" + cbKeyField.Text + "|" + cbDisplayField.Text + "|" ;

        }

        private void SetTableFrom(string tablename = "")
        {
            cbTableFrom.Text = "";
            cbKeyField.Text = "";
            cbDisplayField.Text = "";
            cbTableFrom.Items.Clear();
            cbTableFrom.Items.AddRange(AzMetaTableHandle.Handle().GetDBTableList().ToArray());
            cbKeyField.Items.Clear();
            cbDisplayField.Items.Clear();
            if (!string.IsNullOrWhiteSpace(tablename))
            {
                cbTableFrom.Text = tablename;
            }

        }

        private void SetTableFiedFrom(string tablename, string keyfieldname = "", string displayfieldname = "")
        {
            cbKeyField.Items.Clear();
            cbDisplayField.Items.Clear();
            cbKeyField.Items.AddRange(AzMetaTableHandle.Handle().GetDBTableFieldList(tablename).ToArray());
            cbDisplayField.Items.AddRange(AzMetaTableHandle.Handle().GetDBTableFieldList(tablename).ToArray());
            if (!string.IsNullOrWhiteSpace(keyfieldname))
            {
                cbKeyField.Text = keyfieldname;
            }
            if (!string.IsNullOrWhiteSpace(displayfieldname))
            {
                cbDisplayField.Text = displayfieldname;
            }
        }

        private void cbTableFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTableFiedFrom(cbTableFrom.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetTableFrom();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
