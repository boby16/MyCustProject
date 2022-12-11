namespace BarCodePrintTSB
{
    partial class RPTPrdtStat
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lstDateType = new System.Windows.Forms.ComboBox();
            this.qryPrdNoB = new System.Windows.Forms.Button();
            this.qryPrdNoE = new System.Windows.Forms.Button();
            this.qrySpcNoB = new System.Windows.Forms.Button();
            this.qrySpcNoE = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPrdNoE = new System.Windows.Forms.TextBox();
            this.txtPrdNoB = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDDE = new System.Windows.Forms.DateTimePicker();
            this.txtDDB = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDepE = new System.Windows.Forms.TextBox();
            this.txtDepB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSpcNoE = new System.Windows.Forms.TextBox();
            this.txtSpcNoB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lstPrdtKnd = new System.Windows.Forms.ComboBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lstDateType);
            this.panel1.Controls.Add(this.qryPrdNoB);
            this.panel1.Controls.Add(this.qryPrdNoE);
            this.panel1.Controls.Add(this.qrySpcNoB);
            this.panel1.Controls.Add(this.qrySpcNoE);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtPrdNoE);
            this.panel1.Controls.Add(this.txtPrdNoB);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtDDE);
            this.panel1.Controls.Add(this.txtDDB);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtDepE);
            this.panel1.Controls.Add(this.txtDepB);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtSpcNoE);
            this.panel1.Controls.Add(this.txtSpcNoB);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lstPrdtKnd);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(847, 110);
            this.panel1.TabIndex = 4;
            // 
            // lstDateType
            // 
            this.lstDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstDateType.FormattingEnabled = true;
            this.lstDateType.Items.AddRange(new object[] {
            "1.打印时间",
            "2.品检时间",
            "3.送缴时间"});
            this.lstDateType.Location = new System.Drawing.Point(376, 8);
            this.lstDateType.Name = "lstDateType";
            this.lstDateType.Size = new System.Drawing.Size(90, 21);
            this.lstDateType.TabIndex = 99;
            // 
            // qryPrdNoB
            // 
            this.qryPrdNoB.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qryPrdNoB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qryPrdNoB.Location = new System.Drawing.Point(202, 34);
            this.qryPrdNoB.Name = "qryPrdNoB";
            this.qryPrdNoB.Size = new System.Drawing.Size(20, 20);
            this.qryPrdNoB.TabIndex = 98;
            this.qryPrdNoB.UseVisualStyleBackColor = true;
            this.qryPrdNoB.Click += new System.EventHandler(this.qryPrdNoB_Click);
            // 
            // qryPrdNoE
            // 
            this.qryPrdNoE.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qryPrdNoE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qryPrdNoE.Location = new System.Drawing.Point(350, 35);
            this.qryPrdNoE.Name = "qryPrdNoE";
            this.qryPrdNoE.Size = new System.Drawing.Size(20, 20);
            this.qryPrdNoE.TabIndex = 97;
            this.qryPrdNoE.UseVisualStyleBackColor = true;
            this.qryPrdNoE.Click += new System.EventHandler(this.qryPrdNoE_Click);
            // 
            // qrySpcNoB
            // 
            this.qrySpcNoB.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qrySpcNoB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qrySpcNoB.Location = new System.Drawing.Point(202, 62);
            this.qrySpcNoB.Name = "qrySpcNoB";
            this.qrySpcNoB.Size = new System.Drawing.Size(20, 20);
            this.qrySpcNoB.TabIndex = 96;
            this.qrySpcNoB.UseVisualStyleBackColor = true;
            this.qrySpcNoB.Click += new System.EventHandler(this.qrySpcNoB_Click);
            // 
            // qrySpcNoE
            // 
            this.qrySpcNoE.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qrySpcNoE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qrySpcNoE.Location = new System.Drawing.Point(350, 62);
            this.qrySpcNoE.Name = "qrySpcNoE";
            this.qrySpcNoE.Size = new System.Drawing.Size(20, 20);
            this.qrySpcNoE.TabIndex = 95;
            this.qrySpcNoE.UseVisualStyleBackColor = true;
            this.qrySpcNoE.Click += new System.EventHandler(this.qrySpcNoE_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(225, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 94;
            this.label7.Text = "--";
            // 
            // txtPrdNoE
            // 
            this.txtPrdNoE.Location = new System.Drawing.Point(245, 35);
            this.txtPrdNoE.Name = "txtPrdNoE";
            this.txtPrdNoE.Size = new System.Drawing.Size(105, 20);
            this.txtPrdNoE.TabIndex = 93;
            this.txtPrdNoE.Enter += new System.EventHandler(this.txtPrdNoE_Enter);
            // 
            // txtPrdNoB
            // 
            this.txtPrdNoB.Location = new System.Drawing.Point(97, 35);
            this.txtPrdNoB.Name = "txtPrdNoB";
            this.txtPrdNoB.Size = new System.Drawing.Size(105, 20);
            this.txtPrdNoB.TabIndex = 92;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(36, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 91;
            this.label8.Text = "起止品号";
            // 
            // txtDDE
            // 
            this.txtDDE.CustomFormat = "yyyy-MM-dd HH:mm";
            this.txtDDE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDDE.Location = new System.Drawing.Point(245, 8);
            this.txtDDE.Name = "txtDDE";
            this.txtDDE.ShowCheckBox = true;
            this.txtDDE.Size = new System.Drawing.Size(125, 20);
            this.txtDDE.TabIndex = 90;
            this.txtDDE.Enter += new System.EventHandler(this.txtDDE_Enter);
            // 
            // txtDDB
            // 
            this.txtDDB.CustomFormat = "yyyy-MM-dd HH:mm";
            this.txtDDB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDDB.Location = new System.Drawing.Point(97, 8);
            this.txtDDB.Name = "txtDDB";
            this.txtDDB.ShowCheckBox = true;
            this.txtDDB.Size = new System.Drawing.Size(125, 20);
            this.txtDDB.TabIndex = 89;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(225, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 88;
            this.label5.Text = "--";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 87;
            this.label6.Text = "起止日期";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(638, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 86;
            this.label4.Text = "--";
            // 
            // txtDepE
            // 
            this.txtDepE.Location = new System.Drawing.Point(655, 35);
            this.txtDepE.Name = "txtDepE";
            this.txtDepE.Size = new System.Drawing.Size(100, 20);
            this.txtDepE.TabIndex = 85;
            this.txtDepE.Enter += new System.EventHandler(this.txtDepE_Enter);
            // 
            // txtDepB
            // 
            this.txtDepB.Location = new System.Drawing.Point(537, 35);
            this.txtDepB.Name = "txtDepB";
            this.txtDepB.Size = new System.Drawing.Size(100, 20);
            this.txtDepB.TabIndex = 84;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(225, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 83;
            this.label3.Text = "--";
            // 
            // txtSpcNoE
            // 
            this.txtSpcNoE.Location = new System.Drawing.Point(245, 61);
            this.txtSpcNoE.Name = "txtSpcNoE";
            this.txtSpcNoE.Size = new System.Drawing.Size(105, 20);
            this.txtSpcNoE.TabIndex = 80;
            this.txtSpcNoE.Enter += new System.EventHandler(this.txtSpcNoE_Enter);
            // 
            // txtSpcNoB
            // 
            this.txtSpcNoB.Location = new System.Drawing.Point(97, 61);
            this.txtSpcNoB.Name = "txtSpcNoB";
            this.txtSpcNoB.Size = new System.Drawing.Size(105, 20);
            this.txtSpcNoB.TabIndex = 79;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(468, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 78;
            this.label2.Text = "起止机台号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 77;
            this.label1.Text = "起止异常原因";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(480, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "产品类别";
            // 
            // lstPrdtKnd
            // 
            this.lstPrdtKnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstPrdtKnd.FormattingEnabled = true;
            this.lstPrdtKnd.Items.AddRange(new object[] {
            "1.所有",
            "2.制板",
            "3.半成品",
            "4.成品"});
            this.lstPrdtKnd.Location = new System.Drawing.Point(537, 7);
            this.lstPrdtKnd.Name = "lstPrdtKnd";
            this.lstPrdtKnd.Size = new System.Drawing.Size(100, 21);
            this.lstPrdtKnd.TabIndex = 27;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(692, 73);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 15;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(588, 72);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 25);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 110);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(847, 432);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.dataGridView1.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseDoubleClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // RPTPrdtStat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 542);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "RPTPrdtStat";
            this.Text = "产品生产统计表";
            this.Load += new System.EventHandler(this.RPTBCStat_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox lstPrdtKnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSpcNoE;
        private System.Windows.Forms.TextBox txtSpcNoB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDepE;
        private System.Windows.Forms.TextBox txtDepB;
        private System.Windows.Forms.DateTimePicker txtDDE;
        private System.Windows.Forms.DateTimePicker txtDDB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPrdNoE;
        private System.Windows.Forms.TextBox txtPrdNoB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button qrySpcNoB;
        private System.Windows.Forms.Button qrySpcNoE;
        private System.Windows.Forms.Button qryPrdNoB;
        private System.Windows.Forms.Button qryPrdNoE;
        private System.Windows.Forms.ComboBox lstDateType;
    }
}