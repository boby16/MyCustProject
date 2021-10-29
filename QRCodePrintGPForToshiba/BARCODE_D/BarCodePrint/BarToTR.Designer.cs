namespace BarCodePrintTSB
{
    partial class BarToTR
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
            this.btnMONo = new System.Windows.Forms.Button();
            this.txtMONo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lstReject = new System.Windows.Forms.ListBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lstPrcId = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPrdType = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUnReject = new System.Windows.Forms.Button();
            this.btnReject = new System.Windows.Forms.Button();
            this.cbMoNull = new System.Windows.Forms.CheckBox();
            this.txtWhName = new System.Windows.Forms.TextBox();
            this.btnWH = new System.Windows.Forms.Button();
            this.txtWH = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtQtyLost = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtUT = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSpc = new System.Windows.Forms.TextBox();
            this.txtPrdNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIdx = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtQtyFin = new System.Windows.Forms.TextBox();
            this.txtYIQty = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtPMark = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "制令单号";
            // 
            // btnMONo
            // 
            this.btnMONo.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btnMONo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMONo.Location = new System.Drawing.Point(203, 32);
            this.btnMONo.Name = "btnMONo";
            this.btnMONo.Size = new System.Drawing.Size(20, 20);
            this.btnMONo.TabIndex = 4;
            this.btnMONo.UseVisualStyleBackColor = true;
            this.btnMONo.Click += new System.EventHandler(this.btnMONo_Click);
            // 
            // txtMONo
            // 
            this.txtMONo.Location = new System.Drawing.Point(84, 32);
            this.txtMONo.Name = "txtMONo";
            this.txtMONo.Size = new System.Drawing.Size(120, 20);
            this.txtMONo.TabIndex = 1;
            this.txtMONo.Leave += new System.EventHandler(this.txtMONo_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "制令单号";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lstReject);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 520);
            this.panel1.TabIndex = 6;
            // 
            // lstReject
            // 
            this.lstReject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstReject.FormattingEnabled = true;
            this.lstReject.Location = new System.Drawing.Point(0, 107);
            this.lstReject.Name = "lstReject";
            this.lstReject.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstReject.Size = new System.Drawing.Size(200, 407);
            this.lstReject.TabIndex = 27;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.lstPrcId);
            this.panel5.Controls.Add(this.btnRefresh);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.cbPrdType);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 107);
            this.panel5.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "处理方式";
            // 
            // lstPrcId
            // 
            this.lstPrcId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstPrcId.FormattingEnabled = true;
            this.lstPrcId.Items.AddRange(new object[] {
            "1.报废处理",
            "2.重开制令单"});
            this.lstPrcId.Location = new System.Drawing.Point(73, 42);
            this.lstPrcId.Name = "lstPrcId";
            this.lstPrcId.Size = new System.Drawing.Size(120, 21);
            this.lstPrcId.TabIndex = 29;
            this.lstPrcId.SelectedIndexChanged += new System.EventHandler(this.lstPrcId_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(119, 70);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 25);
            this.btnRefresh.TabIndex = 27;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "货品";
            // 
            // cbPrdType
            // 
            this.cbPrdType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrdType.FormattingEnabled = true;
            this.cbPrdType.Items.AddRange(new object[] {
            "板材",
            "半成品",
            "成品"});
            this.cbPrdType.Location = new System.Drawing.Point(73, 15);
            this.cbPrdType.Name = "cbPrdType";
            this.cbPrdType.Size = new System.Drawing.Size(121, 21);
            this.cbPrdType.TabIndex = 25;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnUnReject);
            this.panel2.Controls.Add(this.btnReject);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(200, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(40, 520);
            this.panel2.TabIndex = 28;
            // 
            // btnUnReject
            // 
            this.btnUnReject.Location = new System.Drawing.Point(5, 302);
            this.btnUnReject.Name = "btnUnReject";
            this.btnUnReject.Size = new System.Drawing.Size(30, 23);
            this.btnUnReject.TabIndex = 5;
            this.btnUnReject.Text = "<<";
            this.btnUnReject.UseVisualStyleBackColor = true;
            this.btnUnReject.Click += new System.EventHandler(this.btnUnReject_Click);
            // 
            // btnReject
            // 
            this.btnReject.Location = new System.Drawing.Point(5, 234);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(30, 23);
            this.btnReject.TabIndex = 4;
            this.btnReject.Text = ">>";
            this.btnReject.UseVisualStyleBackColor = true;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // cbMoNull
            // 
            this.cbMoNull.AutoSize = true;
            this.cbMoNull.Location = new System.Drawing.Point(73, 308);
            this.cbMoNull.Name = "cbMoNull";
            this.cbMoNull.Size = new System.Drawing.Size(110, 17);
            this.cbMoNull.TabIndex = 58;
            this.cbMoNull.Text = "制令单允许为空";
            this.cbMoNull.UseVisualStyleBackColor = true;
            // 
            // txtWhName
            // 
            this.txtWhName.Location = new System.Drawing.Point(84, 265);
            this.txtWhName.Name = "txtWhName";
            this.txtWhName.Size = new System.Drawing.Size(120, 20);
            this.txtWhName.TabIndex = 57;
            this.txtWhName.Leave += new System.EventHandler(this.txtWhName_Leave);
            this.txtWhName.Enter += new System.EventHandler(this.txtWhName_Enter);
            // 
            // btnWH
            // 
            this.btnWH.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btnWH.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnWH.Location = new System.Drawing.Point(203, 265);
            this.btnWH.Name = "btnWH";
            this.btnWH.Size = new System.Drawing.Size(20, 20);
            this.btnWH.TabIndex = 56;
            this.btnWH.UseVisualStyleBackColor = true;
            this.btnWH.Click += new System.EventHandler(this.btnWH_Click);
            // 
            // txtWH
            // 
            this.txtWH.Location = new System.Drawing.Point(31, 265);
            this.txtWH.Name = "txtWH";
            this.txtWH.Size = new System.Drawing.Size(13, 20);
            this.txtWH.TabIndex = 55;
            this.txtWH.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(48, 271);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(31, 13);
            this.label15.TabIndex = 54;
            this.label15.Text = "库位";
            // 
            // txtQtyLost
            // 
            this.txtQtyLost.Location = new System.Drawing.Point(84, 189);
            this.txtQtyLost.Name = "txtQtyLost";
            this.txtQtyLost.ReadOnly = true;
            this.txtQtyLost.Size = new System.Drawing.Size(120, 20);
            this.txtQtyLost.TabIndex = 53;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 192);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 13);
            this.label14.TabIndex = 52;
            this.label14.Text = "不合格数量";
            // 
            // txtUT
            // 
            this.txtUT.Location = new System.Drawing.Point(84, 241);
            this.txtUT.Name = "txtUT";
            this.txtUT.ReadOnly = true;
            this.txtUT.Size = new System.Drawing.Size(120, 20);
            this.txtUT.TabIndex = 51;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(49, 244);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 50;
            this.label11.Text = "单位";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(84, 215);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(120, 20);
            this.txtQty.TabIndex = 47;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 218);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 46;
            this.label8.Text = "检验数量";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "品号";
            // 
            // txtSpc
            // 
            this.txtSpc.Location = new System.Drawing.Point(84, 85);
            this.txtSpc.Name = "txtSpc";
            this.txtSpc.ReadOnly = true;
            this.txtSpc.Size = new System.Drawing.Size(120, 20);
            this.txtSpc.TabIndex = 45;
            // 
            // txtPrdNo
            // 
            this.txtPrdNo.Location = new System.Drawing.Point(84, 59);
            this.txtPrdNo.Name = "txtPrdNo";
            this.txtPrdNo.ReadOnly = true;
            this.txtPrdNo.Size = new System.Drawing.Size(120, 20);
            this.txtPrdNo.TabIndex = 37;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(49, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 44;
            this.label7.Text = "规格";
            // 
            // txtIdx
            // 
            this.txtIdx.Location = new System.Drawing.Point(84, 111);
            this.txtIdx.Name = "txtIdx";
            this.txtIdx.ReadOnly = true;
            this.txtIdx.Size = new System.Drawing.Size(120, 20);
            this.txtIdx.TabIndex = 43;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 167);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 38;
            this.label9.Text = "已缴库数量";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(49, 114);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 13);
            this.label12.TabIndex = 42;
            this.label12.Text = "中类";
            // 
            // txtQtyFin
            // 
            this.txtQtyFin.Location = new System.Drawing.Point(84, 163);
            this.txtQtyFin.Name = "txtQtyFin";
            this.txtQtyFin.ReadOnly = true;
            this.txtQtyFin.Size = new System.Drawing.Size(120, 20);
            this.txtQtyFin.TabIndex = 39;
            // 
            // txtYIQty
            // 
            this.txtYIQty.Location = new System.Drawing.Point(84, 137);
            this.txtYIQty.Name = "txtYIQty";
            this.txtYIQty.ReadOnly = true;
            this.txtYIQty.Size = new System.Drawing.Size(120, 20);
            this.txtYIQty.TabIndex = 41;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 140);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 13);
            this.label13.TabIndex = 40;
            this.label13.Text = "应缴库数量";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtPMark);
            this.panel4.Controls.Add(this.cbMoNull);
            this.panel4.Controls.Add(this.txtWhName);
            this.panel4.Controls.Add(this.btnOK);
            this.panel4.Controls.Add(this.btnWH);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.txtWH);
            this.panel4.Controls.Add(this.txtMONo);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.btnMONo);
            this.panel4.Controls.Add(this.txtQtyLost);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.txtUT);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.txtYIQty);
            this.panel4.Controls.Add(this.txtQty);
            this.panel4.Controls.Add(this.txtQtyFin);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.txtSpc);
            this.panel4.Controls.Add(this.txtIdx);
            this.panel4.Controls.Add(this.txtPrdNo);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(281, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(244, 520);
            this.panel4.TabIndex = 12;
            // 
            // txtPMark
            // 
            this.txtPMark.Location = new System.Drawing.Point(133, 463);
            this.txtPMark.Name = "txtPMark";
            this.txtPMark.Size = new System.Drawing.Size(13, 20);
            this.txtPMark.TabIndex = 59;
            this.txtPMark.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(133, 401);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 21);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "转异常通知单";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(281, 520);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(240, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(525, 520);
            this.panel3.TabIndex = 8;
            // 
            // BarToTR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 520);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "BarToTR";
            this.Text = "转异常通知单";
            this.Load += new System.EventHandler(this.BarToTR_Load);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMONo;
        private System.Windows.Forms.TextBox txtMONo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtUT;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSpc;
        private System.Windows.Forms.TextBox txtPrdNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtIdx;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtQtyFin;
        private System.Windows.Forms.TextBox txtYIQty;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtQtyLost;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtWhName;
        private System.Windows.Forms.Button btnWH;
        private System.Windows.Forms.TextBox txtWH;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox cbMoNull;
        private System.Windows.Forms.ComboBox cbPrdType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstReject;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnUnReject;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox lstPrcId;
        private System.Windows.Forms.TextBox txtPMark;
    }
}