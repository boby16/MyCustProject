namespace BarCodePrintTSB
{
    partial class BarSQNOMng
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
            this.label10 = new System.Windows.Forms.Label();
            this.txtIdx1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSpc = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.btn4BatNo = new System.Windows.Forms.Button();
            this.btn4Prdt = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBatNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrdNo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(72, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 58;
            this.label10.Text = "中类";
            // 
            // txtIdx1
            // 
            this.txtIdx1.Location = new System.Drawing.Point(107, 62);
            this.txtIdx1.Name = "txtIdx1";
            this.txtIdx1.ReadOnly = true;
            this.txtIdx1.Size = new System.Drawing.Size(150, 20);
            this.txtIdx1.TabIndex = 57;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "规格";
            // 
            // txtSpc
            // 
            this.txtSpc.Location = new System.Drawing.Point(107, 89);
            this.txtSpc.Name = "txtSpc";
            this.txtSpc.ReadOnly = true;
            this.txtSpc.Size = new System.Drawing.Size(150, 20);
            this.txtSpc.TabIndex = 55;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(60, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 54;
            this.label6.Text = "流水号";
            // 
            // txtSN
            // 
            this.txtSN.Location = new System.Drawing.Point(107, 147);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(150, 20);
            this.txtSN.TabIndex = 53;
            // 
            // btn4BatNo
            // 
            this.btn4BatNo.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn4BatNo.Location = new System.Drawing.Point(257, 119);
            this.btn4BatNo.Name = "btn4BatNo";
            this.btn4BatNo.Size = new System.Drawing.Size(25, 20);
            this.btn4BatNo.TabIndex = 52;
            this.btn4BatNo.Text = "...";
            this.btn4BatNo.UseVisualStyleBackColor = true;
            this.btn4BatNo.Click += new System.EventHandler(this.btn4BatNo_Click);
            // 
            // btn4Prdt
            // 
            this.btn4Prdt.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn4Prdt.Location = new System.Drawing.Point(257, 34);
            this.btn4Prdt.Name = "btn4Prdt";
            this.btn4Prdt.Size = new System.Drawing.Size(25, 20);
            this.btn4Prdt.TabIndex = 51;
            this.btn4Prdt.Text = "...";
            this.btn4Prdt.UseVisualStyleBackColor = true;
            this.btn4Prdt.Click += new System.EventHandler(this.btn4Prdt_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(72, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 50;
            this.label5.Text = "批号";
            // 
            // txtBatNo
            // 
            this.txtBatNo.Location = new System.Drawing.Point(107, 118);
            this.txtBatNo.Name = "txtBatNo";
            this.txtBatNo.Size = new System.Drawing.Size(150, 20);
            this.txtBatNo.TabIndex = 49;
            this.txtBatNo.Leave += new System.EventHandler(this.txtBatNo_Leave);
            this.txtBatNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatNo_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(72, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "品号";
            // 
            // txtPrdNo
            // 
            this.txtPrdNo.BackColor = System.Drawing.SystemColors.Window;
            this.txtPrdNo.Location = new System.Drawing.Point(107, 34);
            this.txtPrdNo.Name = "txtPrdNo";
            this.txtPrdNo.Size = new System.Drawing.Size(150, 20);
            this.txtPrdNo.TabIndex = 47;
            this.txtPrdNo.Leave += new System.EventHandler(this.txtPrdNo_Leave);
            this.txtPrdNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrdNo_KeyDown);
            // 
            // BarSQNOMng
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 216);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtIdx1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSpc);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtSN);
            this.Controls.Add(this.btn4BatNo);
            this.Controls.Add(this.btn4Prdt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtBatNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPrdNo);
            this.Name = "BarSQNOMng";
            this.Text = "条码流水号调整(暂没用)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtIdx1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSpc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.Button btn4BatNo;
        private System.Windows.Forms.Button btn4Prdt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBatNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPrdNo;
    }
}