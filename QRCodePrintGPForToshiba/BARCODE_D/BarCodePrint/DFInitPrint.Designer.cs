namespace BarCodePrintTSB
{
	partial class DFInitPrint
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
            this.txtIdxNo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtIdx1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSpc = new System.Windows.Forms.TextBox();
            this.cbFormat = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.bnXpAddPrint = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.btn4BatNo = new System.Windows.Forms.Button();
            this.btn4Prdt = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBatNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrdNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt4Dep = new System.Windows.Forms.TextBox();
            this.btnDept = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.bnXpClear = new System.Windows.Forms.Button();
            this.bnXpPrint = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtIdxNo);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtIdx1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtSpc);
            this.panel1.Controls.Add(this.cbFormat);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.bnXpAddPrint);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.txtCount);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtSN);
            this.panel1.Controls.Add(this.btn4BatNo);
            this.panel1.Controls.Add(this.btn4Prdt);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtBatNo);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtPrdNo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 485);
            this.panel1.TabIndex = 0;
            // 
            // txtIdxNo
            // 
            this.txtIdxNo.Location = new System.Drawing.Point(17, 41);
            this.txtIdxNo.Name = "txtIdxNo";
            this.txtIdxNo.Size = new System.Drawing.Size(16, 20);
            this.txtIdxNo.TabIndex = 50;
            this.txtIdxNo.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(39, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 46;
            this.label10.Text = "中类";
            // 
            // txtIdx1
            // 
            this.txtIdx1.Location = new System.Drawing.Point(74, 41);
            this.txtIdx1.Name = "txtIdx1";
            this.txtIdx1.ReadOnly = true;
            this.txtIdx1.Size = new System.Drawing.Size(150, 20);
            this.txtIdx1.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "规格";
            // 
            // txtSpc
            // 
            this.txtSpc.Location = new System.Drawing.Point(74, 68);
            this.txtSpc.Name = "txtSpc";
            this.txtSpc.ReadOnly = true;
            this.txtSpc.Size = new System.Drawing.Size(150, 20);
            this.txtSpc.TabIndex = 24;
            // 
            // cbFormat
            // 
            this.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormat.FormattingEnabled = true;
            this.cbFormat.Items.AddRange(new object[] {
            "标准版"});
            this.cbFormat.Location = new System.Drawing.Point(74, 185);
            this.cbFormat.Name = "cbFormat";
            this.cbFormat.Size = new System.Drawing.Size(150, 21);
            this.cbFormat.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 188);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "打印版式";
            // 
            // bnXpAddPrint
            // 
            this.bnXpAddPrint.Location = new System.Drawing.Point(169, 241);
            this.bnXpAddPrint.Name = "bnXpAddPrint";
            this.bnXpAddPrint.Size = new System.Drawing.Size(75, 25);
            this.bnXpAddPrint.TabIndex = 1;
            this.bnXpAddPrint.Text = "加入打印";
            this.bnXpAddPrint.UseVisualStyleBackColor = true;
            this.bnXpAddPrint.Click += new System.EventHandler(this.bnXpAddPrint_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(88, 241);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 25);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 159);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "数量";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 431);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(266, 52);
            this.panel2.TabIndex = 1;
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(74, 155);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(150, 20);
            this.txtCount.TabIndex = 14;
            this.txtCount.Text = "1";
            this.txtCount.Leave += new System.EventHandler(this.txtCount_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "流水号";
            // 
            // txtSN
            // 
            this.txtSN.Location = new System.Drawing.Point(74, 126);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(150, 20);
            this.txtSN.TabIndex = 12;
            this.txtSN.Leave += new System.EventHandler(this.txtSN_Leave);
            // 
            // btn4BatNo
            // 
            this.btn4BatNo.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btn4BatNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn4BatNo.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn4BatNo.Location = new System.Drawing.Point(224, 98);
            this.btn4BatNo.Name = "btn4BatNo";
            this.btn4BatNo.Size = new System.Drawing.Size(20, 20);
            this.btn4BatNo.TabIndex = 11;
            this.btn4BatNo.UseVisualStyleBackColor = true;
            this.btn4BatNo.Click += new System.EventHandler(this.btn4BatNo_Click);
            // 
            // btn4Prdt
            // 
            this.btn4Prdt.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btn4Prdt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn4Prdt.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn4Prdt.Location = new System.Drawing.Point(224, 13);
            this.btn4Prdt.Name = "btn4Prdt";
            this.btn4Prdt.Size = new System.Drawing.Size(20, 20);
            this.btn4Prdt.TabIndex = 10;
            this.btn4Prdt.UseVisualStyleBackColor = true;
            this.btn4Prdt.Click += new System.EventHandler(this.btn4Prdt_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "批号";
            // 
            // txtBatNo
            // 
            this.txtBatNo.Location = new System.Drawing.Point(74, 98);
            this.txtBatNo.Name = "txtBatNo";
            this.txtBatNo.Size = new System.Drawing.Size(150, 20);
            this.txtBatNo.TabIndex = 8;
            this.txtBatNo.Leave += new System.EventHandler(this.txtBatNo_Leave);
            this.txtBatNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatNo_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "半成品品号";
            // 
            // txtPrdNo
            // 
            this.txtPrdNo.BackColor = System.Drawing.SystemColors.Window;
            this.txtPrdNo.Location = new System.Drawing.Point(74, 13);
            this.txtPrdNo.Name = "txtPrdNo";
            this.txtPrdNo.Size = new System.Drawing.Size(150, 20);
            this.txtPrdNo.TabIndex = 6;
            this.txtPrdNo.Leave += new System.EventHandler(this.txtPrdNo_Leave);
            this.txtPrdNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrdNo_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 467);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "部门";
            // 
            // txt4Dep
            // 
            this.txt4Dep.Location = new System.Drawing.Point(63, 463);
            this.txt4Dep.Name = "txt4Dep";
            this.txt4Dep.Size = new System.Drawing.Size(120, 20);
            this.txt4Dep.TabIndex = 1;
            // 
            // btnDept
            // 
            this.btnDept.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDept.Location = new System.Drawing.Point(196, 466);
            this.btnDept.Name = "btnDept";
            this.btnDept.Size = new System.Drawing.Size(25, 20);
            this.btnDept.TabIndex = 16;
            this.btnDept.Text = "...";
            this.btnDept.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(268, 435);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(524, 50);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.bnXpClear);
            this.panel4.Controls.Add(this.bnXpPrint);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(298, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(224, 48);
            this.panel4.TabIndex = 17;
            // 
            // bnXpClear
            // 
            this.bnXpClear.Location = new System.Drawing.Point(18, 14);
            this.bnXpClear.Name = "bnXpClear";
            this.bnXpClear.Size = new System.Drawing.Size(75, 25);
            this.bnXpClear.TabIndex = 2;
            this.bnXpClear.Text = "清除";
            this.bnXpClear.UseVisualStyleBackColor = true;
            this.bnXpClear.Click += new System.EventHandler(this.bnXpClear_Click);
            // 
            // bnXpPrint
            // 
            this.bnXpPrint.Location = new System.Drawing.Point(121, 14);
            this.bnXpPrint.Name = "bnXpPrint";
            this.bnXpPrint.Size = new System.Drawing.Size(75, 25);
            this.bnXpPrint.TabIndex = 3;
            this.bnXpPrint.Text = "打印";
            this.bnXpPrint.UseVisualStyleBackColor = true;
            this.bnXpPrint.Click += new System.EventHandler(this.bnXpPrint_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(268, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(524, 435);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.dataGridView1.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_UserDeletedRow);
            // 
            // DFInitPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 485);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txt4Dep);
            this.Controls.Add(this.btnDept);
            this.Controls.Add(this.label3);
            this.Name = "DFInitPrint";
            this.Text = "期初半成品条码打印";
            this.Load += new System.EventHandler(this.DFInitPrint_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox txt4Dep;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btn4BatNo;
		private System.Windows.Forms.Button btn4Prdt;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtBatNo;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtPrdNo;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtSN;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtCount;
		private System.Windows.Forms.Button bnXpAddPrint;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button bnXpPrint;
		private System.Windows.Forms.Button bnXpClear;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Button btnDept;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.ComboBox cbFormat;
		private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSpc;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtIdx1;
        private System.Windows.Forms.TextBox txtIdxNo;
	}
}