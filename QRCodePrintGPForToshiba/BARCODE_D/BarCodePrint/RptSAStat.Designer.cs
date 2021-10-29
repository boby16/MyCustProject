namespace BarCodePrintTSB
{
    partial class RptSAStat
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
            this.qryCusNoE = new System.Windows.Forms.Button();
            this.qryCusNoB = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.txtSaDDE = new System.Windows.Forms.DateTimePicker();
            this.txtSaDDB = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCusNoE = new System.Windows.Forms.TextBox();
            this.txtCusNoB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtTotalAmt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTotalSb = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtToTalSa = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotalSASum = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTotalAmtn = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.qryCusNoE);
            this.panel1.Controls.Add(this.qryCusNoB);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.txtSaDDE);
            this.panel1.Controls.Add(this.txtSaDDB);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtCusNoE);
            this.panel1.Controls.Add(this.txtCusNoB);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(732, 102);
            this.panel1.TabIndex = 0;
            // 
            // qryCusNoE
            // 
            this.qryCusNoE.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qryCusNoE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qryCusNoE.Location = new System.Drawing.Point(616, 17);
            this.qryCusNoE.Name = "qryCusNoE";
            this.qryCusNoE.Size = new System.Drawing.Size(20, 20);
            this.qryCusNoE.TabIndex = 26;
            this.qryCusNoE.UseVisualStyleBackColor = true;
            this.qryCusNoE.Click += new System.EventHandler(this.qryCusNoE_Click);
            // 
            // qryCusNoB
            // 
            this.qryCusNoB.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qryCusNoB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qryCusNoB.Location = new System.Drawing.Point(479, 17);
            this.qryCusNoB.Name = "qryCusNoB";
            this.qryCusNoB.Size = new System.Drawing.Size(20, 20);
            this.qryCusNoB.TabIndex = 25;
            this.qryCusNoB.UseVisualStyleBackColor = true;
            this.qryCusNoB.Click += new System.EventHandler(this.qryCusNoB_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(602, 59);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 11;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtSaDDE
            // 
            this.txtSaDDE.CustomFormat = "yyyy-MM-dd";
            this.txtSaDDE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtSaDDE.Location = new System.Drawing.Point(188, 16);
            this.txtSaDDE.Name = "txtSaDDE";
            this.txtSaDDE.Size = new System.Drawing.Size(96, 20);
            this.txtSaDDE.TabIndex = 10;
            // 
            // txtSaDDB
            // 
            this.txtSaDDB.CustomFormat = "yyyy-MM-dd";
            this.txtSaDDB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtSaDDB.Location = new System.Drawing.Point(77, 16);
            this.txtSaDDB.Name = "txtSaDDB";
            this.txtSaDDB.Size = new System.Drawing.Size(96, 20);
            this.txtSaDDB.TabIndex = 9;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(502, 57);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 25);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(174, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "--";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(499, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "--";
            // 
            // txtCusNoE
            // 
            this.txtCusNoE.Location = new System.Drawing.Point(516, 17);
            this.txtCusNoE.Name = "txtCusNoE";
            this.txtCusNoE.Size = new System.Drawing.Size(100, 20);
            this.txtCusNoE.TabIndex = 5;
            this.txtCusNoE.Enter += new System.EventHandler(this.txtCusNoE_Enter);
            // 
            // txtCusNoB
            // 
            this.txtCusNoB.Location = new System.Drawing.Point(379, 17);
            this.txtCusNoB.Name = "txtCusNoB";
            this.txtCusNoB.Size = new System.Drawing.Size(100, 20);
            this.txtCusNoB.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(320, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "起止客户";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "销货日期";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(732, 308);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtTotalAmt);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.txtTotalSb);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtToTalSa);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtTotalSASum);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtTotalAmtn);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 410);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(732, 72);
            this.panel2.TabIndex = 2;
            // 
            // txtTotalAmt
            // 
            this.txtTotalAmt.Location = new System.Drawing.Point(527, 7);
            this.txtTotalAmt.Name = "txtTotalAmt";
            this.txtTotalAmt.ReadOnly = true;
            this.txtTotalAmt.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTotalAmt.Size = new System.Drawing.Size(150, 20);
            this.txtTotalAmt.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(461, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "总销货金额";
            // 
            // txtTotalSb
            // 
            this.txtTotalSb.Location = new System.Drawing.Point(290, 36);
            this.txtTotalSb.Name = "txtTotalSb";
            this.txtTotalSb.ReadOnly = true;
            this.txtTotalSb.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTotalSb.Size = new System.Drawing.Size(150, 20);
            this.txtTotalSb.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(231, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "退货数量";
            // 
            // txtToTalSa
            // 
            this.txtToTalSa.Location = new System.Drawing.Point(77, 36);
            this.txtToTalSa.Name = "txtToTalSa";
            this.txtToTalSa.ReadOnly = true;
            this.txtToTalSa.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtToTalSa.Size = new System.Drawing.Size(150, 20);
            this.txtToTalSa.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "销货数量";
            // 
            // txtTotalSASum
            // 
            this.txtTotalSASum.Location = new System.Drawing.Point(77, 7);
            this.txtTotalSASum.Name = "txtTotalSASum";
            this.txtTotalSASum.ReadOnly = true;
            this.txtTotalSASum.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTotalSASum.Size = new System.Drawing.Size(150, 20);
            this.txtTotalSASum.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "总销货数量";
            // 
            // txtTotalAmtn
            // 
            this.txtTotalAmtn.Location = new System.Drawing.Point(527, 39);
            this.txtTotalAmtn.Name = "txtTotalAmtn";
            this.txtTotalAmtn.ReadOnly = true;
            this.txtTotalAmtn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTotalAmtn.Size = new System.Drawing.Size(150, 20);
            this.txtTotalAmtn.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(461, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "总未税金额";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 102);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(732, 308);
            this.panel3.TabIndex = 3;
            // 
            // RptSAStat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 482);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "RptSAStat";
            this.Text = "销货统计表";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCusNoE;
        private System.Windows.Forms.TextBox txtCusNoB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker txtSaDDB;
        private System.Windows.Forms.DateTimePicker txtSaDDE;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtTotalAmtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtToTalSa;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTotalSASum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTotalSb;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTotalAmt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button qryCusNoE;
        private System.Windows.Forms.Button qryCusNoB;
    }
}