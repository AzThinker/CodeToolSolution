namespace WinCodeView
{
    partial class AzClassDesign
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AzClassDesign));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mspCreateCode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem02 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem0102 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem0201 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem03 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem0301 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem0303 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem04 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem0401 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem0402 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem0403 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem06 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem0501 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem0601 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem0602 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem0603 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem0604 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem0605 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem0606 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem0607 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem07ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mspBatchOperate = new System.Windows.Forms.ToolStripMenuItem();
            this.mspMetaOperate = new System.Windows.Forms.ToolStripMenuItem();
            this.mspOtherHandle = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.azCdgnDBSchema1 = new WinCodeView.UI.AzCdgnDBSchema();
            this.about1 = new WinCodeView.UI.about();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.azCdgnDetail1 = new WinCodeView.UI.AzCdgnDetail();
            this.azCdgnClassProperty1 = new WinCodeView.UI.AzCdgnClassProperty();
            this.azCdgnMasterDisplay1 = new WinCodeView.UI.AzCdgnMasterDisplay();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mspCreateCode,
            this.mspBatchOperate,
            this.mspMetaOperate,
            this.mspOtherHandle});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1478, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mspCreateCode
            // 
            this.mspCreateCode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem02,
            this.toolStripMenuItem03,
            this.toolStripMenuItem04,
            this.toolStripMenuItem06,
            this.toolStripSeparator1,
            this.toolStripMenuItem07ToolStripMenuItem});
            this.mspCreateCode.Name = "mspCreateCode";
            this.mspCreateCode.Size = new System.Drawing.Size(68, 21);
            this.mspCreateCode.Text = "代码生成";
            // 
            // toolStripMenuItem02
            // 
            this.toolStripMenuItem02.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem0102,
            this.toolStripMenuItem0201});
            this.toolStripMenuItem02.Name = "toolStripMenuItem02";
            this.toolStripMenuItem02.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem02.Text = "数据层";
            // 
            // toolStripMenuItem0102
            // 
            this.toolStripMenuItem0102.Name = "toolStripMenuItem0102";
            this.toolStripMenuItem0102.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem0102.Text = "数据层访问接口";
            this.toolStripMenuItem0102.Click += new System.EventHandler(this.toolStripMenuItem0102_Click);
            // 
            // toolStripMenuItem0201
            // 
            this.toolStripMenuItem0201.Name = "toolStripMenuItem0201";
            this.toolStripMenuItem0201.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem0201.Text = "数据库访问类";
            this.toolStripMenuItem0201.Click += new System.EventHandler(this.toolStripMenuItem0201_Click);
            // 
            // toolStripMenuItem03
            // 
            this.toolStripMenuItem03.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem0301,
            this.toolStripSeparator2,
            this.toolStripMenuItem0303});
            this.toolStripMenuItem03.Name = "toolStripMenuItem03";
            this.toolStripMenuItem03.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem03.Text = "业务层";
            // 
            // toolStripMenuItem0301
            // 
            this.toolStripMenuItem0301.Name = "toolStripMenuItem0301";
            this.toolStripMenuItem0301.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItem0301.Text = "业务类";
            this.toolStripMenuItem0301.Click += new System.EventHandler(this.toolStripMenuItem0301_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(133, 6);
            // 
            // toolStripMenuItem0303
            // 
            this.toolStripMenuItem0303.Name = "toolStripMenuItem0303";
            this.toolStripMenuItem0303.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItem0303.Text = "业务列表类";
            this.toolStripMenuItem0303.Click += new System.EventHandler(this.toolStripMenuItem0303_Click);
            // 
            // toolStripMenuItem04
            // 
            this.toolStripMenuItem04.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem0401,
            this.toolStripMenuItem0402,
            this.toolStripMenuItem0403});
            this.toolStripMenuItem04.Name = "toolStripMenuItem04";
            this.toolStripMenuItem04.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem04.Text = "Web服务层";
            // 
            // toolStripMenuItem0401
            // 
            this.toolStripMenuItem0401.Name = "toolStripMenuItem0401";
            this.toolStripMenuItem0401.Size = new System.Drawing.Size(176, 22);
            this.toolStripMenuItem0401.Text = "生成WebUIDto类";
            this.toolStripMenuItem0401.Click += new System.EventHandler(this.toolStripMenuItem0401_Click);
            // 
            // toolStripMenuItem0402
            // 
            this.toolStripMenuItem0402.Name = "toolStripMenuItem0402";
            this.toolStripMenuItem0402.Size = new System.Drawing.Size(176, 22);
            this.toolStripMenuItem0402.Text = "生成WebUIList类";
            this.toolStripMenuItem0402.Click += new System.EventHandler(this.toolStripMenuItem0402_Click);
            // 
            // toolStripMenuItem0403
            // 
            this.toolStripMenuItem0403.Name = "toolStripMenuItem0403";
            this.toolStripMenuItem0403.Size = new System.Drawing.Size(176, 22);
            this.toolStripMenuItem0403.Text = "生成WebUI操作类";
            this.toolStripMenuItem0403.Click += new System.EventHandler(this.toolStripMenuItem0403_Click);
            // 
            // toolStripMenuItem06
            // 
            this.toolStripMenuItem06.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem0501,
            this.toolStripSeparator3,
            this.toolStripMenuItem0601,
            this.toolStripMenuItem0602,
            this.toolStripMenuItem0603,
            this.toolStripMenuItem0604,
            this.toolStripMenuItem2,
            this.toolStripMenuItem0605,
            this.toolStripMenuItem0606,
            this.toolStripMenuItem0607});
            this.toolStripMenuItem06.Name = "toolStripMenuItem06";
            this.toolStripMenuItem06.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem06.Text = "Web前端";
            // 
            // toolStripMenuItem0501
            // 
            this.toolStripMenuItem0501.Name = "toolStripMenuItem0501";
            this.toolStripMenuItem0501.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem0501.Text = "控制器生成";
            this.toolStripMenuItem0501.Click += new System.EventHandler(this.toolStripMenuItem0501_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(145, 6);
            // 
            // toolStripMenuItem0601
            // 
            this.toolStripMenuItem0601.Name = "toolStripMenuItem0601";
            this.toolStripMenuItem0601.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem0601.Text = "增加页面";
            this.toolStripMenuItem0601.Click += new System.EventHandler(this.toolStripMenuItem0601_Click);
            // 
            // toolStripMenuItem0602
            // 
            this.toolStripMenuItem0602.Name = "toolStripMenuItem0602";
            this.toolStripMenuItem0602.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem0602.Text = "删除页面";
            this.toolStripMenuItem0602.Click += new System.EventHandler(this.toolStripMenuItem0602_Click);
            // 
            // toolStripMenuItem0603
            // 
            this.toolStripMenuItem0603.Name = "toolStripMenuItem0603";
            this.toolStripMenuItem0603.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem0603.Text = "更改页面";
            this.toolStripMenuItem0603.Click += new System.EventHandler(this.toolStripMenuItem0603_Click);
            // 
            // toolStripMenuItem0604
            // 
            this.toolStripMenuItem0604.Name = "toolStripMenuItem0604";
            this.toolStripMenuItem0604.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem0604.Text = "明细页面";
            this.toolStripMenuItem0604.Click += new System.EventHandler(this.toolStripMenuItem0604_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(145, 6);
            // 
            // toolStripMenuItem0605
            // 
            this.toolStripMenuItem0605.Name = "toolStripMenuItem0605";
            this.toolStripMenuItem0605.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem0605.Text = "单页列表";
            this.toolStripMenuItem0605.Click += new System.EventHandler(this.toolStripMenuItem0605_Click);
            // 
            // toolStripMenuItem0606
            // 
            this.toolStripMenuItem0606.Name = "toolStripMenuItem0606";
            this.toolStripMenuItem0606.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem0606.Text = "分页主页面";
            this.toolStripMenuItem0606.Click += new System.EventHandler(this.toolStripMenuItem0606_Click);
            // 
            // toolStripMenuItem0607
            // 
            this.toolStripMenuItem0607.Name = "toolStripMenuItem0607";
            this.toolStripMenuItem0607.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem0607.Text = "分页明细页面";
            this.toolStripMenuItem0607.Click += new System.EventHandler(this.toolStripMenuItem0607_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // toolStripMenuItem07ToolStripMenuItem
            // 
            this.toolStripMenuItem07ToolStripMenuItem.Name = "toolStripMenuItem07ToolStripMenuItem";
            this.toolStripMenuItem07ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem07ToolStripMenuItem.Text = "退出";
            this.toolStripMenuItem07ToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem07ToolStripMenuItem_Click);
            // 
            // mspBatchOperate
            // 
            this.mspBatchOperate.Name = "mspBatchOperate";
            this.mspBatchOperate.Size = new System.Drawing.Size(68, 21);
            this.mspBatchOperate.Text = "批量生成";
            this.mspBatchOperate.Visible = false;
            // 
            // mspMetaOperate
            // 
            this.mspMetaOperate.Name = "mspMetaOperate";
            this.mspMetaOperate.Size = new System.Drawing.Size(56, 21);
            this.mspMetaOperate.Text = "元数据";
            this.mspMetaOperate.Visible = false;
            // 
            // mspOtherHandle
            // 
            this.mspOtherHandle.Name = "mspOtherHandle";
            this.mspOtherHandle.Size = new System.Drawing.Size(68, 21);
            this.mspOtherHandle.Text = "辅助功能";
            this.mspOtherHandle.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.azCdgnDBSchema1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.about1);
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.azCdgnMasterDisplay1);
            this.splitContainer1.Size = new System.Drawing.Size(1478, 725);
            this.splitContainer1.SplitterDistance = 291;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 2;
            // 
            // azCdgnDBSchema1
            // 
            this.azCdgnDBSchema1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.azCdgnDBSchema1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.azCdgnDBSchema1.Location = new System.Drawing.Point(0, 0);
            this.azCdgnDBSchema1.Name = "azCdgnDBSchema1";
            this.azCdgnDBSchema1.Size = new System.Drawing.Size(291, 725);
            this.azCdgnDBSchema1.TabIndex = 0;
            this.azCdgnDBSchema1.TreeNodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.azCdgnDBSchema1_TreeNodeMouseClick);
            this.azCdgnDBSchema1.Atk_MetaDataExportXLS += new System.EventHandler(this.azCdgnDBSchema1_Atk_MetaDataExportXLS);
            this.azCdgnDBSchema1.Atk_ReCreateDbData += new System.EventHandler(this.azCdgnDBSchema1_Atk_ReCreateDbData);
            this.azCdgnDBSchema1.Atk_ClearClassInf += new System.EventHandler(this.azCdgnDBSchema1_Atk_ClearClassInf);
            this.azCdgnDBSchema1.Atk_MenuStrip_Opening += new System.ComponentModel.CancelEventHandler(this.azCdgnDBSchema1_Atk_MenuStrip_Opening);
            this.azCdgnDBSchema1.OnClearAllAfter += new System.Action(this.azCdgnDBSchema1_OnClearAllAfter);
            this.azCdgnDBSchema1.Atk_CurrentCreateCode += new System.EventHandler(this.azCdgnDBSchema1_Atk_CurrentCreateCode);
            this.azCdgnDBSchema1.Atk_BatchCreateCode += new System.EventHandler(this.azCdgnDBSchema1_Atk_BatchCreateCode);
            this.azCdgnDBSchema1.Load += new System.EventHandler(this.azCdgnDBSchema1_Load);
            // 
            // about1
            // 
            this.about1.Location = new System.Drawing.Point(628, 4);
            this.about1.Name = "about1";
            this.about1.Size = new System.Drawing.Size(544, 40);
            this.about1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 80);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.azCdgnDetail1);
            this.splitContainer2.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.azCdgnClassProperty1);
            this.splitContainer2.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer2.Size = new System.Drawing.Size(1184, 645);
            this.splitContainer2.SplitterDistance = 910;
            this.splitContainer2.TabIndex = 1;
            // 
            // azCdgnDetail1
            // 
            this.azCdgnDetail1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.azCdgnDetail1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.azCdgnDetail1.Location = new System.Drawing.Point(0, 0);
            this.azCdgnDetail1.Name = "azCdgnDetail1";
            this.azCdgnDetail1.Size = new System.Drawing.Size(910, 645);
            this.azCdgnDetail1.TabIndex = 0;
            // 
            // azCdgnClassProperty1
            // 
            this.azCdgnClassProperty1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.azCdgnClassProperty1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.azCdgnClassProperty1.Location = new System.Drawing.Point(0, 0);
            this.azCdgnClassProperty1.Name = "azCdgnClassProperty1";
            this.azCdgnClassProperty1.Size = new System.Drawing.Size(270, 645);
            this.azCdgnClassProperty1.TabIndex = 0;
            this.azCdgnClassProperty1.OnMasterSaveNotification += new System.Action(this.azCdgnClassProperty1_OnMasterSaveNotification);
            this.azCdgnClassProperty1.Load += new System.EventHandler(this.azCdgnClassProperty1_Load);
            // 
            // azCdgnMasterDisplay1
            // 
            this.azCdgnMasterDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.azCdgnMasterDisplay1.Dock = System.Windows.Forms.DockStyle.Top;
            this.azCdgnMasterDisplay1.Location = new System.Drawing.Point(0, 0);
            this.azCdgnMasterDisplay1.MinimumSize = new System.Drawing.Size(628, 80);
            this.azCdgnMasterDisplay1.Name = "azCdgnMasterDisplay1";
            this.azCdgnMasterDisplay1.Size = new System.Drawing.Size(1184, 80);
            this.azCdgnMasterDisplay1.TabIndex = 0;
            this.azCdgnMasterDisplay1.Atk_LookCurrentDbData += new System.EventHandler(this.azCdgnMasterDisplay1_Atk_LookCurrentDbData);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "txt";
            this.saveFileDialog1.Filter = "txt(*.txt)|";
            // 
            // AzClassDesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1478, 750);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AzClassDesign";
            this.Text = "业务设计";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AzClassDesign_FormClosed);
            this.Load += new System.EventHandler(this.AzClassDesign_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UI.AzCdgnDBSchema azCdgnDBSchema1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mspCreateCode;
        private System.Windows.Forms.ToolStripMenuItem mspBatchOperate;
        private System.Windows.Forms.ToolStripMenuItem mspMetaOperate;
        private System.Windows.Forms.ToolStripMenuItem mspOtherHandle;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private UI.AzCdgnMasterDisplay azCdgnMasterDisplay1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private UI.AzCdgnClassProperty azCdgnClassProperty1;
        private UI.AzCdgnDetail azCdgnDetail1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem02;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem0201;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem03;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem0301;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem0303;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem04;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem0401;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem0402;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem0403;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem06;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem0601;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem0602;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem0603;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem0604;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem0605;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem0606;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem0607;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem07ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem0102;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem0501;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private UI.about about1;
    }
}