using System;

namespace LoyalFilial.MarketResearch.Update
{
	public class SurveyConfigDal
	{
		public bool Exists(string string_0)
		{
			string string_ = string.Format("select count(*) from SurveyConfig where CODE='{0}'", string_0);
			return this.oDB.ExecuteScalarInt(string_) > 0;
		}

		public string GetByCodeText(string string_0)
		{
			string string_ = string.Format("select CODE_TEXT from SurveyConfig where CODE='{0}'", string_0);
			return this.oDB.ExecuteScalarString(string_);
		}

		public bool ExistsRead(string string_0)
		{
			string string_ = string.Format("select count(*) from SurveyConfig where CODE='{0}'", string_0);
			return this.oDBRead.ExecuteScalarInt(string_) > 0;
		}

		public string GetByCodeTextRead(string string_0)
		{
			string string_ = string.Format("select CODE_TEXT from SurveyConfig where CODE='{0}'", string_0);
			return this.oDBRead.ExecuteScalarString(string_);
		}

		private DBProvider oDBRead = new DBProvider(1);

		private DBProvider oDB = new DBProvider(2);
	}
}
