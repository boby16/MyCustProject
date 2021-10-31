namespace BarCodePrintTSB
{
    partial class QueryBox
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
            this.txtPrdNoB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.qryPrdNoB = new System.Windows.Forms.Button();
            this.qryPrdNoE = new System.Windows.Forms.Button();
            this.txtBoxNoB = new System.Windows.Forms.TextBox();
            this.txtBoxNoE = new System.Windows.Forms.TextBox();
            this.txtPrdNoE = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
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
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "箱条码";
            // 
            // txtPrdNoB
            // 
            this.txtPrdNoB.Location = new System.Drawing.Point(78, 41);
            this.txtPrdNoB.Name = "txtPrdNoB";
            this.txtPrdNoB.Size = new System.Drawing.Size(100, 20);
            this.txtPrdNoB.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "品号";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.qryPrdNoB);
            this.panel1.Controls.Add(this.qryPrdNoE);
            this.panel1.Controls.Add(this.txtBoxNoB);
            this.panel1.Controls.Add(this.txtBoxNoE);
            this.panel1.Controls.Add(this.txtPrdNoE);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtPrdNoB);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(627, 74);
            this.panel1.TabIndex = 5;
            // 
            // qryPrdNoB
            // 
            this.qryPrdNoB.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qryPrdNoB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qryPrdNoB.Location = new System.Drawing.Point(178, 41);
            this.qryPrdNoB.Name = "qryPrdNoB";
            this.qryPrdNoB.Size = new System.Drawing.Size(20, 20);
            this.qryPrdNoB.TabIndex = 26;
            this.qryPrdNoB.UseVisualStyleBackColor = true;
            this.qryPrdNoB.Click += new System.EventHandler(this.qryPrdNoB_Click);
            // 
            // qryPrdNoE
            // 
            this.qryPrdNoE.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qryPrdNoE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qryPrdNoE.Location = new System.Drawing.Point(323, 41);
            this.qryPrdNoE.Name = "qryPrdNoE";
            this.qryPrdNoE.Size = new System.Drawing.Size(20, 20);
            this.qryPrdNoE.TabIndex = 25;
            this.qryPrdNoE.UseVisualStyleBackColor = true;
            this.qryPrdNoE.Click += new System.EventHandler(this.qryPrdNoE_Click);
            // 
            // txtBoxNoB
            // 
            this.txtBoxNoB.Location = new System.Drawing.Point(78, 15);
            this.txtBoxNoB.Name = "txtBoxNoB";
            this.txtBoxNoB.Size = new System.Drawing.Size(120, 20);
            this.txtBoxNoB.TabIndex = 11;
            // 
            // txtBoxNoE
            // 
            this.txtBoxNoE.Location = new System.Drawing.Point(223, 15);
            this.txtBoxNoE.Name = "txtBoxNoE";
            this.txtBoxNoE.Size = new System.Drawing.Size(120, 20);
            this.txtBoxNoE.TabIndex = 10;
            this.txtBoxNoE.Enter += new System.EventHandler(this.txtBoxNoE_Enter);
            // 
            // txtPrdNoE
            // 
            this.txtPrdNoE.Location = new System.Drawing.Point(223, 41);
            this.txtPrdNoE.Name = "txtPrdNoE";
            this.txtPrdNoE.Size = new System.Drawing.Size(100, 20);
            this.txtPrdNoE.TabIndex = 9;
            this.txtPrdNoE.Enter += new System.EventHandler(this.txtPrdNoE_Enter);
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
            this.btnSearch.Location = new System.Drawing.Point(419, 41);
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
            this.dataGridView1.Location = new System.Drawing.Point(0, 74);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(627, 238);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // QueryBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 352);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "QueryBox";
            this.Text = "QueryBox";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPrdNoB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrdNoE;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtBoxNoB;
        private System.Windows.Forms.TextBox txtBoxNoE;
        private System.Windows.Forms.Button qryPrdNoB;
        private System.Windows.Forms.Button qryPrdNoE;
    }
}