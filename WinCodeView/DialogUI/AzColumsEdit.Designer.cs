namespace WinCodeView
{
    partial class AzColumsEdit
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSaveExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ckBoxIsNullable = new System.Windows.Forms.CheckBox();
            this.ckBoxIsKeyField = new System.Windows.Forms.CheckBox();
            this.ckBoxIsIdentity = new System.Windows.Forms.CheckBox();
            this.cboBoxFldType = new System.Windows.Forms.ComboBox();
            this.cboBoxFldCodeType = new System.Windows.Forms.ComboBox();
            this.textFldLen = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textFldDisplay = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textFldName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 247);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(590, 75);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnSaveExit);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(348, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(242, 75);
            this.panel2.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(124, 38);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(21, 38);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "撤消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSaveExit
            // 
            this.btnSaveExit.Location = new System.Drawing.Point(124, 8);
            this.btnSaveExit.Name = "btnSaveExit";
            this.btnSaveExit.Size = new System.Drawing.Size(75, 23);
            this.btnSaveExit.TabIndex = 2;
            this.btnSaveExit.Text = "保存退出";
            this.btnSaveExit.UseVisualStyleBackColor = true;
            this.btnSaveExit.Click += new System.EventHandler(this.btnSaveExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(21, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.ckBoxIsNullable);
            this.panel3.Controls.Add(this.ckBoxIsKeyField);
            this.panel3.Controls.Add(this.ckBoxIsIdentity);
            this.panel3.Controls.Add(this.cboBoxFldType);
            this.panel3.Controls.Add(this.cboBoxFldCodeType);
            this.panel3.Controls.Add(this.textFldLen);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.textFldDisplay);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.textFldName);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(590, 247);
            this.panel3.TabIndex = 1;
            // 
            // ckBoxIsNullable
            // 
            this.ckBoxIsNullable.AutoSize = true;
            this.ckBoxIsNullable.Location = new System.Drawing.Point(336, 106);
            this.ckBoxIsNullable.Name = "ckBoxIsNullable";
            this.ckBoxIsNullable.Size = new System.Drawing.Size(72, 16);
            this.ckBoxIsNullable.TabIndex = 36;
            this.ckBoxIsNullable.Text = "可空字段";
            this.ckBoxIsNullable.UseVisualStyleBackColor = true;
            // 
            // ckBoxIsKeyField
            // 
            this.ckBoxIsKeyField.AutoSize = true;
            this.ckBoxIsKeyField.Location = new System.Drawing.Point(336, 63);
            this.ckBoxIsKeyField.Name = "ckBoxIsKeyField";
            this.ckBoxIsKeyField.Size = new System.Drawing.Size(72, 16);
            this.ckBoxIsKeyField.TabIndex = 35;
            this.ckBoxIsKeyField.Text = "关键字段";
            this.ckBoxIsKeyField.UseVisualStyleBackColor = true;
            // 
            // ckBoxIsIdentity
            // 
            this.ckBoxIsIdentity.AutoSize = true;
            this.ckBoxIsIdentity.Location = new System.Drawing.Point(336, 20);
            this.ckBoxIsIdentity.Name = "ckBoxIsIdentity";
            this.ckBoxIsIdentity.Size = new System.Drawing.Size(72, 16);
            this.ckBoxIsIdentity.TabIndex = 34;
            this.ckBoxIsIdentity.Text = "自增字段";
            this.ckBoxIsIdentity.UseVisualStyleBackColor = true;
            // 
            // cboBoxFldType
            // 
            this.cboBoxFldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBoxFldType.FormattingEnabled = true;
            this.cboBoxFldType.Location = new System.Drawing.Point(94, 105);
            this.cboBoxFldType.Name = "cboBoxFldType";
            this.cboBoxFldType.Size = new System.Drawing.Size(156, 20);
            this.cboBoxFldType.TabIndex = 33;
            // 
            // cboBoxFldCodeType
            // 
            this.cboBoxFldCodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBoxFldCodeType.FormattingEnabled = true;
            this.cboBoxFldCodeType.Location = new System.Drawing.Point(94, 63);
            this.cboBoxFldCodeType.Name = "cboBoxFldCodeType";
            this.cboBoxFldCodeType.Size = new System.Drawing.Size(132, 20);
            this.cboBoxFldCodeType.TabIndex = 32;
            // 
            // textFldLen
            // 
            this.textFldLen.Location = new System.Drawing.Point(94, 190);
            this.textFldLen.Name = "textFldLen";
            this.textFldLen.Size = new System.Drawing.Size(159, 21);
            this.textFldLen.TabIndex = 30;
            this.textFldLen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textfldlen_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 31;
            this.label5.Text = "属性长度：";
            // 
            // textFldDisplay
            // 
            this.textFldDisplay.Location = new System.Drawing.Point(94, 147);
            this.textFldDisplay.Name = "textFldDisplay";
            this.textFldDisplay.Size = new System.Drawing.Size(206, 21);
            this.textFldDisplay.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 29;
            this.label4.Text = "显示名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 27;
            this.label3.Text = "DB类型：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "数据类型：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Crimson;
            this.label8.Location = new System.Drawing.Point(264, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 25;
            this.label8.Text = "*";
            // 
            // textFldName
            // 
            this.textFldName.Location = new System.Drawing.Point(94, 20);
            this.textFldName.Name = "textFldName";
            this.textFldName.Size = new System.Drawing.Size(159, 21);
            this.textFldName.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 24;
            this.label1.Text = "属性名称：";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(MetaWorkLib.Domain.AzMetaCustomCloumEntity);
            this.bindingSource1.BindingComplete += new System.Windows.Forms.BindingCompleteEventHandler(this.bindingSource1_BindingComplete);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(342, 150);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 86);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段来源类型";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(24, 22);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(59, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Tag = "1";
            this.radioButton1.Text = "数据库";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(24, 52);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Tag = "2";
            this.radioButton2.Text = "扩展";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // AzColumsEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 322);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "AzColumsEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "元数据字段";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textFldName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckBoxIsNullable;
        private System.Windows.Forms.CheckBox ckBoxIsKeyField;
        private System.Windows.Forms.CheckBox ckBoxIsIdentity;
        private System.Windows.Forms.ComboBox cboBoxFldType;
        private System.Windows.Forms.ComboBox cboBoxFldCodeType;
        private System.Windows.Forms.TextBox textFldLen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textFldDisplay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSaveExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}