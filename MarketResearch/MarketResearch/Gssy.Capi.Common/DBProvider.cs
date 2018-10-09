using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using FirebirdSql.Data.FirebirdClient;
using MySql.Data.MySqlClient;

namespace Gssy.Capi.Common
{
	public class DBProvider
	{
		public DBProvider()
		{
		}

		public DBProvider(DBType dbtype_0)
		{
			this._DatabaseType = dbtype_0;
		}

		public DBProvider(DBType dbtype_0, string string_0)
		{
			this._DatabaseType = dbtype_0;
			this._ConnectionString = string_0;
		}

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
						Charset = "UNICODE_FSS",
						Dialect = 3,
						ClientLibrary = "fbembed.dll",
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
						Charset = "UNICODE_FSS",
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
						DataSource = "",
						InitialCatalog = this.SQLServerDB
					}.ToString();
					this._ConnectionString = "";
					break;
				}
			}
		}

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

		public int ExecuteNonQuery(string string_0)
		{
			IDbConnection dbConnection = null;
			IDbCommand dbCommand = null;
			int result;
			try
			{
				dbConnection = this.method_0();
				dbCommand = this.method_1(string_0, dbConnection);
				Logging.Info.WriteLog("ExecuteNonQuery:", string_0);
				dbConnection.Open();
				result = dbCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Logging.Error.WriteLog("Error ExecuteNonQuery:", string_0 + "," + ex.Message);
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

		public IDataReader ExecuteReader(string string_0)
		{
			IDataReader result;
			try
			{
				IDbConnection dbConnection = this.method_0();
				IDbCommand dbCommand = this.method_1(string_0, dbConnection);
				Logging.Info.WriteLog("ExecuteReader:", string_0);
				dbConnection.Open();
				result = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
			}
			catch (Exception ex)
			{
				Logging.Error.WriteLog("Error ExecuteReader:", string_0 + "," + ex.Message);
				result = null;
			}
			return result;
		}

		public object ExecuteScalar(string string_0)
		{
			IDbConnection dbConnection = null;
			IDbCommand dbCommand = null;
			object result;
			try
			{
				dbConnection = this.method_0();
				dbCommand = this.method_1(string_0, dbConnection);
				Logging.Info.WriteLog("ExecuteScalar:", string_0);
				dbConnection.Open();
				result = dbCommand.ExecuteScalar();
			}
			catch (Exception ex)
			{
				Logging.Error.WriteLog("Error ExecuteScalar:", string_0 + "," + ex.Message);
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

		public int ExecuteScalarInt(string string_0)
		{
			IDbConnection dbConnection = null;
			IDbCommand dbCommand = null;
			int result;
			try
			{
				dbConnection = this.method_0();
				dbCommand = this.method_1(string_0, dbConnection);
				Logging.Info.WriteLog("ExecuteScalarInt:", string_0);
				dbConnection.Open();
				result = Convert.ToInt32(dbCommand.ExecuteScalar().ToString());
			}
			catch (Exception ex)
			{
				Logging.Error.WriteLog("Error ExecuteScalarInt:", string_0 + "," + ex.Message);
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

		public string ExecuteScalarString(string string_0)
		{
			IDbConnection dbConnection = null;
			IDbCommand dbCommand = null;
			string result;
			try
			{
				dbConnection = this.method_0();
				dbCommand = this.method_1(string_0, dbConnection);
				Logging.Info.WriteLog("ExecuteScalarString:", string_0);
				dbConnection.Open();
				result = dbCommand.ExecuteScalar().ToString();
			}
			catch (Exception ex)
			{
				Logging.Error.WriteLog("Error ExecuteScalarString:", string_0 + "," + ex.Message);
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

		private string _ConnectionString = "";

		private string FireBirdDBRead = "Data\\SurveyRead.FDB";

		private string FireBirdDBWrite = "Data\\SurveyDB.FDB";

		private string FireBirdDB = "";

		private string FireBirdUserID = "SurveyDBA";

		private string FireBirdPass = "2014=Survey";

		private DBType _DatabaseType = DBType.FireBird;

		private string FireBirdServerPath = "C:\\inetpub\\wwwroot\\webcapi\\";

		private string FireBirdServerHost = "localhost";

		private string MySqlServer = "localhost";

		private string MySqlDB = "surveydb";

		private string MySqlUserID = "surveydba";

		private string MySqlPass = "2014_survey";

		private string SQLServerDataSource = "localhost";

		private string SQLServerDB = "surveydb";

		private string SQLServerUserID = "surveydba";

		private string SQLServerPass = "2014_Survey";
	}
}
