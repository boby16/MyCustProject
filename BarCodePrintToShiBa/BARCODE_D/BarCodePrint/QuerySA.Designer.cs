namespace BarCodePrintTSB
{
    partial class QuerySA
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
            this.txtCusNoB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSADdB = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.qryCusNOB = new System.Windows.Forms.Button();
            this.qryCusNOE = new System.Windows.Forms.Button();
            this.qryPrdNoB = new System.Windows.Forms.Button();
            this.qryPrdNoE = new System.Windows.Forms.Button();
            this.txtPrdNoE = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPrdNoB = new System.Windows.Forms.TextBox();
            this.txtCusNoE = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSADdE = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
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
            this.label1.Text = "销货日期";
            // 
            // txtCusNoB
            // 
            this.txtCusNoB.Location = new System.Drawing.Point(78, 41);
            this.txtCusNoB.Name = "txtCusNoB";
            this.txtCusNoB.Size = new System.Drawing.Size(100, 20);
            this.txtCusNoB.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "销货客户";
            // 
            // txtSADdB
            // 
            this.txtSADdB.CustomFormat = "yyyy-MM-dd";
            this.txtSADdB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtSADdB.Location = new System.Drawing.Point(78, 15);
            this.txtSADdB.Name = "txtSADdB";
            this.txtSADdB.Size = new System.Drawing.Size(120, 20);
            this.txtSADdB.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.qryCusNOB);
            this.panel1.Controls.Add(this.qryCusNOE);
            this.panel1.Controls.Add(this.qryPrdNoB);
            this.panel1.Controls.Add(this.qryPrdNoE);
            this.panel1.Controls.Add(this.txtPrdNoE);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtPrdNoB);
            this.panel1.Controls.Add(this.txtCusNoE);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtSADdE);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtSADdB);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtCusNoB);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(478, 76);
            this.panel1.TabIndex = 5;
            // 
            // qryCusNOB
            // 
            this.qryCusNOB.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qryCusNOB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qryCusNOB.Location = new System.Drawing.Point(178, 40);
            this.qryCusNOB.Name = "qryCusNOB";
            this.qryCusNOB.Size = new System.Drawing.Size(20, 20);
            this.qryCusNOB.TabIndex = 26;
            this.qryCusNOB.UseVisualStyleBackColor = true;
            this.qryCusNOB.Click += new System.EventHandler(this.qryCusNOB_Click);
            // 
            // qryCusNOE
            // 
            this.qryCusNOE.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qryCusNOE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qryCusNOE.Location = new System.Drawing.Point(329, 40);
            this.qryCusNOE.Name = "qryCusNOE";
            this.qryCusNOE.Size = new System.Drawing.Size(20, 20);
            this.qryCusNOE.TabIndex = 25;
            this.qryCusNOE.UseVisualStyleBackColor = true;
            this.qryCusNOE.Click += new System.EventHandler(this.qryCusNOE_Click);
            // 
            // qryPrdNoB
            // 
            this.qryPrdNoB.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qryPrdNoB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qryPrdNoB.Location = new System.Drawing.Point(178, 96);
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
            this.qryPrdNoE.Location = new System.Drawing.Point(329, 96);
            this.qryPrdNoE.Name = "qryPrdNoE";
            this.qryPrdNoE.Size = new System.Drawing.Size(20, 20);
            this.qryPrdNoE.TabIndex = 23;
            this.qryPrdNoE.UseVisualStyleBackColor = true;
            this.qryPrdNoE.Click += new System.EventHandler(this.qryPrdNoE_Click);
            // 
            // txtPrdNoE
            // 
            this.txtPrdNoE.Location = new System.Drawing.Point(229, 96);
            this.txtPrdNoE.Name = "txtPrdNoE";
            this.txtPrdNoE.Size = new System.Drawing.Size(100, 20);
            this.txtPrdNoE.TabIndex = 14;
            this.txtPrdNoE.Enter += new System.EventHandler(this.txtPrdNoE_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(204, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "－";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "品号";
            // 
            // txtPrdNoB
            // 
            this.txtPrdNoB.Location = new System.Drawing.Point(78, 96);
            this.txtPrdNoB.Name = "txtPrdNoB";
            this.txtPrdNoB.Size = new System.Drawing.Size(100, 20);
            this.txtPrdNoB.TabIndex = 12;
            // 
            // txtCusNoE
            // 
            this.txtCusNoE.Location = new System.Drawing.Point(229, 41);
            this.txtCusNoE.Name = "txtCusNoE";
            this.txtCusNoE.Size = new System.Drawing.Size(100, 20);
            this.txtCusNoE.TabIndex = 9;
            this.txtCusNoE.Enter += new System.EventHandler(this.txtCusNoE_Enter);
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
            // txtSADdE
            // 
            this.txtSADdE.CustomFormat = "yyyy-MM-dd";
            this.txtSADdE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtSADdE.Location = new System.Drawing.Point(229, 14);
            this.txtSADdE.Name = "txtSADdE";
            this.txtSADdE.Size = new System.Drawing.Size(120, 20);
            this.txtSADdE.TabIndex = 7;
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
            this.btnSearch.Location = new System.Drawing.Point(373, 40);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(67, 21);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 311);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(478, 40);
            this.panel2.TabIndex = 7;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(373, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(67, 21);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "选择";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 76);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(478, 235);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // QuerySA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 351);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "QuerySA";
            this.Text = "销货单查询";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCusNoB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker txtSADdB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker txtSADdE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCusNoE;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtPrdNoE;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPrdNoB;
        private System.Windows.Forms.Button qryPrdNoB;
        private System.Windows.Forms.Button qryPrdNoE;
        private System.Windows.Forms.Button qryCusNOB;
        private System.Windows.Forms.Button qryCusNOE;
    }
}