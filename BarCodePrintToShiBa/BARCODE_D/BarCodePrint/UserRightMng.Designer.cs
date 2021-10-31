namespace BarCodePrintTSB
{
    partial class UserRightMng
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
            this.btnSave = new System.Windows.Forms.Button();
            this.txtNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lstNoRight = new System.Windows.Forms.ListBox();
            this.lstYesRight = new System.Windows.Forms.ListBox();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnYes = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn4Usr = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(458, 26);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtNo
            // 
            this.txtNo.Location = new System.Drawing.Point(93, 28);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(120, 20);
            this.txtNo.TabIndex = 1;
            this.txtNo.Leave += new System.EventHandler(this.txtNo_Leave);
            this.txtNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNo_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "用户代号";
            // 
            // lstNoRight
            // 
            this.lstNoRight.FormattingEnabled = true;
            this.lstNoRight.Location = new System.Drawing.Point(36, 66);
            this.lstNoRight.Name = "lstNoRight";
            this.lstNoRight.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstNoRight.Size = new System.Drawing.Size(200, 433);
            this.lstNoRight.TabIndex = 3;
            // 
            // lstYesRight
            // 
            this.lstYesRight.FormattingEnabled = true;
            this.lstYesRight.Location = new System.Drawing.Point(333, 66);
            this.lstYesRight.Name = "lstYesRight";
            this.lstYesRight.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstYesRight.Size = new System.Drawing.Size(200, 433);
            this.lstYesRight.TabIndex = 4;
            // 
            // btnNo
            // 
            this.btnNo.Location = new System.Drawing.Point(270, 288);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(30, 23);
            this.btnNo.TabIndex = 8;
            this.btnNo.Text = "<<";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(270, 208);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(30, 23);
            this.btnYes.TabIndex = 7;
            this.btnYes.Text = ">>";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(320, 28);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(120, 20);
            this.txtName.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "用户名称";
            // 
            // btn4Usr
            // 
            this.btn4Usr.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btn4Usr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn4Usr.Location = new System.Drawing.Point(212, 28);
            this.btn4Usr.Name = "btn4Usr";
            this.btn4Usr.Size = new System.Drawing.Size(20, 20);
            this.btn4Usr.TabIndex = 11;
            this.btn4Usr.UseVisualStyleBackColor = true;
            this.btn4Usr.Click += new System.EventHandler(this.btn4Usr_Click);
            // 
            // UserRightMng
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 514);
            this.Controls.Add(this.btn4Usr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.lstYesRight);
            this.Controls.Add(this.lstNoRight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNo);
            this.Controls.Add(this.btnSave);
            this.Name = "UserRightMng";
            this.Text = "权限设定";
            this.Load += new System.EventHandler(this.UserRightMng_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstNoRight;
        private System.Windows.Forms.ListBox lstYesRight;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn4Usr;
    }
}