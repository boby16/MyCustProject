namespace BarCodePrintTSB
{
    partial class FTMM
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
            this.txtBoxNo = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnMONo = new System.Windows.Forms.Button();
            this.txtMONo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrdNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtQtyFin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtYIQty = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtIdx = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSpc = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstBoxMM = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtBoxQty = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQtyLost = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtWhName = new System.Windows.Forms.TextBox();
            this.btnWH = new System.Windows.Forms.Button();
            this.txtWH = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUT = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBoxNo
            // 
            this.txtBoxNo.Location = new System.Drawing.Point(6, 17);
            this.txtBoxNo.Name = "txtBoxNo";
            this.txtBoxNo.Size = new System.Drawing.Size(178, 20);
            this.txtBoxNo.TabIndex = 2;
            this.txtBoxNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxNo_KeyDown);
            this.txtBoxNo.Leave += new System.EventHandler(this.txtBoxNo_Leave);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(110, 342);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "保存";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnMONo
            // 
            this.btnMONo.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btnMONo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMONo.Location = new System.Drawing.Point(205, 46);
            this.btnMONo.Name = "btnMONo";
            this.btnMONo.Size = new System.Drawing.Size(20, 22);
            this.btnMONo.TabIndex = 7;
            this.btnMONo.UseVisualStyleBackColor = true;
            this.btnMONo.Visible = false;
            this.btnMONo.Click += new System.EventHandler(this.btnMONo_Click);
            // 
            // txtMONo
            // 
            this.txtMONo.Location = new System.Drawing.Point(85, 46);
            this.txtMONo.Name = "txtMONo";
            this.txtMONo.ReadOnly = true;
            this.txtMONo.Size = new System.Drawing.Size(120, 20);
            this.txtMONo.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "制令单号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "成品品号";
            // 
            // txtPrdNo
            // 
            this.txtPrdNo.Location = new System.Drawing.Point(85, 72);
            this.txtPrdNo.Name = "txtPrdNo";
            this.txtPrdNo.ReadOnly = true;
            this.txtPrdNo.Size = new System.Drawing.Size(120, 20);
            this.txtPrdNo.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "已缴库数量";
            // 
            // txtQtyFin
            // 
            this.txtQtyFin.Location = new System.Drawing.Point(85, 176);
            this.txtQtyFin.Name = "txtQtyFin";
            this.txtQtyFin.ReadOnly = true;
            this.txtQtyFin.Size = new System.Drawing.Size(120, 20);
            this.txtQtyFin.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "应缴库数量";
            // 
            // txtYIQty
            // 
            this.txtYIQty.Location = new System.Drawing.Point(85, 150);
            this.txtYIQty.Name = "txtYIQty";
            this.txtYIQty.ReadOnly = true;
            this.txtYIQty.Size = new System.Drawing.Size(120, 20);
            this.txtYIQty.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "中类";
            // 
            // txtIdx
            // 
            this.txtIdx.Location = new System.Drawing.Point(85, 124);
            this.txtIdx.Name = "txtIdx";
            this.txtIdx.ReadOnly = true;
            this.txtIdx.Size = new System.Drawing.Size(120, 20);
            this.txtIdx.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(48, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "规格";
            // 
            // txtSpc
            // 
            this.txtSpc.Location = new System.Drawing.Point(85, 98);
            this.txtSpc.Name = "txtSpc";
            this.txtSpc.ReadOnly = true;
            this.txtSpc.Size = new System.Drawing.Size(120, 20);
            this.txtSpc.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 231);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "缴库数量";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(85, 228);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(120, 20);
            this.txtQty.TabIndex = 19;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtBoxNo);
            this.groupBox2.Controls.Add(this.lstBoxMM);
            this.groupBox2.Location = new System.Drawing.Point(12, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 502);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "箱条码扫描";
            // 
            // lstBoxMM
            // 
            this.lstBoxMM.FormattingEnabled = true;
            this.lstBoxMM.Location = new System.Drawing.Point(6, 49);
            this.lstBoxMM.Name = "lstBoxMM";
            this.lstBoxMM.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstBoxMM.Size = new System.Drawing.Size(178, 446);
            this.lstBoxMM.TabIndex = 5;
            this.lstBoxMM.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxMM_KeyDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtBoxQty);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtQtyLost);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.txtWhName);
            this.groupBox3.Controls.Add(this.btnWH);
            this.groupBox3.Controls.Add(this.txtWH);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtUT);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtMONo);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.btnOK);
            this.groupBox3.Controls.Add(this.txtQty);
            this.groupBox3.Controls.Add(this.btnMONo);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtSpc);
            this.groupBox3.Controls.Add(this.txtPrdNo);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtIdx);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtQtyFin);
            this.groupBox3.Controls.Add(this.txtYIQty);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(208, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(248, 502);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "成品缴库";
            // 
            // txtBoxQty
            // 
            this.txtBoxQty.Location = new System.Drawing.Point(85, 280);
            this.txtBoxQty.Name = "txtBoxQty";
            this.txtBoxQty.ReadOnly = true;
            this.txtBoxQty.Size = new System.Drawing.Size(120, 20);
            this.txtBoxQty.TabIndex = 64;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 284);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 63;
            this.label1.Text = "缴库箱数";
            // 
            // txtQtyLost
            // 
            this.txtQtyLost.Location = new System.Drawing.Point(85, 202);
            this.txtQtyLost.Name = "txtQtyLost";
            this.txtQtyLost.ReadOnly = true;
            this.txtQtyLost.Size = new System.Drawing.Size(120, 20);
            this.txtQtyLost.TabIndex = 57;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(14, 205);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 13);
            this.label14.TabIndex = 56;
            this.label14.Text = "不合格数量";
            // 
            // txtWhName
            // 
            this.txtWhName.Location = new System.Drawing.Point(85, 306);
            this.txtWhName.Name = "txtWhName";
            this.txtWhName.Size = new System.Drawing.Size(120, 20);
            this.txtWhName.TabIndex = 47;
            this.txtWhName.Leave += new System.EventHandler(this.txtWhName_Leave);
            this.txtWhName.Enter += new System.EventHandler(this.txtWhName_Enter);
            // 
            // btnWH
            // 
            this.btnWH.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btnWH.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnWH.Location = new System.Drawing.Point(205, 306);
            this.btnWH.Name = "btnWH";
            this.btnWH.Size = new System.Drawing.Size(20, 22);
            this.btnWH.TabIndex = 42;
            this.btnWH.UseVisualStyleBackColor = true;
            this.btnWH.Click += new System.EventHandler(this.btnWH_Click);
            // 
            // txtWH
            // 
            this.txtWH.Location = new System.Drawing.Point(15, 306);
            this.txtWH.Name = "txtWH";
            this.txtWH.Size = new System.Drawing.Size(16, 20);
            this.txtWH.TabIndex = 41;
            this.txtWH.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(48, 309);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 40;
            this.label9.Text = "库位";
            // 
            // txtUT
            // 
            this.txtUT.Location = new System.Drawing.Point(85, 254);
            this.txtUT.Name = "txtUT";
            this.txtUT.ReadOnly = true;
            this.txtUT.Size = new System.Drawing.Size(120, 20);
            this.txtUT.TabIndex = 37;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(48, 257);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 36;
            this.label11.Text = "单位";
            // 
            // FTMM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 531);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "FTMM";
            this.Text = "成品缴库";
            this.Load += new System.EventHandler(this.FTMM_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxNo;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnMONo;
        private System.Windows.Forms.TextBox txtMONo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrdNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtQtyFin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtYIQty;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtIdx;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSpc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lstBoxMM;
        private System.Windows.Forms.TextBox txtUT;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtWH;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnWH;
        private System.Windows.Forms.TextBox txtWhName;
        private System.Windows.Forms.TextBox txtQtyLost;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtBoxQty;
        private System.Windows.Forms.Label label1;

    }
}