using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using Sunlike.Common.Utility;

namespace SystemModify
{
	/// <summary>
	/// Summary description for ConvertPic.
	/// </summary>
	public class ConvertPic : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblArea;
		private System.Windows.Forms.TextBox txtFrom;
		private System.Windows.Forms.TextBox txtTo;
		private System.Windows.Forms.Label lblLine;
		private System.Windows.Forms.Button btnConvert;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.ProgressBar prgBar;
		public string _compNo = "";
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ConvertPic()
		{
			InitializeComponent();
			this.Text = "图片转换";
			this.prgBar.Visible = false;
			if (this.Get_Conn() != "")
			{
				this.GetData();
			}
			else
			{
				MessageBox.Show("请先设置帐套及站点url名称");
			}

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region 获取货品范围
		private void GetData()
		{
			string _conn = this.GetDBConnectionString(this._compNo);
			string _sql = "select PRD_NO from PRDT order by PRD_NO";
			SqlDataAdapter sda = new SqlDataAdapter(_sql,_conn);
			DataSet ds = new DataSet();
			sda.Fill(ds);
			if (ds.Tables[0].Rows.Count!=0)
			{
				int i = ds.Tables[0].Rows.Count;
				this.txtFrom.Text = ds.Tables[0].Rows[0]["PRD_NO"].ToString();
				this.txtTo.Text = ds.Tables[0].Rows[i-1]["PRD_NO"].ToString();
			}
		}
		#endregion

		#region 获取connectionString
		public string Get_Conn()
		{
			string _conn = "";
			datamanage dm = new datamanage();
			string _path = dm.Get_Path();
			string _dmpara = dm.check_appSetting("ConnectionString",_path);
			if (_dmpara != "N")
			{
				string _server = "";
				string _uid = "";
				string _pwd = "";
				string[] _para = new String[3];
			
				if (dm.check_appSetting("ConnectionString",_path) != "N")
				{
					_conn = dm.check_appSetting("ConnectionString",_path);
					int x = _conn.IndexOf(";");
					int i = 0 ;
					try
					{
						while ( x > 0 )
						{
							_para[i] = _conn.Substring(0,x);
							_conn = _conn.Substring( x+1,_conn.Length-x-1);
							x = _conn.IndexOf(";");
							i = i + 1;
						}
						if ( _para[0].Trim() != "" )
						{
							int y = _para[0].IndexOf("=");
							_server = _para[0].Substring(y+1,_para[0].Length-y-1);
						}
						if ( _para[1].Trim() != "" )
						{
							int y = _para[1].IndexOf("=");
							_uid = _para[1].Substring(y+1,_para[1].Length-y-1);
						}
						if ( _para[2].Trim() != "" )
						{
							int y = _para[2].IndexOf("=");
							_pwd = Security.DecodingPswd(_para[2].Substring(y+1,_para[2].Length-y-1));
						}
						_compNo = dm.check_appSetting("DatabaseName",_path);
					}
					catch ( Exception _ex )
					{						
						MessageBox.Show(_ex.Message);
					}
				}
				_conn = "server="+_server+";uid="+_uid+";pwd="+_pwd+";database=sunsystem";
			}
			return _conn;
		}
		#endregion

		#region 从COMP表中取得帐套的连接字串
		public string GetDBConnectionString(string CompNo)
		{
			string _conn = this.Get_Conn();
			string _sql = "select SERVER_NAME,DATABASE_NAME,LOGIN_USER,LOGIN_PSWD from COMP where COMPNO='"+this._compNo+"'";
			SqlDataAdapter _sda = new SqlDataAdapter(_sql,_conn);
			DataSet _ds = new DataSet();
			_sda.Fill(_ds);
			DataTable _dt = _ds.Tables[0];
			if (_dt.Rows.Count > 0)
			{
				string _server = Security.DecodingCompData(_dt.Rows[0]["SERVER_NAME"].ToString(),CompNo);	
				string _database = Security.DecodingCompData(_dt.Rows[0]["DATABASE_NAME"].ToString(),CompNo);
				string _user = Security.DecodingCompData(_dt.Rows[0]["LOGIN_USER"].ToString(),CompNo);
				string _pswd = Security.DecodingCompData(_dt.Rows[0]["LOGIN_PSWD"].ToString(),CompNo);
				if (_pswd.ToLower() == "blank")
				{
					_pswd = "";
				}
				_conn = ReplaceConnectionString(_conn,"server",_server);
				_conn = ReplaceConnectionString(_conn,"uid",_user);
				_conn = ReplaceConnectionString(_conn,"pwd",_pswd);
				_conn = ReplaceConnectionString(_conn,"database",_database);
			}
			return _conn;
		}

		private static string ReplaceConnectionString(string ConnStr,string ReplaceName,string ReplaceValue)
		{
			int _pos = ConnStr.ToLower().IndexOf(ReplaceName.ToLower() + "=");
			if (_pos >= 0)
			{
				int _len = ReplaceName.Length+1;
				string _connStr1 = ConnStr.Substring(0,_pos+_len).Trim();
				ConnStr = ConnStr.Substring(_pos+_len,ConnStr.Length-_pos-_len).Trim();
				_pos = ConnStr.ToLower().IndexOf(";");
				string _connStr2;
				if (_pos < 0)
				{
					_connStr2 = ";";
				}
				else
				{
					_connStr2 = ConnStr.Substring(_pos,ConnStr.Length-_pos).Trim();
				}
				ConnStr = _connStr1 + ReplaceValue + _connStr2;
			}
			return ConnStr;
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblArea = new System.Windows.Forms.Label();
			this.txtFrom = new System.Windows.Forms.TextBox();
			this.txtTo = new System.Windows.Forms.TextBox();
			this.lblLine = new System.Windows.Forms.Label();
			this.btnConvert = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.prgBar = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// lblArea
			// 
			this.lblArea.Location = new System.Drawing.Point(24, 40);
			this.lblArea.Name = "lblArea";
			this.lblArea.Size = new System.Drawing.Size(64, 16);
			this.lblArea.TabIndex = 0;
			this.lblArea.Text = "货品范围:";
			// 
			// txtFrom
			// 
			this.txtFrom.Location = new System.Drawing.Point(88, 40);
			this.txtFrom.Name = "txtFrom";
			this.txtFrom.TabIndex = 1;
			this.txtFrom.Text = "";
			// 
			// txtTo
			// 
			this.txtTo.Location = new System.Drawing.Point(256, 40);
			this.txtTo.Name = "txtTo";
			this.txtTo.TabIndex = 2;
			this.txtTo.Text = "";
			// 
			// lblLine
			// 
			this.lblLine.Location = new System.Drawing.Point(200, 40);
			this.lblLine.Name = "lblLine";
			this.lblLine.Size = new System.Drawing.Size(48, 16);
			this.lblLine.TabIndex = 3;
			this.lblLine.Text = "——————";
			// 
			// btnConvert
			// 
			this.btnConvert.Location = new System.Drawing.Point(112, 120);
			this.btnConvert.Name = "btnConvert";
			this.btnConvert.Size = new System.Drawing.Size(56, 23);
			this.btnConvert.TabIndex = 4;
			this.btnConvert.Text = "转换";
			this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(216, 120);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(56, 23);
			this.btnClose.TabIndex = 5;
			this.btnClose.Text = "关闭";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// prgBar
			// 
			this.prgBar.Location = new System.Drawing.Point(40, 80);
			this.prgBar.Name = "prgBar";
			this.prgBar.Size = new System.Drawing.Size(320, 23);
			this.prgBar.TabIndex = 6;
			this.prgBar.Value = 10;
			// 
			// ConvertPic
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(408, 197);
			this.Controls.Add(this.prgBar);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnConvert);
			this.Controls.Add(this.lblLine);
			this.Controls.Add(this.txtTo);
			this.Controls.Add(this.txtFrom);
			this.Controls.Add(this.lblArea);
			this.Name = "ConvertPic";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnConvert_Click(object sender, System.EventArgs e)
		{
			string _conn = this.GetDBConnectionString(this._compNo);
			string _sql = "select PRD_NO from PRDT where PRD_NO>='" + this.txtFrom.Text + "' and"
				+ " PRD_NO<='" + this.txtTo.Text + "' and isnull(datalength(PIC),0)>0 order by PRD_NO";
			SqlDataAdapter _sda = new SqlDataAdapter(_sql,_conn);
			DataSet _ds = new DataSet();
			_sda.Fill(_ds);

			this.prgBar.Minimum = 0;
			this.prgBar.Maximum = _ds.Tables[0].Rows.Count;
			this.prgBar.Value = 0;
			for (int i=0;i<_ds.Tables[0].Rows.Count;i++)
			{
				this.prgBar.Visible = true;
				string _sql2 = "select SMALLPIC,PIC,PRD_NO from PRDT where PRD_NO='"
					+ _ds.Tables[0].Rows[i]["PRD_NO"].ToString() + "'";
				DataSet _ds2 = new DataSet();
				SqlDataAdapter _sda2 = new SqlDataAdapter(_sql2,_conn);
				SqlCommandBuilder _scb = new SqlCommandBuilder(_sda2);
				_sda2.Fill(_ds2);
				byte[] _pic = (byte[])_ds2.Tables[0].Rows[0]["PIC"];
				MemoryStream _ms = new MemoryStream(_pic);
				Bitmap _bmp = new Bitmap(_ms);
				int _bmpWidth = _bmp.Width;
				int _bmpHeight = _bmp.Height;
				int _imgSize = 121;
				int _startX,_startY;
				if (_bmpWidth < _imgSize && _bmpHeight < _imgSize)
				{
					_startX = Convert.ToInt32((_imgSize - _bmpWidth) / 2);;
					_startY = Convert.ToInt32((_imgSize - _bmpHeight) / 2);
				}
				else
				{
					if (_bmpWidth > _bmpHeight)
					{
						_bmpHeight = Convert.ToInt32(_bmpHeight * _imgSize / _bmpWidth);
						_bmpWidth = _imgSize;
						_startX = 0;
						_startY = Convert.ToInt32((_imgSize - _bmpHeight) / 2);
					}
					else
					{
						_bmpWidth = Convert.ToInt32(_bmpWidth * _imgSize / _bmpHeight);
						_bmpHeight = _imgSize;
						_startX = Convert.ToInt32((_imgSize - _bmpWidth) / 2);;
						_startY = 0;
					}
				}
				Bitmap _img = new Bitmap(_imgSize,_imgSize);
				Graphics _gph = Graphics.FromImage(_img);
				_gph.DrawImage(_bmp,_startX,_startY,_bmpWidth,_bmpHeight);
				_img.Save(_ms,System.Drawing.Imaging.ImageFormat.Jpeg);
				_ms.Position = 0;
				BinaryReader _br = new BinaryReader(_ms);
				_pic = _br.ReadBytes((int)_ms.Length);
				_ds2.Tables[0].Rows[0]["SMALLPIC"] = _pic;
				_br.Close();
				_ms.Close();
				_bmp.Dispose();
				_img.Dispose();
				_gph.Dispose();
				_sda2.Update(_ds2);
				this.prgBar.Value = i+1;
			}
			MessageBox.Show("图片转换完成");
			this.prgBar.Visible = false;
		}
	}
}
