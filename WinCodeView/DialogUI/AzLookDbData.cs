using MetaWorkLib.MetaInit;
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
    public partial class AzLookDbData : Form
    {
        public AzLookDbData()
        {
            InitializeComponent();
        }


        private void SetLookDB(string tablename,int top=100)
        {
            bindingSource1.DataSource = DataHelper.LookDbData(tablename,top);
            dataGridView1.DataSource = bindingSource1;
        }

        public static void ShowAzLookDbData(string tablename)
        {
            AzLookDbData azLookDbData = new AzLookDbData();
            azLookDbData.SetLookDB(tablename);
            azLookDbData.ShowDialog();

        }
    }
}
