namespace BarCodePrintTSB
{
    partial class QueryMo
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
            this.txtMoNoB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMoDdB = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.qryMONOB = new System.Windows.Forms.Button();
            this.qryMONOE = new System.Windows.Forms.Button();
            this.qryPrdNoB = new System.Windows.Forms.Button();
            this.qryPrdNoE = new System.Windows.Forms.Button();
            this.txtPrdNoE = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPrdNoB = new System.Windows.Forms.TextBox();
            this.cbMOCloseId = new System.Windows.Forms.CheckBox();
            this.txtMoNoE = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMoDdE = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "制单日期";
            // 
            // txtMoNoB
            // 
            this.txtMoNoB.Location = new System.Drawing.Point(78, 41);
            this.txtMoNoB.Name = "txtMoNoB";
            this.txtMoNoB.Size = new System.Drawing.Size(100, 20);
            this.txtMoNoB.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "制令单号";
            // 
            // txtMoDdB
            // 
            this.txtMoDdB.CustomFormat = "yyyy-MM-dd";
            this.txtMoDdB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtMoDdB.Location = new System.Drawing.Point(78, 15);
            this.txtMoDdB.Name = "txtMoDdB";
            this.txtMoDdB.Size = new System.Drawing.Size(120, 20);
            this.txtMoDdB.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.qryMONOB);
            this.panel1.Controls.Add(this.qryMONOE);
            this.panel1.Controls.Add(this.qryPrdNoB);
            this.panel1.Controls.Add(this.qryPrdNoE);
            this.panel1.Controls.Add(this.txtPrdNoE);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtPrdNoB);
            this.panel1.Controls.Add(this.cbMOCloseId);
            this.panel1.Controls.Add(this.txtMoNoE);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtMoDdE);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtMoDdB);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtMoNoB);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(627, 96);
            this.panel1.TabIndex = 5;
            // 
            // qryMONOB
            // 
            this.qryMONOB.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qryMONOB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qryMONOB.Location = new System.Drawing.Point(178, 40);
            this.qryMONOB.Name = "qryMONOB";
            this.qryMONOB.Size = new System.Drawing.Size(20, 20);
            this.qryMONOB.TabIndex = 26;
            this.qryMONOB.UseVisualStyleBackColor = true;
            this.qryMONOB.Click += new System.EventHandler(this.qryMONOB_Click);
            // 
            // qryMONOE
            // 
            this.qryMONOE.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qryMONOE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qryMONOE.Location = new System.Drawing.Point(329, 40);
            this.qryMONOE.Name = "qryMONOE";
            this.qryMONOE.Size = new System.Drawing.Size(20, 20);
            this.qryMONOE.TabIndex = 25;
            this.qryMONOE.UseVisualStyleBackColor = true;
            this.qryMONOE.Click += new System.EventHandler(this.qryMONOE_Click);
            // 
            // qryPrdNoB
            // 
            this.qryPrdNoB.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qryPrdNoB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qryPrdNoB.Location = new System.Drawing.Point(178, 67);
            this.qryPrdNoB.Name = "qryPrdNoB";
            this.qryPrdNoB.Size = new System.Drawing.Size(20, 20);
            this.qryPrdNoB.TabIndex = 24;
            this.qryPrdNoB.UseVisualStyleBackColor = true;
            this.qryPrdNoB.Click += new System.EventHandler(this.qryPrdNoB_Click);
            // 
            // qryPrdNoE
            // 
            this.qryPrdNoE.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qryPrdNoE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qryPrdNoE.Location = new System.Drawing.Point(329, 67);
            this.qryPrdNoE.Name = "qryPrdNoE";
            this.qryPrdNoE.Size = new System.Drawing.Size(20, 20);
            this.qryPrdNoE.TabIndex = 23;
            this.qryPrdNoE.UseVisualStyleBackColor = true;
            this.qryPrdNoE.Click += new System.EventHandler(this.qryPrdNoE_Click);
            // 
            // txtPrdNoE
            // 
            this.txtPrdNoE.Location = new System.Drawing.Point(229, 67);
            this.txtPrdNoE.Name = "txtPrdNoE";
            this.txtPrdNoE.Size = new System.Drawing.Size(100, 20);
            this.txtPrdNoE.TabIndex = 14;
            this.txtPrdNoE.Enter += new System.EventHandler(this.txtPrdNoE_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(204, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "－";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "品号";
            // 
            // txtPrdNoB
            // 
            this.txtPrdNoB.Location = new System.Drawing.Point(78, 67);
            this.txtPrdNoB.Name = "txtPrdNoB";
            this.txtPrdNoB.Size = new System.Drawing.Size(100, 20);
            this.txtPrdNoB.TabIndex = 12;
            // 
            // cbMOCloseId
            // 
            this.cbMOCloseId.AutoSize = true;
            this.cbMOCloseId.Location = new System.Drawing.Point(378, 17);
            this.cbMOCloseId.Name = "cbMOCloseId";
            this.cbMOCloseId.Size = new System.Drawing.Size(122, 17);
            this.cbMOCloseId.TabIndex = 10;
            this.cbMOCloseId.Text = "过滤已结案制令单";
            this.cbMOCloseId.UseVisualStyleBackColor = true;
            // 
            // txtMoNoE
            // 
            this.txtMoNoE.Location = new System.Drawing.Point(229, 41);
            this.txtMoNoE.Name = "txtMoNoE";
            this.txtMoNoE.Size = new System.Drawing.Size(100, 20);
            this.txtMoNoE.TabIndex = 9;
            this.txtMoNoE.Enter += new System.EventHandler(this.txtMoNoE_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(204, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "－";
            // 
            // txtMoDdE
            // 
            this.txtMoDdE.CustomFormat = "yyyy-MM-dd";
            this.txtMoDdE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtMoDdE.Location = new System.Drawing.Point(229, 14);
            this.txtMoDdE.Name = "txtMoDdE";
            this.txtMoDdE.Size = new System.Drawing.Size(120, 20);
            this.txtMoDdE.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(204, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "－";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(412, 69);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(67, 21);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnClear);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 312);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(627, 40);
            this.panel2.TabIndex = 7;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(534, 7);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(67, 21);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "取消";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(438, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(67, 21);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 96);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(627, 216);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // QueryMo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 352);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "QueryMo";
            this.Text = "制令单查询";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMoNoB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker txtMoDdB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker txtMoDdE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMoNoE;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox cbMOCloseId;
        private System.Windows.Forms.TextBox txtPrdNoE;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPrdNoB;
        private System.Windows.Forms.Button qryPrdNoB;
        private System.Windows.Forms.Button qryPrdNoE;
        private System.Windows.Forms.Button qryMONOB;
        private System.Windows.Forms.Button qryMONOE;
    }
}