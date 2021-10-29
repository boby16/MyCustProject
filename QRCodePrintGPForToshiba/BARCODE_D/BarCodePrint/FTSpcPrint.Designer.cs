namespace BarCodePrintTSB
{
	partial class FTSpcPrint
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
            this.label10 = new System.Windows.Forms.Label();
            this.txtLCount = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBCPrdt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBCSpc = new System.Windows.Forms.TextBox();
            this.cbFormat = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btn4BatNo = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBatNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFTSpc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBCBar = new System.Windows.Forms.TextBox();
            this.bnXpAddPrint = new System.Windows.Forms.Button();
            this.btn4Prdt = new System.Windows.Forms.Button();
            this.txtWCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrdNo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bnXpPrint = new System.Windows.Forms.Button();
            this.bnXpClear = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtLCount);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtBCPrdt);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtBCSpc);
            this.panel1.Controls.Add(this.cbFormat);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.btn4BatNo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtBatNo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtFTSpc);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtBCBar);
            this.panel1.Controls.Add(this.bnXpAddPrint);
            this.panel1.Controls.Add(this.btn4Prdt);
            this.panel1.Controls.Add(this.txtWCount);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtPrdNo);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(303, 475);
            this.panel1.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 233);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 13);
            this.label10.TabIndex = 41;
            this.label10.Text = "份数(长度)";
            // 
            // txtLCount
            // 
            this.txtLCount.Location = new System.Drawing.Point(90, 230);
            this.txtLCount.Name = "txtLCount";
            this.txtLCount.Size = new System.Drawing.Size(180, 20);
            this.txtLCount.TabIndex = 40;
            this.txtLCount.Leave += new System.EventHandler(this.txtLCount_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 203);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 39;
            this.label11.Text = "份数(宽度)";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(134, 308);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 25);
            this.btnClear.TabIndex = 34;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "板材品名";
            // 
            // txtBCPrdt
            // 
            this.txtBCPrdt.Location = new System.Drawing.Point(90, 40);
            this.txtBCPrdt.Name = "txtBCPrdt";
            this.txtBCPrdt.ReadOnly = true;
            this.txtBCPrdt.Size = new System.Drawing.Size(180, 20);
            this.txtBCPrdt.TabIndex = 31;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "板材规格";
            // 
            // txtBCSpc
            // 
            this.txtBCSpc.Location = new System.Drawing.Point(90, 70);
            this.txtBCSpc.Name = "txtBCSpc";
            this.txtBCSpc.ReadOnly = true;
            this.txtBCSpc.Size = new System.Drawing.Size(180, 20);
            this.txtBCSpc.TabIndex = 29;
            // 
            // cbFormat
            // 
            this.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormat.FormattingEnabled = true;
            this.cbFormat.Items.AddRange(new object[] {
            "标准版"});
            this.cbFormat.Location = new System.Drawing.Point(90, 261);
            this.cbFormat.Name = "cbFormat";
            this.cbFormat.Size = new System.Drawing.Size(180, 21);
            this.cbFormat.TabIndex = 28;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 264);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "打印版式";
            // 
            // btn4BatNo
            // 
            this.btn4BatNo.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btn4BatNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn4BatNo.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn4BatNo.Location = new System.Drawing.Point(270, 170);
            this.btn4BatNo.Name = "btn4BatNo";
            this.btn4BatNo.Size = new System.Drawing.Size(20, 20);
            this.btn4BatNo.TabIndex = 26;
            this.btn4BatNo.UseVisualStyleBackColor = true;
            this.btn4BatNo.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "成品批号";
            // 
            // txtBatNo
            // 
            this.txtBatNo.Location = new System.Drawing.Point(90, 170);
            this.txtBatNo.Name = "txtBatNo";
            this.txtBatNo.ReadOnly = true;
            this.txtBatNo.Size = new System.Drawing.Size(180, 20);
            this.txtBatNo.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "成品规格";
            // 
            // txtFTSpc
            // 
            this.txtFTSpc.Location = new System.Drawing.Point(90, 141);
            this.txtFTSpc.Name = "txtFTSpc";
            this.txtFTSpc.ReadOnly = true;
            this.txtFTSpc.Size = new System.Drawing.Size(180, 20);
            this.txtFTSpc.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "板材条码";
            // 
            // txtBCBar
            // 
            this.txtBCBar.Location = new System.Drawing.Point(90, 11);
            this.txtBCBar.Name = "txtBCBar";
            this.txtBCBar.Size = new System.Drawing.Size(180, 20);
            this.txtBCBar.TabIndex = 17;
            this.txtBCBar.Leave += new System.EventHandler(this.txtBCBar_Leave);
            this.txtBCBar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBCBar_KeyDown);
            // 
            // bnXpAddPrint
            // 
            this.bnXpAddPrint.Location = new System.Drawing.Point(215, 308);
            this.bnXpAddPrint.Name = "bnXpAddPrint";
            this.bnXpAddPrint.Size = new System.Drawing.Size(75, 25);
            this.bnXpAddPrint.TabIndex = 1;
            this.bnXpAddPrint.Text = "加入打印";
            this.bnXpAddPrint.UseVisualStyleBackColor = true;
            this.bnXpAddPrint.Click += new System.EventHandler(this.bnXpAddPrint_Click);
            // 
            // btn4Prdt
            // 
            this.btn4Prdt.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btn4Prdt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn4Prdt.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn4Prdt.Location = new System.Drawing.Point(270, 112);
            this.btn4Prdt.Name = "btn4Prdt";
            this.btn4Prdt.Size = new System.Drawing.Size(20, 20);
            this.btn4Prdt.TabIndex = 10;
            this.btn4Prdt.UseVisualStyleBackColor = true;
            this.btn4Prdt.Click += new System.EventHandler(this.btn4Prdt_Click);
            // 
            // txtWCount
            // 
            this.txtWCount.Location = new System.Drawing.Point(90, 200);
            this.txtWCount.Name = "txtWCount";
            this.txtWCount.Size = new System.Drawing.Size(180, 20);
            this.txtWCount.TabIndex = 8;
            this.txtWCount.Text = "1";
            this.txtWCount.Leave += new System.EventHandler(this.txtWCount_Leave);
            this.txtWCount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWCount_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "成品品号";
            // 
            // txtPrdNo
            // 
            this.txtPrdNo.Location = new System.Drawing.Point(90, 112);
            this.txtPrdNo.Name = "txtPrdNo";
            this.txtPrdNo.Size = new System.Drawing.Size(180, 20);
            this.txtPrdNo.TabIndex = 6;
            this.txtPrdNo.Leave += new System.EventHandler(this.txtPrdNo_Leave);
            this.txtPrdNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrdNo_KeyDown);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 93);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(292, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "................................................................................." +
                "............";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(303, 425);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(489, 50);
            this.panel3.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bnXpPrint);
            this.panel2.Controls.Add(this.bnXpClear);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(275, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(212, 48);
            this.panel2.TabIndex = 4;
            // 
            // bnXpPrint
            // 
            this.bnXpPrint.Location = new System.Drawing.Point(120, 11);
            this.bnXpPrint.Name = "bnXpPrint";
            this.bnXpPrint.Size = new System.Drawing.Size(75, 25);
            this.bnXpPrint.TabIndex = 3;
            this.bnXpPrint.Text = "打印";
            this.bnXpPrint.UseVisualStyleBackColor = true;
            this.bnXpPrint.Click += new System.EventHandler(this.bnXpPrint_Click);
            // 
            // bnXpClear
            // 
            this.bnXpClear.Location = new System.Drawing.Point(19, 11);
            this.bnXpClear.Name = "bnXpClear";
            this.bnXpClear.Size = new System.Drawing.Size(75, 25);
            this.bnXpClear.TabIndex = 2;
            this.bnXpClear.Text = "清除";
            this.bnXpClear.UseVisualStyleBackColor = true;
            this.bnXpClear.Click += new System.EventHandler(this.bnXpClear_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(303, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(489, 425);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // FTSpcPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 475);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "FTSpcPrint";
            this.Text = "成品条码打印(特殊)";
            this.Load += new System.EventHandler(this.FTSpcPrint_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn4Prdt;
		private System.Windows.Forms.TextBox txtWCount;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtPrdNo;
		private System.Windows.Forms.Button bnXpAddPrint;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button bnXpPrint;
		private System.Windows.Forms.Button bnXpClear;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtBCBar;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtFTSpc;
		private System.Windows.Forms.Button btn4BatNo;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtBatNo;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ComboBox cbFormat;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtBCPrdt;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtBCSpc;
		private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtLCount;
        private System.Windows.Forms.Label label11;
	}
}