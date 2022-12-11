namespace BarCodePrintTSB
{
    partial class RPTPrdtHisList
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
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBoxNoB = new System.Windows.Forms.TextBox();
            this.txtBoxNoE = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lstPrdtKnd = new System.Windows.Forms.ComboBox();
            this.qryBatNoE = new System.Windows.Forms.Button();
            this.qryBatNoB = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBatNoB = new System.Windows.Forms.TextBox();
            this.txtBatNoE = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBarNoB = new System.Windows.Forms.TextBox();
            this.txtBarNoE = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtBoxNoB);
            this.panel1.Controls.Add(this.txtBoxNoE);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lstPrdtKnd);
            this.panel1.Controls.Add(this.qryBatNoE);
            this.panel1.Controls.Add(this.qryBatNoB);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtBatNoB);
            this.panel1.Controls.Add(this.txtBatNoE);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtBarNoB);
            this.panel1.Controls.Add(this.txtBarNoE);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(847, 104);
            this.panel1.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(376, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "起止箱条码:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(629, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 13);
            this.label8.TabIndex = 32;
            this.label8.Text = "--";
            // 
            // txtBoxNoB
            // 
            this.txtBoxNoB.Location = new System.Drawing.Point(452, 40);
            this.txtBoxNoB.Name = "txtBoxNoB";
            this.txtBoxNoB.Size = new System.Drawing.Size(170, 20);
            this.txtBoxNoB.TabIndex = 30;
            // 
            // txtBoxNoE
            // 
            this.txtBoxNoE.Location = new System.Drawing.Point(648, 40);
            this.txtBoxNoE.Name = "txtBoxNoE";
            this.txtBoxNoE.Size = new System.Drawing.Size(170, 20);
            this.txtBoxNoE.TabIndex = 31;
            this.txtBoxNoE.Enter += new System.EventHandler(this.txtBoxNoE_Enter);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "产品类别:";
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
            this.lstPrdtKnd.Location = new System.Drawing.Point(78, 40);
            this.lstPrdtKnd.Name = "lstPrdtKnd";
            this.lstPrdtKnd.Size = new System.Drawing.Size(121, 21);
            this.lstPrdtKnd.TabIndex = 27;
            // 
            // qryBatNoE
            // 
            this.qryBatNoE.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qryBatNoE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qryBatNoE.Location = new System.Drawing.Point(327, 14);
            this.qryBatNoE.Name = "qryBatNoE";
            this.qryBatNoE.Size = new System.Drawing.Size(20, 20);
            this.qryBatNoE.TabIndex = 20;
            this.qryBatNoE.UseVisualStyleBackColor = true;
            this.qryBatNoE.Click += new System.EventHandler(this.qryBatNoE_Click);
            // 
            // qryBatNoB
            // 
            this.qryBatNoB.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qryBatNoB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qryBatNoB.Location = new System.Drawing.Point(178, 14);
            this.qryBatNoB.Name = "qryBatNoB";
            this.qryBatNoB.Size = new System.Drawing.Size(20, 20);
            this.qryBatNoB.TabIndex = 19;
            this.qryBatNoB.UseVisualStyleBackColor = true;
            this.qryBatNoB.Click += new System.EventHandler(this.qryBatNoB_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(730, 67);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 21);
            this.btnExport.TabIndex = 15;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(631, 67);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 21);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "起止批号:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(204, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "--";
            // 
            // txtBatNoB
            // 
            this.txtBatNoB.Location = new System.Drawing.Point(78, 14);
            this.txtBatNoB.Name = "txtBatNoB";
            this.txtBatNoB.Size = new System.Drawing.Size(100, 20);
            this.txtBatNoB.TabIndex = 9;
            // 
            // txtBatNoE
            // 
            this.txtBatNoE.Location = new System.Drawing.Point(227, 15);
            this.txtBatNoE.Name = "txtBatNoE";
            this.txtBatNoE.Size = new System.Drawing.Size(100, 20);
            this.txtBatNoE.TabIndex = 10;
            this.txtBatNoE.Enter += new System.EventHandler(this.txtBatNoE_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(377, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "起止序列号:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(629, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "--";
            // 
            // txtBarNoB
            // 
            this.txtBarNoB.Location = new System.Drawing.Point(452, 14);
            this.txtBarNoB.Name = "txtBarNoB";
            this.txtBarNoB.Size = new System.Drawing.Size(170, 20);
            this.txtBarNoB.TabIndex = 5;
            // 
            // txtBarNoE
            // 
            this.txtBarNoE.Location = new System.Drawing.Point(648, 14);
            this.txtBarNoE.Name = "txtBarNoE";
            this.txtBarNoE.Size = new System.Drawing.Size(170, 20);
            this.txtBarNoE.TabIndex = 6;
            this.txtBarNoE.Enter += new System.EventHandler(this.txtBarNoE_Enter);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 104);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(847, 438);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.dataGridView1.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseDoubleClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // RPTPrdtHisList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 542);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "RPTPrdtHisList";
            this.Text = "产品历史查询";
            this.Load += new System.EventHandler(this.RPTPrdtHisList_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBatNoB;
        private System.Windows.Forms.TextBox txtBatNoE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBarNoB;
        private System.Windows.Forms.TextBox txtBarNoE;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button qryBatNoE;
        private System.Windows.Forms.Button qryBatNoB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox lstPrdtKnd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBoxNoB;
        private System.Windows.Forms.TextBox txtBoxNoE;
    }
}