using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinCodeView
{
    public partial class F_Progress : Form
    {
        public F_Progress(BackgroundWorker worker)
        {
            InitializeComponent();
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        public void MsgText(string msg)
        {
            label1.Text ="正在"+ msg+"，请等待... ...";
        }

        public void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }
    }
}
