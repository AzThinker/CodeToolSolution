namespace WinCodeView
{
    partial class AzBaseSetEdit
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textSaveCodeFileFloder = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button_templatefolder = new System.Windows.Forms.Button();
            this.textAzTemplateFolder = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textAzTablePrefix = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textAzDbSqlConnectionName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textAzProjectSpace = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textAzClassPrefix = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textAzProjectName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textAzToolTable = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textAzNick = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.配置操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ACM_Reset = new System.Windows.Forms.ToolStripMenuItem();
            this.ACM_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.ACM_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.about1 = new WinCodeView.UI.about();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.about1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.textSaveCodeFileFloder);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.button_templatefolder);
            this.groupBox1.Controls.Add(this.textAzTemplateFolder);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textAzTablePrefix);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textAzDbSqlConnectionName);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textAzProjectSpace);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textAzClassPrefix);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textAzProjectName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textAzToolTable);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textAzNick);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(872, 361);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(602, 202);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(31, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textSaveCodeFileFloder
            // 
            this.textSaveCodeFileFloder.Location = new System.Drawing.Point(119, 202);
            this.textSaveCodeFileFloder.Name = "textSaveCodeFileFloder";
            this.textSaveCodeFileFloder.Size = new System.Drawing.Size(481, 21);
            this.textSaveCodeFileFloder.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 205);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "代码保存位置：";
            // 
            // button_templatefolder
            // 
            this.button_templatefolder.Location = new System.Drawing.Point(796, 164);
            this.button_templatefolder.Name = "button_templatefolder";
            this.button_templatefolder.Size = new System.Drawing.Size(31, 23);
            this.button_templatefolder.TabIndex = 16;
            this.button_templatefolder.Text = "...";
            this.button_templatefolder.UseVisualStyleBackColor = true;
            this.button_templatefolder.Click += new System.EventHandler(this.button_templatefolder_Click);
            // 
            // textAzTemplateFolder
            // 
            this.textAzTemplateFolder.Location = new System.Drawing.Point(428, 164);
            this.textAzTemplateFolder.Name = "textAzTemplateFolder";
            this.textAzTemplateFolder.Size = new System.Drawing.Size(362, 21);
            this.textAzTemplateFolder.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(304, 167);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "生成代码模板名称：";
            // 
            // textAzTablePrefix
            // 
            this.textAzTablePrefix.Location = new System.Drawing.Point(428, 118);
            this.textAzTablePrefix.Name = "textAzTablePrefix";
            this.textAzTablePrefix.Size = new System.Drawing.Size(159, 21);
            this.textAzTablePrefix.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(328, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "数据表格前缀：";
            // 
            // textAzDbSqlConnectionName
            // 
            this.textAzDbSqlConnectionName.Location = new System.Drawing.Point(119, 161);
            this.textAzDbSqlConnectionName.Name = "textAzDbSqlConnectionName";
            this.textAzDbSqlConnectionName.Size = new System.Drawing.Size(159, 21);
            this.textAzDbSqlConnectionName.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "数据库连接名：";
            // 
            // textAzProjectSpace
            // 
            this.textAzProjectSpace.Location = new System.Drawing.Point(119, 116);
            this.textAzProjectSpace.Name = "textAzProjectSpace";
            this.textAzProjectSpace.Size = new System.Drawing.Size(159, 21);
            this.textAzProjectSpace.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "工程空间名：";
            // 
            // textAzClassPrefix
            // 
            this.textAzClassPrefix.Location = new System.Drawing.Point(428, 72);
            this.textAzClassPrefix.Name = "textAzClassPrefix";
            this.textAzClassPrefix.Size = new System.Drawing.Size(159, 21);
            this.textAzClassPrefix.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(364, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "类前缀：";
            // 
            // textAzProjectName
            // 
            this.textAzProjectName.Location = new System.Drawing.Point(119, 71);
            this.textAzProjectName.Name = "textAzProjectName";
            this.textAzProjectName.Size = new System.Drawing.Size(178, 21);
            this.textAzProjectName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "工程名称：";
            // 
            // textAzToolTable
            // 
            this.textAzToolTable.Location = new System.Drawing.Point(428, 26);
            this.textAzToolTable.Name = "textAzToolTable";
            this.textAzToolTable.Size = new System.Drawing.Size(132, 21);
            this.textAzToolTable.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(340, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "工具表前缀：";
            // 
            // textAzNick
            // 
            this.textAzNick.Location = new System.Drawing.Point(119, 26);
            this.textAzNick.Name = "textAzNick";
            this.textAzNick.Size = new System.Drawing.Size(159, 21);
            this.textAzNick.TabIndex = 1;
            this.textAzNick.Validating += new System.ComponentModel.CancelEventHandler(this.textAzNick_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "解决方案昵称：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Controls.Add(this.buttonReset);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 327);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(872, 59);
            this.panel1.TabIndex = 1;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(737, 19);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "关闭";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ACM_Close_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(614, 19);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ACM_Save_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(491, 19);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 0;
            this.buttonReset.Text = "重置";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.配置操作ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(872, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 配置操作ToolStripMenuItem
            // 
            this.配置操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ACM_Reset,
            this.ACM_Save,
            this.ACM_Close});
            this.配置操作ToolStripMenuItem.Name = "配置操作ToolStripMenuItem";
            this.配置操作ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.配置操作ToolStripMenuItem.Text = "配置操作";
            // 
            // ACM_Reset
            // 
            this.ACM_Reset.Name = "ACM_Reset";
            this.ACM_Reset.Size = new System.Drawing.Size(148, 22);
            this.ACM_Reset.Text = "重新导入配置";
            // 
            // ACM_Save
            // 
            this.ACM_Save.Name = "ACM_Save";
            this.ACM_Save.Size = new System.Drawing.Size(148, 22);
            this.ACM_Save.Text = "保存配置";
            this.ACM_Save.Click += new System.EventHandler(this.ACM_Save_Click);
            // 
            // ACM_Close
            // 
            this.ACM_Close.Name = "ACM_Close";
            this.ACM_Close.Size = new System.Drawing.Size(148, 22);
            this.ACM_Close.Text = "退出";
            this.ACM_Close.Click += new System.EventHandler(this.ACM_Close_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "请选择代码模板所在文件夹";
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // about1
            // 
            this.about1.Location = new System.Drawing.Point(56, 252);
            this.about1.Name = "about1";
            this.about1.Size = new System.Drawing.Size(740, 40);
            this.about1.TabIndex = 20;
            // 
            // AzBaseSetEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 386);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AzBaseSetEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "基础设置";
            this.Load += new System.EventHandler(this.AzBaseSetEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 配置操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ACM_Reset;
        private System.Windows.Forms.ToolStripMenuItem ACM_Save;
        private System.Windows.Forms.ToolStripMenuItem ACM_Close;
        private System.Windows.Forms.TextBox textAzNick;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textAzToolTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textAzProjectSpace;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textAzClassPrefix;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textAzProjectName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textAzTablePrefix;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textAzDbSqlConnectionName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textAzTemplateFolder;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button_templatefolder;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textSaveCodeFileFloder;
        private System.Windows.Forms.Label label9;
        private UI.about about1;
    }
}