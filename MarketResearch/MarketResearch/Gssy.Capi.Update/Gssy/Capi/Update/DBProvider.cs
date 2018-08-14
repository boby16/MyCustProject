using System;
using System.Data;
using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;

namespace Gssy.Capi.Update
{
	// Token: 0x02000006 RID: 6
	public class DBProvider
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002127 File Offset: 0x00000327
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000212F File Offset: 0x0000032F
		public DBType DatabaseType
		{
			get
			{
				return this._DatabaseType;
			}
			set
			{
				this._DatabaseType = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002138 File Offset: 0x00000338
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002140 File Offset: 0x00000340
		public string ConnectionString
		{
			get
			{
				return this._ConnectionString;
			}
			set
			{
				this._ConnectionString = value;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002149 File Offset: 0x00000349
		public DBProvider()
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002168 File Offset: 0x00000368
		public DBProvider(DBType dbtype_0)
		{
			this._DatabaseType = dbtype_0;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000218E File Offset: 0x0000038E
		public DBProvider(DBType dbtype_0, string string_0)
		{
			this._DatabaseType = dbtype_0;
			this._ConnectionString = string_0;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002710 File Offset: 0x00000910
		private IDbConnection method_0()
		{
			DBType databaseType = this.DatabaseType;
			IDbConnection result;
			if (databaseType != DBType.FireBird)
			{
				if (databaseType != DBType.FireBirdServer)
				{
					result = new SqlConnection(this.ConnectionString);
				}
				else
				{
					result = new FbConnection(this.ConnectionString);
				}
			}
			else
			{
				result = new FbConnection(this.ConnectionString);
			}
			return result;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002758 File Offset: 0x00000958
		private IDbCommand method_1(string string_0, IDbConnection idbConnection_0)
		{
			DBType databaseType = this.DatabaseType;
			IDbCommand result;
			if (databaseType != DBType.FireBird)
			{
				if (databaseType != DBType.FireBirdServer)
				{
					result = new SqlCommand(string_0, (SqlConnection)idbConnection_0);
				}
				else
				{
					result = new FbCommand(string_0, (FbConnection)idbConnection_0);
				}
			}
			else
			{
				result = new FbCommand(string_0, (FbConnection)idbConnection_0);
			}
			return result;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000027A4 File Offset: 0x000009A4
		private IDataAdapter method_2(string string_0, string string_1)
		{
			DBType databaseType = this._DatabaseType;
			IDataAdapter result;
			if (databaseType != DBType.FireBird)
			{
				if (databaseType != DBType.FireBirdServer)
				{
					result = new SqlDataAdapter(string_0, string_1);
				}
				else
				{
					result = new FbDataAdapter(string_0, string_1);
				}
			}
			else
			{
				result = new FbDataAdapter(string_0, string_1);
			}
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000027E0 File Offset: 0x000009E0
		private IDataAdapter method_3(IDbCommand idbCommand_0)
		{
			DBType databaseType = this.DatabaseType;
			IDataAdapter result;
			if (databaseType != DBType.FireBird)
			{
				if (databaseType != DBType.FireBirdServer)
				{
					result = new SqlDataAdapter((SqlCommand)idbCommand_0);
				}
				else
				{
					result = new FbDataAdapter((FbCommand)idbCommand_0);
				}
			}
			else
			{
				result = new FbDataAdapter((FbCommand)idbCommand_0);
			}
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002828 File Offset: 0x00000A28
		public int ExecuteNonQuery(string string_0)
		{
			IDbConnection dbConnection = null;
			IDbCommand dbCommand = null;
			int result;
			try
			{
				dbConnection = this.method_0();
				dbCommand = this.method_1(string_0, dbConnection);
				dbConnection.Open();
				result = dbCommand.ExecuteNonQuery();
			}
			catch (Exception)
			{
				result = 0;
			}
			finally
			{
				if (dbCommand != null)
				{
					dbCommand.Dispose();
				}
				if (dbConnection != null)
				{
					dbConnection.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002890 File Offset: 0x00000A90
		public IDataReader ExecuteReader(string string_0)
		{
			IDataReader result;
			try
			{
				IDbConnection dbConnection = this.method_0();
				IDbCommand dbCommand = this.method_1(string_0, dbConnection);
				dbConnection.Open();
				result = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000028D4 File Offset: 0x00000AD4
		public object ExecuteScalar(string string_0)
		{
			IDbConnection dbConnection = null;
			IDbCommand dbCommand = null;
			object result;
			try
			{
				dbConnection = this.method_0();
				dbCommand = this.method_1(string_0, dbConnection);
				dbConnection.Open();
				result = dbCommand.ExecuteScalar();
			}
			catch (Exception)
			{
				result = null;
			}
			finally
			{
				if (dbCommand != null)
				{
					dbCommand.Dispose();
				}
				if (dbConnection != null)
				{
					dbConnection.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000293C File Offset: 0x00000B3C
		public int ExecuteScalarInt(string string_0)
		{
			IDbConnection dbConnection = null;
			IDbCommand dbCommand = null;
			int result;
			try
			{
				dbConnection = this.method_0();
				dbCommand = this.method_1(string_0, dbConnection);
				dbConnection.Open();
				result = Convert.ToInt32(dbCommand.ExecuteScalar().ToString());
			}
			catch (Exception)
			{
				result = -1;
			}
			finally
			{
				if (dbCommand != null)
				{
					dbCommand.Dispose();
				}
				if (dbConnection != null)
				{
					dbConnection.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000029AC File Offset: 0x00000BAC
		public string ExecuteScalarString(string string_0)
		{
			IDbConnection dbConnection = null;
			IDbCommand dbCommand = null;
			string result;
			try
			{
				dbConnection = this.method_0();
				dbCommand = this.method_1(string_0, dbConnection);
				dbConnection.Open();
				result = dbCommand.ExecuteScalar().ToString();
			}
			catch (Exception)
			{
				result = null;
			}
			finally
			{
				if (dbCommand != null)
				{
					dbCommand.Dispose();
				}
				if (dbConnection != null)
				{
					dbConnection.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A18 File Offset: 0x00000C18
		public DataSet ExecuteDataSet(string string_0, string string_1)
		{
			DataSet result;
			try
			{
				IDataAdapter dataAdapter = this.method_2(string_0, string_1);
				DataSet dataSet = new DataSet();
				dataAdapter.Fill(dataSet);
				result = dataSet;
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002A58 File Offset: 0x00000C58
		public DataSet ExecuteDataSet(string string_0)
		{
			IDbConnection dbConnection = null;
			IDbCommand dbCommand = null;
			DataSet result;
			try
			{
				dbConnection = this.method_0();
				dbCommand = this.method_1(string_0, dbConnection);
				IDataAdapter dataAdapter = this.method_3(dbCommand);
				DataSet dataSet = new DataSet();
				dataAdapter.Fill(dataSet);
				result = dataSet;
			}
			catch (Exception)
			{
				result = null;
			}
			finally
			{
				if (dbCommand != null)
				{
					dbCommand.Dispose();
				}
				if (dbConnection != null)
				{
					dbConnection.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public DBProvider(int int_0)
		{
			this._DatabaseType = DBType.FireBird;
			if (int_0 == 1 || int_0 == 2)
			{
				string text = GClass0.smethod_0("Wųɥͱѓ՝ٸݾࡽ९ੰ୚ౢ൧๡༪၅ᅆቃ");
				string text2 = GClass0.smethod_0("Uűɻͯё՟پݸࡿ७੾ୂేപๅཆ၃");
				string userID = GClass0.smethod_0("ZŽɵͰѠսه݀ࡀ");
				string password = GClass0.smethod_0("9ĺȸ̼кՕٰݶࡵ१੸");
				switch (this.DatabaseType)
				{
				case DBType.FireBird:
				{
					string text3 = text;
					if (int_0 == 2)
					{
						text3 = text2;
					}
					this._ConnectionString = new FbConnectionStringBuilder
					{
						UserID = userID,
						Password = password,
						Database = text3,
						Charset = GClass0.smethod_0("^ńɀ͋шՂـݛࡅ॑੒"),
						Dialect = 3,
						ClientLibrary = GClass0.smethod_0("mŨɬͥѥգ١ܪࡧ८੭"),
						ServerType = FbServerType.Embedded
					}.ToString();
					return;
				}
				case DBType.FireBirdServer:
				{
					string text3 = text;
					if (int_0 == 2)
					{
						text3 = text2;
					}
					FbConnectionStringBuilder fbConnectionStringBuilder = new FbConnectionStringBuilder();
					fbConnectionStringBuilder.UserID = userID;
					fbConnectionStringBuilder.Password = password;
					fbConnectionStringBuilder.Charset = GClass0.smethod_0("^ńɀ͋шՂـݛࡅ॑੒");
					fbConnectionStringBuilder.Dialect = 3;
					string str = GClass0.smethod_0("nēɴ͠ѕՖٝݿࡥ॒੓୦ర൞๽ཫၳᄷቕ፸ᑴᕼᙸ᝶ᡎᥖᩣ᭼ᱷᴣṏὪ⁺Ⅰ∦⍊⑩╧♭❯⡧⥝");
					fbConnectionStringBuilder.DataSource = GClass0.smethod_0("eŧɤͧѩլ٬ݱࡵ");
					fbConnectionStringBuilder.Database = str + text3;
					this._ConnectionString = fbConnectionStringBuilder.ToString();
					return;
				}
				case DBType.MySql:
					break;
				case DBType.SQLServer:
				{
					SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
					sqlConnectionStringBuilder.UserID = userID;
					sqlConnectionStringBuilder.Password = password;
					sqlConnectionStringBuilder.DataSource = GClass0.smethod_0("");
					string text3 = GClass0.smethod_0("[Ųɴͳѡպن݃");
					sqlConnectionStringBuilder.InitialCatalog = text3;
					this._ConnectionString = sqlConnectionStringBuilder.ToString();
					this._ConnectionString = GClass0.smethod_0("");
					break;
				}
				default:
					return;
				}
			}
		}

		// Token: 0x04000039 RID: 57
		private DBType _DatabaseType = DBType.FireBird;

		// Token: 0x0400003A RID: 58
		private string _ConnectionString = GClass0.smethod_0("");
	}
}
