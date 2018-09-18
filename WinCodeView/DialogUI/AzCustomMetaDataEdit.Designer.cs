namespace WinCodeView
{
    partial class AzCustomMetaDataEdit
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
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textRemark = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textSchemaFrom = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btMetaSource = new System.Windows.Forms.Button();
            this.textTableName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rdbMetaChild = new System.Windows.Forms.RadioButton();
            this.rdbMetaClass = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.textClassName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textClassDisPlay = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textObjModeName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btSave = new System.Windows.Forms.Button();
            this.btCancle = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.azMetaTableEntityBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.azMetaTableEntityBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.textRemark);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textSchemaFrom);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btMetaSource);
            this.panel1.Controls.Add(this.textTableName);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.rdbMetaChild);
            this.panel1.Controls.Add(this.rdbMetaClass);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textClassName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textClassDisPlay);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textObjModeName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(654, 449);
            this.panel1.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Crimson;
            this.label9.Location = new System.Drawing.Point(306, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 23;
            this.label9.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Crimson;
            this.label8.Location = new System.Drawing.Point(290, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 22;
            this.label8.Text = "*";
            // 
            // textRemark
            // 
            this.textRemark.Location = new System.Drawing.Point(118, 282);
            this.textRemark.Multiline = true;
            this.textRemark.Name = "textRemark";
            this.textRemark.Size = new System.Drawing.Size(474, 110);
            this.textRemark.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(72, 285);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 20;
            this.label7.Text = "说明：";
            // 
            // textSchemaFrom
            // 
            this.textSchemaFrom.Location = new System.Drawing.Point(120, 237);
            this.textSchemaFrom.Name = "textSchemaFrom";
            this.textSchemaFrom.ReadOnly = true;
            this.textSchemaFrom.Size = new System.Drawing.Size(270, 21);
            this.textSchemaFrom.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 242);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "元数据来源：";
            // 
            // btMetaSource
            // 
            this.btMetaSource.Location = new System.Drawing.Point(396, 236);
            this.btMetaSource.Name = "btMetaSource";
            this.btMetaSource.Size = new System.Drawing.Size(34, 23);
            this.btMetaSource.TabIndex = 7;
            this.btMetaSource.Text = "...";
            this.btMetaSource.UseVisualStyleBackColor = true;
            this.btMetaSource.Click += new System.EventHandler(this.btMetaSource_Click);
            // 
            // textTableName
            // 
            this.textTableName.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.textTableName.Location = new System.Drawing.Point(120, 193);
            this.textTableName.Name = "textTableName";
            this.textTableName.ReadOnly = true;
            this.textTableName.Size = new System.Drawing.Size(312, 21);
            this.textTableName.TabIndex = 5;
            this.textTableName.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "持续化名称：";
            // 
            // rdbMetaChild
            // 
            this.rdbMetaChild.AutoSize = true;
            this.rdbMetaChild.Location = new System.Drawing.Point(193, 154);
            this.rdbMetaChild.Name = "rdbMetaChild";
            this.rdbMetaChild.Size = new System.Drawing.Size(47, 16);
            this.rdbMetaChild.TabIndex = 4;
            this.rdbMetaChild.Text = "子类";
            this.rdbMetaChild.UseVisualStyleBackColor = true;
            // 
            // rdbMetaClass
            // 
            this.rdbMetaClass.AutoSize = true;
            this.rdbMetaClass.Checked = true;
            this.rdbMetaClass.Location = new System.Drawing.Point(120, 154);
            this.rdbMetaClass.Name = "rdbMetaClass";
            this.rdbMetaClass.Size = new System.Drawing.Size(47, 16);
            this.rdbMetaClass.TabIndex = 3;
            this.rdbMetaClass.TabStop = true;
            this.rdbMetaClass.Text = "元类";
            this.rdbMetaClass.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "业务类类型：";
            // 
            // textClassName
            // 
            this.textClassName.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.textClassName.Location = new System.Drawing.Point(120, 110);
            this.textClassName.Name = "textClassName";
            this.textClassName.ReadOnly = true;
            this.textClassName.Size = new System.Drawing.Size(242, 21);
            this.textClassName.TabIndex = 2;
            this.textClassName.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "类名称：";
            // 
            // textClassDisPlay
            // 
            this.textClassDisPlay.Location = new System.Drawing.Point(120, 66);
            this.textClassDisPlay.Name = "textClassDisPlay";
            this.textClassDisPlay.Size = new System.Drawing.Size(178, 21);
            this.textClassDisPlay.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "显示名称：";
            // 
            // textObjModeName
            // 
            this.errorProvider1.SetIconPadding(this.textObjModeName, 5);
            this.textObjModeName.Location = new System.Drawing.Point(120, 22);
            this.textObjModeName.Name = "textObjModeName";
            this.textObjModeName.Size = new System.Drawing.Size(159, 21);
            this.textObjModeName.TabIndex = 0;
            this.toolTip1.SetToolTip(this.textObjModeName, "不带前缀的表名");
            this.textObjModeName.TextChanged += new System.EventHandler(this.textModelName_TextChanged);
            this.textObjModeName.Validating += new System.ComponentModel.CancelEventHandler(this.textModelName_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "对象模型名称：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 405);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(654, 44);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btSave);
            this.panel3.Controls.Add(this.btCancle);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(438, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(216, 44);
            this.panel3.TabIndex = 2;
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(6, 12);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 0;
            this.btSave.Text = "保存";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btCancle
            // 
            this.btCancle.Location = new System.Drawing.Point(128, 12);
            this.btCancle.Name = "btCancle";
            this.btCancle.Size = new System.Drawing.Size(75, 23);
            this.btCancle.TabIndex = 1;
            this.btCancle.Text = "取消";
            this.btCancle.UseVisualStyleBackColor = true;
            this.btCancle.Click += new System.EventHandler(this.btCancle_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // azMetaTableEntityBindingSource
            // 
            this.azMetaTableEntityBindingSource.DataSource = typeof(MetaWorkLib.Domain.AzMetaTableEntity);
            // 
            // AzCustomMetaDataEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 449);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            //this.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.azMetaTableEntityBindingSource, "ClassNamed", true));
            this.Name = "AzCustomMetaDataEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "类名编辑（元数据）";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.azMetaTableEntityBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btCancle;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textClassName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textClassDisPlay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textObjModeName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textTableName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rdbMetaChild;
        private System.Windows.Forms.RadioButton rdbMetaClass;
        private System.Windows.Forms.TextBox textRemark;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textSchemaFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btMetaSource;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.BindingSource azMetaTableEntityBindingSource;
    }
}