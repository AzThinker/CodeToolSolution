namespace WinCodeView
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.基础设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ACM_BaseSet = new System.Windows.Forms.ToolStripMenuItem();
            this.ACM_AzDBSet = new System.Windows.Forms.ToolStripMenuItem();
            this.AzExitApp = new System.Windows.Forms.ToolStripMenuItem();
            this.业务设计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ACM_CustomMateManage = new System.Windows.Forms.ToolStripMenuItem();
            this.ACM_BusinessDesign = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.基础设置ToolStripMenuItem,
            this.业务设计ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1334, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 基础设置ToolStripMenuItem
            // 
            this.基础设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ACM_BaseSet,
            this.ACM_AzDBSet,
            this.AzExitApp});
            this.基础设置ToolStripMenuItem.Name = "基础设置ToolStripMenuItem";
            this.基础设置ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.基础设置ToolStripMenuItem.Text = "基础设置";
            // 
            // ACM_BaseSet
            // 
            this.ACM_BaseSet.Name = "ACM_BaseSet";
            this.ACM_BaseSet.Size = new System.Drawing.Size(136, 22);
            this.ACM_BaseSet.Text = "基础配置";
            this.ACM_BaseSet.Click += new System.EventHandler(this.ACM_BaseSet_Click);
            // 
            // ACM_AzDBSet
            // 
            this.ACM_AzDBSet.Name = "ACM_AzDBSet";
            this.ACM_AzDBSet.Size = new System.Drawing.Size(136, 22);
            this.ACM_AzDBSet.Text = "数据库设置";
            this.ACM_AzDBSet.Click += new System.EventHandler(this.ACM_AzDBSet_Click);
            // 
            // AzExitApp
            // 
            this.AzExitApp.Name = "AzExitApp";
            this.AzExitApp.Size = new System.Drawing.Size(136, 22);
            this.AzExitApp.Text = "退出程序";
            this.AzExitApp.Click += new System.EventHandler(this.AzExitApp_Click);
            // 
            // 业务设计ToolStripMenuItem
            // 
            this.业务设计ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ACM_CustomMateManage,
            this.ACM_BusinessDesign});
            this.业务设计ToolStripMenuItem.Name = "业务设计ToolStripMenuItem";
            this.业务设计ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.业务设计ToolStripMenuItem.Text = "业务设计";
            // 
            // ACM_CustomMateManage
            // 
            this.ACM_CustomMateManage.Name = "ACM_CustomMateManage";
            this.ACM_CustomMateManage.Size = new System.Drawing.Size(148, 22);
            this.ACM_CustomMateManage.Text = "业务模型设计";
            this.ACM_CustomMateManage.Click += new System.EventHandler(this.ACM_CustomMateManage_Click);
            // 
            // ACM_BusinessDesign
            // 
            this.ACM_BusinessDesign.Name = "ACM_BusinessDesign";
            this.ACM_BusinessDesign.Size = new System.Drawing.Size(148, 22);
            this.ACM_BusinessDesign.Text = "业务设计";
            this.ACM_BusinessDesign.Click += new System.EventHandler(this.ACM_BusinessDesign_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1334, 619);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "AzThinker代码生成工具";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 基础设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ACM_BaseSet;
        private System.Windows.Forms.ToolStripMenuItem ACM_AzDBSet;
        private System.Windows.Forms.ToolStripMenuItem AzExitApp;
        private System.Windows.Forms.ToolStripMenuItem 业务设计ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ACM_CustomMateManage;
        private System.Windows.Forms.ToolStripMenuItem ACM_BusinessDesign;
    }
}

