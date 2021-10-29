namespace Sunlike.Windows.Forms
{
    partial class CheckQTY
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
            this.cLabel1 = new Sunlike.Windows.Forms.CLabel();
            this.txtFormula = new Sunlike.Windows.Forms.CTextBox();
            this.cLabel2 = new Sunlike.Windows.Forms.CLabel();
            this.btnOK = new Sunlike.Windows.Forms.CButtonXP();
            this.btnCancel = new Sunlike.Windows.Forms.CButtonXP();
            this.lbl0 = new Sunlike.Windows.Forms.CLabel();
            this.lbl1 = new Sunlike.Windows.Forms.CLabel();
            this.txt0 = new Sunlike.Windows.Forms.CTextBox();
            this.txt1 = new Sunlike.Windows.Forms.CTextBox();
            this.txt2 = new Sunlike.Windows.Forms.CTextBox();
            this.lbl2 = new Sunlike.Windows.Forms.CLabel();
            this.txt3 = new Sunlike.Windows.Forms.CTextBox();
            this.lbl3 = new Sunlike.Windows.Forms.CLabel();
            this.txt4 = new Sunlike.Windows.Forms.CTextBox();
            this.lbl4 = new Sunlike.Windows.Forms.CLabel();
            this.panButton.SuspendLayout();
            this.panMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panButton
            // 
            this.panButton.Controls.Add(this.btnCancel);
            this.panButton.Controls.Add(this.btnOK);
            this.panButton.Location = new System.Drawing.Point(0, 144);
            this.panButton.Size = new System.Drawing.Size(389, 32);
            // 
            // panMain
            // 
            this.panMain.Controls.Add(this.txt4);
            this.panMain.Controls.Add(this.lbl4);
            this.panMain.Controls.Add(this.txt3);
            this.panMain.Controls.Add(this.lbl3);
            this.panMain.Controls.Add(this.txt2);
            this.panMain.Controls.Add(this.lbl2);
            this.panMain.Controls.Add(this.txt1);
            this.panMain.Controls.Add(this.txt0);
            this.panMain.Controls.Add(this.lbl1);
            this.panMain.Controls.Add(this.lbl0);
            this.panMain.Controls.Add(this.cLabel2);
            this.panMain.Controls.Add(this.txtFormula);
            this.panMain.Controls.Add(this.cLabel1);
            this.panMain.Size = new System.Drawing.Size(389, 142);
            // 
            // cLabel1
            // 
            this.cLabel1.AutoSize = true;
            this.cLabel1.Location = new System.Drawing.Point(22, 20);
            this.cLabel1.Name = "cLabel1";
            this.cLabel1.Size = new System.Drawing.Size(68, 18);
            this.cLabel1.TabIndex = 0;
            this.cLabel1.Text = "主单位数量";
            this.cLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cLabel1.UseCompatibleTextRendering = true;
            // 
            // txtFormula
            // 
            this.txtFormula.Location = new System.Drawing.Point(106, 17);
            this.txtFormula.Name = "txtFormula";
            this.txtFormula.ReadOnly = true;
            this.txtFormula.Size = new System.Drawing.Size(252, 21);
            this.txtFormula.TabIndex = 1;
            // 
            // cLabel2
            // 
            this.cLabel2.AutoSize = true;
            this.cLabel2.Location = new System.Drawing.Point(89, 20);
            this.cLabel2.Name = "cLabel2";
            this.cLabel2.Size = new System.Drawing.Size(11, 18);
            this.cLabel2.TabIndex = 2;
            this.cLabel2.Text = "=";
            this.cLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cLabel2.UseCompatibleTextRendering = true;
            // 
            // btnOK
            // 
            this.btnOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.btnOK.Location = new System.Drawing.Point(200, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(60, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.btnCancel.Location = new System.Drawing.Point(284, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbl0
            // 
            this.lbl0.AutoSize = true;
            this.lbl0.Location = new System.Drawing.Point(12, 61);
            this.lbl0.Name = "lbl0";
            this.lbl0.Size = new System.Drawing.Size(36, 18);
            this.lbl0.TabIndex = 3;
            this.lbl0.Text = "变量0";
            this.lbl0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl0.UseCompatibleTextRendering = true;
            this.lbl0.Visible = false;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(86, 61);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(36, 18);
            this.lbl1.TabIndex = 4;
            this.lbl1.Text = "变量1";
            this.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl1.UseCompatibleTextRendering = true;
            this.lbl1.Visible = false;
            // 
            // txt0
            // 
            this.txt0.Location = new System.Drawing.Point(10, 89);
            this.txt0.Name = "txt0";
            this.txt0.Size = new System.Drawing.Size(68, 21);
            this.txt0.TabIndex = 8;
            this.txt0.TextDataType = Sunlike.Windows.Forms.EDataType.DTDecimal;
            this.txt0.Visible = false;
            // 
            // txt1
            // 
            this.txt1.Location = new System.Drawing.Point(84, 89);
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(68, 21);
            this.txt1.TabIndex = 9;
            this.txt1.TextDataType = Sunlike.Windows.Forms.EDataType.DTDecimal;
            this.txt1.Visible = false;
            // 
            // txt2
            // 
            this.txt2.Location = new System.Drawing.Point(158, 89);
            this.txt2.Name = "txt2";
            this.txt2.Size = new System.Drawing.Size(68, 21);
            this.txt2.TabIndex = 11;
            this.txt2.TextDataType = Sunlike.Windows.Forms.EDataType.DTDecimal;
            this.txt2.Visible = false;
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(160, 61);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(36, 18);
            this.lbl2.TabIndex = 10;
            this.lbl2.Text = "变量2";
            this.lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl2.UseCompatibleTextRendering = true;
            this.lbl2.Visible = false;
            // 
            // txt3
            // 
            this.txt3.Location = new System.Drawing.Point(232, 89);
            this.txt3.Name = "txt3";
            this.txt3.Size = new System.Drawing.Size(68, 21);
            this.txt3.TabIndex = 13;
            this.txt3.TextDataType = Sunlike.Windows.Forms.EDataType.DTDecimal;
            this.txt3.Visible = false;
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Location = new System.Drawing.Point(234, 61);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(36, 18);
            this.lbl3.TabIndex = 12;
            this.lbl3.Text = "变量3";
            this.lbl3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl3.UseCompatibleTextRendering = true;
            this.lbl3.Visible = false;
            // 
            // txt4
            // 
            this.txt4.Location = new System.Drawing.Point(306, 89);
            this.txt4.Name = "txt4";
            this.txt4.Size = new System.Drawing.Size(68, 21);
            this.txt4.TabIndex = 15;
            this.txt4.TextDataType = Sunlike.Windows.Forms.EDataType.DTDecimal;
            this.txt4.Visible = false;
            // 
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.Location = new System.Drawing.Point(308, 61);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(36, 18);
            this.lbl4.TabIndex = 14;
            this.lbl4.Text = "变量4";
            this.lbl4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl4.UseCompatibleTextRendering = true;
            this.lbl4.Visible = false;
            // 
            // CheckQTY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 176);
            this.Name = "CheckQTY";
            this.Text = "CheckQTY";
            this.Load += new System.EventHandler(this.CheckQTY_Load);
            this.panButton.ResumeLayout(false);
            this.panMain.ResumeLayout(false);
            this.panMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CLabel cLabel1;
        private CButtonXP btnCancel;
        private CButtonXP btnOK;
        private CLabel cLabel2;
        private CTextBox txtFormula;
        private CLabel lbl1;
        private CLabel lbl0;
        private CTextBox txt1;
        private CTextBox txt0;
        private CTextBox txt4;
        private CLabel lbl4;
        private CTextBox txt3;
        private CLabel lbl3;
        private CTextBox txt2;
        private CLabel lbl2;
    }
}