namespace BarCodePrintTSB
{
    partial class BCToMM
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstUnToMM = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lstUnToMMR = new System.Windows.Forms.ListBox();
            this.btnRefresh = new System.Windows.Forms.Button();
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbMoNull = new System.Windows.Forms.CheckBox();
            this.txtQtyLost = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtWhName = new System.Windows.Forms.TextBox();
            this.btnWH = new System.Windows.Forms.Button();
            this.txtWH = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUT = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lstToMM = new System.Windows.Forms.ListBox();
            this.btnUnmm = new System.Windows.Forms.Button();
            this.btnTomm = new System.Windows.Forms.Button();
            this.btnUnmmR = new System.Windows.Forms.Button();
            this.btnTommR = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 546);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "待缴库板材条码";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstUnToMM);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(3, 45);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(194, 249);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "合格品";
            // 
            // lstUnToMM
            // 
            this.lstUnToMM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUnToMM.FormattingEnabled = true;
            this.lstUnToMM.Location = new System.Drawing.Point(3, 16);
            this.lstUnToMM.Name = "lstUnToMM";
            this.lstUnToMM.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstUnToMM.Size = new System.Drawing.Size(188, 225);
            this.lstUnToMM.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lstUnToMMR);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Location = new System.Drawing.Point(3, 294);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(194, 249);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "不合格强制缴库";
            // 
            // lstUnToMMR
            // 
            this.lstUnToMMR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUnToMMR.FormattingEnabled = true;
            this.lstUnToMMR.Location = new System.Drawing.Point(3, 16);
            this.lstUnToMMR.Name = "lstUnToMMR";
            this.lstUnToMMR.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstUnToMMR.Size = new System.Drawing.Size(188, 225);
            this.lstUnToMMR.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(128, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(60, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(323, 369);
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
            this.btnMONo.Location = new System.Drawing.Point(410, 47);
            this.btnMONo.Name = "btnMONo";
            this.btnMONo.Size = new System.Drawing.Size(20, 22);
            this.btnMONo.TabIndex = 7;
            this.btnMONo.UseVisualStyleBackColor = true;
            this.btnMONo.Click += new System.EventHandler(this.btnMONo_Click);
            // 
            // txtMONo
            // 
            this.txtMONo.Location = new System.Drawing.Point(290, 47);
            this.txtMONo.Name = "txtMONo";
            this.txtMONo.Size = new System.Drawing.Size(120, 20);
            this.txtMONo.TabIndex = 6;
            this.txtMONo.Leave += new System.EventHandler(this.txtMONo_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(229, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "制令单号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(229, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "成品品号";
            // 
            // txtPrdNo
            // 
            this.txtPrdNo.Location = new System.Drawing.Point(290, 73);
            this.txtPrdNo.Name = "txtPrdNo";
            this.txtPrdNo.ReadOnly = true;
            this.txtPrdNo.Size = new System.Drawing.Size(120, 20);
            this.txtPrdNo.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(217, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "已缴库数量";
            // 
            // txtQtyFin
            // 
            this.txtQtyFin.Location = new System.Drawing.Point(290, 177);
            this.txtQtyFin.Name = "txtQtyFin";
            this.txtQtyFin.ReadOnly = true;
            this.txtQtyFin.Size = new System.Drawing.Size(120, 20);
            this.txtQtyFin.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(217, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "应缴库数量";
            // 
            // txtYIQty
            // 
            this.txtYIQty.Location = new System.Drawing.Point(290, 151);
            this.txtYIQty.Name = "txtYIQty";
            this.txtYIQty.ReadOnly = true;
            this.txtYIQty.Size = new System.Drawing.Size(120, 20);
            this.txtYIQty.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(253, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "中类";
            // 
            // txtIdx
            // 
            this.txtIdx.Location = new System.Drawing.Point(290, 125);
            this.txtIdx.Name = "txtIdx";
            this.txtIdx.ReadOnly = true;
            this.txtIdx.Size = new System.Drawing.Size(120, 20);
            this.txtIdx.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(253, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "规格";
            // 
            // txtSpc
            // 
            this.txtSpc.Location = new System.Drawing.Point(290, 99);
            this.txtSpc.Name = "txtSpc";
            this.txtSpc.ReadOnly = true;
            this.txtSpc.Size = new System.Drawing.Size(120, 20);
            this.txtSpc.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(229, 232);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "缴库数量";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(290, 229);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(120, 20);
            this.txtQty.TabIndex = 19;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbMoNull);
            this.groupBox3.Controls.Add(this.txtQtyLost);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.txtWhName);
            this.groupBox3.Controls.Add(this.btnWH);
            this.groupBox3.Controls.Add(this.txtWH);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtUT);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.lstToMM);
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
            this.groupBox3.Location = new System.Drawing.Point(241, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(444, 546);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "板材送缴";
            // 
            // cbMoNull
            // 
            this.cbMoNull.AutoSize = true;
            this.cbMoNull.Location = new System.Drawing.Point(290, 310);
            this.cbMoNull.Name = "cbMoNull";
            this.cbMoNull.Size = new System.Drawing.Size(110, 17);
            this.cbMoNull.TabIndex = 58;
            this.cbMoNull.Text = "制令单允许为空";
            this.cbMoNull.UseVisualStyleBackColor = true;
            this.cbMoNull.Visible = false;
            // 
            // txtQtyLost
            // 
            this.txtQtyLost.Location = new System.Drawing.Point(290, 203);
            this.txtQtyLost.Name = "txtQtyLost";
            this.txtQtyLost.ReadOnly = true;
            this.txtQtyLost.Size = new System.Drawing.Size(120, 20);
            this.txtQtyLost.TabIndex = 57;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(219, 206);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 13);
            this.label14.TabIndex = 56;
            this.label14.Text = "不合格数量";
            // 
            // txtWhName
            // 
            this.txtWhName.Location = new System.Drawing.Point(290, 281);
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
            this.btnWH.Location = new System.Drawing.Point(410, 281);
            this.btnWH.Name = "btnWH";
            this.btnWH.Size = new System.Drawing.Size(20, 22);
            this.btnWH.TabIndex = 42;
            this.btnWH.Text = "...";
            this.btnWH.UseVisualStyleBackColor = true;
            this.btnWH.Click += new System.EventHandler(this.btnWH_Click);
            // 
            // txtWH
            // 
            this.txtWH.Location = new System.Drawing.Point(220, 281);
            this.txtWH.Name = "txtWH";
            this.txtWH.Size = new System.Drawing.Size(16, 20);
            this.txtWH.TabIndex = 41;
            this.txtWH.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(253, 284);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 40;
            this.label9.Text = "库位";
            // 
            // txtUT
            // 
            this.txtUT.Location = new System.Drawing.Point(290, 255);
            this.txtUT.Name = "txtUT";
            this.txtUT.ReadOnly = true;
            this.txtUT.Size = new System.Drawing.Size(120, 20);
            this.txtUT.TabIndex = 37;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(253, 258);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 36;
            this.label11.Text = "单位";
            // 
            // lstToMM
            // 
            this.lstToMM.FormattingEnabled = true;
            this.lstToMM.Location = new System.Drawing.Point(6, 28);
            this.lstToMM.Name = "lstToMM";
            this.lstToMM.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstToMM.Size = new System.Drawing.Size(180, 511);
            this.lstToMM.TabIndex = 5;
            // 
            // btnUnmm
            // 
            this.btnUnmm.Location = new System.Drawing.Point(207, 198);
            this.btnUnmm.Name = "btnUnmm";
            this.btnUnmm.Size = new System.Drawing.Size(30, 23);
            this.btnUnmm.TabIndex = 23;
            this.btnUnmm.Text = "<<";
            this.btnUnmm.UseVisualStyleBackColor = true;
            this.btnUnmm.Click += new System.EventHandler(this.btnUnmm_Click);
            // 
            // btnTomm
            // 
            this.btnTomm.Location = new System.Drawing.Point(207, 127);
            this.btnTomm.Name = "btnTomm";
            this.btnTomm.Size = new System.Drawing.Size(30, 23);
            this.btnTomm.TabIndex = 22;
            this.btnTomm.Text = ">>";
            this.btnTomm.UseVisualStyleBackColor = true;
            this.btnTomm.Click += new System.EventHandler(this.btnTomm_Click);
            // 
            // btnUnmmR
            // 
            this.btnUnmmR.Location = new System.Drawing.Point(207, 456);
            this.btnUnmmR.Name = "btnUnmmR";
            this.btnUnmmR.Size = new System.Drawing.Size(30, 23);
            this.btnUnmmR.TabIndex = 25;
            this.btnUnmmR.Text = "<<";
            this.btnUnmmR.UseVisualStyleBackColor = true;
            this.btnUnmmR.Click += new System.EventHandler(this.btnUnmmR_Click);
            // 
            // btnTommR
            // 
            this.btnTommR.Location = new System.Drawing.Point(207, 385);
            this.btnTommR.Name = "btnTommR";
            this.btnTommR.Size = new System.Drawing.Size(30, 23);
            this.btnTommR.TabIndex = 24;
            this.btnTommR.Text = ">>";
            this.btnTommR.UseVisualStyleBackColor = true;
            this.btnTommR.Click += new System.EventHandler(this.btnTommR_Click);
            // 
            // BCToMM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 548);
            this.Controls.Add(this.btnUnmmR);
            this.Controls.Add(this.btnTommR);
            this.Controls.Add(this.btnUnmm);
            this.Controls.Add(this.btnTomm);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "BCToMM";
            this.Text = "板材送缴";
            this.Load += new System.EventHandler(this.BCToMM_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstUnToMM;
        private System.Windows.Forms.Button btnRefresh;
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lstToMM;
        private System.Windows.Forms.Button btnUnmm;
        private System.Windows.Forms.Button btnTomm;
        private System.Windows.Forms.TextBox txtUT;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtWH;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtWhName;
        private System.Windows.Forms.TextBox txtQtyLost;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox cbMoNull;
        private System.Windows.Forms.Button btnWH;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox lstUnToMMR;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnUnmmR;
        private System.Windows.Forms.Button btnTommR;

    }
}