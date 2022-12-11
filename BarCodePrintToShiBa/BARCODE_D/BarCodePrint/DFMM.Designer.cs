namespace BarCodePrintTSB
{
    partial class DFMM
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
            this.txtMONo = new System.Windows.Forms.TextBox();
            this.txtSalNo = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.txtBarNo = new System.Windows.Forms.TextBox();
            this.lstMM = new System.Windows.Forms.ListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.cbMoNull = new System.Windows.Forms.CheckBox();
            this.txtQtyLost = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtWhName = new System.Windows.Forms.TextBox();
            this.btnWH = new System.Windows.Forms.Button();
            this.txtWH = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtUT = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSpc = new System.Windows.Forms.TextBox();
            this.txtPrdNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIdx = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtQtyFin = new System.Windows.Forms.TextBox();
            this.txtYIQty = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnMONo = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "制令单号";
            // 
            // txtMONo
            // 
            this.txtMONo.Location = new System.Drawing.Point(83, 29);
            this.txtMONo.Name = "txtMONo";
            this.txtMONo.ReadOnly = true;
            this.txtMONo.Size = new System.Drawing.Size(120, 20);
            this.txtMONo.TabIndex = 1;
            // 
            // txtSalNo
            // 
            this.txtSalNo.Location = new System.Drawing.Point(21, 312);
            this.txtSalNo.Name = "txtSalNo";
            this.txtSalNo.Size = new System.Drawing.Size(30, 20);
            this.txtSalNo.TabIndex = 3;
            this.txtSalNo.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtBarNo);
            this.panel1.Controls.Add(this.lstMM);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(241, 527);
            this.panel1.TabIndex = 4;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(115, 13);
            this.label13.TabIndex = 60;
            this.label13.Text = "待缴库半成品序列号";
            // 
            // txtBarNo
            // 
            this.txtBarNo.Location = new System.Drawing.Point(12, 33);
            this.txtBarNo.Name = "txtBarNo";
            this.txtBarNo.Size = new System.Drawing.Size(209, 20);
            this.txtBarNo.TabIndex = 59;
            this.txtBarNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarNo_KeyDown);
            this.txtBarNo.Leave += new System.EventHandler(this.txtBarNo_Leave);
            // 
            // lstMM
            // 
            this.lstMM.FormattingEnabled = true;
            this.lstMM.Location = new System.Drawing.Point(12, 65);
            this.lstMM.Name = "lstMM";
            this.lstMM.Size = new System.Drawing.Size(209, 446);
            this.lstMM.TabIndex = 58;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(161, 336);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(67, 21);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(88, 336);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(67, 21);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "转缴库单";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cbMoNull
            // 
            this.cbMoNull.AutoSize = true;
            this.cbMoNull.Location = new System.Drawing.Point(83, 289);
            this.cbMoNull.Name = "cbMoNull";
            this.cbMoNull.Size = new System.Drawing.Size(110, 17);
            this.cbMoNull.TabIndex = 56;
            this.cbMoNull.Text = "制令单允许为空";
            this.cbMoNull.UseVisualStyleBackColor = true;
            // 
            // txtQtyLost
            // 
            this.txtQtyLost.Location = new System.Drawing.Point(83, 185);
            this.txtQtyLost.Name = "txtQtyLost";
            this.txtQtyLost.ReadOnly = true;
            this.txtQtyLost.Size = new System.Drawing.Size(120, 20);
            this.txtQtyLost.TabIndex = 55;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 189);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 13);
            this.label14.TabIndex = 54;
            this.label14.Text = "不合格数量";
            // 
            // txtWhName
            // 
            this.txtWhName.Location = new System.Drawing.Point(83, 263);
            this.txtWhName.Name = "txtWhName";
            this.txtWhName.Size = new System.Drawing.Size(120, 20);
            this.txtWhName.TabIndex = 46;
            this.txtWhName.Leave += new System.EventHandler(this.txtWhName_Leave);
            this.txtWhName.Enter += new System.EventHandler(this.txtWhName_Enter);
            // 
            // btnWH
            // 
            this.btnWH.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btnWH.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnWH.Location = new System.Drawing.Point(203, 263);
            this.btnWH.Name = "btnWH";
            this.btnWH.Size = new System.Drawing.Size(20, 20);
            this.btnWH.TabIndex = 45;
            this.btnWH.UseVisualStyleBackColor = true;
            this.btnWH.Click += new System.EventHandler(this.btnWH_Click);
            // 
            // txtWH
            // 
            this.txtWH.Location = new System.Drawing.Point(21, 263);
            this.txtWH.Name = "txtWH";
            this.txtWH.Size = new System.Drawing.Size(13, 20);
            this.txtWH.TabIndex = 44;
            this.txtWH.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(48, 267);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 13);
            this.label12.TabIndex = 43;
            this.label12.Text = "库位";
            // 
            // txtUT
            // 
            this.txtUT.Location = new System.Drawing.Point(83, 237);
            this.txtUT.Name = "txtUT";
            this.txtUT.ReadOnly = true;
            this.txtUT.Size = new System.Drawing.Size(120, 20);
            this.txtUT.TabIndex = 35;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(48, 241);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 34;
            this.label11.Text = "单位";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(83, 211);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(120, 20);
            this.txtQty.TabIndex = 31;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 215);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "缴库数量";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "品号";
            // 
            // txtSpc
            // 
            this.txtSpc.Location = new System.Drawing.Point(83, 81);
            this.txtSpc.Name = "txtSpc";
            this.txtSpc.ReadOnly = true;
            this.txtSpc.Size = new System.Drawing.Size(120, 20);
            this.txtSpc.TabIndex = 29;
            // 
            // txtPrdNo
            // 
            this.txtPrdNo.Location = new System.Drawing.Point(83, 55);
            this.txtPrdNo.Name = "txtPrdNo";
            this.txtPrdNo.ReadOnly = true;
            this.txtPrdNo.Size = new System.Drawing.Size(120, 20);
            this.txtPrdNo.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(48, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "规格";
            // 
            // txtIdx
            // 
            this.txtIdx.Location = new System.Drawing.Point(83, 107);
            this.txtIdx.Name = "txtIdx";
            this.txtIdx.ReadOnly = true;
            this.txtIdx.Size = new System.Drawing.Size(120, 20);
            this.txtIdx.TabIndex = 27;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "已缴库数量";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "中类";
            // 
            // txtQtyFin
            // 
            this.txtQtyFin.Location = new System.Drawing.Point(83, 159);
            this.txtQtyFin.Name = "txtQtyFin";
            this.txtQtyFin.ReadOnly = true;
            this.txtQtyFin.Size = new System.Drawing.Size(120, 20);
            this.txtQtyFin.TabIndex = 23;
            // 
            // txtYIQty
            // 
            this.txtYIQty.Location = new System.Drawing.Point(83, 133);
            this.txtYIQty.Name = "txtYIQty";
            this.txtYIQty.ReadOnly = true;
            this.txtYIQty.Size = new System.Drawing.Size(120, 20);
            this.txtYIQty.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 137);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "应缴库数量";
            // 
            // btnMONo
            // 
            this.btnMONo.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btnMONo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMONo.Location = new System.Drawing.Point(203, 28);
            this.btnMONo.Name = "btnMONo";
            this.btnMONo.Size = new System.Drawing.Size(20, 20);
            this.btnMONo.TabIndex = 4;
            this.btnMONo.UseVisualStyleBackColor = true;
            this.btnMONo.Visible = false;
            this.btnMONo.Click += new System.EventHandler(this.btnMONo_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbMoNull);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Controls.Add(this.txtQtyLost);
            this.panel2.Controls.Add(this.txtSalNo);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.txtWhName);
            this.panel2.Controls.Add(this.btnWH);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtWH);
            this.panel2.Controls.Add(this.txtMONo);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.btnMONo);
            this.panel2.Controls.Add(this.txtUT);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.txtYIQty);
            this.panel2.Controls.Add(this.txtQty);
            this.panel2.Controls.Add(this.txtQtyFin);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtSpc);
            this.panel2.Controls.Add(this.txtIdx);
            this.panel2.Controls.Add(this.txtPrdNo);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(241, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(242, 527);
            this.panel2.TabIndex = 6;
            // 
            // DFMM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 527);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DFMM";
            this.Text = "半成品缴库";
            this.Load += new System.EventHandler(this.DFMM_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMONo;
        private System.Windows.Forms.TextBox txtSalNo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnMONo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSpc;
        private System.Windows.Forms.TextBox txtPrdNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtIdx;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtQtyFin;
        private System.Windows.Forms.TextBox txtYIQty;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtUT;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnWH;
        private System.Windows.Forms.TextBox txtWH;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtWhName;
        private System.Windows.Forms.TextBox txtQtyLost;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox cbMoNull;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtBarNo;
        private System.Windows.Forms.ListBox lstMM;
    }
}