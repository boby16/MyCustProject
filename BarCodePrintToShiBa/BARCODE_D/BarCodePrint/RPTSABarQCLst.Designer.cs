namespace BarCodePrintTSB
{
    partial class RPTSABarQCLst
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.lstIsChg = new System.Windows.Forms.ComboBox();
            this.txtSysDateE = new System.Windows.Forms.DateTimePicker();
            this.txtSysDateB = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.qrySANOE = new System.Windows.Forms.Button();
            this.qrySANOB = new System.Windows.Forms.Button();
            this.qryCusNoE = new System.Windows.Forms.Button();
            this.qryCusNoB = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtDateE = new System.Windows.Forms.DateTimePicker();
            this.txtDateB = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSANOB = new System.Windows.Forms.TextBox();
            this.txtSANOE = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCusNoB = new System.Windows.Forms.TextBox();
            this.txtCusNoE = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "审核日期";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "--";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lstIsChg);
            this.panel1.Controls.Add(this.txtSysDateE);
            this.panel1.Controls.Add(this.txtSysDateB);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.qrySANOE);
            this.panel1.Controls.Add(this.qrySANOB);
            this.panel1.Controls.Add(this.qryCusNoE);
            this.panel1.Controls.Add(this.qryCusNoB);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtDateE);
            this.panel1.Controls.Add(this.txtDateB);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtSANOB);
            this.panel1.Controls.Add(this.txtSANOE);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtCusNoB);
            this.panel1.Controls.Add(this.txtCusNoE);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(731, 110);
            this.panel1.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "替换否";
            // 
            // lstIsChg
            // 
            this.lstIsChg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstIsChg.FormattingEnabled = true;
            this.lstIsChg.Items.AddRange(new object[] {
            "1.所有",
            "2.未替换",
            "3.已替换"});
            this.lstIsChg.Location = new System.Drawing.Point(71, 66);
            this.lstIsChg.Name = "lstIsChg";
            this.lstIsChg.Size = new System.Drawing.Size(130, 21);
            this.lstIsChg.TabIndex = 31;
            // 
            // txtSysDateE
            // 
            this.txtSysDateE.CustomFormat = "yyyy-MM-dd";
            this.txtSysDateE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtSysDateE.Location = new System.Drawing.Point(219, 39);
            this.txtSysDateE.Name = "txtSysDateE";
            this.txtSysDateE.Size = new System.Drawing.Size(130, 20);
            this.txtSysDateE.TabIndex = 30;
            // 
            // txtSysDateB
            // 
            this.txtSysDateB.CustomFormat = "yyyy-MM-dd";
            this.txtSysDateB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtSysDateB.Location = new System.Drawing.Point(71, 40);
            this.txtSysDateB.Name = "txtSysDateB";
            this.txtSysDateB.Size = new System.Drawing.Size(130, 20);
            this.txtSysDateB.TabIndex = 29;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "制单日期";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(202, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "--";
            // 
            // qrySANOE
            // 
            this.qrySANOE.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qrySANOE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qrySANOE.Location = new System.Drawing.Point(680, 15);
            this.qrySANOE.Name = "qrySANOE";
            this.qrySANOE.Size = new System.Drawing.Size(20, 20);
            this.qrySANOE.TabIndex = 26;
            this.qrySANOE.UseVisualStyleBackColor = true;
            this.qrySANOE.Click += new System.EventHandler(this.qrySANOE_Click);
            // 
            // qrySANOB
            // 
            this.qrySANOB.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qrySANOB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qrySANOB.Location = new System.Drawing.Point(530, 15);
            this.qrySANOB.Name = "qrySANOB";
            this.qrySANOB.Size = new System.Drawing.Size(20, 20);
            this.qrySANOB.TabIndex = 25;
            this.qrySANOB.UseVisualStyleBackColor = true;
            this.qrySANOB.Click += new System.EventHandler(this.qrySANOB_Click);
            // 
            // qryCusNoE
            // 
            this.qryCusNoE.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qryCusNoE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qryCusNoE.Location = new System.Drawing.Point(680, 41);
            this.qryCusNoE.Name = "qryCusNoE";
            this.qryCusNoE.Size = new System.Drawing.Size(20, 20);
            this.qryCusNoE.TabIndex = 24;
            this.qryCusNoE.UseVisualStyleBackColor = true;
            this.qryCusNoE.Click += new System.EventHandler(this.qryCusNoE_Click);
            // 
            // qryCusNoB
            // 
            this.qryCusNoB.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qryCusNoB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qryCusNoB.Location = new System.Drawing.Point(530, 41);
            this.qryCusNoB.Name = "qryCusNoB";
            this.qryCusNoB.Size = new System.Drawing.Size(20, 20);
            this.qryCusNoB.TabIndex = 23;
            this.qryCusNoB.UseVisualStyleBackColor = true;
            this.qryCusNoB.Click += new System.EventHandler(this.qryCusNoB_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(618, 75);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 15;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(514, 74);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 25);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtDateE
            // 
            this.txtDateE.CustomFormat = "yyyy-MM-dd HH:mm";
            this.txtDateE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDateE.Location = new System.Drawing.Point(219, 13);
            this.txtDateE.Name = "txtDateE";
            this.txtDateE.ShowCheckBox = true;
            this.txtDateE.Size = new System.Drawing.Size(130, 20);
            this.txtDateE.TabIndex = 13;
            // 
            // txtDateB
            // 
            this.txtDateB.CustomFormat = "yyyy-MM-dd HH:mm";
            this.txtDateB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDateB.Location = new System.Drawing.Point(71, 14);
            this.txtDateB.Name = "txtDateB";
            this.txtDateB.ShowCheckBox = true;
            this.txtDateB.Size = new System.Drawing.Size(130, 20);
            this.txtDateB.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(371, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "销货单号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(557, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "--";
            // 
            // txtSANOB
            // 
            this.txtSANOB.Location = new System.Drawing.Point(430, 15);
            this.txtSANOB.Name = "txtSANOB";
            this.txtSANOB.Size = new System.Drawing.Size(100, 20);
            this.txtSANOB.TabIndex = 9;
            // 
            // txtSANOE
            // 
            this.txtSANOE.Location = new System.Drawing.Point(580, 15);
            this.txtSANOE.Name = "txtSANOE";
            this.txtSANOE.Size = new System.Drawing.Size(100, 20);
            this.txtSANOE.TabIndex = 10;
            this.txtSANOE.Enter += new System.EventHandler(this.txtSANOE_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(371, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "客户代号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(557, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "--";
            // 
            // txtCusNoB
            // 
            this.txtCusNoB.Location = new System.Drawing.Point(430, 41);
            this.txtCusNoB.Name = "txtCusNoB";
            this.txtCusNoB.Size = new System.Drawing.Size(100, 20);
            this.txtCusNoB.TabIndex = 5;
            // 
            // txtCusNoE
            // 
            this.txtCusNoE.Location = new System.Drawing.Point(580, 41);
            this.txtCusNoE.Name = "txtCusNoE";
            this.txtCusNoE.Size = new System.Drawing.Size(100, 20);
            this.txtCusNoE.TabIndex = 6;
            this.txtCusNoE.Enter += new System.EventHandler(this.txtCusNoE_Enter);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 110);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(731, 432);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.dataGridView1.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseDoubleClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // RPTSABarQCLst
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 542);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "RPTSABarQCLst";
            this.Text = "出货检验统计表";
            this.Load += new System.EventHandler(this.RPTSABarQCLst_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSANOB;
        private System.Windows.Forms.TextBox txtSANOE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCusNoB;
        private System.Windows.Forms.TextBox txtCusNoE;
        private System.Windows.Forms.DateTimePicker txtDateB;
        private System.Windows.Forms.DateTimePicker txtDateE;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button qryCusNoE;
        private System.Windows.Forms.Button qryCusNoB;
        private System.Windows.Forms.Button qrySANOE;
        private System.Windows.Forms.Button qrySANOB;
        private System.Windows.Forms.DateTimePicker txtSysDateE;
        private System.Windows.Forms.DateTimePicker txtSysDateB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox lstIsChg;
    }
}