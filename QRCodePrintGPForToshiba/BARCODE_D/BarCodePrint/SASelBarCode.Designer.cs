namespace BarCodePrintTSB
{
    partial class SASelBarCode
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
            this.label5 = new System.Windows.Forms.Label();
            this.txtSANO = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.qrySA = new System.Windows.Forms.Button();
            this.dgSABar = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnLock = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSABar)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 60;
            this.label5.Text = "销货单号";
            // 
            // txtSANO
            // 
            this.txtSANO.Location = new System.Drawing.Point(96, 12);
            this.txtSANO.Name = "txtSANO";
            this.txtSANO.Size = new System.Drawing.Size(150, 20);
            this.txtSANO.TabIndex = 61;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtSANO);
            this.panel1.Controls.Add(this.qrySA);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(785, 47);
            this.panel1.TabIndex = 63;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(284, 11);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 63;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // qrySA
            // 
            this.qrySA.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.qrySA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qrySA.Location = new System.Drawing.Point(246, 12);
            this.qrySA.Name = "qrySA";
            this.qrySA.Size = new System.Drawing.Size(20, 20);
            this.qrySA.TabIndex = 62;
            this.qrySA.UseVisualStyleBackColor = true;
            this.qrySA.Click += new System.EventHandler(this.qrySA_Click);
            // 
            // dgSABar
            // 
            this.dgSABar.AllowUserToAddRows = false;
            this.dgSABar.AllowUserToDeleteRows = false;
            this.dgSABar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSABar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSABar.Location = new System.Drawing.Point(0, 47);
            this.dgSABar.Name = "dgSABar";
            this.dgSABar.Size = new System.Drawing.Size(785, 365);
            this.dgSABar.TabIndex = 64;
            this.dgSABar.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSABar_CellLeave);
            this.dgSABar.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSABar_CellDoubleClick);
            this.dgSABar.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgSABar_CellMouseMove);
            this.dgSABar.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgSABar_DataBindingComplete);
            this.dgSABar.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSABar_CellEnter);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 412);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(785, 32);
            this.panel2.TabIndex = 65;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnLock);
            this.panel3.Controls.Add(this.btnOK);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(503, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(282, 32);
            this.panel3.TabIndex = 65;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(41, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 64;
            this.btnOK.Text = "保存";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnLock
            // 
            this.btnLock.Location = new System.Drawing.Point(170, 6);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(75, 23);
            this.btnLock.TabIndex = 65;
            this.btnLock.Text = "锁单";
            this.btnLock.UseVisualStyleBackColor = true;
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // SASelBarCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 444);
            this.Controls.Add(this.dgSABar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "SASelBarCode";
            this.Text = "销货条码品检";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSABar)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button qrySA;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSANO;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgSABar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnLock;
    }
}