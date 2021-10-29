namespace BarCodePrintTSB
{
	partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.MenuGP = new System.Windows.Forms.MenuStrip();
            this.MenuBarPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuWX = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuWXPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSys = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuLogOff = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.lblLogInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNotice = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTimeInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuBarRepPrt = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuWxRep = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuWXEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuGP.SuspendLayout();
            this.stsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuGP
            // 
            this.MenuGP.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBarPrint,
            this.menuBarRepPrt,
            this.MenuSys});
            this.MenuGP.Location = new System.Drawing.Point(0, 0);
            this.MenuGP.MdiWindowListItem = this.MenuBarPrint;
            this.MenuGP.Name = "MenuGP";
            this.MenuGP.Size = new System.Drawing.Size(976, 25);
            this.MenuGP.TabIndex = 1;
            this.MenuGP.Text = "menuStrip1";
            // 
            // MenuBarPrint
            // 
            this.MenuBarPrint.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator8,
            this.toolStripSeparator9,
            this.toolStripSeparator10,
            this.MenuWX});
            this.MenuBarPrint.Name = "MenuBarPrint";
            this.MenuBarPrint.Size = new System.Drawing.Size(68, 21);
            this.MenuBarPrint.Text = "条码打印";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(149, 6);
            // 
            // MenuWX
            // 
            this.MenuWX.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuWXPrint,
            this.MenuWXEdit});
            this.MenuWX.Name = "MenuWX";
            this.MenuWX.Size = new System.Drawing.Size(152, 22);
            this.MenuWX.Text = "外箱条码打印";
            // 
            // MenuWXPrint
            // 
            this.MenuWXPrint.Name = "MenuWXPrint";
            this.MenuWXPrint.Size = new System.Drawing.Size(152, 22);
            this.MenuWXPrint.Text = "外箱条码打印";
            this.MenuWXPrint.Click += new System.EventHandler(this.MenuWXPrint_Click);
            // 
            // MenuSys
            // 
            this.MenuSys.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator7,
            this.MenuLogOff,
            this.MenuExit});
            this.MenuSys.Name = "MenuSys";
            this.MenuSys.Size = new System.Drawing.Size(44, 21);
            this.MenuSys.Text = "系统";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(97, 6);
            // 
            // MenuLogOff
            // 
            this.MenuLogOff.Name = "MenuLogOff";
            this.MenuLogOff.Size = new System.Drawing.Size(100, 22);
            this.MenuLogOff.Text = "注销";
            this.MenuLogOff.Click += new System.EventHandler(this.MenuLogOff_Click);
            // 
            // MenuExit
            // 
            this.MenuExit.Name = "MenuExit";
            this.MenuExit.Size = new System.Drawing.Size(100, 22);
            this.MenuExit.Text = "退出";
            this.MenuExit.Click += new System.EventHandler(this.MenuExit_Click);
            // 
            // stsMain
            // 
            this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblLogInfo,
            this.lblNotice,
            this.lblTimeInfo});
            this.stsMain.Location = new System.Drawing.Point(0, 452);
            this.stsMain.Name = "stsMain";
            this.stsMain.Size = new System.Drawing.Size(976, 22);
            this.stsMain.TabIndex = 3;
            // 
            // lblLogInfo
            // 
            this.lblLogInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(222)))), ((int)(((byte)(231)))));
            this.lblLogInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogInfo.Name = "lblLogInfo";
            this.lblLogInfo.Size = new System.Drawing.Size(727, 17);
            this.lblLogInfo.Spring = true;
            this.lblLogInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNotice
            // 
            this.lblNotice.AutoSize = false;
            this.lblNotice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(222)))), ((int)(((byte)(231)))));
            this.lblNotice.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblNotice.Name = "lblNotice";
            this.lblNotice.Size = new System.Drawing.Size(230, 17);
            this.lblNotice.Text = "注意：此版本必须配合SUNLIKE v9.0使用  ";
            this.lblNotice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTimeInfo
            // 
            this.lblTimeInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(222)))), ((int)(((byte)(231)))));
            this.lblTimeInfo.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblTimeInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeInfo.Name = "lblTimeInfo";
            this.lblTimeInfo.Size = new System.Drawing.Size(4, 17);
            this.lblTimeInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuBarRepPrt
            // 
            this.menuBarRepPrt.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuWxRep});
            this.menuBarRepPrt.Name = "menuBarRepPrt";
            this.menuBarRepPrt.Size = new System.Drawing.Size(92, 21);
            this.menuBarRepPrt.Text = "条码重复打印";
            // 
            // MenuWxRep
            // 
            this.MenuWxRep.Name = "MenuWxRep";
            this.MenuWxRep.Size = new System.Drawing.Size(172, 22);
            this.MenuWxRep.Text = "外箱条码重复打印";
            this.MenuWxRep.Click += new System.EventHandler(this.MenuWxRep_Click);
            // 
            // MenuWXEdit
            // 
            this.MenuWXEdit.Name = "MenuWXEdit";
            this.MenuWXEdit.Size = new System.Drawing.Size(152, 22);
            this.MenuWXEdit.Text = "修改外箱条码";
            this.MenuWXEdit.Click += new System.EventHandler(this.MenuWXEdit_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.BG;
            this.ClientSize = new System.Drawing.Size(976, 474);
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.MenuGP);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MenuGP;
            this.Name = "Main";
            this.Text = "条码管理系统v9.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.MenuGP.ResumeLayout(false);
            this.MenuGP.PerformLayout();
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip MenuGP;
        private System.Windows.Forms.ToolStripMenuItem MenuBarPrint;
		private System.Windows.Forms.ToolStripMenuItem MenuSys;
        private System.Windows.Forms.ToolStripMenuItem MenuExit;
        private System.Windows.Forms.ToolStripMenuItem MenuWX;
        private System.Windows.Forms.ToolStripMenuItem MenuWXPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem MenuLogOff;
        private System.Windows.Forms.StatusStrip stsMain;
        private System.Windows.Forms.ToolStripStatusLabel lblLogInfo;
        private System.Windows.Forms.ToolStripStatusLabel lblTimeInfo;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel lblNotice;
        private System.Windows.Forms.ToolStripMenuItem menuBarRepPrt;
        private System.Windows.Forms.ToolStripMenuItem MenuWxRep;
        private System.Windows.Forms.ToolStripMenuItem MenuWXEdit;
	}
}