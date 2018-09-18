namespace WinCodeView.UI
{
    partial class AzCdgnMasterDisplay
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
            this.label1 = new System.Windows.Forms.Label();
            this.labCurrentClass = new System.Windows.Forms.Label();
            this.labCurrentSchemaName = new System.Windows.Forms.Label();
            this.labDisplayName = new System.Windows.Forms.Label();
            this.chkMasterInit = new System.Windows.Forms.CheckBox();
            this.chkCreatFiles = new System.Windows.Forms.CheckBox();
            this.btnLookDBdata = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前选择：";
            // 
            // labCurrentClass
            // 
            this.labCurrentClass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labCurrentClass.Location = new System.Drawing.Point(69, 8);
            this.labCurrentClass.Name = "labCurrentClass";
            this.labCurrentClass.Size = new System.Drawing.Size(204, 23);
            this.labCurrentClass.TabIndex = 1;
            this.labCurrentClass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labCurrentSchemaName
            // 
            this.labCurrentSchemaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labCurrentSchemaName.Location = new System.Drawing.Point(275, 8);
            this.labCurrentSchemaName.Name = "labCurrentSchemaName";
            this.labCurrentSchemaName.Size = new System.Drawing.Size(202, 23);
            this.labCurrentSchemaName.TabIndex = 2;
            this.labCurrentSchemaName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labDisplayName
            // 
            this.labDisplayName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labDisplayName.Location = new System.Drawing.Point(479, 8);
            this.labDisplayName.Name = "labDisplayName";
            this.labDisplayName.Size = new System.Drawing.Size(140, 23);
            this.labDisplayName.TabIndex = 3;
            this.labDisplayName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkMasterInit
            // 
            this.chkMasterInit.AutoSize = true;
            this.chkMasterInit.Enabled = false;
            this.chkMasterInit.Location = new System.Drawing.Point(10, 36);
            this.chkMasterInit.Name = "chkMasterInit";
            this.chkMasterInit.Size = new System.Drawing.Size(15, 14);
            this.chkMasterInit.TabIndex = 4;
            this.chkMasterInit.UseVisualStyleBackColor = true;
            // 
            // chkCreatFiles
            // 
            this.chkCreatFiles.AutoSize = true;
            this.chkCreatFiles.Location = new System.Drawing.Point(10, 58);
            this.chkCreatFiles.Name = "chkCreatFiles";
            this.chkCreatFiles.Size = new System.Drawing.Size(132, 16);
            this.chkCreatFiles.TabIndex = 5;
            this.chkCreatFiles.Text = "生成时同时生成文件";
            this.chkCreatFiles.UseVisualStyleBackColor = true;
            // 
            // btnLookDBdata
            // 
            this.btnLookDBdata.AutoSize = true;
            this.btnLookDBdata.Enabled = false;
            this.btnLookDBdata.Location = new System.Drawing.Point(576, 44);
            this.btnLookDBdata.Name = "btnLookDBdata";
            this.btnLookDBdata.Size = new System.Drawing.Size(99, 29);
            this.btnLookDBdata.TabIndex = 6;
            this.btnLookDBdata.Text = "数据库数据查询";
            this.btnLookDBdata.UseVisualStyleBackColor = true;
            this.btnLookDBdata.Click += new System.EventHandler(this.btnLookDBdata_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "主信息未初始化";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(146, 54);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(360, 21);
            this.textBox1.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(506, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AzCdgnMasterDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLookDBdata);
            this.Controls.Add(this.chkCreatFiles);
            this.Controls.Add(this.chkMasterInit);
            this.Controls.Add(this.labDisplayName);
            this.Controls.Add(this.labCurrentSchemaName);
            this.Controls.Add(this.labCurrentClass);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(628, 80);
            this.Name = "AzCdgnMasterDisplay";
            this.Size = new System.Drawing.Size(711, 78);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labCurrentClass;
        private System.Windows.Forms.Label labCurrentSchemaName;
        private System.Windows.Forms.Label labDisplayName;
        private System.Windows.Forms.CheckBox chkMasterInit;
        private System.Windows.Forms.CheckBox chkCreatFiles;
        private System.Windows.Forms.Button btnLookDBdata;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}
