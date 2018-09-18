namespace WinCodeView
{
    partial class AzDataBSetEdit
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
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.数据库操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ACM_DBCreateOp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ACM_DBClearData = new System.Windows.Forms.ToolStripMenuItem();
            this.ACM_DBIniData = new System.Windows.Forms.ToolStripMenuItem();
            this.ACM_Exist = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(20, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 27);
            this.button1.TabIndex = 0;
            this.button1.Text = "数据库";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtConnectionString);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(29, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(718, 210);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(50, 71);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(649, 118);
            this.txtConnectionString.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Location = new System.Drawing.Point(605, 246);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.数据库操作ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(781, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 数据库操作ToolStripMenuItem
            // 
            this.数据库操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ACM_DBCreateOp,
            this.toolStripMenuItem1,
            this.ACM_DBClearData,
            this.ACM_DBIniData,
            this.ACM_Exist});
            this.数据库操作ToolStripMenuItem.Name = "数据库操作ToolStripMenuItem";
            this.数据库操作ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.数据库操作ToolStripMenuItem.Text = "数据库操作";
            // 
            // ACM_DBCreateOp
            // 
            this.ACM_DBCreateOp.Name = "ACM_DBCreateOp";
            this.ACM_DBCreateOp.Size = new System.Drawing.Size(180, 22);
            this.ACM_DBCreateOp.Text = "初始工具用数据";
            this.ACM_DBCreateOp.Click += new System.EventHandler(this.ACM_DBCreateOp_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // ACM_DBClearData
            // 
            this.ACM_DBClearData.Name = "ACM_DBClearData";
            this.ACM_DBClearData.Size = new System.Drawing.Size(180, 22);
            this.ACM_DBClearData.Text = "清除已生成元数据";
            this.ACM_DBClearData.Click += new System.EventHandler(this.ACM_DBClearData_Click);
            // 
            // ACM_DBIniData
            // 
            this.ACM_DBIniData.Name = "ACM_DBIniData";
            this.ACM_DBIniData.Size = new System.Drawing.Size(180, 22);
            this.ACM_DBIniData.Text = "初始元数据值";
            this.ACM_DBIniData.Click += new System.EventHandler(this.ACM_DBIniData_Click);
            // 
            // ACM_Exist
            // 
            this.ACM_Exist.Name = "ACM_Exist";
            this.ACM_Exist.Size = new System.Drawing.Size(180, 22);
            this.ACM_Exist.Text = "退出";
            this.ACM_Exist.Click += new System.EventHandler(this.ACM_Exist_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(56, 246);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "初始工具用数据";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.ACM_DBCreateOp_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(239, 246);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(105, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "清除元数据";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.ACM_DBClearData_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(422, 246);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(105, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "初始元数据值";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.ACM_DBIniData_Click);
            // 
            // AzDataBSetEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 286);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AzDataBSetEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据库配置";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AzDataBSetEdit_Paint);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 数据库操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ACM_DBCreateOp;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ACM_DBClearData;
        private System.Windows.Forms.ToolStripMenuItem ACM_DBIniData;
        private System.Windows.Forms.ToolStripMenuItem ACM_Exist;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}