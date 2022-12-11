namespace BarCodePrint
{
    partial class BarIdxSet
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
            this.label6 = new System.Windows.Forms.Label();
            this.txtLH = new System.Windows.Forms.TextBox();
            this.btn4IdxNo = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIdxNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrdNo = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPrtName = new System.Windows.Forms.TextBox();
            this.btn4Prdt = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 54;
            this.label6.Text = "料号";
            // 
            // txtLH
            // 
            this.txtLH.Location = new System.Drawing.Point(47, 58);
            this.txtLH.Name = "txtLH";
            this.txtLH.Size = new System.Drawing.Size(120, 20);
            this.txtLH.TabIndex = 53;
            // 
            // btn4IdxNo
            // 
            this.btn4IdxNo.BackgroundImage = global::BarCodePrint.Properties.Resources.qry;
            this.btn4IdxNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn4IdxNo.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn4IdxNo.Location = new System.Drawing.Point(193, 32);
            this.btn4IdxNo.Name = "btn4IdxNo";
            this.btn4IdxNo.Size = new System.Drawing.Size(20, 20);
            this.btn4IdxNo.TabIndex = 52;
            this.btn4IdxNo.UseVisualStyleBackColor = true;
            this.btn4IdxNo.Click += new System.EventHandler(this.btn4IdxNo_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 50;
            this.label5.Text = "中类";
            // 
            // txtIdxNo
            // 
            this.txtIdxNo.Location = new System.Drawing.Point(73, 32);
            this.txtIdxNo.Name = "txtIdxNo";
            this.txtIdxNo.Size = new System.Drawing.Size(120, 20);
            this.txtIdxNo.TabIndex = 49;
            this.txtIdxNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdxNo_KeyDown);
            this.txtIdxNo.Leave += new System.EventHandler(this.txtIdxNo_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "品号";
            // 
            // txtPrdNo
            // 
            this.txtPrdNo.BackColor = System.Drawing.SystemColors.Window;
            this.txtPrdNo.Location = new System.Drawing.Point(47, 32);
            this.txtPrdNo.Name = "txtPrdNo";
            this.txtPrdNo.Size = new System.Drawing.Size(120, 20);
            this.txtPrdNo.TabIndex = 47;
            this.txtPrdNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrdNo_KeyDown);
            this.txtPrdNo.Leave += new System.EventHandler(this.txtPrdNo_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtPrtName);
            this.groupBox1.Controls.Add(this.txtIdxNo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btn4IdxNo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(199, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 97);
            this.groupBox1.TabIndex = 55;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印品名设定";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "打印品名";
            // 
            // txtPrtName
            // 
            this.txtPrtName.Location = new System.Drawing.Point(73, 58);
            this.txtPrtName.Name = "txtPrtName";
            this.txtPrtName.Size = new System.Drawing.Size(120, 20);
            this.txtPrtName.TabIndex = 55;
            // 
            // btn4Prdt
            // 
            this.btn4Prdt.BackgroundImage = global::BarCodePrint.Properties.Resources.qry;
            this.btn4Prdt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn4Prdt.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn4Prdt.Location = new System.Drawing.Point(167, 32);
            this.btn4Prdt.Name = "btn4Prdt";
            this.btn4Prdt.Size = new System.Drawing.Size(20, 20);
            this.btn4Prdt.TabIndex = 51;
            this.btn4Prdt.UseVisualStyleBackColor = true;
            this.btn4Prdt.Click += new System.EventHandler(this.btn4Prdt_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtLH);
            this.groupBox2.Controls.Add(this.txtPrdNo);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btn4Prdt);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(199, 97);
            this.groupBox2.TabIndex = 56;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "料号设定";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 97);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(423, 34);
            this.panel1.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(238, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(423, 97);
            this.panel2.TabIndex = 57;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(336, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // BarIdxSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 131);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BarIdxSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "料号/打印品名设定";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLH;
        private System.Windows.Forms.Button btn4IdxNo;
        private System.Windows.Forms.Button btn4Prdt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIdxNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPrdNo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPrtName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancel;
    }
}