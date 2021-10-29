namespace BarCodePrintTSB
{
    partial class FTToMM
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
            this.dgBoxUnToMM = new System.Windows.Forms.DataGridView();
            this.btnOK = new System.Windows.Forms.Button();
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
            this.dgBoxToMM = new System.Windows.Forms.DataGridView();
            this.cbMoNull = new System.Windows.Forms.CheckBox();
            this.txtQtyLost = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtWhName = new System.Windows.Forms.TextBox();
            this.btnWH = new System.Windows.Forms.Button();
            this.txtWH = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUT = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnMONo = new System.Windows.Forms.Button();
            this.btnUnmm = new System.Windows.Forms.Button();
            this.btnTomm = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnUnmmR = new System.Windows.Forms.Button();
            this.btnTommR = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgBoxUnToMMR = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBoxQty = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgBoxUnToMM)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBoxToMM)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBoxUnToMMR)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgBoxUnToMM
            // 
            this.dgBoxUnToMM.AllowUserToAddRows = false;
            this.dgBoxUnToMM.AllowUserToDeleteRows = false;
            this.dgBoxUnToMM.AllowUserToResizeRows = false;
            this.dgBoxUnToMM.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgBoxUnToMM.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgBoxUnToMM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBoxUnToMM.Location = new System.Drawing.Point(3, 19);
            this.dgBoxUnToMM.Name = "dgBoxUnToMM";
            this.dgBoxUnToMM.ReadOnly = true;
            this.dgBoxUnToMM.RowHeadersVisible = false;
            this.dgBoxUnToMM.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgBoxUnToMM.RowTemplate.Height = 23;
            this.dgBoxUnToMM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBoxUnToMM.Size = new System.Drawing.Size(300, 308);
            this.dgBoxUnToMM.TabIndex = 59;
            this.dgBoxUnToMM.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgBoxUnToMM_DataBindingComplete);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(415, 339);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "保存";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtMONo
            // 
            this.txtMONo.Location = new System.Drawing.Point(390, 42);
            this.txtMONo.Name = "txtMONo";
            this.txtMONo.Size = new System.Drawing.Size(120, 20);
            this.txtMONo.TabIndex = 6;
            this.txtMONo.Leave += new System.EventHandler(this.txtMONo_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(329, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "制令单号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(329, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "成品品号";
            // 
            // txtPrdNo
            // 
            this.txtPrdNo.Location = new System.Drawing.Point(390, 68);
            this.txtPrdNo.Name = "txtPrdNo";
            this.txtPrdNo.ReadOnly = true;
            this.txtPrdNo.Size = new System.Drawing.Size(120, 20);
            this.txtPrdNo.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(317, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "已缴库数量";
            // 
            // txtQtyFin
            // 
            this.txtQtyFin.Location = new System.Drawing.Point(390, 172);
            this.txtQtyFin.Name = "txtQtyFin";
            this.txtQtyFin.ReadOnly = true;
            this.txtQtyFin.Size = new System.Drawing.Size(120, 20);
            this.txtQtyFin.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(317, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "应缴库数量";
            // 
            // txtYIQty
            // 
            this.txtYIQty.Location = new System.Drawing.Point(390, 146);
            this.txtYIQty.Name = "txtYIQty";
            this.txtYIQty.ReadOnly = true;
            this.txtYIQty.Size = new System.Drawing.Size(120, 20);
            this.txtYIQty.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(353, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "中类";
            // 
            // txtIdx
            // 
            this.txtIdx.Location = new System.Drawing.Point(390, 120);
            this.txtIdx.Name = "txtIdx";
            this.txtIdx.ReadOnly = true;
            this.txtIdx.Size = new System.Drawing.Size(120, 20);
            this.txtIdx.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(353, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "规格";
            // 
            // txtSpc
            // 
            this.txtSpc.Location = new System.Drawing.Point(390, 94);
            this.txtSpc.Name = "txtSpc";
            this.txtSpc.ReadOnly = true;
            this.txtSpc.Size = new System.Drawing.Size(120, 20);
            this.txtSpc.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(329, 228);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "缴库数量";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(390, 224);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(120, 20);
            this.txtQty.TabIndex = 19;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtBoxQty);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.dgBoxToMM);
            this.groupBox3.Controls.Add(this.cbMoNull);
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
            this.groupBox3.Location = new System.Drawing.Point(340, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(540, 531);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "成品送缴";
            // 
            // dgBoxToMM
            // 
            this.dgBoxToMM.AllowUserToAddRows = false;
            this.dgBoxToMM.AllowUserToDeleteRows = false;
            this.dgBoxToMM.AllowUserToResizeRows = false;
            this.dgBoxToMM.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgBoxToMM.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgBoxToMM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBoxToMM.Location = new System.Drawing.Point(6, 42);
            this.dgBoxToMM.Name = "dgBoxToMM";
            this.dgBoxToMM.ReadOnly = true;
            this.dgBoxToMM.RowHeadersVisible = false;
            this.dgBoxToMM.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgBoxToMM.RowTemplate.Height = 23;
            this.dgBoxToMM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBoxToMM.Size = new System.Drawing.Size(300, 483);
            this.dgBoxToMM.TabIndex = 60;
            this.dgBoxToMM.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgBoxToMM_DataBindingComplete);
            // 
            // cbMoNull
            // 
            this.cbMoNull.AutoSize = true;
            this.cbMoNull.Location = new System.Drawing.Point(390, 378);
            this.cbMoNull.Name = "cbMoNull";
            this.cbMoNull.Size = new System.Drawing.Size(110, 17);
            this.cbMoNull.TabIndex = 58;
            this.cbMoNull.Text = "制令单允许为空";
            this.cbMoNull.UseVisualStyleBackColor = true;
            this.cbMoNull.Visible = false;
            // 
            // txtQtyLost
            // 
            this.txtQtyLost.Location = new System.Drawing.Point(390, 198);
            this.txtQtyLost.Name = "txtQtyLost";
            this.txtQtyLost.ReadOnly = true;
            this.txtQtyLost.Size = new System.Drawing.Size(120, 20);
            this.txtQtyLost.TabIndex = 57;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(319, 202);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 13);
            this.label14.TabIndex = 56;
            this.label14.Text = "不合格数量";
            // 
            // txtWhName
            // 
            this.txtWhName.Location = new System.Drawing.Point(390, 302);
            this.txtWhName.Name = "txtWhName";
            this.txtWhName.Size = new System.Drawing.Size(120, 20);
            this.txtWhName.TabIndex = 47;
            this.txtWhName.Enter += new System.EventHandler(this.txtWhName_Enter);
            this.txtWhName.Leave += new System.EventHandler(this.txtWhName_Leave);
            // 
            // btnWH
            // 
            this.btnWH.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btnWH.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnWH.Location = new System.Drawing.Point(510, 302);
            this.btnWH.Name = "btnWH";
            this.btnWH.Size = new System.Drawing.Size(20, 20);
            this.btnWH.TabIndex = 42;
            this.btnWH.UseVisualStyleBackColor = true;
            this.btnWH.Click += new System.EventHandler(this.btnWH_Click);
            // 
            // txtWH
            // 
            this.txtWH.Location = new System.Drawing.Point(331, 303);
            this.txtWH.Name = "txtWH";
            this.txtWH.Size = new System.Drawing.Size(16, 20);
            this.txtWH.TabIndex = 41;
            this.txtWH.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(353, 306);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 40;
            this.label9.Text = "库位";
            // 
            // txtUT
            // 
            this.txtUT.Location = new System.Drawing.Point(390, 250);
            this.txtUT.Name = "txtUT";
            this.txtUT.ReadOnly = true;
            this.txtUT.Size = new System.Drawing.Size(120, 20);
            this.txtUT.TabIndex = 37;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(353, 254);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 36;
            this.label11.Text = "单位";
            // 
            // btnMONo
            // 
            this.btnMONo.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btnMONo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMONo.Location = new System.Drawing.Point(510, 42);
            this.btnMONo.Name = "btnMONo";
            this.btnMONo.Size = new System.Drawing.Size(20, 20);
            this.btnMONo.TabIndex = 7;
            this.btnMONo.UseVisualStyleBackColor = true;
            this.btnMONo.Click += new System.EventHandler(this.btnMONo_Click);
            // 
            // btnUnmm
            // 
            this.btnUnmm.Location = new System.Drawing.Point(310, 283);
            this.btnUnmm.Name = "btnUnmm";
            this.btnUnmm.Size = new System.Drawing.Size(30, 23);
            this.btnUnmm.TabIndex = 23;
            this.btnUnmm.Text = "<<";
            this.btnUnmm.UseVisualStyleBackColor = true;
            this.btnUnmm.Click += new System.EventHandler(this.btnUnmm_Click);
            // 
            // btnTomm
            // 
            this.btnTomm.Location = new System.Drawing.Point(310, 230);
            this.btnTomm.Name = "btnTomm";
            this.btnTomm.Size = new System.Drawing.Size(30, 23);
            this.btnTomm.TabIndex = 22;
            this.btnTomm.Text = ">>";
            this.btnTomm.UseVisualStyleBackColor = true;
            this.btnTomm.Click += new System.EventHandler(this.btnTomm_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgBoxUnToMM);
            this.groupBox2.Location = new System.Drawing.Point(3, 38);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(309, 333);
            this.groupBox2.TabIndex = 60;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "合格品";
            // 
            // btnUnmmR
            // 
            this.btnUnmmR.Location = new System.Drawing.Point(310, 478);
            this.btnUnmmR.Name = "btnUnmmR";
            this.btnUnmmR.Size = new System.Drawing.Size(30, 23);
            this.btnUnmmR.TabIndex = 62;
            this.btnUnmmR.Text = "<<";
            this.btnUnmmR.UseVisualStyleBackColor = true;
            this.btnUnmmR.Click += new System.EventHandler(this.btnUnmmR_Click);
            // 
            // btnTommR
            // 
            this.btnTommR.Location = new System.Drawing.Point(310, 425);
            this.btnTommR.Name = "btnTommR";
            this.btnTommR.Size = new System.Drawing.Size(30, 23);
            this.btnTommR.TabIndex = 61;
            this.btnTommR.Text = ">>";
            this.btnTommR.UseVisualStyleBackColor = true;
            this.btnTommR.Click += new System.EventHandler(this.btnTommR_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(202, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 20);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgBoxUnToMMR);
            this.groupBox4.Location = new System.Drawing.Point(0, 372);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(309, 160);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "不合格强制缴库";
            // 
            // dgBoxUnToMMR
            // 
            this.dgBoxUnToMMR.AllowUserToAddRows = false;
            this.dgBoxUnToMMR.AllowUserToDeleteRows = false;
            this.dgBoxUnToMMR.AllowUserToResizeRows = false;
            this.dgBoxUnToMMR.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgBoxUnToMMR.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgBoxUnToMMR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBoxUnToMMR.Location = new System.Drawing.Point(3, 19);
            this.dgBoxUnToMMR.Name = "dgBoxUnToMMR";
            this.dgBoxUnToMMR.ReadOnly = true;
            this.dgBoxUnToMMR.RowHeadersVisible = false;
            this.dgBoxUnToMMR.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgBoxUnToMMR.RowTemplate.Height = 23;
            this.dgBoxUnToMMR.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBoxUnToMMR.Size = new System.Drawing.Size(300, 137);
            this.dgBoxUnToMMR.TabIndex = 60;
            this.dgBoxUnToMMR.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgBoxUnToMMR_DataBindingComplete);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 531);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "待送缴箱条码";
            // 
            // txtBoxQty
            // 
            this.txtBoxQty.Location = new System.Drawing.Point(390, 276);
            this.txtBoxQty.Name = "txtBoxQty";
            this.txtBoxQty.ReadOnly = true;
            this.txtBoxQty.Size = new System.Drawing.Size(120, 20);
            this.txtBoxQty.TabIndex = 62;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(329, 280);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 61;
            this.label1.Text = "缴库箱数";
            // 
            // FTToMM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 546);
            this.Controls.Add(this.btnUnmmR);
            this.Controls.Add(this.btnTommR);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnUnmm);
            this.Controls.Add(this.btnTomm);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "FTToMM";
            this.Text = "成品送缴";
            this.Load += new System.EventHandler(this.FTToMM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgBoxUnToMM)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBoxToMM)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgBoxUnToMMR)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

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
        private System.Windows.Forms.Button btnUnmm;
        private System.Windows.Forms.Button btnTomm;
        private System.Windows.Forms.TextBox txtUT;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtWH;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnWH;
        private System.Windows.Forms.TextBox txtWhName;
        private System.Windows.Forms.TextBox txtQtyLost;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox cbMoNull;
        private System.Windows.Forms.DataGridView dgBoxUnToMM;
        private System.Windows.Forms.DataGridView dgBoxToMM;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnUnmmR;
        private System.Windows.Forms.Button btnTommR;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgBoxUnToMMR;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBoxQty;
        private System.Windows.Forms.Label label1;

    }
}