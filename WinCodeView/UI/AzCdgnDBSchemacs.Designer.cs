namespace WinCodeView.UI
{
    partial class AzCdgnDBSchema
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AzCdgnDBSchema));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tspCurrentCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.tspBatchCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.CMMetaDataExportXLS = new System.Windows.Forms.ToolStripMenuItem();
            this.aCMMetaDataExportAllXLSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.CMReCreateDbData = new System.Windows.Forms.ToolStripMenuItem();
            this.CMUpdateDisplayName = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aCMClearClassInfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CMClearAllClassInf = new System.Windows.Forms.ToolStripMenuItem();
            this.tspInitAllDBtoCodeType = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.aCMRefToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClearAllSelect = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnImportSelect = new System.Windows.Forms.Button();
            this.btnSaveSelect = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labAppNameDisplay = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "tick.png");
            this.imageList1.Images.SetKeyName(1, "tables.png");
            this.imageList1.Images.SetKeyName(2, "tables-stacks.png");
            this.imageList1.Images.SetKeyName(3, "table-select.png");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 663);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.ImageIndex = 1;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(3, 47);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(314, 538);
            this.treeView1.TabIndex = 5;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspCurrentCreate,
            this.tspBatchCreate,
            this.CMMetaDataExportXLS,
            this.aCMMetaDataExportAllXLSToolStripMenuItem,
            this.toolStripMenuItem1,
            this.CMReCreateDbData,
            this.CMUpdateDisplayName,
            this.toolStripSeparator1,
            this.aCMClearClassInfToolStripMenuItem,
            this.CMClearAllClassInf,
            this.tspInitAllDBtoCodeType,
            this.toolStripMenuItem2,
            this.aCMRefToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(221, 242);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // tspCurrentCreate
            // 
            this.tspCurrentCreate.Name = "tspCurrentCreate";
            this.tspCurrentCreate.Size = new System.Drawing.Size(220, 22);
            this.tspCurrentCreate.Text = "生成当前项代码";
            this.tspCurrentCreate.Click += new System.EventHandler(this.tspCurrentCreate_Click);
            // 
            // tspBatchCreate
            // 
            this.tspBatchCreate.Name = "tspBatchCreate";
            this.tspBatchCreate.Size = new System.Drawing.Size(220, 22);
            this.tspBatchCreate.Text = "生成所有已选项代码";
            this.tspBatchCreate.Click += new System.EventHandler(this.tspBatchCreate_Click);
            // 
            // CMMetaDataExportXLS
            // 
            this.CMMetaDataExportXLS.Name = "CMMetaDataExportXLS";
            this.CMMetaDataExportXLS.Size = new System.Drawing.Size(220, 22);
            this.CMMetaDataExportXLS.Text = "元数据导出";
            this.CMMetaDataExportXLS.Click += new System.EventHandler(this.aCMMetaDataExportXLSToolStripMenuItem_Click);
            // 
            // aCMMetaDataExportAllXLSToolStripMenuItem
            // 
            this.aCMMetaDataExportAllXLSToolStripMenuItem.Name = "aCMMetaDataExportAllXLSToolStripMenuItem";
            this.aCMMetaDataExportAllXLSToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.aCMMetaDataExportAllXLSToolStripMenuItem.Text = "导出所有选择项的元数据";
            this.aCMMetaDataExportAllXLSToolStripMenuItem.Click += new System.EventHandler(this.aCMMetaDataExportAllXLSToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(217, 6);
            // 
            // CMReCreateDbData
            // 
            this.CMReCreateDbData.Name = "CMReCreateDbData";
            this.CMReCreateDbData.Size = new System.Drawing.Size(220, 22);
            this.CMReCreateDbData.Text = "重新生成当前表数据";
            this.CMReCreateDbData.Click += new System.EventHandler(this.aCMReCreateDbDataToolStripMenuItem_Click);
            // 
            // CMUpdateDisplayName
            // 
            this.CMUpdateDisplayName.Name = "CMUpdateDisplayName";
            this.CMUpdateDisplayName.Size = new System.Drawing.Size(220, 22);
            this.CMUpdateDisplayName.Text = "更新显示名";
            this.CMUpdateDisplayName.Click += new System.EventHandler(this.aCMUpdateDisplayNameToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(217, 6);
            // 
            // aCMClearClassInfToolStripMenuItem
            // 
            this.aCMClearClassInfToolStripMenuItem.Name = "aCMClearClassInfToolStripMenuItem";
            this.aCMClearClassInfToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.aCMClearClassInfToolStripMenuItem.Text = "初始当前类信息";
            this.aCMClearClassInfToolStripMenuItem.Click += new System.EventHandler(this.aCMClearClassInfToolStripMenuItem_Click);
            // 
            // CMClearAllClassInf
            // 
            this.CMClearAllClassInf.Name = "CMClearAllClassInf";
            this.CMClearAllClassInf.Size = new System.Drawing.Size(220, 22);
            this.CMClearAllClassInf.Text = "初始所有类信息";
            this.CMClearAllClassInf.Click += new System.EventHandler(this.CMClearAllClassInf_Click);
            // 
            // tspInitAllDBtoCodeType
            // 
            this.tspInitAllDBtoCodeType.Name = "tspInitAllDBtoCodeType";
            this.tspInitAllDBtoCodeType.Size = new System.Drawing.Size(220, 22);
            this.tspInitAllDBtoCodeType.Text = "初始所有字段代码数据类型";
            this.tspInitAllDBtoCodeType.Click += new System.EventHandler(this.tspInitAllDBtoCodeType_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(217, 6);
            // 
            // aCMRefToolStripMenuItem
            // 
            this.aCMRefToolStripMenuItem.Name = "aCMRefToolStripMenuItem";
            this.aCMRefToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.aCMRefToolStripMenuItem.Text = "刷新";
            this.aCMRefToolStripMenuItem.Click += new System.EventHandler(this.aCMRefToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClearAllSelect);
            this.panel1.Controls.Add(this.btnSelectAll);
            this.panel1.Controls.Add(this.btnImportSelect);
            this.panel1.Controls.Add(this.btnSaveSelect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 585);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(314, 75);
            this.panel1.TabIndex = 4;
            // 
            // btnClearAllSelect
            // 
            this.btnClearAllSelect.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClearAllSelect.Location = new System.Drawing.Point(164, 44);
            this.btnClearAllSelect.Name = "btnClearAllSelect";
            this.btnClearAllSelect.Size = new System.Drawing.Size(75, 23);
            this.btnClearAllSelect.TabIndex = 3;
            this.btnClearAllSelect.Text = "清除所选";
            this.btnClearAllSelect.UseVisualStyleBackColor = true;
            this.btnClearAllSelect.Click += new System.EventHandler(this.btnClearAllSelect_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelectAll.Location = new System.Drawing.Point(28, 44);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 2;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnImportSelect
            // 
            this.btnImportSelect.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnImportSelect.Location = new System.Drawing.Point(164, 10);
            this.btnImportSelect.Name = "btnImportSelect";
            this.btnImportSelect.Size = new System.Drawing.Size(75, 23);
            this.btnImportSelect.TabIndex = 1;
            this.btnImportSelect.Text = "导入所选";
            this.btnImportSelect.UseVisualStyleBackColor = true;
            this.btnImportSelect.Click += new System.EventHandler(this.btnImportSelect_Click);
            // 
            // btnSaveSelect
            // 
            this.btnSaveSelect.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveSelect.Location = new System.Drawing.Point(28, 10);
            this.btnSaveSelect.Name = "btnSaveSelect";
            this.btnSaveSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSaveSelect.TabIndex = 0;
            this.btnSaveSelect.Text = "保存所选";
            this.btnSaveSelect.UseVisualStyleBackColor = true;
            this.btnSaveSelect.Click += new System.EventHandler(this.BtnSaveSelect_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labAppNameDisplay);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(314, 42);
            this.panel2.TabIndex = 3;
            // 
            // labAppNameDisplay
            // 
            this.labAppNameDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labAppNameDisplay.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labAppNameDisplay.Location = new System.Drawing.Point(0, 0);
            this.labAppNameDisplay.Name = "labAppNameDisplay";
            this.labAppNameDisplay.Size = new System.Drawing.Size(314, 42);
            this.labAppNameDisplay.TabIndex = 0;
            this.labAppNameDisplay.Text = "未指定工程名";
            this.labAppNameDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "txt";
            this.saveFileDialog1.Filter = "txt(*.txt)|";
            // 
            // AzCdgnDBSchema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.groupBox1);
            this.Name = "AzCdgnDBSchema";
            this.Size = new System.Drawing.Size(320, 663);
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClearAllSelect;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnImportSelect;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labAppNameDisplay;
        public System.Windows.Forms.Button btnSaveSelect;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CMMetaDataExportXLS;
        private System.Windows.Forms.ToolStripMenuItem aCMMetaDataExportAllXLSToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem CMReCreateDbData;
        private System.Windows.Forms.ToolStripMenuItem CMUpdateDisplayName;
        private System.Windows.Forms.ToolStripMenuItem aCMClearClassInfToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem aCMRefToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem CMClearAllClassInf;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tspInitAllDBtoCodeType;
        private System.Windows.Forms.ToolStripMenuItem tspCurrentCreate;
        private System.Windows.Forms.ToolStripMenuItem tspBatchCreate;
    }
}
