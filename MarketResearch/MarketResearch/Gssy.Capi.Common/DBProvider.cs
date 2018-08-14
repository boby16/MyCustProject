using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using FirebirdSql.Data.FirebirdClient;
using MySql.Data.MySqlClient;

namespace Gssy.Capi.Common
{
	// Token: 0x02000004 RID: 4
	public class DBProvider
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002464 File Offset: 0x00000664
		public DBProvider()
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002580 File Offset: 0x00000780
		public DBProvider(DBType dbtype_0)
		{
			this._DatabaseType = dbtype_0;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000026A4 File Offset: 0x000008A4
		public DBProvider(DBType dbtype_0, string string_0)
		{
			this._DatabaseType = dbtype_0;
			this._ConnectionString = string_0;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000027CC File Offset: 0x000009CC
		public DBProvider(int int_0)
		{
			if (int_0 == 1 || int_0 == 2)
			{
				switch (this.DatabaseType)
				{
				case DBType.FireBird:
					this.FireBirdDB = this.FireBirdDBRead;
					if (int_0 == 2)
					{
						this.FireBirdDB = this.FireBirdDBWrite;
					}
					this._ConnectionString = new FbConnectionStringBuilder
					{
						UserID = this.FireBirdUserID,
						Password = this.FireBirdPass,
						Database = this.FireBirdDB,
						Charset = GClass0.smethod_0("^ńɀ͋шՂـݛࡅ॑੒"),
						Dialect = 3,
						ClientLibrary = GClass0.smethod_0("mŨɬͥѥգ١ܪࡧ८੭"),
						ServerType = FbServerType.Embedded
					}.ToString();
					break;
				case DBType.FireBirdServer:
					this.FireBirdDB = this.FireBirdDBRead;
					if (int_0 == 2)
					{
						this.FireBirdDB = this.FireBirdDBWrite;
					}
					this._ConnectionString = new FbConnectionStringBuilder
					{
						UserID = this.FireBirdUserID,
						Password = this.FireBirdPass,
						Charset = GClass0.smethod_0("^ńɀ͋шՂـݛࡅ॑੒"),
						Dialect = 3,
						DataSource = this.FireBirdServerHost,
						Database = this.FireBirdServerPath + this.FireBirdDB
					}.ToString();
					break;
				case DBType.MySql:
					this._ConnectionString = new MySqlConnectionStringBuilder
					{
						Server = this.MySqlServer,
						UserID = this.MySqlUserID,
						Password = this.MySqlPass,
						Database = this.MySqlDB,
						Port = 3306u
					}.ToString();
					break;
				case DBType.SQLServer:
					this._ConnectionString = new SqlConnectionStringBuilder
					{
						UserID = this.SQLServerUserID,
						Password = this.SQLServerPass,
						DataSource = GClass0.smethod_0(""),
						InitialCatalog = this.SQLServerDB
					}.ToString();
					this._ConnectionString = GClass0.smethod_0("");
					break;
				}
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002ADC File Offset: 0x00000CDC
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000020D8 File Offset: 0x000002D8
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

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002AF4 File Offset: 0x00000CF4
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000020E1 File Offset: 0x000002E1
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

		// Token: 0x06000014 RID: 20 RVA: 0x00002B0C File Offset: 0x00000D0C
		private IDbConnection method_0()
		{
			IDbConnection result;
			switch (this.DatabaseType)
			{
			case DBType.FireBird:
				result = new FbConnection(this.ConnectionString);
				break;
			case DBType.FireBirdServer:
				result = new FbConnection(this.ConnectionString);
				break;
			case DBType.MySql:
				result = new MySqlConnection(this.ConnectionString);
				break;
			case DBType.SQLServer:
				result = new SqlConnection(this.ConnectionString);
				break;
			default:
				result = new SqlConnection(this.ConnectionString);
				break;
			}
			return result;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002B84 File Offset: 0x00000D84
		private IDbCommand method_1(string string_0, IDbConnection idbConnection_0)
		{
			IDbCommand result;
			switch (this.DatabaseType)
			{
			case DBType.FireBird:
				result = new FbCommand(string_0, (FbConnection)idbConnection_0);
				break;
			case DBType.FireBirdServer:
				result = new FbCommand(string_0, (FbConnection)idbConnection_0);
				break;
			case DBType.MySql:
				result = new MySqlCommand(string_0, (MySqlConnection)idbConnection_0);
				break;
			case DBType.SQLServer:
				result = new SqlCommand(string_0, (SqlConnection)idbConnection_0);
				break;
			default:
				result = new SqlCommand(string_0, (SqlConnection)idbConnection_0);
				break;
			}
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002C00 File Offset: 0x00000E00
		private IDataAdapter method_2(string string_0, string string_1)
		{
			IDataAdapter result;
			switch (this._DatabaseType)
			{
			case DBType.FireBird:
				result = new FbDataAdapter(string_0, string_1);
				break;
			case DBType.FireBirdServer:
				result = new FbDataAdapter(string_0, string_1);
				break;
			case DBType.MySql:
				result = new MySqlDataAdapter(string_0, string_1);
				break;
			case DBType.SQLServer:
				result = new SqlDataAdapter(string_0, string_1);
				break;
			default:
				result = new SqlDataAdapter(string_0, string_1);
				break;
			}
			return result;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002C64 File Offset: 0x00000E64
		private IDataAdapter method_3(IDbCommand idbCommand_0)
		{
			IDataAdapter result;
			switch (this.DatabaseType)
			{
			case DBType.FireBird:
				result = new FbDataAdapter((FbCommand)idbCommand_0);
				break;
			case DBType.FireBirdServer:
				result = new FbDataAdapter((FbCommand)idbCommand_0);
				break;
			case DBType.MySql:
				result = new MySqlDataAdapter((MySqlCommand)idbCommand_0);
				break;
			case DBType.SQLServer:
				result = new SqlDataAdapter((SqlCommand)idbCommand_0);
				break;
			default:
				result = new SqlDataAdapter((SqlCommand)idbCommand_0);
				break;
			}
			return result;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002CDC File Offset: 0x00000EDC
		public int ExecuteNonQuery(string string_0)
		{
			IDbConnection dbConnection = null;
			IDbCommand dbCommand = null;
			int result;
			try
			{
				dbConnection = this.method_0();
				dbCommand = this.method_1(string_0, dbConnection);
				Logging.Info.WriteLog(GClass0.smethod_0("Uŷɫͮѹտٯ݇ࡧ३੗୰ౡ൱๻༻"), string_0);
				dbConnection.Open();
				result = dbCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Logging.Error.WriteLog(GClass0.smethod_0("SŧɦͼѠԱٕݷ࡫८੹୿౯േ๧ཀྵၗᅰቡ፱ᑻᔻ"), string_0 + GClass0.smethod_0("-") + ex.Message);
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

		// Token: 0x06000019 RID: 25 RVA: 0x00002D88 File Offset: 0x00000F88
		public IDataReader ExecuteReader(string string_0)
		{
			IDataReader result;
			try
			{
				IDbConnection dbConnection = this.method_0();
				IDbCommand dbCommand = this.method_1(string_0, dbConnection);
				Logging.Info.WriteLog(GClass0.smethod_0("Kŵɩͨѿս٭ݕࡣ।੠୦౰഻"), string_0);
				dbConnection.Open();
				result = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
			}
			catch (Exception ex)
			{
				Logging.Error.WriteLog(GClass0.smethod_0("Qšɠ;Ѣԯًݵࡩ२੿୽౭ൕ๣ཤၠᅦተጻ"), string_0 + GClass0.smethod_0("-") + ex.Message);
				result = null;
			}
			return result;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002E10 File Offset: 0x00001010
		public object ExecuteScalar(string string_0)
		{
			IDbConnection dbConnection = null;
			IDbCommand dbCommand = null;
			object result;
			try
			{
				dbConnection = this.method_0();
				dbCommand = this.method_1(string_0, dbConnection);
				Logging.Info.WriteLog(GClass0.smethod_0("Kŵɩͨѿս٭ݔࡥ।੨ୢ౰഻"), string_0);
				dbConnection.Open();
				result = dbCommand.ExecuteScalar();
			}
			catch (Exception ex)
			{
				Logging.Error.WriteLog(GClass0.smethod_0("Qšɠ;Ѣԯًݵࡩ२੿୽౭ൔ๥ཤၨᅢተጻ"), string_0 + GClass0.smethod_0("-") + ex.Message);
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

		// Token: 0x0600001B RID: 27 RVA: 0x00002EBC File Offset: 0x000010BC
		public int ExecuteScalarInt(string string_0)
		{
			IDbConnection dbConnection = null;
			IDbCommand dbCommand = null;
			int result;
			try
			{
				dbConnection = this.method_0();
				dbCommand = this.method_1(string_0, dbConnection);
				Logging.Info.WriteLog(GClass0.smethod_0("TŨɪͭѸոٮݙࡪ३੫୧౷്๭ྲྀျ"), string_0);
				dbConnection.Open();
				result = Convert.ToInt32(dbCommand.ExecuteScalar().ToString());
			}
			catch (Exception ex)
			{
				Logging.Error.WriteLog(GClass0.smethod_0("RŤɧͻѡԲٔݨࡪ७੸୸౮൙๪ཀྵၫᅧቷፍᑭᕶᘻ"), string_0 + GClass0.smethod_0("-") + ex.Message);
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

		// Token: 0x0600001C RID: 28 RVA: 0x00002F70 File Offset: 0x00001170
		public string ExecuteScalarString(string string_0)
		{
			IDbConnection dbConnection = null;
			IDbCommand dbCommand = null;
			string result;
			try
			{
				dbConnection = this.method_0();
				dbCommand = this.method_1(string_0, dbConnection);
				Logging.Info.WriteLog(GClass0.smethod_0("QūɷͲѥջ٫ݞ࡯४੦୨౺ൔ๲ཷၭᅭብጻ"), string_0);
				dbConnection.Open();
				result = dbCommand.ExecuteScalar().ToString();
			}
			catch (Exception ex)
			{
				Logging.Error.WriteLog(GClass0.smethod_0("_ūɪ͸ѤԵّݫࡷॲ੥୻౫൞๯ཪၦᅨቺፔᑲᕷ᙭᝭ᡥ᤻"), string_0 + GClass0.smethod_0("-") + ex.Message);
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

		// Token: 0x0600001D RID: 29 RVA: 0x00003020 File Offset: 0x00001220
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
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				result = null;
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003070 File Offset: 0x00001270
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
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
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

		// Token: 0x0400000A RID: 10
		private string _ConnectionString = GClass0.smethod_0("");

		// Token: 0x0400000B RID: 11
		private string FireBirdDBRead = GClass0.smethod_0("Wųɥͱѓ՝ٸݾࡽ९ੰ୚ౢ൧๡༪၅ᅆቃ");

		// Token: 0x0400000C RID: 12
		private string FireBirdDBWrite = GClass0.smethod_0("Uűɻͯё՟پݸࡿ७੾ୂేപๅཆ၃");

		// Token: 0x0400000D RID: 13
		private string FireBirdDB = GClass0.smethod_0("");

		// Token: 0x0400000E RID: 14
		private string FireBirdUserID = GClass0.smethod_0("ZŽɵͰѠսه݀ࡀ");

		// Token: 0x0400000F RID: 15
		private string FireBirdPass = GClass0.smethod_0("9ĺȸ̼кՕٰݶࡵ१੸");

		// Token: 0x04000010 RID: 16
		private DBType _DatabaseType = DBType.FireBird;

		// Token: 0x04000011 RID: 17
		private string FireBirdServerPath = GClass0.smethod_0("XĠɅͱѹճ١ݤࡦ॰੍୧౸൹๿ལၤᅾቕ፿ᑢᕤᙦᝥᡳᥫᩝ");

		// Token: 0x04000012 RID: 18
		private string FireBirdServerHost = GClass0.smethod_0("eŧɤͧѩլ٬ݱࡵ");

		// Token: 0x04000013 RID: 19
		private string MySqlServer = GClass0.smethod_0("eŧɤͧѩլ٬ݱࡵ");

		// Token: 0x04000014 RID: 20
		private string MySqlDB = GClass0.smethod_0("{Ųɴͳѡպ٦ݣ");

		// Token: 0x04000015 RID: 21
		private string MySqlUserID = GClass0.smethod_0("zŽɵͰѠս٧ݠࡠ");

		// Token: 0x04000016 RID: 22
		private string MySqlPass = GClass0.smethod_0("9ĺȸ̼јյٰݶࡵ१੸");

		// Token: 0x04000017 RID: 23
		private string SQLServerDataSource = GClass0.smethod_0("eŧɤͧѩլ٬ݱࡵ");

		// Token: 0x04000018 RID: 24
		private string SQLServerDB = GClass0.smethod_0("{Ųɴͳѡպ٦ݣ");

		// Token: 0x04000019 RID: 25
		private string SQLServerUserID = GClass0.smethod_0("zŽɵͰѠս٧ݠࡠ");

		// Token: 0x0400001A RID: 26
		private string SQLServerPass = GClass0.smethod_0("9ĺȸ̼јՕٰݶࡵ१੸");
	}
}
