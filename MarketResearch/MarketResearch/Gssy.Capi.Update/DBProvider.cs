using System;
using System.Data;
using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;

namespace LoyalFilial.MarketResearch.Update
{
	public class DBProvider
	{
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

		public DBProvider(int int_0)
		{
			this._DatabaseType = DBType.FireBird;
			if (int_0 == 1 || int_0 == 2)
			{
				string text = "Data\\SurveyRead.FDB";
				string text2 = "Data\\SurveyDB.FDB";
				string userID = "SurveyDBA";
				string password = "2014=Survey";
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
						Charset = "UNICODE_FSS",
						Dialect = 3,
						ClientLibrary = "fbembed.dll",
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
					fbConnectionStringBuilder.Charset = "UNICODE_FSS";
					fbConnectionStringBuilder.Dialect = 3;
					string str = "D:\\MarketResearch\\LoyalFilial.MarketResearch.Mobile\\LoyalFilial.MarketResearch.Mobile\\";
					fbConnectionStringBuilder.DataSource = "localhost";
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
					sqlConnectionStringBuilder.DataSource = "";
					string text3 = "SurveyDB";
					sqlConnectionStringBuilder.InitialCatalog = text3;
					this._ConnectionString = sqlConnectionStringBuilder.ToString();
					this._ConnectionString = "";
					break;
				}
				default:
					return;
				}
			}
		}

		private DBType _DatabaseType = DBType.FireBird;

		private string _ConnectionString = "";
	}
}
