namespace BarCodePrintTSB
{
    partial class BCInitMM
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
            this.txtSalNo = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUnmm = new System.Windows.Forms.Button();
            this.btnTomm = new System.Windows.Forms.Button();
            this.lstMM = new System.Windows.Forms.ListBox();
            this.lstUnMM = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtWhName = new System.Windows.Forms.TextBox();
            this.btnWH = new System.Windows.Forms.Button();
            this.txtWH = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSalNo
            // 
            this.txtSalNo.Location = new System.Drawing.Point(37, 87);
            this.txtSalNo.Name = "txtSalNo";
            this.txtSalNo.Size = new System.Drawing.Size(30, 20);
            this.txtSalNo.TabIndex = 3;
            this.txtSalNo.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnUnmm);
            this.panel1.Controls.Add(this.btnTomm);
            this.panel1.Controls.Add(this.lstMM);
            this.panel1.Controls.Add(this.lstUnMM);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(475, 527);
            this.panel1.TabIndex = 4;
            // 
            // btnUnmm
            // 
            this.btnUnmm.Location = new System.Drawing.Point(224, 280);
            this.btnUnmm.Name = "btnUnmm";
            this.btnUnmm.Size = new System.Drawing.Size(30, 23);
            this.btnUnmm.TabIndex = 61;
            this.btnUnmm.Text = "<<";
            this.btnUnmm.UseVisualStyleBackColor = true;
            this.btnUnmm.Click += new System.EventHandler(this.btnUnmm_Click);
            // 
            // btnTomm
            // 
            this.btnTomm.Location = new System.Drawing.Point(224, 226);
            this.btnTomm.Name = "btnTomm";
            this.btnTomm.Size = new System.Drawing.Size(30, 23);
            this.btnTomm.TabIndex = 60;
            this.btnTomm.Text = ">>";
            this.btnTomm.UseVisualStyleBackColor = true;
            this.btnTomm.Click += new System.EventHandler(this.btnTomm_Click);
            // 
            // lstMM
            // 
            this.lstMM.FormattingEnabled = true;
            this.lstMM.Location = new System.Drawing.Point(257, 13);
            this.lstMM.Name = "lstMM";
            this.lstMM.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstMM.Size = new System.Drawing.Size(209, 498);
            this.lstMM.TabIndex = 59;
            // 
            // lstUnMM
            // 
            this.lstUnMM.FormattingEnabled = true;
            this.lstUnMM.Location = new System.Drawing.Point(12, 13);
            this.lstUnMM.Name = "lstUnMM";
            this.lstUnMM.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstUnMM.Size = new System.Drawing.Size(209, 498);
            this.lstUnMM.TabIndex = 58;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(124, 87);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(67, 21);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "×ª½É¿âµ¥";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtWhName
            // 
            this.txtWhName.Location = new System.Drawing.Point(51, 46);
            this.txtWhName.Name = "txtWhName";
            this.txtWhName.Size = new System.Drawing.Size(120, 20);
            this.txtWhName.TabIndex = 46;
            this.txtWhName.Enter += new System.EventHandler(this.txtWhName_Enter);
            this.txtWhName.Leave += new System.EventHandler(this.txtWhName_Leave);
            // 
            // btnWH
            // 
            this.btnWH.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btnWH.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnWH.Location = new System.Drawing.Point(171, 45);
            this.btnWH.Name = "btnWH";
            this.btnWH.Size = new System.Drawing.Size(20, 20);
            this.btnWH.TabIndex = 45;
            this.btnWH.UseVisualStyleBackColor = true;
            this.btnWH.Click += new System.EventHandler(this.btnWH_Click);
            // 
            // txtWH
            // 
            this.txtWH.Location = new System.Drawing.Point(18, 87);
            this.txtWH.Name = "txtWH";
            this.txtWH.Size = new System.Drawing.Size(13, 20);
            this.txtWH.TabIndex = 44;
            this.txtWH.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 49);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 13);
            this.label12.TabIndex = 43;
            this.label12.Text = "¿âÎ»";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Controls.Add(this.txtSalNo);
            this.panel2.Controls.Add(this.txtWhName);
            this.panel2.Controls.Add(this.btnWH);
            this.panel2.Controls.Add(this.txtWH);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(475, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(218, 527);
            this.panel2.TabIndex = 6;
            // 
            // BCInitMM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 527);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "BCInitMM";
            this.Text = "°å²Ä/°ë³ÉÆ·½É¿â";
            this.Load += new System.EventHandler(this.BCInitMM_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtSalNo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnWH;
        private System.Windows.Forms.TextBox txtWH;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtWhName;
        private System.Windows.Forms.ListBox lstUnMM;
        private System.Windows.Forms.ListBox lstMM;
        private System.Windows.Forms.Button btnUnmm;
        private System.Windows.Forms.Button btnTomm;
    }
}